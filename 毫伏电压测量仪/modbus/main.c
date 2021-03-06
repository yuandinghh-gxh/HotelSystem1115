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
#include	"STC15Fxxxx.H"	

#define	MODE 	1	  //1: 开发系统 ，  0  目标检测电压单元
#define MAIN_Fosc	11059000L	//	22118400L	//定义主时钟
#define FOSC 11059000L   //16000000
#define localAddr 01    //本地地址
#define uint8 unsigned char
#define uint16 unsigned int
/***********************************************************/
#define AD3			0	   //1:ADC 基准电压  ，0 ADC 外部电源电压
#define DIS_DOT		0x20
#define DIS_BLACK	0x10
#define DIS_		0x11
#define P1n_pure_input(bitn)		P1M1 |=  (bitn),	P1M0 &= ~(bitn)

#define	LED_TYPE	0x00		//定义LED类型, 0x00--共阴, 0xff--共阳
#define	Timer0_Reload	(65536UL -(MAIN_Fosc / 1000))		//Timer 0 中断频率, 1000次/秒
		#if ( MODE == 1)
#define	 LED0 	P27	
#define	 LED1	P27	
		#else
#define	 LED0 	P10
#define	 LED1	 P11
#define	 AD3	 P13
		#endif
//u8	TX1_Cnt;	//发送计数
//u8	RX1_Cnt;	//接收计数
bit	B_TX1_Busy;	//发送忙标志
#define		Baudrate1	9600L
 uint16 BAUD=9600;
//#define		UART1_BUF_LENGTH	32
void	UART1_config(u8 brt);	// 选择波特率, 2: 使用Timer2做波特率, 其它值: 使用Timer1做波特率.

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
/* CRC 高位字节值表 */ 
u8 code auchCRCHi[] = { 
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0/**/, 
    0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 
    0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 
    0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 
    0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 
    0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 
    0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 
    0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 
    0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 
    0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 
    0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 
    0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 
    0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 
    0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 
    0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 
    0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 
    0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 
    0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 
    0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 
    0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 
    0x80, 0x41, 0x00, 0xC1, 0x81, 0x40 
  } ; 
/* CRC低位字节值表*/ 
u8 code auchCRCLo[] = { 
    0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06/**/, 
    0x07, 0xC7, 0x05, 0xC5, 0xC4, 0x04, 0xCC, 0x0C, 0x0D, 0xCD, 
    0x0F, 0xCF, 0xCE, 0x0E, 0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09,     
    0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9, 0x1B, 0xDB, 0xDA, 0x1A, 
    0x1E, 0xDE, 0xDF, 0x1F, 0xDD, 0x1D, 0x1C, 0xDC, 0x14, 0xD4, 
    0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3, 
    0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3, 
    0xF2, 0x32, 0x36, 0xF6, 0xF7, 0x37, 0xF5, 0x35, 0x34, 0xF4, 
    0x3C, 0xFC, 0xFD, 0x3D, 0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A, 
    0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38, 0x28, 0xE8, 0xE9, 0x29, 
    0xEB, 0x2B, 0x2A, 0xEA, 0xEE, 0x2E, 0x2F, 0xEF, 0x2D, 0xED, 
    0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26, 
    0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60, 
    0x61, 0xA1, 0x63, 0xA3, 0xA2, 0x62, 0x66, 0xA6, 0xA7, 0x67, 
    0xA5, 0x65, 0x64, 0xA4, 0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F, 
    0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB, 0x69, 0xA9, 0xA8, 0x68, 
    0x78, 0xB8, 0xB9, 0x79, 0xBB, 0x7B, 0x7A, 0xBA, 0xBE, 0x7E, 
    0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5, 
    0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71, 
    0x70, 0xB0, 0x50, 0x90, 0x91, 0x51, 0x93, 0x53, 0x52, 0x92, 
    0x96, 0x56, 0x57, 0x97, 0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C, 
    0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E, 0x5A, 0x9A, 0x9B, 0x5B, 
    0x99, 0x59, 0x58, 0x98, 0x88, 0x48, 0x49, 0x89, 0x4B, 0x8B, 
    0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C, 
    0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42, 
    0x43, 0x83, 0x41, 0x81, 0x80, 0x40 
  } ;



/*************	IO口定义	**************/
sbit	P_HC595_SER   = P4^0;	//pin 14	SER		data input
sbit	P_HC595_RCLK  = P5^4;	//pin 12	RCLk	store (latch) clock
sbit	P_HC595_SRCLK = P4^3;	//pin 11	SRCLK	Shift data clock
/*************	本地变量声明	**************/
const uint8 code auchCRCHi[];
const uint8 code auchCRCLo[];
uint16 crc16(uint8 *puchMsg, uint16 usDataLen) ;
u8 LED8[8],crccount;	//显示缓冲   ,crccount crc出错次数
u8	display_index;	//显示位索引
u16	msecond;
u16	Bandgap;	//
u16	Get_ADC10bitResult(u8 channel);	//channel = 0~7

