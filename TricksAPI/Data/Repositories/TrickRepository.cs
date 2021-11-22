
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TricksAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace TricksAPI.Data.Repositories
{
    public interface ITrickRepository
    {
        Task<Trick> Get(string userId, int trickId);
        Task<List<Trick>> GetAll(string userId);
        Task Create(Trick trick);
        Task Update(Trick trick);
        Task Delete(Trick trick);
    }

    public class TrickRepository : ITrickRepository
    {
        private readonly RestContext _restContext;
        public TrickRepository(RestContext restContext)
        {
            _restContext = restContext;
        }

        public async Task<List<Trick>> GetAll(string userId)
        {
            return await _restContext.Tricks.Where(o => o.UserId == userId).ToListAsync();
        }

        public async Task<Trick> Get(string userId, int trickId)
        {
            return await _restContext.Tricks.FirstOrDefaultAsync(o => o.UserId == userId && o.Id == trickId);
        }

        public async Task Create(Trick trick)
        {
            _restContext.Tricks.Add(trick);
            await _restContext.SaveChangesAsync();
        }

        public async Task Update(Trick trick)
        {
            _restContext.Tricks.Update(trick);
            await _restContext.SaveChangesAsync();
        }

        public async Task Delete(Trick trick)
        {
            _restContext.Tricks.Remove(trick);
            await _restContext.SaveChangesAsync();
        }
    }
}
