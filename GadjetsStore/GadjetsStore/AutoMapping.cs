using AutoMapper;
using DTOs;
using Entities;


namespace GadjetsStore 
{
    public class AutoMapping : Profile
    {
        public AutoMapping() // Added 'public' access modifier and fixed constructor syntax
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.categoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();

            CreateMap<UserRegisterDTO, User>();

            CreateMap<UserLoginDTO, User>();

            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();

            CreateMap<Order, OrderDTO>()
                 .ForMember(dest => dest.Orderitems, opt => opt.MapFrom(src => src.OrderItems))
                .ReverseMap();

            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
