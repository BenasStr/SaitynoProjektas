using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TricksAPI.Data.Repositories;
using TricksAPI.Data.Entities;
using TricksAPI.Data.Dtos.Lessons;
using TricksAPI.Auth.Model;

namespace TricksAPI.Controllers
{
    /*
        LESSON
    /api/lesson	        GET ALL 200
    /api/lesson/{id}	GET USER 200
    /api/lesson	        POST 201
    /api/lesson/{id}	PUT 200
    /api/lesson/{id}	DELETE 200/204
     */

    [ApiController]
    [Route("api/lessons")]
    public class LessonController : ControllerBase
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly IMapper _mapper;

        public LessonController(ILessonRepository lessonRepository, IMapper mapper)
        {
            _lessonRepository = lessonRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<LessonDto>> GetAll()
        {
            var lesson = await _lessonRepository.GetAll();
            return lesson.Select(o => _mapper.Map<LessonDto>(o));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Lesson>> Get(int id)
        {
            var lesson = await _lessonRepository.Get(id);
            if (lesson == null) return NotFound($"Lesson with id '{id}' not found.");

            return Ok(_mapper.Map<LessonDto>(lesson));
        }

        [HttpPost]
        [Authorize(Roles = RestUserRoles.Admin)]
        public async Task<ActionResult<LessonDto>> Post(CreateLessonDto lessonDto)
        {
            var lesson = _mapper.Map<Lesson>(lessonDto);

            await _lessonRepository.Create(lesson);

            return Created($"/api/lesson/{lesson.Id}", _mapper.Map<LessonDto>(lesson));
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = RestUserRoles.Admin)]
        public async Task<ActionResult<Lesson>> Patch(int id, UpdateLessonDto lessonDto)
        {
            var lesson = await _lessonRepository.Get(id);
            if (lesson == null) return NotFound($"Lesson with id '{id}' not found.");

            _mapper.Map(lessonDto, lesson);

            await _lessonRepository.Update(lesson);

            return Ok(_mapper.Map<LessonDto>(lesson));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RestUserRoles.Admin)]
        public async Task<ActionResult<Lesson>> Delete(int id)
        {
            var lesson = await _lessonRepository.Get(id);
            if (lesson == null) return NotFound($"Lesson with id '{id}' not found.");

            await _lessonRepository.Delete(lesson);

            return NoContent();
        }
    }
}
