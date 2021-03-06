/*读ADC测量外部电压，使用内部基准计算电压.
用STC的MCU的IO方式控制74HC595驱动8位数码管。
用户可以修改宏来选择时钟频率.
用户可以在"用户定义宏"中选择共阴或共阳. 推荐尽量使用共阴数码管.
使用Timer0的16位自动重装来产生1ms节拍,程序运行于这个节拍下, 用户修改MCU主时钟频率时,自动定时于1ms.
右边4位数码管显示测量的电压值值.
外部电压从板上测温电阻两端输入, 输入电压0~VDD, 不要超过VDD或低于0V. 
实际项目使用请串一个1K的电阻到ADC输入口, ADC输入口再并一个电容到地.
2020-7-22 
 ******************************************/
#define	MODE 	1	  //1: 开发系统 ，  0  目标检测电压单元
#define MAIN_Fosc	11059000L	//	22118400L	//定义主时钟
#include	"STC15Fxxxx.H"	
 /***********************************************************/
#define DIS_DOT		0x20
#define DIS_BLACK	0x10
#define DIS_		0x11
#define P1n_pure_input(bitn)		P1M1 |=  (bitn),	P1M0 &= ~(bitn)

#define	LED_TYPE	0x00		//定义LED类型, 0x00--共阴, 0xff--共阳
#define	Timer0_Reload	(65536UL -(MAIN_Fosc / 1000))		//Timer 0 中断频率, 1000次/秒
		#if ( MODE == 1)
#define	 LED0 	P46			 //LED10
#define	 LED1	P47 		  //LED9
		#else
#define	 LED0    P10
#define	 LED1	 P11
#define	 AD3	 P13
		#endif
 #define	 AD3	 P13
//u8	TX1_Cnt;		//发送计数	 //u8	RX1_Cnt;		//接收计数
bit	B_TX1_Busy;			//发送忙标志
#define	 Baudrate1	9600L   //#define		UART1_BUF_LENGTH	32
void   UART1_config(u8 brt);	// 选择波特率, 2: 使用Timer2做波特率, 其它值: 使用Timer1做波特率.
void   DisplayScan(void);
/*************	本地常量声明	**************/
#if ( MODE == 1)
u8 code t_display[]={						//标准字库
//	 0    1    2    3    4    5    6    7    8    9    A    B    C    D    E    F
	0x3F,0x06,0x5B,0x4F,0x66,0x6D,0x7D,0x07,0x7F,0x6F,0x77,0x7C,0x39,0x5E,0x79,0x71,
//black	 -     H    J	 K	  L	   N	o   P	 U     t    G    Q    r   M    y
	0x00,0x40,0x76,0x1E,0x70,0x38,0x37,0x5C,0x73,0x3E,0x78,0x3d,0x67,0x50,0x37,0x6e,
	0xBF,0x86,0xDB,0xCF,0xE6,0xED,0xFD,0x87,0xFF,0xEF,0x46};	//0. 1. 2. 3. 4. 5. 6. 7. 8. 9. -1
u8 code T_COM[]={0x01,0x02,0x04,0x08,0x10,0x20,0x40,0x80};		//位码
		#endif
