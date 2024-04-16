using AutoMapper;
using Talabat.APIs.Dtos;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helpers
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Product, ProductToReturnDto>()
				.ForMember(D => D.Brand , O => O.MapFrom(S => S.Brand.Name))
				.ForMember(D => D.Category, O => O.MapFrom(S => S.Category.Name))
				.ForMember(D => D.PictureUrl , O => O.MapFrom<ProductPictureUrlResolver>());
		}
	}
}
