using Sgot.Domain.Entities;
using Sgot.Domain.Interfaces.Repositories;
using Sgot.Domain.Interfaces.Services;
using Sgot.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgot.Service
{
    public class LenteService : Service<Lente>, ILenteService
    {
        private readonly ILenteRepository _repository;
        private readonly IUnitOfWork _uow;

        public LenteService(ILenteRepository repository, IUnitOfWork uow) : base(repository, uow)
        {
            _repository = repository;
            _uow = uow;
        }
    }
}
