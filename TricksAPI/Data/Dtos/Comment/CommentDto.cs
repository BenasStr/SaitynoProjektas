using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TricksAPI.Data.Dtos.Comment
{
    public record CommentDto(int Id, int Likes, string Text);
}
