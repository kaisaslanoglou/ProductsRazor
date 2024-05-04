using AutoMapper;
using ProductsApp.DTO;
using ProductsApp.Models;

namespace ProductsApp.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<ProductInsertDTO, Product>().ReverseMap();
            CreateMap<ProductUpdateDTO, Product>().ReverseMap();
            CreateMap<ProductReadOnlyDTO, Product>().ReverseMap();
       
    }

    }
}
