using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TricksAPI.Data.Dtos.Auth
{
    public class RestUser : IdentityUser
    {
        [PersonalData]
        public string Tricks { get; set; }
    }
}
