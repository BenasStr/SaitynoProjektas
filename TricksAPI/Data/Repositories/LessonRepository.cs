using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TricksAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace TricksAPI.Data.Repositories
{
    public interface ILessonRepository
    {
        Task<List<Lesson>> GetAll();
        Task<Lesson> Get(int id);
        Task Create(Lesson lesson);
        Task Update(Lesson lesson);
        Task Delete(Lesson lesson);
    }

    public class LessonRepository : ILessonRepository
    {
        private readonly RestContext _restContext;
        public LessonRepository(RestContext restContext)
        {
            _restContext = restContext;
        }

        public async Task<List<Lesson>> GetAll()
        {
            return await _restContext.Lessons.ToListAsync();
        }

        public async Task<Lesson> Get(int id)
        {
            return await _restContext.Lessons.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task Create(Lesson lesson)
        {
            _restContext.Lessons.Add(lesson);
            await _restContext.SaveChangesAsync();
        }

        public async Task Update(Lesson lesson)
        {
            _restContext.Lessons.Update(lesson);
            await _restContext.SaveChangesAsync();
        }

        public async Task Delete(Lesson lesson)
        {
            _restContext.Lessons.Remove(lesson);
            await _restContext.SaveChangesAsync();
        }
    }
}
