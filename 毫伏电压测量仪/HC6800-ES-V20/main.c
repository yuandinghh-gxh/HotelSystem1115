/**************************************************************************************
*		              ��������ADʵ��												  *
ʵ���������س��������ܺ�4λ��ʾ�����������ADֵ.
ע�������������õ���ģ����ʾ�����Խ�74HC595ģ���ϵ�JP595�̽�Ƭ�ε���																				  
***************************************************************************************/
#include "reg52.h"			 //���ļ��ж����˵�Ƭ����һЩ���⹦�ܼĴ���
#include"XPT2046.h"	
//#include <intrins.h>
extern void          _nop_     (void);
#include <string.h>
#define GPIO_DIG P0
#define GPIO_KEY P1
typedef unsigned int u16;	  //���������ͽ�����������
typedef unsigned char u8;
sbit LSA=P2^2;	 sbit LSB=P2^3;	sbit LSC=P2^4;	 //display 	   Nixie tube  address
sbit k1 = P3^1 ;    sbit  k2=  P3^0; sbit  k3 = P3^2; sbit  k4 = P3^3;  //key
sbit led0=P2^0;     sbit led6=P2^6;	sbit led7=P2^7;		   	//LED	
sbit led1=P2^1;	sbit led2=P2^2;  sbit led3=P2^3;  sbit led4=P2^4;   sbit led5=P2^4; 
sbit  onoff = P3^7;
u16  temp,ini=0,temptimecount; min=0;		    	//��ǰ��ѹֵ
u8 disp[8],KeyValue, i,ii,dispms;
u8 code smgduan[18]={0x3f,0x06,0x5b,0x4f,0x66,0x6d,0x7d,0x07,0x7f,0x6f,0x77,0x7c,0x39,0x5e,0x79,0x71,0x76,0x38};//��ʾ0~F��ֵ
char num=0;
static u8 getsubf = 0,voltage;  //�������
static u8 sec,sbufc,sbufb[8],crc;  //  ms �ۼ�,  sbufc,�Ѿ����ܴ����ַ��� ,sbufb ���ջ���
static u8 getbuf[8]	; 			//ת�ַ���  ����
idata u8 datas[8];
u8 loop=0; // ��ʾ��pc����������

// ��������		   : ��ʱ������i=1ʱ����Լ��ʱ10us	 //void delay(u16 i)	{  	while(i--);	}
void delayms(uint x) {	
	  min=0 ;   while(x <= min);
}	   //ms

