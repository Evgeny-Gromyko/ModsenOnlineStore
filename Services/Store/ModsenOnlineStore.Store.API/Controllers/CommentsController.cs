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
        public async Task<IActionResult> GetAllCommentsAsync([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var response = await service.GetAllCommentsAsync(pageNumber, pageSize);

            return Ok(response.Data);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetCommentByIdAsync(int id)
        {
            var response = await service.GetCommentByIdAsync(id);
            
            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddCommentAsync(AddCommentDTO addCommentDto)
        {
            var response = await service.AddCommentAsync(addCommentDto);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpPut]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateCommentAsync(UpdateCommentDTO updateProductDto)
        {
            var response = await service.UpdateCommentAsync(updateProductDto);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpDelete]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> RemoveCommentByIdAsync(int id)
        {
            var response = await service.RemoveCommentByIdAsync(id);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Message);
        }

        [HttpGet("byProduct{id}")]
        public async Task<IActionResult> GetAllCommentsByProductIdAsync(int id, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var response = await service.GetAllCommentsByProductIdAsync(id, pageNumber, pageSize);

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

            return Ok(response.Data);
        }
    }
}
