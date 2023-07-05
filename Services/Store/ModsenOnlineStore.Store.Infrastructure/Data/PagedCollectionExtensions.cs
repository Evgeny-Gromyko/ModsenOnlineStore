namespace ModsenOnlineStore.Store.Infrastructure.Data
{
    public static class PagedCollectionExtensions
    {
        public static IEnumerable<T> ToPagedCollection<T>(this IEnumerable<T> entities, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                return entities;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }

            return entities.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}
