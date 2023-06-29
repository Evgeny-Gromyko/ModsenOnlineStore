using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModsenOnlineStore.Store.Application.Interfaces.CommentInterfaces;
using ModsenOnlineStore.Store.Domain.DTOs.CommentDTOs;

namespace ModsenOnlineStore.Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService service;

        public CommentsController(ICommentService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllComments()
        {
            var response = await service.GetAllComments();
            return Ok(response.Data);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var response = await service.GetCommentById(id);
            
            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddComment(AddCommentDto addCommentDto)
        {
            var response = await service.AddComment(addCommentDto);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpPut]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateComment(UpdateCommentDto updateProductDto)
        {
            var response = await service.UpdateComment(updateProductDto);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpDelete]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> RemoveCommentById(int id)
        {
            var response = await service.RemoveCommentById(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpGet("byProduct{id}")]
        public async Task<IActionResult> GetAllCommentsByProductId(int id)
        {
            var response = await service.GetAllCommentsByProductId(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }
    }
}
