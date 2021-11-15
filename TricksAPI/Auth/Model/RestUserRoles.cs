using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TricksAPI.Auth.Model
{
    public static class RestUserRoles
    {
        public const string Admin = nameof(Admin);
        public const string SimpleUser = nameof(SimpleUser);
        public const string PremiumUser = nameof(PremiumUser);

        public static readonly IReadOnlyCollection<string> All = new[] { Admin, SimpleUser, PremiumUser };
    }
}
