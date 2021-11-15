using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TricksAPI.Data.Dtos.Auth
{
    public record RegisterUserDto(
        [Required] string UserName,
        [EmailAddress][Required] string Email,
        [Required] string Password);
}
