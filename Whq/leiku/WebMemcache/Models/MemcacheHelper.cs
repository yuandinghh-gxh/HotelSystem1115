﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memcached.ClientLibrary;

namespace WebMemcache
{
    public static class MemcacheHelper
    {
        private static MemcachedClient mc;

        static MemcacheHelper()
        {
            //通过客户端来进行memcached的集群配置，在插入数据的时候，使用一致性哈希算法，将对应的value值存入Memcached
            String[] serverlist = { "127.0.0.1:11211" , "127.0.0.1:11212" };

            // 初始化Memcached的服务池
            SockIOPool pool = SockIOPool.GetInstance("test");
            //设置服务器列表
            pool.SetServers(serverlist);
            //各服务器之间负载均衡的设置比例
            pool.SetWeights(new int[] { 1 });
            pool.Initialize();
            //创建一个Memcached的客户端对象
            mc = new MemcachedClient();
            mc.PoolName = "test";
            //是否启用压缩数据：如果启用了压缩，数据压缩长于门槛的数据将被储存在压缩的形式
            mc.EnableCompression = false;
            
        }
        /// <summary>
        /// 插入值
        /// </summary>
        /// <param name="key">建</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间</param>
        /// <returns></returns>
        public static bool Set(string key, object value,DateTime expiry){
            return mc.Set(key, value, expiry);
        }
        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        {
            return mc.Get(key);
        }
    }
}