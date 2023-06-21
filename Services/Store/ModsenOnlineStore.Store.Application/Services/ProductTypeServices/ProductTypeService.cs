using AutoMapper;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Application.Interfaces.ProductTypeInterfaces;
using ModsenOnlineStore.Store.Domain.DTOs.ProductTypeDTOs;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Application.Services.ProductTypeServices;

public class ProductTypeService: IProductTypeService
    {
        private IMapper mapper;
        
        private IProductTypeRepository repository;

        public ProductTypeService(IMapper mapper, IProductTypeRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<DataResponseInfo<List<GetProductTypeDTO>>> GetAllProductTypes()
        {
            var types = await repository.GetAllProductTypes();
            var typeDTOs = types.Select(p => mapper.Map<GetProductTypeDTO>(p)).ToList();

            return new DataResponseInfo<List<GetProductTypeDTO>>(data: typeDTOs, success: true, message: "all types");
        }

        public async Task<DataResponseInfo<GetProductTypeDTO>> GetSingleProductType(int id)
        {
            var type = await repository.GetSingleProductType(id);
            
            if (type is null) {
                return new DataResponseInfo<GetProductTypeDTO>(data: null, success: false, message: "type not found");
            }

            var typeDto = mapper.Map<GetProductTypeDTO>(type);
            
            return new DataResponseInfo<GetProductTypeDTO>(data: typeDto, success: true, message: $"product type with id {id}");
        }

        public async Task<ResponseInfo> AddProductType(AddUpdateProductTypeDTO type)
        {
            var newProductType = mapper.Map<ProductType>(type);
            await repository.AddProductType(newProductType);
            
            return new ResponseInfo(success: true, message: "product type added");
        }

        public async Task<ResponseInfo> UpdateProductType(int id, AddUpdateProductTypeDTO typeDTO)
        {
            var type = await repository.UpdateProductType(id, mapper.Map<ProductType>(typeDTO));
            
            if (type is null)
            {
                return new ResponseInfo(success: false, message: "type not found");
            }

            return new ResponseInfo(success: true, message: $"type with id {id} updated");
        }
        
        public async Task<ResponseInfo> DeleteProductType(int id)
        {
            var type = await repository.DeleteProductType(id);
            
            if (type is null)
            {
                return new ResponseInfo(success: false, message: "type not found");
            }
            
            return new ResponseInfo(success: true, message: "type deleted");
        }
    }
