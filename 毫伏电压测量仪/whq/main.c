/*¶ÁADC²âÁ¿Íâ²¿µçÑ¹£¬Ê¹ÓÃÄÚ²¿»ù×¼¼ÆËãµçÑ¹.
ÓÃSTCµÄMCUµÄIO·½Ê½¿ØÖÆ74HC595Çı¶¯8Î»ÊıÂë¹Ü¡£
ÓÃ»§¿ÉÒÔĞŞ¸ÄºêÀ´Ñ¡ÔñÊ±ÖÓÆµÂÊ.
ÓÃ»§¿ÉÒÔÔÚ"ÓÃ»§¶¨Òåºê"ÖĞÑ¡Ôñ¹²Òõ»ò¹²Ñô. ÍÆ¼ö¾¡Á¿Ê¹ÓÃ¹²ÒõÊıÂë¹Ü.
Ê¹ÓÃTimer0µÄ16Î»×Ô¶¯ÖØ×°À´²úÉú1ms½ÚÅÄ,³ÌĞòÔËĞĞÓÚÕâ¸ö½ÚÅÄÏÂ, ÓÃ»§ĞŞ¸ÄMCUÖ÷Ê±ÖÓÆµÂÊÊ±,×Ô¶¯¶¨Ê±ÓÚ1ms.
ÓÒ±ß4Î»ÊıÂë¹ÜÏÔÊ¾²âÁ¿µÄµçÑ¹ÖµÖµ.
Íâ²¿µçÑ¹´Ó°åÉÏ²âÎÂµç×èÁ½¶ËÊäÈë, ÊäÈëµçÑ¹0~VDD, ²»Òª³¬¹ıVDD»òµÍÓÚ0V. 
Êµ¼ÊÏîÄ¿Ê¹ÓÃÇë´®Ò»¸ö1KµÄµç×èµ½ADCÊäÈë¿Ú, ADCÊäÈë¿ÚÔÙ²¢Ò»¸öµçÈİµ½µØ.
******************************************/
#define MAIN_Fosc		11059000L	//¶¨ÒåÖ÷Ê±ÖÓ
#include	"STC15Fxxxx.H"
 
// #include <reg51.h>

#define		Baudrate1			115200L
#define		UART1_BUF_LENGTH	32

#define	 LED0 P12
#define	 LED1 P13

u8	TX1_Cnt;	//·¢ËÍ¼ÆÊı
u8	RX1_Cnt;	//½ÓÊÕ¼ÆÊı

bit	B_TX1_Busy;	//·¢ËÍÃ¦±êÖ¾

u8 	idata RX1_Buffer[UART1_BUF_LENGTH];	//½ÓÊÕ»º³å

void	UART1_config(u8 brt);	// Ñ¡Ôñ²¨ÌØÂÊ, 2: Ê¹ÓÃTimer2×ö²¨ÌØÂÊ, ÆäËüÖµ: Ê¹ÓÃTimer1×ö²¨ÌØÂÊ.
void 	PrintString1(u8 *puts);


void  delay_ms(u8 ms);

/**************485*******************************/
#define DIS_DOT		0x20
#define DIS_BLACK	0x10
#define DIS_		0x11

#define P1n_pure_input(bitn)		P1M1 |=  (bitn),	P1M0 &= ~(bitn)

/****************************** ÓÃ»§¶¨Òåºê ***********************************/
	#define	Cal_MODE 	0	//Ã¿´Î²âÁ¿Ö»¶Á1´ÎADC. ·Ö±æÂÊ0.01V
//	#define	Cal_MODE 	1	//Ã¿´Î²âÁ¿Á¬Ğø¶Á16´ÎADC ÔÙÆ½¾ù¼ÆËã. ·Ö±æÂÊ0.01V

#define		LED_TYPE	0x00		//¶¨ÒåLEDÀàĞÍ, 0x00--¹²Òõ, 0xff--¹²Ñô
#define	Timer0_Reload	(65536UL -(MAIN_Fosc / 1000))		//Timer 0 ÖĞ¶ÏÆµÂÊ, 1000´Î/Ãë
/*************	±¾µØ³£Á¿ÉùÃ÷	**************/
u8 code t_display[]={						//±ê×¼×Ö¿â
//	 0    1    2    3    4    5    6    7    8    9    A    B    C    D    E    F
	0x3F,0x06,0x5B,0x4F,0x66,0x6D,0x7D,0x07,0x7F,0x6F,0x77,0x7C,0x39,0x5E,0x79,0x71,
//black	 -     H    J	 K	  L	   N	o   P	 U     t    G    Q    r   M    y
	0x00,0x40,0x76,0x1E,0x70,0x38,0x37,0x5C,0x73,0x3E,0x78,0x3d,0x67,0x50,0x37,0x6e,
	0xBF,0x86,0xDB,0xCF,0xE6,0xED,0xFD,0x87,0xFF,0xEF,0x46};	//0. 1. 2. 3. 4. 5. 6. 7. 8. 9. -1
