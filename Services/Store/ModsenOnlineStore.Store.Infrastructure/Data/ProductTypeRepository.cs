using Microsoft.EntityFrameworkCore;
using ModsenOnlineStore.Store.Application.Interfaces.ProductTypeInterfaces;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Infrastructure.Data;

public class ProductTypeRepository : PagedRepository<ProductType>, IProductTypeRepository
{
    private readonly DataContext context;
    
    public ProductTypeRepository(DataContext context)
    {
        this.context = context;
    }
    
    public async Task<List<ProductType>> GetAllProductTypesAsync(int pageNumber, int pageSize)
    {
        var productTypes = await context.ProductTypes.AsNoTracking().ToListAsync();

        return ToPagedList(productTypes, pageNumber, pageSize);
    }

    public async Task<ProductType> GetSingleProductTypeAsync(int id) =>
        await context.ProductTypes.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

    public async Task<ProductType> AddProductTypeAsync(ProductType newProductType)
    { 
        context.ProductTypes.Add(newProductType);
        await context.SaveChangesAsync();

        return newProductType;
    }

    public async Task<ProductType> UpdateProductTypeAsync(int id, ProductType newProductType)
    {
        var prevProductType = await context.ProductTypes.FirstOrDefaultAsync(e => e.Id == id);
        
        if (prevProductType is null) return null;

        prevProductType.TypeName = newProductType.TypeName;

        await context.SaveChangesAsync();

        return newProductType;
    }

    public async Task<ProductType> DeleteProductTypeAsync(int id)
    {
        var productType = await GetSingleProductTypeAsync(id);
        
        if (productType is null) return null;

        context.ProductTypes.Remove(productType);
        await context.SaveChangesAsync();
        
        return productType;
    }
}
