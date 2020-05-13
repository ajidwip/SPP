using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BarcodeTeknik.GetterSetter
{
    public class UserGetSet
    {
        public string UserId { get; set; }
         
        public string UserName { get; set; }

        public string email { get; set; }

        public string NIK { get; set; }

        public string GolonganUser { get; set; }

        public bool Active { get; set; }
    }
}