u8 keyk1_4()   {		//��ⰴ��K1-K4�Ƿ���
	if(k1==0) {		  
		delayms(10);   //�������� һ���Լ10ms
		if(k1==0) {	 //�ٴ��жϰ����Ƿ���
		  	led0=~led0;	return 1;  //led״̬ȡ��
		}
		while(!k1);	 //��ⰴ���Ƿ��ɿ�
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
void KeyDown(void)	{ 		//�������
	char a=0; 	GPIO_KEY=0x0f;
	if(GPIO_KEY!=0x0f)				//��ȡ�����Ƿ���
	{
		delayms(5); 				//��ʱ10ms��������
		if(GPIO_KEY!=0x0f) {		//�ٴμ������Ƿ���
			GPIO_KEY=0X0F;	  //������
			switch(GPIO_KEY) 	{
				case(0X07):	KeyValue=0;break;
				case(0X0b):	KeyValue=1;break;
				case(0X0d): KeyValue=2;break;
				case(0X0e):	KeyValue=3;break;
			}
			//������
			GPIO_KEY=0XF0;
			switch(GPIO_KEY) 			{
				case(0X70):	KeyValue=KeyValue;break;
				case(0Xb0):	KeyValue=KeyValue+4;break;
				case(0Xd0): KeyValue=KeyValue+8;break;
				case(0Xe0):	KeyValue=KeyValue+12;break;
			}
			while((a<50)&&(GPIO_KEY!=0xf0))	 //��ⰴ�����ּ��
			{
				delayms(5);
				a++;
			}
		}
	}
}

/*******************************************************************************
* ������   datapros �ɼ��¶� 
*******************************************************************************/
void datapros()	   {
//	if(ii>10) 	{  		ii=0;
		temp = Read_AD_Data(0xD4)-150;		//   AIN1 ��������
//	}
	ii++;
	disp[0]=smgduan[temp/1000];//ǧλ
	disp[1]=smgduan[temp%1000/100];//��λ
	disp[1] |= 0x80;
	disp[2]=smgduan[temp%1000%100/10];//��λ
	disp[3]=smgduan[temp%1000%100%10];	
	disp[4]=0;	
}
// ������         ��������		 :�������ʾ����
void DigDisplay()	{
	u8 i;
	for(i=0;i<8;i++) 	{
		switch(i) {	 //λѡ��ѡ�����������ܣ�
			case(0): 	LSA=0;LSB=0;LSC=0; break;	case(1): 	LSA=1;LSB=0;LSC=0; break;	//��ʾ��0λ
			case(2): 	LSA=0;LSB=1;LSC=0; break;	case(3): 	LSA=1;LSB=1;LSC=0; break;	//��ʾ��2λ
			case(4): 	LSA=0;LSB=0;LSC=1; break; 	case(5): 	LSA=1;LSB=0;LSC=1; break;
			case(6): 	LSA=0;LSB=1;LSC=1; break;	case(7): 	LSA=1;LSB=1;LSC=1; break;//��ʾ��7λ		
		}
		P0=disp[7-i];//��������
		delayms(3);	//	delay(10); //���һ��ʱ��ɨ��	
 		P0=0x00;//����
	}		
}
//void disp0(u8 fun) {
//  	disp[0]=smgduan[0];		//ǧλ
//	disp[1]=smgduan[0];		//��λ
//  	disp[2]=smgduan[15];		//ǧλ
//	disp[3]=smgduan[16];		//��λ
//	disp[4]=smgduan[fun];		//��λ
//	disp[5]=smgduan[0xff];
//	disp[6]=smgduan[0];			//ǧλ
//	disp[7]=smgduan[0];			//��λ	
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
u8 gettemp(u16 tt) {	   //�����¶�  
u16   timei;
		timei = (tt/1000);			  
		timei = timei*10 + (tt%1000/100);	
		return 	timei;
}

void sbufsend(uchar c) {	 //���пڷ���һ���ַ�
	SBUF=c;					//�����յ������ݷ��뵽���ͼĴ���
	while(!TI);  c=50;	while(c--);	TI=0;	 //�ȴ������������
 }

void sbufsendstr() {  //�����ַ���
u8	ic=0;
sendstr: sbufsend(sbufb[ic]); 
	 if ( sbufb[ic] != 0xa) {
	 	ic++; goto sendstr;
  	 }
 }

void Timer0() interrupt 1	 	{		//����ʱ������ֵ����ʱ1ms
    TH0=0XFC;    TL0=0X18;
    ini++;	min++; 	 temptimecount++; dispms++;
	if (dispms > 2 ) 	{
		DigDisplay();	dispms=0;
	}
	if (temptimecount > 5000 ) 	{		  //һ��
		led0 = !led0 ; temptimecount=0;	 datapros();
	}
 }
 
// ������ : Usart() interrupt 4	   ��������		  : ����ͨ���жϺ���
void Usart() interrupt 4  {
	if(RI) {   //ȷ�����жϽ���
		getbuf[sbufc] = SBUF;   	 
		if ( getbuf[sbufc] == 0xA )  {  
			getsubf=1;  sbufc=0; 	//		sbufc = 1;     //crc = 0xAA;
		 }
		sbufc++; 	RI = 0; 
	}
}

void main()	  {	
	u8 now;		u16 loopc=0;	now = 1 ; 
	SCON=0X50;	ii=0; dispms=0;	 	//����Ϊ������ʽ1
	TMOD=0X21;						//���ü�����������ʽ2
	PCON=0X80;						//�����ʼӱ�
	TH1=0XF3; TL1=0XF3;				//��������ʼֵ���ã�ע�Ⲩ������4800��
    TH0=0XFC; TL0=0X18;				//����ʱ������ֵ����ʱ1ms
	ES=1; 	EA=1;					//�����ж�   	//�򿪽����ж�
	TR1=1;	TR0=1; ET0=1;			//�򿪼����� 
	PS = 1; temptimecount = 0; ini=0; sbufc=0; 						//���п� �ж����ȼ�
	for(i=0;i<20;i++) temp = Read_AD_Data(0xD4);	   //first read temp

	while(1)  	{   //		datapros();	 //���ݴ�����	 //	DigDisplay();//�������ʾ����
	     loopc++;       //			u16toasc(settemp);
		 disp[6]=smgduan[loopc / 10000];   			//�ۿ���������
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
   	 	if ( getsubf == 1) {	//����ͨ��	   //			led1 = !led1 ;
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
		disp[5]=smgduan[keyk1_4()];	   //�ж�K1-K4����

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
//		KeyDown();		   //�����жϺ���
//		LSA=1; 	LSB=1;	LSC=1;	  //��һ��������ṩλѡ
//		GPIO_DIG=smgduan[KeyValue];	  //  ��������

//		P0=0x00;//����
//		LSA=1;LSB=0;LSC=1;	GPIO_DIG=smgduan[16];  	delay(200);	  P0=0x00;//����
//		LSA=0;LSB=1;LSC=1;	GPIO_DIG=smgduan[17];  	delay(200);	  P0=0x00;//����
	}		
}

