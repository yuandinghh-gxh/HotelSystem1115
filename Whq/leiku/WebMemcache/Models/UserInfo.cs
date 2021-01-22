using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebMemcache
{
     [Serializable]
    public class UserInfo
    {
        public string UName { get; set; }

        [Required]
        [MaxLength(32)]
        public string UPwd { get; set; }
      
        public int UserId { get; set; }
    }
}