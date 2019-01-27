using AutoMapper;
using Sgot.Domain.Entities;
using Sgot.Service.ViewModels;

namespace Sgot.Service.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {             
            CreateMap<ClienteViewModel, Cliente>();                        
            CreateMap<FaturaViewModel, Fatura>();
            CreateMap<ParcelaViewModel, Parcela>();
            CreateMap<PedidoViewModel, Pedido>();
            CreateMap<LenteViewModel, Lente>();
            CreateMap<OculosViewModel, Oculos>();
            CreateMap<LoginViewModel, ApplicationUser>();
        }
    }
}
