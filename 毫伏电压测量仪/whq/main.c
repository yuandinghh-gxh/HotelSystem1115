/*��ADC�����ⲿ��ѹ��ʹ���ڲ���׼�����ѹ.
��STC��MCU��IO��ʽ����74HC595����8λ����ܡ�
�û������޸ĺ���ѡ��ʱ��Ƶ��.
�û�������"�û������"��ѡ��������. �Ƽ�����ʹ�ù��������.
ʹ��Timer0��16λ�Զ���װ������1ms����,�������������������, �û��޸�MCU��ʱ��Ƶ��ʱ,�Զ���ʱ��1ms.
�ұ�4λ�������ʾ�����ĵ�ѹֵֵ.
�ⲿ��ѹ�Ӱ��ϲ��µ�����������, �����ѹ0~VDD, ��Ҫ����VDD�����0V. 
ʵ����Ŀʹ���봮һ��1K�ĵ��赽ADC�����, ADC������ٲ�һ�����ݵ���.
******************************************/
#define MAIN_Fosc		11059000L	//������ʱ��
#include	"STC15Fxxxx.H"
 
// #include <reg51.h>

#define		Baudrate1			115200L
#define		UART1_BUF_LENGTH	32

#define	 LED0 P12
#define	 LED1 P13

u8	TX1_Cnt;	//���ͼ���
u8	RX1_Cnt;	//���ռ���

bit	B_TX1_Busy;	//����æ��־

u8 	idata RX1_Buffer[UART1_BUF_LENGTH];	//���ջ���

void	UART1_config(u8 brt);	// ѡ������, 2: ʹ��Timer2��������, ����ֵ: ʹ��Timer1��������.
void 	PrintString1(u8 *puts);


void  delay_ms(u8 ms);

/**************485*******************************/
#define DIS_DOT		0x20
#define DIS_BLACK	0x10
#define DIS_		0x11

#define P1n_pure_input(bitn)		P1M1 |=  (bitn),	P1M0 &= ~(bitn)

/****************************** �û������ ***********************************/
	#define	Cal_MODE 	0	//ÿ�β���ֻ��1��ADC. �ֱ���0.01V
//	#define	Cal_MODE 	1	//ÿ�β���������16��ADC ��ƽ������. �ֱ���0.01V

#define		LED_TYPE	0x00		//����LED����, 0x00--����, 0xff--����
#define	Timer0_Reload	(65536UL -(MAIN_Fosc / 1000))		//Timer 0 �ж�Ƶ��, 1000��/��
/*************	���س�������	**************/
u8 code t_display[]={						//��׼�ֿ�
//	 0    1    2    3    4    5    6    7    8    9    A    B    C    D    E    F
	0x3F,0x06,0x5B,0x4F,0x66,0x6D,0x7D,0x07,0x7F,0x6F,0x77,0x7C,0x39,0x5E,0x79,0x71,
//black	 -     H    J	 K	  L	   N	o   P	 U     t    G    Q    r   M    y
	0x00,0x40,0x76,0x1E,0x70,0x38,0x37,0x5C,0x73,0x3E,0x78,0x3d,0x67,0x50,0x37,0x6e,
	0xBF,0x86,0xDB,0xCF,0xE6,0xED,0xFD,0x87,0xFF,0xEF,0x46};	//0. 1. 2. 3. 4. 5. 6. 7. 8. 9. -1
u8 code T_COM[]={0x01,0x02,0x04,0x08,0x10,0x20,0x40,0x80};		//λ��

/*************	IO�ڶ���	**************/
sbit	P_HC595_SER   = P4^0;	//pin 14	SER		data input
sbit	P_HC595_RCLK  = P5^4;	//pin 12	RCLk	store (latch) clock
sbit	P_HC595_SRCLK = P4^3;	//pin 11	SRCLK	Shift data clock

/*************	���ر�������	**************/
static u8	str[8];		//��ʾ����
u8	display_index;	//��ʾλ����
bit	B_1ms;			//1ms��־
u16	msecond;
u16	Bandgap;	//
u16	Get_ADC10bitResult(u8 channel);	//channel = 0~7
static  u8 crcok,getsubf = 0;  //�������
static u8 sec,sbufc,sbufb[5],crc,min10c,deltimesand,subfmin,beginget,min;   //  ms �ۼ�,  sbufc,�Ѿ����ܴ����ַ��� ,sbufb ���ջ���
u8  data  timei	; 	//��ʱ����
static u16   curv,ini;		//��ǰ��ѹֵ
u8  data  datas[6]	; //ת�ַ���  ����

