/**************************************************************************************
*		              热敏电阻AD实验												  *
实现现象：下载程序后数码管后4位显示热敏电阻检测的AD值.
注意事项：如果不想让点阵模块显示，可以将74HC595模块上的JP595短接片拔掉。																				  
***************************************************************************************/
#include "reg52.h"			 //此文件中定义了单片机的一些特殊功能寄存器
#include"XPT2046.h"	
//#include <intrins.h>
extern void          _nop_     (void);
#include <string.h>
#define GPIO_DIG P0
#define GPIO_KEY P1
typedef unsigned int u16;	  //对数据类型进行声明定义
typedef unsigned char u8;
sbit LSA=P2^2;	 sbit LSB=P2^3;	sbit LSC=P2^4;	 //display 	   Nixie tube  address
sbit k1 = P3^1 ;    sbit  k2=  P3^0; sbit  k3 = P3^2; sbit  k4 = P3^3;  //key
sbit led0=P2^0;     sbit led6=P2^6;	sbit led7=P2^7;		   	//LED	
sbit led1=P2^1;	sbit led2=P2^2;  sbit led3=P2^3;  sbit led4=P2^4;   sbit led5=P2^4; 
sbit  onoff = P3^7;
u16  temp,ini=0,temptimecount; min=0;		    	//当前电压值
u8 disp[8],KeyValue, i,ii,dispms;
u8 code smgduan[18]={0x3f,0x06,0x5b,0x4f,0x66,0x6d,0x7d,0x07,0x7f,0x6f,0x77,0x7c,0x39,0x5e,0x79,0x71,0x76,0x38};//显示0~F的值
char num=0;
static u8 getsubf = 0,voltage;  //检验出错
static u8 sec,sbufc,sbufb[8],crc;  //  ms 累加,  sbufc,已经接受串行字符数 ,sbufb 接收缓存
static u8 getbuf[8]	; 			//转字符串  缓存
idata u8 datas[8];
u8 loop=0; // 显示和pc机相连次数

// 函数功能		   : 延时函数，i=1时，大约延时10us	 //void delay(u16 i)	{  	while(i--);	}
void delayms(uint x) {	
	  min=0 ;   while(x <= min);
}	   //ms

u8 keyk1_4()   {		//检测按键K1-K4是否按下
	if(k1==0) {		  
		delayms(10);   //消除抖动 一般大约10ms
		if(k1==0) {	 //再次判断按键是否按下
		  	led0=~led0;	return 1;  //led状态取反
		}
		while(!k1);	 //检测按键是否松开
	}	
	if(k2==0) {		 
	delayms(10);  
	if(k2==0) {	 
		led1=~led1;	return 2; 	  
	}
	while(!k3);	 
	}
	if(k3==0) {		  
		delayms(10);   
		if(k3==0) {	 
			led2=~led2;	return 3; 	  
	}
	while(!k4);	
	}	
	if(k4==0) {		 
		delayms(10); 
		if(k4==0) {	
			led3=~led3;	return 4; 	 
		}
	}  	return 0;		 }
void KeyDown(void)	{ 		//矩阵键盘
	char a=0; 	GPIO_KEY=0x0f;
	if(GPIO_KEY!=0x0f)				//读取按键是否按下
	{
		delayms(5); 				//延时10ms进行消抖
		if(GPIO_KEY!=0x0f) {		//再次检测键盘是否按下
			GPIO_KEY=0X0F;	  //测试列
			switch(GPIO_KEY) 	{
				case(0X07):	KeyValue=0;break;
				case(0X0b):	KeyValue=1;break;
				case(0X0d): KeyValue=2;break;
				case(0X0e):	KeyValue=3;break;
			}
			//测试行
			GPIO_KEY=0XF0;
			switch(GPIO_KEY) 			{
				case(0X70):	KeyValue=KeyValue;break;
				case(0Xb0):	KeyValue=KeyValue+4;break;
				case(0Xd0): KeyValue=KeyValue+8;break;
				case(0Xe0):	KeyValue=KeyValue+12;break;
			}
			while((a<50)&&(GPIO_KEY!=0xf0))	 //检测按键松手检测
			{
				delayms(5);
				a++;
			}
		}
	}
}

