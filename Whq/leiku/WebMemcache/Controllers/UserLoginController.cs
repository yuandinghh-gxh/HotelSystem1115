using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;

namespace WebMemcache.Controllers
{
    public class UserLoginController : Controller
    {
        // GET: UserLogin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(UserInfo user)
        {

            string sql = "select * from UserInfo where UName=@UName and UPwd=@UPwd";
            using (var conn = SqlHelper.Connection)
            {
                 var  loginUser1 = conn.QueryFirstOrDefault<UserInfo>(sql, new  { UName = user.UName, UPwd = user.UPwd });
                var loginUser = SqlMapper.QueryFirstOrDefault<UserInfo>(conn,sql, new  { UName = user.UName, UPwd = user.UPwd });
                if (loginUser == null)
                {
                    return Content("用户名密码错误！");
                }
                else
                {
                    Guid sessionId = Guid.NewGuid();//申请了一个模拟的GUID：SessionId

                    //把sessionid写到客户端浏览器里面去了（一定要把sessionid写到客户端，这样用户在访问其他web资源的时候，就会把cookie中的信息传给服务器，然后通过sessionid的key到Memcached中去取对应的值）
                    Response.Cookies["sessionId"].Value =  sessionId.ToString();

                    //再把用户的信息插入到Memcached中
                    MemcacheHelper.Set(sessionId.ToString(), loginUser, DateTime.Now.AddMinutes(20));
                    return Content("ok");
                }
            }


            //var loginUser = dbContext.UserInfo.Where(u => u.UName.Equals(user.UName) && u.UPwd.Equals(user.UPwd)).FirstOrDefault();

            //if (loginUser == null)
            //{
            //    return Content("用户名密码错误！");
            //}
            //else
            //{
            //    Guid sessionId = Guid.NewGuid();//申请了一个模拟的GUID：SessionId

            //    //把sessionid写到客户端浏览器里面去了（一定要把sessionid写到客户端，这样用户在访问其他web资源的时候，就会把cookie中的信息传给服务器，然后通过sessionid的key到Memcached中去取对应的值）
            //    Response.Cookies["sessionId"].Value = "zs";//sessionId.ToString();

            //    //再把用户的信息插入到Memcached中
            //    MemcacheHelper.Set("zs", loginUser, DateTime.Now.AddMinutes(20));
            //    return Content("ok");
            //}
        }
    }
}