u8 code T_COM[]={0x01,0x02,0x04,0x08,0x10,0x20,0x40,0x80};		//Î»Âë

/*************	IO¿Ú¶¨Òå	**************/
sbit	P_HC595_SER   = P4^0;	//pin 14	SER		data input
sbit	P_HC595_RCLK  = P5^4;	//pin 12	RCLk	store (latch) clock
sbit	P_HC595_SRCLK = P4^3;	//pin 11	SRCLK	Shift data clock

/*************	±¾µØ±äÁ¿ÉùÃ÷	**************/
static u8	str[8];		//ÏÔÊ¾»º³å
u8	display_index;	//ÏÔÊ¾Î»Ë÷Òı
bit	B_1ms;			//1ms±êÖ¾
u16	msecond;
u16	Bandgap;	//
u16	Get_ADC10bitResult(u8 channel);	//channel = 0~7
static  u8 crcok,getsubf = 0;  //¼ìÑé³ö´í
static u8 sec,sbufc,sbufb[5],crc,min10c,deltimesand,subfmin,beginget,min;   //  ms ÀÛ¼Ó,  sbufc,ÒÑ¾­½ÓÊÜ´®ĞĞ×Ö·ûÊı ,sbufb ½ÓÊÕ»º´æ
u8  data  timei	; 	//ÁÙÊ±±äÁ¿
static u16   curv,ini;		//µ±Ç°µçÑ¹Öµ
u8  data  datas[6]	; //×ª×Ö·û´®  »º´æ

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


void sbufsend(u8 c) {	 //´®ĞĞ¿Ú·¢ËÍÒ»¸ö×Ö·û
	SBUF=c;				//½«½ÓÊÕµ½µÄÊı¾İ·ÅÈëµ½·¢ËÍ¼Ä´æÆ÷
	while(!TI);						 //µÈ´ı·¢ËÍÊı¾İÍê³É
	TI=0;  
		 delayms(1)	;
}

void sendv()  {  //·¢ËÍµ±Ç° µçÁ¿ÊıÖµ
	u8 crci = 0xAA;
	sbufsend(0xAA);
    sbufsend(0x15);
	crci = crci+ 0x15;

    sbufsend(curv);
	crci = crci+ curv;
    sbufsend(crci);
}

void sbufsendstr(char str[]) {  //·¢ËÍ×Ö·û´®
	u8 i;
   	for(i=0;i<5;i++) {
			if( str[i] != 0 ) { 
				 sbufsend(str[i]); 
	    	 }
	} }


 /*   : void Timer0() interrupt 1   ¶¨Ê±Æ÷0ÖĞ¶Ïº¯Êı  **/
void Timer0() interrupt 1	{
//   TH0=0XFC;	//¸ø¶¨Ê±Æ÷¸³³õÖµ£¬¶¨Ê±1ms       TL0=0X18;
//   TL0 = 0x66;
	TH0 = (u8)(Timer0_Reload / 256);
	TL0 = (u8)(Timer0_Reload % 256);
	min++; 	 ini++;
	B_1ms = 1;
    if(ini >= 1000)  {
		subfmin++;
		sec++ ;		  //Ãë
        ini=0;
		min10c=1;
	   LED1 = !LED1;
		if (sec >=60 ) {
			sec = 0;  //·¢ËÍÊ±¼ä Ò»·ÖÖÓ
		}
     }
 //    if (min10 >= 10 ) {
//       min10c++; min10 = 0;
//    }				  
}