/*************	IO口定义	**************/
sbit	P_HC595_SER   = P4^0;	//pin 14	SER		data input
sbit	P_HC595_RCLK  = P5^4;	//pin 12	RCLk	store (latch) clock
sbit	P_HC595_SRCLK = P4^3;	//pin 11	SRCLK	Shift data clock
/*************	本地变量声明	**************/
static  idata u8 	LED8[8];		//显示缓冲
u8	display_index;	//显示位索引
u16	msecond,Bandgap;	//
u16	Get_ADC10bitResult(u8 channel);	//channel = 0~7
static u8 getsubf = 0,voltage;  //检验出错
static u8 sec,sbufc,sbufb[8],crc,subfmin,beginget;  //  ms 累加,  sbufc,已经接受串行字符数 ,sbufb 接收缓存
static u16   curv,ini,min=0;		//当前电压值
static u8  datas[6]	; 			//转字符串  缓存
u8 loop=0; // 显示和pc机相连次数
/********************** Timer0 1ms中断函数 ************************/
void timer0 (void) interrupt TIMER0_VECTOR	 {
#if ( MODE == 1)
	DisplayScan();	//1ms扫描显示一位
 #endif
   	min++; 	 ini++;	 msecond++;
    if(ini >= 1000)  {
		subfmin++;
		sec++ ;		  //秒
        ini=0; 	  LED1 = !LED1;
		if (sec >=60 ) { //	deltimesand++;   //一分钟
			sec = 0; 		 //发送时间 一分钟
		}
     }
}
//--------RS232 串行口中断-----------------------------------
void Usart() interrupt 4	  {
	sbufb[sbufc] = SBUF;   	 
	if ( SBUF == 0xA )  {  
		getsubf=1;   //		sbufc = 1;     //crc = 0xAA;
	 }
    sbufc++; 	RI = 0;//清除接收中断标志�
}
void sbufsend(u8 c) {	 //串行口发送一个字符
u8 i;
	SBUF=c;				//将接收到的数据放入到发送寄存器
	while(!TI);						 //等待发送数据完成
	TI=0; 
 	for(i=0; i<50; i++) NOP(1);
}
void sbufsendstr() {  //发送字符串
	u8 i;
   	for(i=0;i<5;i++) {
		if( str[i] != 0xa ) { 
			 sbufsend(str[i]); 
	   	 } else {
			 sbufsend(0xa);  
			 goto ret;
		 }
	}
 ret: NOP(1);
 }

