using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerCenter.Models
{
    public class RegisterViewModel
    {

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage ="两次输入密码不一致")]
        public string PasswordConfirm { get; set; }
    }
}
