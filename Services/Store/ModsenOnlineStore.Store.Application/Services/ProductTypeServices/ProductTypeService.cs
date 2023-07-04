using AutoMapper;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Application.Interfaces.ProductTypeInterfaces;
using ModsenOnlineStore.Store.Domain.DTOs.ProductTypeDTOs;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Application.Services.ProductTypeServices;

public class ProductTypeService: IProductTypeService
{
    private readonly IMapper mapper;
        
    private readonly IProductTypeRepository repository;

    public ProductTypeService(IMapper mapper, IProductTypeRepository repository)
    {
        this.mapper = mapper;
        this.repository = repository;
    }

    public async Task<DataResponseInfo<List<GetProductTypeDTO>>> GetAllProductTypesAsync(int pageNumber, int pageSize)
    {
        var types = await repository.GetAllProductTypesAsync(pageNumber, pageSize);
        var typeDTOs = types.Select(p => mapper.Map<GetProductTypeDTO>(p)).ToList();

        return new DataResponseInfo<List<GetProductTypeDTO>>(data: typeDTOs, success: true, message: "all types");
    }

    public async Task<DataResponseInfo<GetProductTypeDTO>> GetSingleProductTypeAsync(int id)
    {
        var type = await repository.GetSingleProductTypeAsync(id);
            
        if (type is null) {
            return new DataResponseInfo<GetProductTypeDTO>(data: null, success: false, message: "type not found");
        }

        var typeDto = mapper.Map<GetProductTypeDTO>(type);
            
        return new DataResponseInfo<GetProductTypeDTO>(data: typeDto, success: true, message: $"product type with id {id}");
    }

    public async Task<ResponseInfo> AddProductTypeAsync(AddUpdateProductTypeDTO type)
    {
        var newProductType = mapper.Map<ProductType>(type);
        await repository.AddProductTypeAsync(newProductType);
            
        return new ResponseInfo(success: true, message: "product type added");
    }

    public async Task<ResponseInfo> UpdateProductTypeAsync(int id, AddUpdateProductTypeDTO typeDTO)
    {
        var type = await repository.UpdateProductTypeAsync(id, mapper.Map<ProductType>(typeDTO));
            
        if (type is null)
        {
            return new ResponseInfo(success: false, message: "type not found");
        }

        return new ResponseInfo(success: true, message: $"type with id {id} updated");
    }
        
    public async Task<ResponseInfo> DeleteProductTypeAsync(int id)
    {
        var type = await repository.DeleteProductTypeAsync(id);
            
        if (type is null)
        {
            return new ResponseInfo(success: false, message: "type not found");
        }
            
        return new ResponseInfo(success: true, message: "type deleted");
    }
}