/* :  * º¯Êı¹¦ÄÜ  : ´®¿ÚÍ¨ĞÅÖĞ¶Ïº¯Êı  **/
void Usart() interrupt 4
{
u8 s;
	s =	SBUF;
	if ( s == 0xAA )  {
		beginget = 2;
		sbufb[0]=s;//³öÈ¥½ÓÊÕµ½µÄÊı¾İ
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
	RI = 0;//Çå³ı½ÓÊÕÖĞ¶Ï±êÖ¾»
}


void main(void)		 {
	u8	i;
	u16	j;

	P0M1 = 0;	P0M0 = 0;	//ÉèÖÃÎª×¼Ë«Ïò¿Ú
	P1M1 = 0;	P1M0 = 0;	//ÉèÖÃÎª×¼Ë«Ïò¿Ú
	P2M1 = 0;	P2M0 = 0;	//ÉèÖÃÎª×¼Ë«Ïò¿Ú
	P3M1 = 0;	P3M0 = 0;	//ÉèÖÃÎª×¼Ë«Ïò¿Ú
	P4M1 = 0;	P4M0 = 0;	//ÉèÖÃÎª×¼Ë«Ïò¿Ú
	P5M1 = 0;	P5M0 = 0;	//ÉèÖÃÎª×¼Ë«Ïò¿Ú
	P6M1 = 0;	P6M0 = 0;	//ÉèÖÃÎª×¼Ë«Ïò¿Ú
	P7M1 = 0;	P7M0 = 0;	//ÉèÖÃÎª×¼Ë«Ïò¿Ú
 
	 InitUART();
	 	ET0 = 1;	//Timer0 interrupt enable
	TR0 = 1;	//Tiner0 run
	EA = 1;		//´ò¿ª×ÜÖĞ¶Ï
	   i=0;
while(1) {

	   SendOneByte(i);	  i++;
	 //  	delay_ms(100);
		LED0 = !LED0;
}

//	UART1_config(2);	// Ñ¡Ôñ²¨ÌØÂÊ, 2: Ê¹ÓÃTimer2×ö²¨ÌØÂÊ, ÆäËüÖµ: ÎŞĞ§.	
	display_index = 0;
 	P1M1 |=   (1<<0);		// °ÑADC¿ÚÉèÖÃÎª¸ß×èÊäÈë
	P1M0 &= ~(1<<0);
	P1ASF = (1<<0);		//P1.0×öADC
	ADC_CONTR = 0xE0;	//90T, ADC power on
	
	AUXR = 0x80;	//Timer0 set as 1T, 16 bits timer auto-reload, 
	TH0 = (u8)(Timer0_Reload / 256);
	TL0 = (u8)(Timer0_Reload % 256);
	ET0 = 1;	//Timer0 interrupt enable
	TR0 = 1;	//Tiner0 run
	EA = 1;		//´ò¿ª×ÜÖĞ¶Ï
//	TMOD !=  1;
	 sec = 0; 
  ll: 	TR0 = 1;;
	 if ( sec < 10 ) goto ll;
//	UART1_config(1);
 	PrintString1("STC15F2K60S2 UART1 Test Prgramme!\r\n");	//SUART2·¢ËÍÒ»¸ö×Ö·û´®
 
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

		if((TX1_Cnt != RX1_Cnt) && (!B_TX1_Busy))	//ÊÕµ½Êı¾İ, ·¢ËÍ¿ÕÏĞ
		{
			SBUF = RX1_Buffer[TX1_Cnt];		//°ÑÊÕµ½µÄÊı¾İÔ¶Ñù·µ»Ø
			B_TX1_Busy = 1;
			if(++TX1_Cnt >= UART1_BUF_LENGTH)	TX1_Cnt = 0;
		}
			 j =100;
	  			str[0] = j / 100 + DIS_DOT;	//ÏÔÊ¾Íâ²¿µçÑ¹Öµ
				str[1] = (j % 100) / 10;
				str[2] = j % 10;
				str[3] = 0xaa;
				str[4] = 0x55;
		//	   sbufsendstr(str) ;

		  	LED0 = !LED0;
	   }
		if(B_1ms)	//1msµ½
		{  
		  	LED0 = !LED0;
			B_1ms = 0;
			if(++msecond >= 300)	//300msµ½
			{
				msecond = 0;
  			#if (Cal_MODE == 0)
			//=================== Ö»¶Á1´ÎADC, 10bit ADC. ·Ö±æÂÊ0.01V ===============================
				P1ASF = 0;
				Get_ADC10bitResult(0);	//¸Ä±äP1ASFºóÏÈ¶ÁÒ»´Î²¢¶ªÆú½á¹û, ÈÃÄÚ²¿µÄ²ÉÑùµçÈİµÄµçÑ¹µÈÓÚÊäÈëÖµ.
				Bandgap = Get_ADC10bitResult(0);	//¶ÁÄÚ²¿»ù×¼ADC, P1ASF=0, ¶Á0Í¨µÀ
				P1ASF = 1; //  1<<3;
				j = Get_ADC10bitResult(3);	//¶ÁÍâ²¿µçÑ¹ADC
				j = (u16)((u32)j * 123 / Bandgap);	//¼ÆËãÍâ²¿µçÑ¹, BandgapÎª1.23V, ²âµçÑ¹·Ö±æÂÊ0.01V
			#endif
			//==========================================================================
			//===== Á¬Ğø¶Á16´ÎADC ÔÙÆ½¾ù¼ÆËã. ·Ö±æÂÊ0.01V =========
			#if (Cal_MODE == 1)
				P1ASF = 0;
				Get_ADC10bitResult(0);	//¸Ä±äP1ASFºóÏÈ¶ÁÒ»´Î²¢¶ªÆú½á¹û, ÈÃÄÚ²¿µÄ²ÉÑùµçÈİµÄµçÑ¹µÈÓÚÊäÈëÖµ.
				for(j=0, i=0; i<16; i++)
				{
					j += Get_ADC10bitResult(0);	//¶ÁÄÚ²¿»ù×¼ADC, P1ASF=0, ¶Á0Í¨µÀ
				}
				Bandgap = j >> 4;	//16´ÎÆ½¾ù
				P1ASF = ADC_P10;
				for(j=0, i=0; i<16; i++)
				{
					j += Get_ADC10bitResult(3);	//¶ÁÍâ²¿µçÑ¹ADC
				}
				j = j >> 4;	//16´ÎÆ½¾ù
				j = (u16)((u32)j * 123 / Bandgap);	//¼ÆËãÍâ²¿µçÑ¹, BandgapÎª1.23V, ²âµçÑ¹·Ö±æÂÊ0.01V
			#endif
			//==========================================================================
		
				str[0] = j / 100 + DIS_DOT;	//ÏÔÊ¾Íâ²¿µçÑ¹Öµ
				str[1] = (j % 100) / 10;
				str[2] = j % 10;
				str[3] = 0xaa;
				str[4] = 0x55;
			   sbufsendstr(str) ;

//			j = Bandgap;
//				LED8[0] = j / 1000;		//ÏÔÊ¾Bandgap ADCÖµ
//				LED8[1] = (j % 1000) / 100;
//				LED8[2] = (j % 100) / 10;
//				LED8[3] = j % 10;
		
			}
		}

} 
//========================================================================
// º¯Êı: u16	Get_ADC10bitResult(u8 channel)
// ÃèÊö: ²éÑ¯·¨¶ÁÒ»´ÎADC½á¹û.
// ²ÎÊı: channel: Ñ¡ÔñÒª×ª»»µÄADC.
// ·µ»Ø: 10Î»ADC½á¹û.
// °æ±¾: V1.0, 2012-10-22
//========================================================================
u16	Get_ADC10bitResult(u8 channel)	//channel = 0~7
{
	ADC_RES = 0;
	ADC_RESL = 0;
	ADC_CONTR = (ADC_CONTR & 0xe0) | 0x08 | channel; 	//start the ADC
	NOP(4);
 	while((ADC_CONTR & 0x10) == 0)	;	//wait for ADC finish
	ADC_CONTR &= ~0x10;		//Çå³ıADC½áÊø±êÖ¾
	return	(((u16)ADC_RES << 2) | (ADC_RESL & 3));
}


