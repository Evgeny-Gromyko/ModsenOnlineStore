using Microsoft.EntityFrameworkCore;
using ModsenOnlineStore.Store.Application.Interfaces;
using ModsenOnlineStore.Store.Application.Interfaces.ProductTypeInterfaces;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Infrastructure.Data;

public class ProductTypeRepository : IProductTypeRepository
{
    private DataContext context;
    
    public ProductTypeRepository(DataContext context)
    {
        this.context = context;
    }
    
    public async Task<List<ProductType>> GetAllProductTypes() =>
        await context.ProductTypes.AsNoTracking().ToListAsync();

    public async Task<ProductType> GetSingleProductType(int id) =>
        await context.ProductTypes.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

    public async Task<ProductType> AddProductType(ProductType newProductType)
    { 
        context.ProductTypes.Add(newProductType);
        await context.SaveChangesAsync();

        return newProductType;
    }

    public async Task<ProductType> UpdateProductType(int id, ProductType newProductType)
    {
        var prevProductType = await context.ProductTypes.FirstOrDefaultAsync(e => e.Id == id);
        if (prevProductType is null) return null;

        prevProductType.TypeName = newProductType.TypeName;

        await context.SaveChangesAsync();

        return newProductType;
    }

    public async Task<ProductType> DeleteProductType(int id)
    {
        var _productType = await GetSingleProductType(id);
        
        if (_productType is null) return null;

        context.ProductTypes.Remove(_productType);
        await context.SaveChangesAsync();
        
        return _productType;
    }
}