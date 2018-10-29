using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace prj28102018.Models
{
    public class UserModel
    {
        public int UserID { get; set; }
        [Required(ErrorMessage ="*")]
        public string UserName { get; set;}
        [Required(ErrorMessage = "*")]
        public string UserPwd { get; set; }
        public string ImageURL { get; set; }

    }
}