using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace keyboard  {
    public partial class Form1 : Form {
        public Form1( ) {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            ArrayList students = new ArrayList();
            //实例化几个Student类对象   
            Student rose = new Student( "rose", 25, "reading" );
            Student jack = new Student( "jack", 28, "singing" );
            Student mimi = new Student( "mimi", 26, "dancing" );
            //利用ArrayList类的add()方法添加元素   
            //        students.add();
            students.Add( rose );
            students.Add( jack );
            students.Add( mimi );

            //利用ArrayList的Count属性查看该集合中的元素数量   
            int number = students.Count;
            var box1 = new TextBox { Text = string.Format( "共有元素" + number + "个" ) };
            students.Add( "sdf" );
            textBox2.Text = box1.Text;


            //读取单个元素,因为存入ArrayList中的元素会变为Object类型,   
            //所以，在读取时间，   
            Student stu = students[0] as Student;
            textBox1.Text= stu.say();
            for (int i = 0; i < 20000; i++) {  }
            //遍历元素 -- 通过索引   
            for (int  i = 0; i < students.Count; i++) {
                Student a = students[i] as Student;
                    if (a != null)     textBox1.Text = a.say();
            }
            //利用foreach循环   
            foreach (Object o in students) {
                Student b = o as Student;
                if (b != null) textBox2.Text = b.say();
            }

            //删除元素  通过索引删除               //students.removeAt(0);
            ////删除元素,    通过对象名             //students.remove(jack);
            //清空元素   
            students.Clear();

            //我们知道，ArrayList的容量会随着我们的需要自动按照一定规律   
            //进行填充,当我们确定不再添加元素时，我们要释放多余的空间   
            //这就用到了Capacity属性和TrimtoSize()方法   
            //利用Capacity属性可以查看当前集合的容量      
            //利用TrimtoSize()方法可以释放多余的空间   

            //查看当前容量     
            int numberc = students.Capacity;
            labelX1.Text = numberc.ToString();
            students.TrimToSize();       //去除多余的容量      节省空寂                    

        }
    
    }
        public class Student        {
        public static string strs= "sss";
            public Student()            {
            }
            public Student(String name, int age, String hobby)            {
                this.Name = name;
                this.Age = age;
                this.Hobby = hobby;
            }

            private String name;
            public String Name
            {
                get { return name; }
                set { name = value; }
            }

            private int age;
            public int Age
            {
                get { return age; }
                set { age = value; }
            }

            private String hobby;
         

        public String Hobby
            {
                get { return hobby; }
                set { hobby = value; }
            }
        public string say( ) {
            strs = string.Format( "大家好我是'{0}'，今年{1}岁，我喜欢'{2}'", this.Name, this.Age, this.Hobby );
            return strs;
        }
    }
   }

