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

        public async Task<List<Comment>> GetAllCommentsAsync(int pageNumber, int pageSize)
        {
            var comments = await context.Comments.AsNoTracking().ToListAsync();

            if (pageNumber < 1)
            {
                return comments;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }

            return comments.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<Comment?> GetCommentByIdAsync(int id)
        {
            return await context.Comments.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddCommentAsync(Comment comment)
        {
            context.Comments.Add(comment);
            await context.SaveChangesAsync();
        }

        public async Task UpdateCommentAsync(Comment comment)
        {
            context.Comments.Update(comment);
            await context.SaveChangesAsync();
        }

        public async Task RemoveCommentByIdAsync(int id)
        {
            var comment = await context.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (comment is not null)
            {
                context.Comments.Remove(comment);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Comment>> GetAllCommentsByProductIdAsync(int id, int pageNumber, int pageSize)
        {
            var comments = await context.Comments.AsNoTracking().Where(c => c.ProductId == id).ToListAsync();

            if (pageNumber < 1)
            {
                return comments;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }

            return comments.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
