using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TricksAPI.Data.Dtos.Auth
{
    public record UserDto(string Id, string UserName, string Email);
}
