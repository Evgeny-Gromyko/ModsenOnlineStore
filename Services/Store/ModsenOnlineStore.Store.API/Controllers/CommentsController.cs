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
        public async Task<IActionResult> GetAllComments()
        {
            return Ok(await service.GetAllComments());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var response = await service.GetCommentById(id);
            
            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(AddCommentDto addCommentDto)
        {
            var response = await service.AddComment(addCommentDto);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment(UpdateCommentDto updateProductDto)
        {
            var response = await service.UpdateComment(updateProductDto);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCommentById(int id)
        {
            var response = await service.RemoveCommentById(id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpGet("byProduct{id}")]
        public async Task<IActionResult> GetAllCommentsByProductId(int id)
        {
            var response = await service.GetAllCommentsByProductId(id);

            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
