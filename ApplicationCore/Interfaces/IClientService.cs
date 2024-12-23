﻿using ApplicationCore.Entities;
using ApplicationCore.Models.Client;
using Ardalis.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IClientService
    {
        Task<Result> AddClient(NewClientModel newClient);
        Task<bool> HasActiveMembership(string userId);
        Task<Client> GetClientByUserId(string userId);
    }
}