void InitUART(void)
{
    TMOD = 0x20;
    SCON = 0x50;
    TH1 = 0xFA;
    TL1 = TH1;
    PCON = 0x80;
    EA = 1;
    ES = 1;
    TR1 = 1;
}

void SendOneByte(unsigned char c)
{
    SBUF = c;
    while(!TI);
    TI = 0;
}


void sbufsend(u8 c) {	 //���пڷ���һ���ַ�
	SBUF=c;				//�����յ������ݷ��뵽���ͼĴ���
	while(!TI);						 //�ȴ������������
	TI=0;  
		 delayms(1)	;
}

void sendv()  {  //���͵�ǰ ������ֵ
	u8 crci = 0xAA;
	sbufsend(0xAA);
    sbufsend(0x15);
	crci = crci+ 0x15;

    sbufsend(curv);
	crci = crci+ curv;
    sbufsend(crci);
}

void sbufsendstr(char str[]) {  //�����ַ���
	u8 i;
   	for(i=0;i<5;i++) {
			if( str[i] != 0 ) { 
				 sbufsend(str[i]); 
	    	 }
	} }


 /*   : void Timer0() interrupt 1   ��ʱ��0�жϺ���  **/
void Timer0() interrupt 1	{
//   TH0=0XFC;	//����ʱ������ֵ����ʱ1ms       TL0=0X18;
//   TL0 = 0x66;
	TH0 = (u8)(Timer0_Reload / 256);
	TL0 = (u8)(Timer0_Reload % 256);
	min++; 	 ini++;
	B_1ms = 1;
    if(ini >= 1000)  {
		subfmin++;
		sec++ ;		  //��
        ini=0;
		min10c=1;
	   LED1 = !LED1;
		if (sec >=60 ) {
			sec = 0;  //����ʱ�� һ����
		}
     }
 //    if (min10 >= 10 ) {
//       min10c++; min10 = 0;
//    }				  
}

/* :  * ��������  : ����ͨ���жϺ���  **/
void Usart() interrupt 4
{
u8 s;
	s =	SBUF;
	if ( s == 0xAA )  {
		beginget = 2;
		sbufb[0]=s;//��ȥ���յ�������
		sbufc = 1; crc = 0xAA;
	 }
	 else { 
	 	if ( sbufb[0]== 0xAA ) {
		 	beginget--;
		  	if (  beginget != 0)  {
				sbufb[sbufc] = s;				
				sbufc++;
			   	crc = crc + s;
			} else {
			 	  crcok = 0; 
			      if ( crc ==  s )	crcok = 1; 
				  else 	 	crcok = 0; 
			  getsubf=1; 
	    	}
       }  
//	    else {
//	   	 getsubf=0; 
//	   }
 	 }
	RI = 0;//��������жϱ�־�
}


