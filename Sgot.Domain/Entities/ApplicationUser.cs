using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Sgot.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime CreateDate { get; private set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public virtual IList<string> Roles { get; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public virtual IList<Claim> Claims { get; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public virtual IList<Pedido> Pedidos { get; }


        public ApplicationUser()
        {
            Roles = new List<string>();
            Claims = new List<Claim>();
            Pedidos = new List<Pedido>();
            CreateDate = DateTime.Now;
        }
    }
}
