// ConsoleApplication1.cpp : 定义控制台应用程序的入口点。
//#define  _CRT_SECURE_NO_WARNINGS

#include "stdafx.h"
#pragma warning(disable:4996)
int main()
{
	printf("请输入身高的英尺和英尺，"); //		“如输入、”5 \7"表示5英尺7英寸:"）；
	int foot;
	int inch;
	scanf("%d", &foot);   printf("\n");
	scanf("%d",  &inch);  printf("\n");
	printf("身高是%d米\n", foot);
	printf("inch %f\n", inch);
	printf("身高是%f米。\n",		((foot + inch / 12)*0.3048));
	scanf("%d", &inch);
	return 0; 
}

