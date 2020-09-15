using System.Linq;
using AutoMapper;
using Contact.Dto.Directories;
using Contact.Dto.Products;
using Models.gpn;

namespace Contact.Dto
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<product, ProductShortViewDto>(MemberList.Destination)
                .ForMember(x => x.Id, x => x.MapFrom(t => t.product_id))
                .ForMember(x => x.Name, x => x.MapFrom(t => t.product_name))
                .ForMember(x => x.DirectoryName, x => x.MapFrom(t => t.product_directory.product_directory_name))
                .ForMember(x => x.DescriptionShort, x => x.MapFrom(t => t.description_short))
                .ForMember(x => x.Price, x => x.MapFrom(t => t.price))
                .ForMember(x => x.ReviewCount, x => x.MapFrom(t => t.Responses.Count))
                .ForMember(x => x.Rating, x => x.MapFrom(t => (float)t.Rates.Average(r => r.product_rate_id)))
                .ForMember(x => x.SupplyCount, x => x.Ignore());

            CreateMap<product_directory, DirectoryLazyDto>(MemberList.Destination)
                .ForMember(x => x.Id, x => x.MapFrom(t => t.product_directory_id))
                .ForMember(x => x.Name, x => x.MapFrom(t => t.product_directory_name))
                .ForMember(x => x.HasChildren, x => x.MapFrom(t => t.Subdirectories.Any()));

            CreateMap<product_directory, DirectoryResultGroupDto>()
                .ForMember(x => x.DirectoryId, x => x.MapFrom(t => t.product_directory_id))
                .ForMember(x => x.GroupName, x => x.MapFrom(t => t.product_directory_name))
                .ForMember(x => x.TotalCount, x => x.MapFrom(t => t.Subdirectories.Count));

        }
    }
}