/********************** Timer0 1msÖĞ¶Ïº¯Êı ************************/
//void timer0 (void) interrupt TIMER0_VECTOR
//{
//	DisplayScan();	//1msÉ¨ÃèÏÔÊ¾Ò»Î»
//	B_1ms = 1;		//1ms±êÖ¾
//}

//========================================================================
// º¯Êı: void PrintString1(u8 *puts)
// ÃèÊö: ´®¿Ú1·¢ËÍ×Ö·û´®º¯Êı¡£
// ²ÎÊı: puts:  ×Ö·û´®Ö¸Õë.
// ·µ»Ø: none.
// °æ±¾: VER1.0
// ÈÕÆÚ: 2014-11-28
// ±¸×¢: 
//========================================================================
void PrintString1(u8 *puts)	//·¢ËÍÒ»¸ö×Ö·û´®
{
    for (; *puts != 0;	puts++)   	//Óöµ½Í£Ö¹·û0½áÊø
	{
		SBUF = *puts;
		B_TX1_Busy = 1;
		while(B_TX1_Busy);
	}
}

//========================================================================
// º¯Êı: SetTimer2Baudraye(u16 dat)
// ÃèÊö: ÉèÖÃTimer2×ö²¨ÌØÂÊ·¢ÉúÆ÷¡£
// ²ÎÊı: dat: Timer2µÄÖØ×°Öµ.
// ·µ»Ø: none.
// °æ±¾: VER1.0
// ÈÕÆÚ: 2014-11-28
// ±¸×¢: 
//========================================================================
void	SetTimer2Baudraye(u16 dat)	// Ñ¡Ôñ²¨ÌØÂÊ, 2: Ê¹ÓÃTimer2×ö²¨ÌØÂÊ, ÆäËüÖµ: Ê¹ÓÃTimer1×ö²¨ÌØÂÊ.
{
	AUXR &= ~(1<<4);	//Timer stop
	AUXR &= ~(1<<3);	//Timer2 set As Timer
	AUXR |=  (1<<2);	//Timer2 set as 1T mode
	TH2 = dat / 256;
	TL2 = dat % 256;
	IE2  &= ~(1<<2);	//½ûÖ¹ÖĞ¶Ï
	AUXR |=  (1<<4);	//Timer run enable
}