/*******************************************************************************
* 函数名   datapros 采集温度 
*******************************************************************************/
void datapros()	   {
//	if(ii>10) 	{  		ii=0;
		temp = Read_AD_Data(0xD4)-150;		//   AIN1 热敏电阻
//	}
	ii++;
	disp[0]=smgduan[temp/1000];//千位
	disp[1]=smgduan[temp%1000/100];//百位
	disp[1] |= 0x80;
	disp[2]=smgduan[temp%1000%100/10];//个位
	disp[3]=smgduan[temp%1000%100%10];	
	disp[4]=0;	
}
// 函数名         函数功能		 :数码管显示函数
void DigDisplay()	{
	u8 i;
	for(i=0;i<8;i++) 	{
		switch(i) {	 //位选，选择点亮的数码管，
			case(0): 	LSA=0;LSB=0;LSC=0; break;	case(1): 	LSA=1;LSB=0;LSC=0; break;	//显示第0位
			case(2): 	LSA=0;LSB=1;LSC=0; break;	case(3): 	LSA=1;LSB=1;LSC=0; break;	//显示第2位
			case(4): 	LSA=0;LSB=0;LSC=1; break; 	case(5): 	LSA=1;LSB=0;LSC=1; break;
			case(6): 	LSA=0;LSB=1;LSC=1; break;	case(7): 	LSA=1;LSB=1;LSC=1; break;//显示第7位		
		}
		P0=disp[7-i];//发送数据
		delayms(3);	//	delay(10); //间隔一段时间扫描	
 		P0=0x00;//消隐
	}		
}
//void disp0(u8 fun) {
//  	disp[0]=smgduan[0];		//千位
//	disp[1]=smgduan[0];		//百位
//  	disp[2]=smgduan[15];		//千位
//	disp[3]=smgduan[16];		//百位
//	disp[4]=smgduan[fun];		//个位
//	disp[5]=smgduan[0xff];
//	disp[6]=smgduan[0];			//千位
//	disp[7]=smgduan[0];			//百位	
//		
// }

char * u16toasc(u16 tt) {
u8 t = 0;
	if ( tt == 0 ) {
	{ for(i=0;i<5;i++)  datas[i]='0';	  }	   return   datas;		}
	datas[0] = tt / 10000; 	datas[0] |='0';
	datas[1] = tt % 10000 / 1000;	 datas[1] |='0';
	datas[2] = tt % 1000 / 100; 	 datas[2] |='0';
	datas[3] = tt % 100 / 10;	     datas[3] |='0';
	datas[4] = tt % 10; datas[5] =0; 	datas[4] |='0';
	return   datas;
 }
u8 gettemp(u16 tt) {	   //换算温度  
u16   timei;
		timei = (tt/1000);			  
		timei = timei*10 + (tt%1000/100);	
		return 	timei;
}

void sbufsend(uchar c) {	 //串行口发送一个字符
	SBUF=c;					//将接收到的数据放入到发送寄存器
	while(!TI);  c=50;	while(c--);	TI=0;	 //等待发送数据完成
 }

void sbufsendstr() {  //发送字符串
u8	ic=0;
sendstr: sbufsend(sbufb[ic]); 
	 if ( sbufb[ic] != 0xa) {
	 	ic++; goto sendstr;
  	 }
 }

void Timer0() interrupt 1	 	{		//给定时器赋初值，定时1ms
    TH0=0XFC;    TL0=0X18;
    ini++;	min++; 	 temptimecount++; dispms++;
	if (dispms > 2 ) 	{
		DigDisplay();	dispms=0;
	}
	if (temptimecount > 5000 ) 	{		  //一秒
		led0 = !led0 ; temptimecount=0;	 datapros();
	}
 }
 
