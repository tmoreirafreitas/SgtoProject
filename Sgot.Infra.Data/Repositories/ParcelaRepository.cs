﻿using Microsoft.EntityFrameworkCore;
using Sgot.Domain.Entities;
using Sgot.Domain.Interfaces.Repositories;
using Sgot.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgot.Infra.Data.Repositories
{
    public class ParcelaRepository : Repository<Parcela>, IParcelaRepository
    {
        public ParcelaRepository(DbContext context) : base(context)
        {
        }
    }
}
