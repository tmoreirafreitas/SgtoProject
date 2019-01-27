using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sgot.Domain.Entities;
using Sgot.Domain.Interfaces.Repositories;
using Sgot.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sgot.Infra.Data.Repositories
{
    public class AuthRepository
    {
        public UserManager<ApplicationUser> UserManager { get; }
        public SignInManager<ApplicationUser> SignInManager { get; }

        public AuthRepository(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
    }
}