static u8 getsubf = 0,voltage;  //检验出错
static u8 sec,sbufc; //	sec ms 累加, sbufc,已经接受串行字符数 ,sbufb 接收缓存
static u8   timei	; 	//临时变量
static u16   curv,min;		//当前电压值
//static u8  datas[6]	; //转字符串  缓存	 //static u8 getbuf[8]	; 			//转字符串  缓存
uint8 idata receBuf[12];
uint8 idata sendBuf[12];

//--------RS232 串行口中断----------------------------------------
void Usart() interrupt 4	  {
		if(RI) {   //确认是中断接受
			receBuf[sbufc] = SBUF;
		   	if ( sbufc <=0 ) {
				 	min=0; 
			} else {
				if (min > 1200 ) {  //1.2 秒一个收完
					getsubf=1;
				}
			}
   			sbufc++; 	RI = 0;//清除接收中断标志
		}
}
//void sbufsend(u8 c) {	 //串行口发送一个字符
//u8 ic;
//	SBUF = c;		  	//将接收到的数据放入到发送寄存器
//	while(!TI);						 //等待发送数据完成
//	TI=0; 	for(ic=0; ic<30; ic++) NOP(1) ;
// }
//void sbufsendstr() {  //发送字符串
//u8	ic=0;
//sendstr: sbufsend(sendBuf[ic]); 
//	 if ( sendBuf[ic] != 0xA) {
//	 	ic++; goto sendstr;
//  	 }
// }
//开始发送
void sendmodbus(void) {
 uint16 i=0;
 u8 sendCount = 8,icc;
 while(sendCount--)	  {
  SBUF = sendBuf[i++];
  while(!TI);
  for(icc=0; icc<30; icc++) NOP(1) ;
 }
}

