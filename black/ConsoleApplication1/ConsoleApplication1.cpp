// ConsoleApplication1.cpp : �������̨Ӧ�ó������ڵ㡣
//#define  _CRT_SECURE_NO_WARNINGS

#include "stdafx.h"
#pragma warning(disable:4996)
int main()
{
	printf("��������ߵ�Ӣ�ߺ�Ӣ�ߣ�"); //		�������롢��5 \7"��ʾ5Ӣ��7Ӣ��:"����
	int foot;
	int inch;
	scanf("%d", &foot);   printf("\n");
	scanf("%d",  &inch);  printf("\n");
	printf("�����%d��\n", foot);
	printf("inch %f\n", inch);
	printf("�����%f�ס�\n",		((foot + inch / 12)*0.3048));
	scanf("%d", &inch);
	return 0; 
}

