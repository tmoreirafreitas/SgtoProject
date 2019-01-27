using Sgot.Domain.Entities;
using Sgot.Domain.Interfaces.Repositories;
using Sgot.Domain.Interfaces.Services;
using Sgot.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgot.Service
{
    public class OculosService : Service<Oculos>, IOculosService
    {
        private readonly IOculosRepository _repository;
        private readonly IUnitOfWork _uow;

        public OculosService(IOculosRepository repository, IUnitOfWork uow) : base(repository, uow)
        {
            _repository = repository;
            _uow = uow;
        }
    }
}
