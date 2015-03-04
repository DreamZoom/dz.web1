using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dz.web;
namespace adminTest.Models
{
    public class User : dz.web.model.ModelBase
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
        public DateTime ResigterTime { get; set; }
    }

    public class UserService : dz.web.service.ServiceBase
    {
    }
}