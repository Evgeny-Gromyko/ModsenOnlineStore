using Microsoft.EntityFrameworkCore;
using ModsenOnlineStore.Store.Application.Interfaces.CommentInterfaces;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Infrastructure.Data
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext context;

        public CommentRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<List<Comment>> GetAllComments()
        {
            return await context.Comments.AsNoTracking().ToListAsync();
        }

        public async Task<Comment?> GetCommentById(int id)
        {
            return await context.Comments.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Comment>> AddComment(Comment comment)
        {
            context.Comments.Add(comment);
            await context.SaveChangesAsync();
            return await GetAllComments();
        }

        public async Task<List<Comment>> UpdateComment(Comment comment)
        {
            context.Comments.Update(comment);
            await context.SaveChangesAsync();
            return await GetAllComments();
        }

        public async Task<List<Comment>> RemoveCommentById(int id)
        {
            var comment = await context.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (comment is not null)
            {
                context.Comments.Remove(comment);
                await context.SaveChangesAsync();
            }

            return await GetAllComments();
        }
    }
}