void main(void)		 {
	u8	i;
	u16	j;

	P0M1 = 0;	P0M0 = 0;	//����Ϊ׼˫���
	P1M1 = 0;	P1M0 = 0;	//����Ϊ׼˫���
	P2M1 = 0;	P2M0 = 0;	//����Ϊ׼˫���
	P3M1 = 0;	P3M0 = 0;	//����Ϊ׼˫���
	P4M1 = 0;	P4M0 = 0;	//����Ϊ׼˫���
	P5M1 = 0;	P5M0 = 0;	//����Ϊ׼˫���
	P6M1 = 0;	P6M0 = 0;	//����Ϊ׼˫���
	P7M1 = 0;	P7M0 = 0;	//����Ϊ׼˫���
 
	 InitUART();
	 	ET0 = 1;	//Timer0 interrupt enable
	TR0 = 1;	//Tiner0 run
	EA = 1;		//�����ж�
	   i=0;
while(1) {

	   SendOneByte(i);	  i++;
	 //  	delay_ms(100);
		LED0 = !LED0;
}

//	UART1_config(2);	// ѡ������, 2: ʹ��Timer2��������, ����ֵ: ��Ч.	
	display_index = 0;
 	P1M1 |=   (1<<0);		// ��ADC������Ϊ��������
	P1M0 &= ~(1<<0);
	P1ASF = (1<<0);		//P1.0��ADC
	ADC_CONTR = 0xE0;	//90T, ADC power on
	
	AUXR = 0x80;	//Timer0 set as 1T, 16 bits timer auto-reload, 
	TH0 = (u8)(Timer0_Reload / 256);
	TL0 = (u8)(Timer0_Reload % 256);
	ET0 = 1;	//Timer0 interrupt enable
	TR0 = 1;	//Tiner0 run
	EA = 1;		//�����ж�
//	TMOD !=  1;
	 sec = 0; 
  ll: 	TR0 = 1;;
	 if ( sec < 10 ) goto ll;
//	UART1_config(1);
 	PrintString1("STC15F2K60S2 UART1 Test Prgramme!\r\n");	//SUART2����һ���ַ���
 
// 	while (1)
//	{
//	   	LED0 = 0;
//		delay_ms(250);
//		delay_ms(250);
//		LED0 = 1;
//		delay_ms(250);
//		delay_ms(250);
//	}

	while(1) 	{

		if((TX1_Cnt != RX1_Cnt) && (!B_TX1_Busy))	//�յ�����, ���Ϳ���
		{
			SBUF = RX1_Buffer[TX1_Cnt];		//���յ�������Զ������
			B_TX1_Busy = 1;
			if(++TX1_Cnt >= UART1_BUF_LENGTH)	TX1_Cnt = 0;
		}
			 j =100;
	  			str[0] = j / 100 + DIS_DOT;	//��ʾ�ⲿ��ѹֵ
				str[1] = (j % 100) / 10;
				str[2] = j % 10;
				str[3] = 0xaa;
				str[4] = 0x55;
		//	   sbufsendstr(str) ;

		  	LED0 = !LED0;
	   }
		if(B_1ms)	//1ms��
		{  
		  	LED0 = !LED0;
			B_1ms = 0;
			if(++msecond >= 300)	//300ms��
			{
				msecond = 0;
  			#if (Cal_MODE == 0)
			//=================== ֻ��1��ADC, 10bit ADC. �ֱ���0.01V ===============================
				P1ASF = 0;
				Get_ADC10bitResult(0);	//�ı�P1ASF���ȶ�һ�β��������, ���ڲ��Ĳ������ݵĵ�ѹ��������ֵ.
				Bandgap = Get_ADC10bitResult(0);	//���ڲ���׼ADC, P1ASF=0, ��0ͨ��
				P1ASF = 1; //  1<<3;
				j = Get_ADC10bitResult(3);	//���ⲿ��ѹADC
				j = (u16)((u32)j * 123 / Bandgap);	//�����ⲿ��ѹ, BandgapΪ1.23V, ���ѹ�ֱ���0.01V
			#endif
			//==========================================================================
			//===== ������16��ADC ��ƽ������. �ֱ���0.01V =========
			#if (Cal_MODE == 1)
				P1ASF = 0;
				Get_ADC10bitResult(0);	//�ı�P1ASF���ȶ�һ�β��������, ���ڲ��Ĳ������ݵĵ�ѹ��������ֵ.
				for(j=0, i=0; i<16; i++)
				{
					j += Get_ADC10bitResult(0);	//���ڲ���׼ADC, P1ASF=0, ��0ͨ��
				}
				Bandgap = j >> 4;	//16��ƽ��
				P1ASF = ADC_P10;
				for(j=0, i=0; i<16; i++)
				{
					j += Get_ADC10bitResult(3);	//���ⲿ��ѹADC
				}
				j = j >> 4;	//16��ƽ��
				j = (u16)((u32)j * 123 / Bandgap);	//�����ⲿ��ѹ, BandgapΪ1.23V, ���ѹ�ֱ���0.01V
			#endif
			//==========================================================================
		
				str[0] = j / 100 + DIS_DOT;	//��ʾ�ⲿ��ѹֵ
				str[1] = (j % 100) / 10;
				str[2] = j % 10;
				str[3] = 0xaa;
				str[4] = 0x55;
			   sbufsendstr(str) ;

//			j = Bandgap;
//				LED8[0] = j / 1000;		//��ʾBandgap ADCֵ
//				LED8[1] = (j % 1000) / 100;
//				LED8[2] = (j % 100) / 10;
//				LED8[3] = j % 10;
		
			}
		}

} 
//========================================================================
// ����: u16	Get_ADC10bitResult(u8 channel)
// ����: ��ѯ����һ��ADC���.
// ����: channel: ѡ��Ҫת����ADC.
// ����: 10λADC���.
// �汾: V1.0, 2012-10-22
//========================================================================
u16	Get_ADC10bitResult(u8 channel)	//channel = 0~7
{
	ADC_RES = 0;
	ADC_RESL = 0;
	ADC_CONTR = (ADC_CONTR & 0xe0) | 0x08 | channel; 	//start the ADC
	NOP(4);
 	while((ADC_CONTR & 0x10) == 0)	;	//wait for ADC finish
	ADC_CONTR &= ~0x10;		//���ADC������־
	return	(((u16)ADC_RES << 2) | (ADC_RESL & 3));
}


/********************** Timer0 1ms�жϺ��� ************************/
//void timer0 (void) interrupt TIMER0_VECTOR
//{
//	DisplayScan();	//1msɨ����ʾһλ
//	B_1ms = 1;		//1ms��־
//}

