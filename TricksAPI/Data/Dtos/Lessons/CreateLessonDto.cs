using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TricksAPI.Data.Dtos.Lessons
{
    public record CreateLessonDto([Required]string Video, [Required]string Description);
}
