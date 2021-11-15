using System;
using System.Collections.Generic;
using System.Linq;
using TricksAPI.Data.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TricksAPI.Data.Repositories
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAll(int lessonId);
        Task<Comment> Get(int lessonId, int commentId);
        Task Create(Comment comment);
        Task Update(Comment comment);
        Task Delete(Comment comment);
    }

    public class CommentRepository : ICommentRepository
    {
        private readonly RestContext _restContext;

        public CommentRepository(RestContext restContext)
        {
            _restContext = restContext;
        }

        public async Task<List<Comment>> GetAll(int lessonId)
        {
            return await _restContext.Comments.Where(o => o.LessonId == lessonId).ToListAsync();
        }

        public async Task<Comment> Get(int lessonId, int commentId)
        {
            return await _restContext.Comments.FirstOrDefaultAsync(o => o.LessonId == lessonId && o.Id == commentId);
        }

        public async Task Create(Comment comment)
        {
            _restContext.Comments.Add(comment);
            await _restContext.SaveChangesAsync();
        }

        public async Task Update(Comment comment)
        {
            _restContext.Comments.Update(comment);
            await _restContext.SaveChangesAsync();
        }

        public async Task Delete(Comment comment)
        {
            _restContext.Comments.Remove(comment);
            await _restContext.SaveChangesAsync();
        }
    }
}
