using AutoMapper;
using Sgot.Domain.Entities;
using Sgot.Service.Core.Commands;
using Sgot.Service.ViewModels;

namespace Sgot.Service.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {             
            CreateMap<Cliente, ClienteViewModel>();            
            CreateMap<Fatura, FaturaViewModel>();
            CreateMap<Parcela, ParcelaViewModel>();
            CreateMap<Pedido, PedidoViewModel>();
            CreateMap<Lente, LenteViewModel>();
            CreateMap<Oculos, OculosViewModel>();
            CreateMap<ApplicationUser, LoginViewModel>();            
        }
    }
}
