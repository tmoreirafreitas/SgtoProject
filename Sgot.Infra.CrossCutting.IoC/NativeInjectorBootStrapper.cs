using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sgot.Domain.Entities;
using Sgot.Domain.Interfaces.Repositories;
using Sgot.Domain.Interfaces.Services;
using Sgot.Infra.Data.Context;
using Sgot.Infra.Data.Repositories;
using Sgot.Infra.Data.UnitOfWork;
using Sgot.Service;
using Sgot.Service.Core.Commands.AccountRequest;
using Sgot.Service.Core.Commands.ClienteRequest;
using Sgot.Service.Core.Commands.FaturaRequest;
using Sgot.Service.Core.Commands.ParcelaRequest;
using Sgot.Service.Core.Commands.PedidoRequest;
using Sgot.Service.Core.Handles;
using Sgot.Service.Core.Handles.ClienteHandler;
using Sgot.Service.Core.Handles.FaturaHandler;
using Sgot.Service.Core.Handles.ParcelaHandler;
using Sgot.Service.Core.Handles.PedidoHandler;
using Sgot.Service.Core.Notifications;
using Sgot.Service.Core.Responses;

namespace Sgot.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Services Core dependency
            services.AddScoped<IMediator, Mediator>();

            //Account Request
            services.AddTransient<IRequestHandler<LoginAccount, LoginResponse>, LoginAccountHandle>();
            services.AddTransient<IRequestHandler<RegisterAccount, LoginResponse>, RegisterAccountHandle>();
            services.AddTransient<IRequestHandler<ResetPasswordAccount, ResetPasswordResponse>, ResetPasswordAccountHandle>();

            // Cliente Request
            services.AddTransient<IRequestHandler<CreateCliente, EntityResponse>, CreateClienteHandle>();
            services.AddTransient<IRequestHandler<UpdateCliente, EntityResponse>, UpdateClienteCommandHandle>();
            services.AddTransient<IRequestHandler<DeleteCliente, EntityResponse>, DeleteClienteHandle>();

            //Pedido Request
            services.AddTransient<IRequestHandler<CreatePedido, EntityResponse>, CreatePedidoHandle>();
            services.AddTransient<IRequestHandler<UpdatePedido, EntityResponse>, UpdatePedidoHandle>();
            services.AddTransient<IRequestHandler<DeletePedido, EntityResponse>, DeletePedidoHandle>();            

            //Parcela Request
            services.AddTransient<IRequestHandler<CreateParcela, EntityResponse>, CreateParcelaHandle>();
            services.AddTransient<IRequestHandler<UpdateParcela, EntityResponse>, UpdateParcelaHandle>();
            services.AddTransient<IRequestHandler<DeleteParcela, EntityResponse>, DeleteParcelaHandle>();

            //Fatura Request
            services.AddTransient<IRequestHandler<CreateFatura, EntityResponse>, CreateFaturaHandle>();
            services.AddTransient<IRequestHandler<UpdateFatura, EntityResponse>, UpdateFaturaHandle>();
            services.AddTransient<IRequestHandler<DeleteFatura, EntityResponse>, DeleteFaturaHandle>();

            // Notifications
            services.AddTransient<INotificationHandler<AccountLogged>, AccountLoggedNotificationHandle>();
            services.AddTransient<INotificationHandler<ClienteCreated>, ClienteCreatedNotificationToConsole>();
            services.AddTransient<INotificationHandler<ClienteCreated>, ClienteCreatedNotificationToFile>();

            // Services dependency
            services.AddScoped<IClienteService, ClienteService>();            
            services.AddScoped<IFaturaService, FaturaService>();
            services.AddScoped<ILenteService, LenteService>();
            services.AddScoped<IParcelaService, ParcelaService>();
            services.AddScoped<IPedidoService, PedidoService>();
            services.AddScoped<IOculosService, OculosService>();

            // Infra-Data dependency
            services.AddScoped<IClienteRepository, ClienteRepository>();            
            services.AddScoped<IFaturaRepository, FaturaRepository>();
            services.AddScoped<ILenteRepository, LenteRepository>();
            services.AddScoped<IOculosRepository, OculosRepository>();
            services.AddScoped<IParcelaRepository, ParcelaRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Instance Context
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<DbContext, SgotDbContext>();
            services.AddScoped<SgotDbContext>();
            services.AddScoped<UserManager<ApplicationUser>>();
            services.AddScoped<SignInManager<ApplicationUser>>();
        }
    }
}
