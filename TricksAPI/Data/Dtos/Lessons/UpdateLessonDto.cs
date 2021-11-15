using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TricksAPI.Data.Dtos.Lessons
{
    public record UpdateLessonDto(string Video, string Description);
}