void main(void)		 {
	u8	i,t;	u16	j;
 	P0M1 = 0;	P0M0 = 0;	//设置为准双向口
	P1M1 = 0;	P1M0 = 0;	P2M1 = 0;	P2M0 = 0;	P3M1 = 0;	P3M0 = 0;		P4M1 = 0;	P4M0 = 0;
	display_index = 0;	 	P1M1 |= 1;  // (1<<3);		// 把ADC口设置为高阻输入
	P1M0 &= ~ 1 ; //  (1<<3);		//	P1ASF = (1<<0);		//P1.3做ADC
	ADC_CONTR = 0xE0;	//90T, ADC power on
	AUXR = 0x80;	//Timer0 set as 1T, 16 bits timer auto-reload, 
	TH0 = (u8)(Timer0_Reload / 256); 	TL0 = (u8)(Timer0_Reload % 256);
 	ET0 = 1;	TR0 = 1;	//Timer0 interrupt enable  	//Tiner0 run
	EA = 1;		//打开总中断
	PS = 1; //串行口 中断优先级
	msecond	=0 ;   //300MS 启动ad 转换
    ES = 1;	  getsubf=0;		min=0;	 sbufb[0] =0;	t=0;
//	timei = 0;  //60每 六十分钟 上传一次	 电量指示
   	UART1_config(1);	// 选择波特率, 2: 使用Timer2做波特率, 其它值: 使用Timer1做波特率.
	for(i=0; i<8; i++)	LED8[i] = 0x10;	//上电消隐
//	 while(1) {
//	 	if ( getsubf== 1) {
//			  if ( sbufb[0] == 'P' ) {
//		  		datas[0]='V';  	datas[1]=0xa;  sbufsendstr(datas);
//		  		getsubf =0;	 sbufc=0;
//				goto ll;
//			  }
//		}
//	 } 
 	while(1)   	{
			WDT_CONTR = 0xB6 ;	// 10110110   看门狗 驱动 分频
 			if(msecond >= 3000) {	//300ms到	 			//	DisplayScan();
 				msecond = 0;   LED0 = !LED0;	//循环工作 指示灯
			  }
 			//===== 连续读16次ADC 再平均计算. 分辨率0.01V =========
//				P1ASF = 0;
//				Get_ADC10bitResult(AD3);	//改变P1ASF后先读一次并丢弃结果, 让内部的采样电容的电压等于输入值.
//				for(j=0, i=0; i<16; i++) 	{
//					j += Get_ADC10bitResult(AD3);	//读内部基准ADC, P1ASF=0, 读0通道
//				}
//				Bandgap = j >> 4;	//16次平均
//				P1ASF = 1 ;  //ADC_P13;
//				for(j=0, i=0; i<16; i++) 	{
//					j += Get_ADC10bitResult(AD3);	//读外部电压ADC	  p1.0 
//				}
//				j = j >> 4;	//16次平均
//				j = (u16)((u32)j * 123 / Bandgap);	//计算外部电压, Bandgap为1.23V, 测电压分辨率0.01V
			//==========================================================================
#if ( MODE == 1)
//				LED8[5] = j / 100 + DIS_DOT;	//显示外部电压值
//				LED8[6] = (j % 100) / 10;
//				LED8[7] = j % 10;
 #endif
 //	 datas[0] = 0xfe; datas[1]=j/1000;  datas[2] =(j % 1000) / 100;	 datas[3]=(j % 100) / 10; datas[4] = j % 10; 
//	j = sec ;  // 秒 Bandgap; 	LED8[0] = j / 1000;		//显示Bandgap ADC值
//		LED8[1] = (j % 1000) / 100;	//		LED8[2] = (j % 100) / 10; //LED8[3] = j % 10;
//			}	  //msecond >= 3000)

		     if (j > 470)  { voltage = 100; goto tli;}
			 if (j > 450) { voltage = 80; goto tli;}
		 	 if (j > 440) {voltage= 60; goto tli;}
		 	 if (j > 400) { voltage= 40; goto tli;}
			 if (j > 300) { voltage= 20;  goto tli; }
			 if (j > 200) voltage= 0;
tli:	 curv = voltage;
// 		if ( timei == 0 ) {	     //如果 == 0 测试 输出
// 			 if (subfmin > 30 ) {  //测试 30秒发一组 检测数据
//			//	 sendv() ;		 subfmin =0;
//			 }
//		} 
   	 	if ( getsubf == 1) {	//保持通信
			  if ( sbufb[0] == 'P' ) {
		  		datas[0]='V';  datas[1]='V'; datas[2]=0xd; datas[3]=0xa;  sbufsendstr(datas);
				loop++; if (loop > 15) loop=0;
				LED8[0] = loop;			//t_display[loop];
				LED8[6] = (loop % 100) / 10;
				LED8[7] = loop % 10;
		  	   }
			  if ( sbufb[0] == 'A' ) {
			  	LED8[2] = t; datas[0]='A';  datas[1]=0xd; datas[2]=0xa;  sbufsendstr(datas);
				t++; if  (t>15) t=0;
			  }
			  getsubf =0;	 sbufc=0;
		}


	}  //while(1)
} 

//========================================================================
// 函数: u16	Get_ADC10bitResult(u8 channel)
// 描述: 查询法读一次ADC结果.
// 参数: channel: 选择要转换的ADC.
// 返回: 10位ADC结果.
// 版本: V1.0, 2012-10-22
//========================================================================
u16	Get_ADC10bitResult(u8 channel)	//channel = 0~7
{
	ADC_RES = 0;
	ADC_RESL = 0;
	ADC_CONTR = (ADC_CONTR & 0xe0) | 0x08 | channel; 	//start the ADC
	NOP(4);
 	while((ADC_CONTR & 0x10) == 0)	;	//wait for ADC finish
	ADC_CONTR &= ~0x10;		//清除ADC结束标志
	return	(((u16)ADC_RES << 2) | (ADC_RESL & 3));
}

