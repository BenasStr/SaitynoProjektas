using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TricksAPI.Data.Repositories;
using AutoMapper;
using TricksAPI.Data.Dtos.Trick;
using TricksAPI.Data.Entities;
using TricksAPI.Auth.Model;

namespace TricksAPI.Controllers
{
    /*  
        TRICK
    /api/user/{id}/tricks	    GET ALL 200
    /api/user/{id}/tricks/{id}	GET USER 200
    /api/user/{id}/tricks	    POST 201
    /api/user/{id}/tricks/{id}	PUT 200
    /api/user/{id}/tricks/{id}	DELETE 200/204
     */

    [ApiController]
    [Route("api/user/{userId}/tricks")]
    public class TricksController : ControllerBase
    {
        private readonly ITrickRepository _trickRepository;
        private readonly IMapper _mapper;

        public TricksController(ITrickRepository trickRepository, IMapper mapper)
        {
            _trickRepository = trickRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = RestUserRoles.SimpleUser)]
        public async Task<IEnumerable<TrickDto>> GetAll(int userId)
        {
            var trick = await _trickRepository.GetAll(userId);
            return trick.Select(o => _mapper.Map<TrickDto>(o));
        }

        [HttpGet("{trickId}")]
        [Authorize(Roles = RestUserRoles.SimpleUser)]
        public async Task<ActionResult<TrickDto>> Get(int userId, int trickId)
        {
            /*var user = await _userRepository.Get(userId);
            if (user == null) return NotFound($"User with id '{userId}' not found.");*/

            var trick = await _trickRepository.Get(userId, trickId);
            if (trick == null) return NotFound($"Trick with id '{trickId}' not found.");

            return Ok(_mapper.Map<TrickDto>(trick));
        }

        [HttpPost]
        [Authorize(Roles = RestUserRoles.Admin)]
        public async Task<ActionResult<TrickDto>> Post(int userId, TrickDto trickDto)
        {
            /*var user = await _userRepository.Get(userId);
            if (user == null) return NotFound($"Couldn't find a user with id of {userId}");*/

            var trick = _mapper.Map<Trick>(trickDto);
            trick.Id = userId;
            await _trickRepository.Create(trick);

            return Created($"/api/lesson/{userId}/comment/{trick.Id}", _mapper.Map<TrickDto>(trick));
        }

        [HttpPatch("{trickId}")]
        [Authorize(Roles = RestUserRoles.Admin)]
        public async Task<ActionResult<Lesson>> Patch(int userId, int trickId, TrickDto trickDto)
        {
            /*var user = await _userRepository.Get(userId);
            if (user == null) return NotFound($"User with id '{userId}' not found.");*/

            var oldTrick = await _trickRepository.Get(userId, trickId);
            if (oldTrick == null) return NotFound($"Trick with id '{trickId}' not found.");

            _mapper.Map(trickDto, oldTrick);

            await _trickRepository.Update(oldTrick);

            return Ok(_mapper.Map<TrickDto>(oldTrick));
        }

        [HttpDelete("{trickId}")]
        [Authorize(Roles = RestUserRoles.Admin)]
        public async Task<ActionResult> Delete(int userId, int trickId)
        {
            /*var user = await _userRepository.Get(userId);
            if (user == null) return NotFound($"User with id '{userId}' not found.");*/

            var trick = await _trickRepository.Get(userId, trickId);
            if (trick == null) return NotFound($"Trick with id '{trickId}' not found.");

            await _trickRepository.Delete(trick);

            return NoContent();
        }
    }
}