void main(void)	 {
	u8	i,loop;	 
	u8 ic;   
	u16	j,loopc,crcData;
 	P0M1 = 0;	P0M0 = 0;	//设置为准双向口
	P1M1 = 0;P1M0 = 0;P2M1 = 0;	P2M0 = 0;P3M1 = 0;	P3M0 = 0;	P4M1 = 0;	P4M0 = 0;
	P5M1 = 0;P5M0 = 0;	P6M1 = 0;	P6M0 = 0;	P7M1 = 0;	P7M0 = 0;	
	display_index = 0;	 sbufc=0;  
	P1M1 |= 1;  // (1<<3);		// 把ADC口设置为高阻输入
	P1M0 &= ~ 1 ; //  (1<<3);
//	P1ASF = (1<<0);		//P1.3做ADC
	ADC_CONTR = 0xE0;	//90T, ADC power on
	AUXR = 0x80;	//Timer0 set as 1T, 16 bits timer auto-reload, 
	TH0 = (u8)(Timer0_Reload / 256);	TL0 = (u8)(Timer0_Reload % 256);
 	ET0 = 1;	//Timer0 interrupt enable
	TR0 = 1;	//Tiner0 run
	EA = 1;		//打开总中断
	PS = 1; //串行口 中断优先级
	msecond	=0 ;   //300MS 启动ad 转换
    ES = 1;	    getsubf=0;		 sendBuf[0] =0;	  crccount=0;
//	f = 0;  //60每 六十分钟 上传一次	 电量指示
   	UART1_config(1);	// 选择波特率, 2: 使用Timer2做波特率, 其它值: 使用Timer1做波特率.
	for(i=0; i<8; i++)	LED8[i] = 0x10;	//上电消隐

 	while(1) {	WDT_CONTR = 0xB6 ;	// 10110110 
	loop++;	if (loop> 150) {	loop=0; loopc++;  LED8[0]=loopc/10000;  LED8[1]=loopc % 10000 / 1000;  }

 			if(msecond >= 2000) {	//300ms到
 				msecond = 0;   LED0 = !LED0;	//循环工作 指示灯
 			//===== 连续读16次ADC 再平均计算. 分辨率0.01V =========
				P1ASF = 0;
				Get_ADC10bitResult(AD3);	//AD3改变P1ASF后先读一次并丢弃结果, 让内部的采样电容的电压等于输入值.
				for(j=0, i=0; i<16; i++)
				{
					j += Get_ADC10bitResult(0);   //读内部基准ADC, P1ASF=0, 读0通道,	AD3
				}
				Bandgap = j >> 4;	//16次平均
				P1ASF = 1 ;  //ADC_P13;
				for(j=0, i=0; i<16; i++)
				{
					j += Get_ADC10bitResult(AD3);	//读外部电压ADC	  p1.0 
				}
				j = j >> 4;	//16次平均
				j = (u16)((u32)j * 123 / Bandgap);	//计算外部电压, Bandgap为1.23V, 测电压分辨率0.01V
			//==========================================================================
#if ( MODE == 1)
				LED8[5] = j / 100 + DIS_DOT;	//显示外部电压值
				LED8[6] = (j % 100) / 10;
				LED8[7] = j % 10;
#endif
 			}   //5000ms 
   
  		if (getsubf == 1 )  {		//收到一组modusb-Rtu数据
			getsubf = 0; 
			if (sbufc >= 8) {   //有效数据
				crcData = crc16(sendBuf,sbufc-2);//生成CRC校验码
	 			sendBuf[6] = crcData & 0xff;  //CRC代码低位在前
	 			sendBuf[7] = crcData >> 8;   //高位在后
				if (( sendBuf[6] != receBuf[6]) || ( sendBuf[7] != receBuf[7])) {
					 	sendBuf[1] = 0xff; 
						for(ic =0;ic<3;ic++) {
							 sendBuf[2+i]=0;
						}
						sendBuf[5]=crccount;sendmodbus();	  //出错
				} else { //crc 正确
					if (receBuf[0] != localAddr) {   //不是本机请求
						sbufc =0; goto error;	 //不处理 直接返回。
					}
				    if (receBuf[1] == 03 ) { //读功能码
						sendBuf[0] = receBuf[0]; sendBuf[1] = receBuf[1]; sendBuf[2]=0;	sendBuf[3]=0;
						sendBuf[4] = j & 0xff; 	sendBuf[4] = j >>8;
						crcData = crc16(sendBuf,6);sendBuf[6] = crcData & 0xff; sendBuf[7] = crcData >> 8; 
						sendmodbus();
					}
				}
			}
			sbufc =0;
		}
	
//01  03  00 00  00 4 crcl crch		主机 读功能吗
		
error:				  
#if ( MODE == 1)
					timei =  sendBuf[7]; LED8[0]= timei&0xf0; LED8[1] = timei&0xf;
					timei =  sendBuf[7]; LED8[2] =  timei&0xf0;	LED8[3] = timei&0xf;
#endif
	
//	   	 	if ( getsubf == 1) {	//保持通信	   //			led1 = !led1 ;
//			    if ( receBuf[0] == 'P' ) {
//			  		sendBuf[0]='V';  sendBuf[1]='V'; sendBuf[2]=0xa; sendBuf[3]=0;   sbufsendstr();
//	  				i++; if (i > 15) loop=0;
//					LED8[4] = loop % 10;	  //				disp[5] = smgduan[loop % 10];
//			  	  }
//		}

	}   //winle(1);
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
void DisplayScan(void)
{	
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

/********************** Timer0 1ms中断函数 ************************/
void timer0 (void) interrupt TIMER0_VECTOR
{
	DisplayScan();	//1ms扫描显示一位
   	min++; 	  msecond++;  	sec++ ;		  //秒
  	if (sec >= 1000) {
	    sec=0;	LED1 = !LED1;
	}
}

//========================================================================
// 函数: SetTimer2Baudraye(u16 dat)
// 描述: 设置Timer2做波特率发生器。
// 参数: dat: Timer2的重装值.
//========================================================================
void SetTimer2Baudraye(u16 dat)	//SetTimer2Baudraye(u16 dat) 选择波特率, 2: 使用Timer2做波特率, 其它值: 使用Timer1做波特率.
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
void UART1_config(u8 brt)	// UART1_config(u8 brt) 2: 使用Timer2做波特率, 其它值: 使用Timer1做波特率.
{
	/*********** 波特率使用定时器2 *****************/
	if(brt == 2)
	{
		AUXR |= 0x01;		//S1 BRT Use Timer2;
		SetTimer2Baudraye(65536UL - (MAIN_Fosc / 4) / Baudrate1);
	}
	/*********** 波特率使用定时器1 *****************/
	else
	{
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

///***************************CRC校验码生成函数 ********************************/
//////函数功能：生成CRC校验码
//////本代码中使用查表法，以提高运算速度
///****************************************************************************/
uint16 crc16(uint8 *puchMsg, uint16 usDataLen) 	{ 
 u8 uchCRCHi = 0xFF ; /* 高CRC字节初始化 */ 
 u8 uchCRCLo = 0xFF ; /* 低CRC 字节初始化 */ 
 u16 uIndex ; /* CRC循环中的索引 */ 
 while (usDataLen--) /* 传输消息缓冲区 */ 
 { 
  uIndex = uchCRCHi ^ *puchMsg++ ; /* 计算CRC */ 
  uchCRCHi = uchCRCLo ^ auchCRCHi[uIndex] ; 
  uchCRCLo = auchCRCLo[uIndex] ; 
 } 
 return (uchCRCLo << 8 | uchCRCHi) ; 
}