/**************** 向HC595发送一个字节函数 ******************/
 void Send_595(u8 dat)	  {		
	u8	i;
	for(i=0; i<8; i++)
	{
		dat <<= 1;
		P_HC595_SER   = CY;
		P_HC595_SRCLK = 1;
		P_HC595_SRCLK = 0;
	}
}
	
/********************** 显示扫描函数 ************************/
	#if ( MODE == 1)
void DisplayScan(void)  {	
	Send_595(~LED_TYPE ^ T_COM[display_index]);				//输出位码
	Send_595( LED_TYPE ^ t_display[LED8[display_index]]);	//输出段码

	P_HC595_RCLK = 1;
	P_HC595_RCLK = 0;							//锁存输出数据
	if(++display_index >= 8)	display_index = 0;	//8位结束回0
}
#else
void DisplayScan(void){
 NOP(1);
 }

#endif

//========================================================================
// 函数: SetTimer2Baudraye(u16 dat)
// 描述: 设置Timer2做波特率发生器。
// 参数: dat: Timer2的重装值.
//========================================================================
void	SetTimer2Baudraye(u16 dat)	// 选择波特率, 2: 使用Timer2做波特率, 其它值: 使用Timer1做波特率.
{
	AUXR &= ~(1<<4);	//Timer stop
	AUXR &= ~(1<<3);	//Timer2 set As Timer
	AUXR |=  (1<<2);	//Timer2 set as 1T mode
	TH2 = dat / 256;
	TL2 = dat % 256;
	IE2  &= ~(1<<2);	//禁止中断
	AUXR |=  (1<<4);	//Timer run enable
}

//========================================================================
// 函数: void	UART1_config(u8 brt)
// 描述: UART1初始化函数。
// 参数: brt: 选择波特率, 2: 使用Timer2做波特率, 其它值: 使用Timer1做波特率.
//========================================================================
void	UART1_config(u8 brt) {	// 选择波特率, 2: 使用Timer2做波特率, 其它值: 使用Timer1做波特率.
	if(brt == 2)		 	/*********** 波特率使用定时器2 *****************/
	{
		AUXR |= 0x01;		//S1 BRT Use Timer2;
		SetTimer2Baudraye(65536UL - (MAIN_Fosc / 4) / Baudrate1);
	}	 	/*********** 波特率使用定时器1 *****************/
	else	{
		TR1 = 0;
		AUXR &= ~0x01;		//S1 BRT Use Timer1;
		AUXR |=  (1<<6);	//Timer1 set as 1T mode
		TMOD &= ~(1<<6);	//Timer1 set As Timer
		TMOD &= ~0x30;		//Timer1_16bitAutoReload;
		TH1 = (u8)((65536UL - (MAIN_Fosc / 4) / Baudrate1) / 256);
		TL1 = (u8)((65536UL - (MAIN_Fosc / 4) / Baudrate1) % 256);
		ET1 = 0;	//禁止中断
		INT_CLKO &= ~0x02;	//不输出时钟
		TR1  = 1;
	}
	/*************************************************/
  	SCON = (SCON & 0x3f) | 0x40;	//UART1模式, 0x00: 同步移位输出, 0x40: 8位数据,可变波特率, 0x80: 9位数据,固定波特率, 0xc0: 9位数据,可变波特率
	PS  = 1;	//高优先级中断
	ES  = 1;	//允许中断
	REN = 1;	//允许接收
	P_SW1 &= 0x3f;
	P_SW1 |= 0x00;		//UART1 switch to, 0x00: P3.0 P3.1, 0x40: P3.6 P3.7, 0x80: P1.6 P1.7 (必须使用内部时钟)
//	PCON2 |=  (1<<4);	//内部短路RXD与TXD, 做中继, ENABLE,DISABLE
	B_TX1_Busy = 0;		//	TX1_Cnt = 0;  	RX1_Cnt = 0;
}


