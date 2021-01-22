using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//namespace ConsoleApplication2
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//        }
//    }
//}

public class IndexerClass
{
    private Hashtable name = new Hashtable();

    //1：通过key存取Values
    public string this[int index]
    {
        get { return name[index].ToString(); }
        set { name.Add(index, value); }
    }

    //2：通过Values存取key
    public int this[string aName]
    {
        get
        {
            //Hashtable中实际存放的是DictionaryEntry(字典)类型，如果要遍历一个Hashtable，就需要使用到DictionaryEntry
            foreach (DictionaryEntry d in name)
            {
                if (d.Value.ToString() == aName)
                {
                    return Convert.ToInt32(d.Key);
                }
            }
            return -1;
        }
        set
        {
            name.Add(value, aName);
        }
    }
}
// 测试 object 类
/*
public class Object
{
    public static bool Equals(object objA, object objB) { }
    public static bool ReferenceEquals(object objA, object objB) { }

    public Object() { }

    public virtual bool Equals(object obj) { }
    public virtual int GetHashCode() { }
    public Type GetType() { }
    public virtual string ToString() { }

    protected virtual void Finalize() { }
    protected object MemberwiseClone() { }
}
 */

struct A
{
    public int count;
}
class B
{
    public int number;
}
class C
{
    public int integer = 0;
    public override bool Equals(object obj)
    {
        C c = obj as C;
        if (c != null)
            return this.integer == c.integer;
        else
            return false;
    }
    public override int GetHashCode()
    {
        return 2 ^ integer;
    }
}

public class Test
{
    static void Main()
    {
        IndexerClass Indexer = new IndexerClass();

        //第一种索引器的使用
        Indexer[1] = "张三";//set访问器的使用
        Indexer[2] = "李四";
        Console.WriteLine("编号为1的名字：" + Indexer[1]);//get访问器的使用
        Console.WriteLine("编号为2的名字：" + Indexer[2]);

        Console.WriteLine();
        //第二种索引器的使用
        Console.WriteLine("张三的编号是：" + Indexer["张三"]);//get访问器的使用
        Console.WriteLine("李四的编号是：" + Indexer["李四"]);
        Indexer["王五"] = 3;//set访问器的使用
        Console.WriteLine("王五的编号是：" + Indexer["王五"]);

        A a1, a2;
        a1.count = 10;
        a2 = a1;

        //Console.Write(a1==a2);没有定义“==”操作符
        Console.Write(a1.Equals(a2));//True
        Console.WriteLine(object.ReferenceEquals(a1, a2));//False

        B b1 = new B();
        B b2 = new B();

        b1.number = 10;
        b2.number = 10;
        Console.Write(b1 == b2);//False
        Console.Write(b1.Equals(b2));//False
        Console.WriteLine(object.ReferenceEquals(b1, b2));//False

        b2 = b1;
        Console.Write(b1 == b2);//True
        Console.Write(b1.Equals(b2));//True
        Console.WriteLine(object.ReferenceEquals(b1, b2));//True

        C c1 = new C();
        C c2 = new C();

        c1.integer = 10;
        c2.integer = 10;
        Console.Write(c1 == c2);//False
        Console.Write(c1.Equals(c2));//True
        Console.WriteLine(object.ReferenceEquals(c1, c2));//False

        c2 = c1;
        Console.Write(c1 == c2);//True
        Console.Write(c1.Equals(c2));//True
        Console.WriteLine(object.ReferenceEquals(c1, c2));//True
        int i=b1.GetHashCode();
        string s= c1.GetType().ToString();
        Console.WriteLine("type:{0},get:{1}", s, i.ToString());

    }
}