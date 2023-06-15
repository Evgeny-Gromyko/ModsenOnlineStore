using AutoMapper;
using ModsenOnlineStore.Common;
using ModsenOnlineStore.Store.Application.Interfaces.ProductTypeInterfaces;
using ModsenOnlineStore.Store.Domain.DTOs.ProductTypeDTOs;
using ModsenOnlineStore.Store.Domain.Entities;

namespace ModsenOnlineStore.Store.Infrastructure.Services.ProductTypeServices;

public class ProductTypeService: IProductTypeService
    {
        private IMapper mapper;
        
        private IProductTypeRepository repository;

        public ProductTypeService(IMapper mapper, IProductTypeRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<ResponseInfo<List<GetProductTypeDTO>>> GetAllProductTypes()
        {
            var types = await repository.GetAllProductTypes();
            var typeDTOs = types.Select(p => mapper.Map<GetProductTypeDTO>(p)).ToList();
            
            if (typeDTOs.Count == 0)
            {
                return new ResponseInfo<List<GetProductTypeDTO>>(typeDTOs, true, "no types yet");
            }  
            
            return new ResponseInfo<List<GetProductTypeDTO>>(typeDTOs, true, "all types");
        }

        public async Task<ResponseInfo<GetProductTypeDTO>> GetSingleProductType(int id)
        {
            var type = await repository.GetSingleProductType(id);
            
            if (type is null) {
                return new ResponseInfo<GetProductTypeDTO>(null, false, "type not found");
            }
            
            return new ResponseInfo<GetProductTypeDTO>(mapper.Map<GetProductTypeDTO>(type), true, $"product type with id {id}");
        }

        public async Task<ResponseInfo<string>> AddProductType(AddUpdateProductTypeDTO type)
        {
            var newProductType = mapper.Map<ProductType>(type);
            await repository.AddProductType(newProductType);
            
            return new ResponseInfo<string>("added successfully", true, "type");
        }

        public async Task<ResponseInfo<string>> UpdateProductType(int id, AddUpdateProductTypeDTO typeDTO)
        {
            var type = await repository.UpdateProductType(id, mapper.Map<ProductType>(typeDTO));
            
            if (type is null)
            {
                return new ResponseInfo<string>(null, false, "type not found");
            }

            return new ResponseInfo<string>("updated successfully", true, $"type with id {id}");
        }
        
        public async Task<ResponseInfo<string>> DeleteProductType(int id)
        {
            var type = await repository.DeleteProductType(id);
            
            if (type is null)
            {
                return new ResponseInfo<string>(null, false, "type not found");
            }
            
            var typeDTO = mapper.Map<GetProductTypeDTO>(type);
            
            return new ResponseInfo<string>("deleted successfully", true, "type");
        }
    }
