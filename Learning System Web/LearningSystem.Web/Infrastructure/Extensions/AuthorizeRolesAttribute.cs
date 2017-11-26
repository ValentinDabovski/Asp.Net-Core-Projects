using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace LearningSystem.Web.Infrastructure.Extensions
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {

        public AuthorizeRolesAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }
}
