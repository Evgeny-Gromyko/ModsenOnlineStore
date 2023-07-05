namespace ModsenOnlineStore.Store.Infrastructure.Data
{
    public abstract class PagedRepository<T>
    {
        protected List<T> ToPagedList(List<T> entities, int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                return entities;
            }

            if (pageSize < 1)
            {
                pageSize = 10;
            }

            return entities.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
