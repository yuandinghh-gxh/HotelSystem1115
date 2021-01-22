using System;

namespace MemecacheDemo {
    class Program
    {
        static void Main(string[] args)
        {
            string[] serverlist = { "127.0.0.1:11211" };
            SockIOPool sock = SockIOPool.GetInstance();
            sock.SetServers(serverlist);//添加服务器列表 
            sock.InitConnections = 5;//设置连接池初始数目
            sock.MinConnections = 3;//设置最小连接数目
            sock.MaxConnections = 6;//设置最大连接数目
            sock.SocketTimeout = 3000;//设置套接字超时读取      
            sock.MaintenanceSleep = 30; //设置维护线程运行的睡眠时间。如果设置为0，那么维护线程将不会启动;
            sock.Failover = true;      //获取或设置池的故障标志。           
            sock.MaxBusy = 1000 * 10;  //socket单次任务的最大时间（单位ms），超过这个时间socket会被强行中断，当前任务失败
            sock.Initialize();

            MemcachedClient mc = new MemcachedClient();
            //清除所有的缓存数据
            mc.FlushAll();

            //增加
            mc.Add("k", "hello");
            //增加一个键k1,值是hello1,有效时间是30s
            mc.Add("k1", "hello1", DateTime.Now.AddSeconds(30));

            // 将k的值重新设置为123  ，用set时，假如键存在，就直接修改，键不存在，就直接创建，再赋值
            mc.Set("k", "123");
            //将k的值重置为456 ， 用Replace时，假如键存在，就直接修改，键不存在，报错
            mc.Replace("k", "456");

            //删除
            mc.Delete("k");
            Console.WriteLine(mc.Get("k1"));
            Console.ReadKey();
        }
    }
}
