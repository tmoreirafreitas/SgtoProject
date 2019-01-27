using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;

namespace Sgot.Service.ViewModels
{
    public class LoginViewModel
    {
        public string Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string FullName { get; set; }

        [Display(Name = "UserName")]
        public string UserName { get; set; }

        public string Token { get; set; }

        public IList<string> Roles { get; }
        public IList<Claim> Claims { get; }

        public DateTime CreateDate { get; set; }

        public LoginViewModel()
        {
            Roles = new List<string>();
            Claims = new List<Claim>();
        }
    }
}