//========================================================================
// ����: void PrintString1(u8 *puts)
// ����: ����1�����ַ���������
// ����: puts:  �ַ���ָ��.
// ����: none.
// �汾: VER1.0
// ����: 2014-11-28
// ��ע: 
//========================================================================
void PrintString1(u8 *puts)	//����һ���ַ���
{
    for (; *puts != 0;	puts++)   	//����ֹͣ��0����
	{
		SBUF = *puts;
		B_TX1_Busy = 1;
		while(B_TX1_Busy);
	}
}

//========================================================================
// ����: SetTimer2Baudraye(u16 dat)
// ����: ����Timer2�������ʷ�������
// ����: dat: Timer2����װֵ.
// ����: none.
// �汾: VER1.0
// ����: 2014-11-28
// ��ע: 
//========================================================================
void	SetTimer2Baudraye(u16 dat)	// ѡ������, 2: ʹ��Timer2��������, ����ֵ: ʹ��Timer1��������.
{
	AUXR &= ~(1<<4);	//Timer stop
	AUXR &= ~(1<<3);	//Timer2 set As Timer
	AUXR |=  (1<<2);	//Timer2 set as 1T mode
	TH2 = dat / 256;
	TL2 = dat % 256;
	IE2  &= ~(1<<2);	//��ֹ�ж�
	AUXR |=  (1<<4);	//Timer run enable
}

//========================================================================
// ����: void	UART1_config(u8 brt)
// ����: UART1��ʼ��������
// ����: brt: ѡ������, 2: ʹ��Timer2��������, ����ֵ: ʹ��Timer1��������.
// ����: none.
// �汾: VER1.0
// ����: 2014-11-28
// ��ע: 
//========================================================================
void	UART1_config(u8 brt)	// ѡ������, 2: ʹ��Timer2��������, ����ֵ: ʹ��Timer1��������.
{
	/*********** ������ʹ�ö�ʱ��2 *****************/
	if(brt == 2)
	{
		AUXR |= 0x01;		//S1 BRT Use Timer2;
		SetTimer2Baudraye(65536UL - (MAIN_Fosc / 4) / Baudrate1);
	}

	/*********** ������ʹ�ö�ʱ��1 *****************/
	else
	{
		TR1 = 0;
		AUXR &= ~0x01;		//S1 BRT Use Timer1;
		AUXR |=  (1<<6);	//Timer1 set as 1T mode
		TMOD &= ~(1<<6);	//Timer1 set As Timer
		TMOD &= ~0x30;		//Timer1_16bitAutoReload;
		TH1 = (u8)((65536UL - (MAIN_Fosc / 4) / Baudrate1) / 256);
		TL1 = (u8)((65536UL - (MAIN_Fosc / 4) / Baudrate1) % 256);
		ET1 = 0;	//��ֹ�ж�
		INT_CLKO &= ~0x02;	//�����ʱ��
		TR1  = 1;
	}
	/*************************************************/

	SCON = (SCON & 0x3f) | 0x40;	//UART1ģʽ, 0x00: ͬ����λ���, 0x40: 8λ����,�ɱ䲨����, 0x80: 9λ����,�̶�������, 0xc0: 9λ����,�ɱ䲨����
	PS  = 1;	//�����ȼ��ж�
	ES  = 1;	//�����ж�
	REN = 1;	//�������
	P_SW1 &= 0x3f;
	P_SW1 |= 0x00;		//UART1 switch to, 0x00: P3.0 P3.1, 0x40: P3.6 P3.7, 0x80: P1.6 P1.7 (����ʹ���ڲ�ʱ��)
//	PCON2 |=  (1<<4);	//�ڲ���·RXD��TXD, ���м�, ENABLE,DISABLE

	B_TX1_Busy = 0;
	TX1_Cnt = 0;
	RX1_Cnt = 0;
}


//========================================================================
// ����: void UART1_int (void) interrupt UART1_VECTOR
// ����: UART1�жϺ�����
// ����: nine.
// ����: none.
// �汾: VER1.0
// ����: 2014-11-28
// ��ע: 
//========================================================================
//void UART1_int (void) interrupt 4
//{
//	if(RI)
//	{
//		RI = 0;
//		RX1_Buffer[RX1_Cnt] = SBUF;
//		if(++RX1_Cnt >= UART1_BUF_LENGTH)	RX1_Cnt = 0;	//�����
//	}
//
//	if(TI)
//	{
//		TI = 0;
//		B_TX1_Busy = 0;
//	}
//}


void  delay_ms(u8 ms)
{
     u16 i;
	 do{
	      i = MAIN_Fosc / 13000;
		  while(--i)	;   //14T per loop
     }while(--ms);
}
