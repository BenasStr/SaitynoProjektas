using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TricksAPI.Data.Repositories;
using AutoMapper;
using TricksAPI.Data.Dtos.Comment;
using TricksAPI.Data.Entities;
using TricksAPI.Auth.Model;

namespace TricksAPI.Controllers
{
    /*
        COMMENTS
    /api/lesson/{id}/comment            GET ALL 200
    /api/lesson/{id}/comment/{id}	    GET USER 200
    /api/lesson/{id}/comment	        POST 201
    /api/lesson/{id}/comment/{id}	    PUT 200
    /api/lesson/{id}/comment/{id}	    DELETE 200/204
    */
    [ApiController]
    [Route("api/lessons/{lessonId}/comments")]
    public class CommentController : ControllerBase
    {
        private readonly ILessonRepository _lessonRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;

        public CommentController(ILessonRepository lessonRepository, ICommentRepository commentRepository,
            IMapper mapper, IAuthorizationService authorizationService)
        {
            _lessonRepository = lessonRepository;
            _commentRepository = commentRepository;
            _mapper = mapper;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<IEnumerable<CommentDto>> GetAll(int lessonId)
        {
            var comments = await _commentRepository.GetAll(lessonId);
            return comments.Select(o => _mapper.Map<CommentDto>(o));
        }

        [HttpGet("{commentId}")]
        public async Task<ActionResult<CommentDto>> Get(int lessonId, int commentId)
        {
            var lesson = await _lessonRepository.Get(lessonId);
            if (lesson == null) return NotFound($"Lesson with id '{lessonId}' not found.");

            var comment = await _commentRepository.Get(lessonId, commentId);
            if (comment == null) return NotFound($"Comment with id '{commentId}' not found.");

            return Ok(_mapper.Map<CommentDto>(comment));
        }

        [HttpPost]
        [Authorize(Roles = RestUserRoles.SimpleUser)]
        public async Task<ActionResult<CommentDto>> Post(int lessonId, CreateCommentDto commentDto)
        {
            var lesson = await _lessonRepository.Get(lessonId);
            if (lesson == null) return NotFound($"Couldn't find a lesson with id of {lessonId}");

            var comment = _mapper.Map<Comment>(commentDto);
            comment.LessonId = lessonId;
            //comment.UserId = User;
            var userId = User.Identity;

            await _commentRepository.Create(comment);

            return Created($"/api/lessons/{lessonId}/comments/{comment.Id}", _mapper.Map<CommentDto>(comment));
        }

        [HttpPatch("{commentId}")]
        [Authorize(Roles = RestUserRoles.SimpleUser)]
        public async Task<ActionResult<Lesson>> Patch(int lessonId, int commentId, UpdateCommentDto commentDto)
        {
            var lesson = await _lessonRepository.Get(lessonId);
            if (lesson == null) return NotFound($"Lesson with id '{lessonId}' not found.");

            var oldComment = await _commentRepository.Get(lessonId, commentId);
            if (oldComment == null) return NotFound($"Comment with id '{commentId}' not found.");

            var authResult = await _authorizationService.AuthorizeAsync(User, commentDto, PolicyNames.SameUser);

            if (!authResult.Succeeded)
                return Forbid();

            _mapper.Map(commentDto, oldComment);

            await _commentRepository.Update(oldComment);

            return Ok(_mapper.Map<CommentDto>(oldComment));
        }

        [HttpDelete("{commentId}")]
        [Authorize(Roles = RestUserRoles.SimpleUser)]
        public async Task<ActionResult> Delete(int lessonId, int commentId)
        {
            var lesson = await _lessonRepository.Get(lessonId) ;
            if (lesson == null) return NotFound($"Lesson with id '{lessonId}' not found.");

            var comment = await _commentRepository.Get(lessonId, commentId);
            if (comment == null) return NotFound($"Comment with id '{commentId}' not found.");

            /*var authResult = await _authorizationService.AuthorizeAsync(User, commentDto, PolicyNames.SameUser);
            if (!authResult.Succeeded)
                return Forbid();*/

            await _commentRepository.Delete(comment);

            return NoContent();
        }
    }
}
