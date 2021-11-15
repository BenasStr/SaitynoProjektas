using System;
using System.ComponentModel.DataAnnotations;
using TricksAPI.Data.Dtos.Auth;
using TricksAPI.Auth.Model;

namespace TricksAPI.Data.Entities
{
    public class Comment : IUserOwnedResource
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Likes { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }

        [Required]
        public string UserId { get; set; }
        public RestUser User { get; set; }
    }
}