// 函数名 : Usart() interrupt 4	   函数功能		  : 串口通信中断函数
void Usart() interrupt 4  {
	if(RI) {   //确认是中断接受
		getbuf[sbufc] = SBUF;   	 
		if ( getbuf[sbufc] == 0xA )  {  
			getsubf=1;  sbufc=0; 	//		sbufc = 1;     //crc = 0xAA;
		 }
		sbufc++; 	RI = 0; 
	}
}

void main()	  {	
	u8 now;		u16 loopc=0;	now = 1 ; 
	SCON=0X50;	ii=0; dispms=0;	 	//设置为工作方式1
	TMOD=0X21;						//设置计数器工作方式2
	PCON=0X80;						//波特率加倍
	TH1=0XF3; TL1=0XF3;				//计数器初始值设置，注意波特率是4800的
    TH0=0XFC; TL0=0X18;				//给定时器赋初值，定时1ms
	ES=1; 	EA=1;					//打开总中断   	//打开接收中断
	TR1=1;	TR0=1; ET0=1;			//打开计数器 
	PS = 1; temptimecount = 0; ini=0; sbufc=0; 						//串行口 中断优先级
	for(i=0;i<20;i++) temp = Read_AD_Data(0xD4);	   //first read temp

	while(1)  	{   //		datapros();	 //数据处理函数	 //	DigDisplay();//数码管显示函数
	     loopc++;       //			u16toasc(settemp);
		 disp[6]=smgduan[loopc / 10000];   			//观看程序运行
		 disp[7]=smgduan[loopc % 10000 / 1000];
		 		// disp[7]=smgduan[loopc % 1000 /100];	
		 ini++;	
		 if (ini>=8000) {
		 	u16toasc( Read_AD_Data(0xD4)); 	 	
		   onoff = !onoff;  ini=0; sbufb[0]='C';
		   for(i=0;i<5;i++) {
		   		 sbufb[1+i]= datas[i];
		   }
		   sbufb[2+i]=0x0; sbufb[3+i]=0xa;
		   sbufsendstr();
		 }
   	 	if ( getsubf == 1) {	//保持通信	   //			led1 = !led1 ;
		    if ( getbuf[0] == 'P' ) {
		  		sbufb[0]='V';  sbufb[1]='V'; sbufb[2]=0xd; sbufb[3]=0xa;   sbufsendstr();
  				loop++; if (loop > 15) loop=0;
				disp[4] =smgduan[loop % 10];	  //				disp[5] = smgduan[loop % 10];
		  	  }
			if ( getbuf[0] == 'A' ) {
			   sbufb[0]='A';  sbufb[1]=0xd; sbufb[2]=0xa;  sbufsendstr();
             }
			 getsubf =0;	 sbufc=0;	sbufb[0] = 0;
		}
		disp[5]=smgduan[keyk1_4()];	   //判断K1-K4按键

//		if ( temp >2200 ) {	onoff=1; now= 0; // CLOSE POWER
//		if ( temp < 1800 ) {
//			if (onw == 1) {    //curren open power
//				count++;
//	 
//			onoff = 0;  //open power
//		}
//		u16toasc( temp);   sbufsend('.'); 		sbufsendstr(datas);		 //	sbufsendstr(disp);	
//	    if ( keyk1_4() == k1) {	 //up temp
//			settemp	++ ;
//		}	 //		delayms(1000);
//		KeyDown();		   //按键判断函数
//		LSA=1; 	LSB=1;	LSC=1;	  //给一个数码管提供位选
//		GPIO_DIG=smgduan[KeyValue];	  //  发送数据

//		P0=0x00;//消隐
//		LSA=1;LSB=0;LSC=1;	GPIO_DIG=smgduan[16];  	delay(200);	  P0=0x00;//消隐
//		LSA=0;LSB=1;LSC=1;	GPIO_DIG=smgduan[17];  	delay(200);	  P0=0x00;//消隐
	}		
}