//========================================================================
// º¯Êı: void	UART1_config(u8 brt)
// ÃèÊö: UART1³õÊ¼»¯º¯Êı¡£
// ²ÎÊı: brt: Ñ¡Ôñ²¨ÌØÂÊ, 2: Ê¹ÓÃTimer2×ö²¨ÌØÂÊ, ÆäËüÖµ: Ê¹ÓÃTimer1×ö²¨ÌØÂÊ.
// ·µ»Ø: none.
// °æ±¾: VER1.0
// ÈÕÆÚ: 2014-11-28
// ±¸×¢: 
//========================================================================
void	UART1_config(u8 brt)	// Ñ¡Ôñ²¨ÌØÂÊ, 2: Ê¹ÓÃTimer2×ö²¨ÌØÂÊ, ÆäËüÖµ: Ê¹ÓÃTimer1×ö²¨ÌØÂÊ.
{
	/*********** ²¨ÌØÂÊÊ¹ÓÃ¶¨Ê±Æ÷2 *****************/
	if(brt == 2)
	{
		AUXR |= 0x01;		//S1 BRT Use Timer2;
		SetTimer2Baudraye(65536UL - (MAIN_Fosc / 4) / Baudrate1);
	}

	/*********** ²¨ÌØÂÊÊ¹ÓÃ¶¨Ê±Æ÷1 *****************/
	else
	{
		TR1 = 0;
		AUXR &= ~0x01;		//S1 BRT Use Timer1;
		AUXR |=  (1<<6);	//Timer1 set as 1T mode
		TMOD &= ~(1<<6);	//Timer1 set As Timer
		TMOD &= ~0x30;		//Timer1_16bitAutoReload;
		TH1 = (u8)((65536UL - (MAIN_Fosc / 4) / Baudrate1) / 256);
		TL1 = (u8)((65536UL - (MAIN_Fosc / 4) / Baudrate1) % 256);
		ET1 = 0;	//½ûÖ¹ÖĞ¶Ï
		INT_CLKO &= ~0x02;	//²»Êä³öÊ±ÖÓ
		TR1  = 1;
	}
	/*************************************************/

	SCON = (SCON & 0x3f) | 0x40;	//UART1Ä£Ê½, 0x00: Í¬²½ÒÆÎ»Êä³ö, 0x40: 8Î»Êı¾İ,¿É±ä²¨ÌØÂÊ, 0x80: 9Î»Êı¾İ,¹Ì¶¨²¨ÌØÂÊ, 0xc0: 9Î»Êı¾İ,¿É±ä²¨ÌØÂÊ
	PS  = 1;	//¸ßÓÅÏÈ¼¶ÖĞ¶Ï
	ES  = 1;	//ÔÊĞíÖĞ¶Ï
	REN = 1;	//ÔÊĞí½ÓÊÕ
	P_SW1 &= 0x3f;
	P_SW1 |= 0x00;		//UART1 switch to, 0x00: P3.0 P3.1, 0x40: P3.6 P3.7, 0x80: P1.6 P1.7 (±ØĞëÊ¹ÓÃÄÚ²¿Ê±ÖÓ)
//	PCON2 |=  (1<<4);	//ÄÚ²¿¶ÌÂ·RXDÓëTXD, ×öÖĞ¼Ì, ENABLE,DISABLE

	B_TX1_Busy = 0;
	TX1_Cnt = 0;
	RX1_Cnt = 0;
}


//========================================================================
// º¯Êı: void UART1_int (void) interrupt UART1_VECTOR
// ÃèÊö: UART1ÖĞ¶Ïº¯Êı¡£
// ²ÎÊı: nine.
// ·µ»Ø: none.
// °æ±¾: VER1.0
// ÈÕÆÚ: 2014-11-28
// ±¸×¢: 
//========================================================================
//void UART1_int (void) interrupt 4
//{
//	if(RI)
//	{
//		RI = 0;
//		RX1_Buffer[RX1_Cnt] = SBUF;
//		if(++RX1_Cnt >= UART1_BUF_LENGTH)	RX1_Cnt = 0;	//·ÀÒç³ö
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
