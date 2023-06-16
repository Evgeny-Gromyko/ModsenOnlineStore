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
            return Ok(await service.GetCommentById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(AddCommentDto addCommentDto)
        {
            return Ok(await service.AddComment(addCommentDto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment(UpdateCommentDto updateProductDto)
        {
            return Ok(await service.UpdateComment(updateProductDto));
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveCommentById(int id)
        {
            return Ok(await service.RemoveCommentById(id));
        }
    }
}
