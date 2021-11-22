using System.ComponentModel.DataAnnotations;
using TricksAPI.Auth.Model;
using TricksAPI.Data.Dtos.Auth;

namespace TricksAPI.Data.Entities
{
    public class Trick : IUserOwnedResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

        [Required]
        public string UserId { get; set; }
        public RestUser User { get; set; }
    }
}
