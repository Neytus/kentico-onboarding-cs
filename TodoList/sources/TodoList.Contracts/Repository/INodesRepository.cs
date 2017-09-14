﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Contracts.Models;

namespace TodoList.Contracts.Repository
{
    public interface INodesRepository
    {
        Task<IEnumerable<NodeModel>> GetAllAsync();

        Task<NodeModel> GetByIdAsync(Guid id);

        Task AddAsync(NodeModel model);

        Task UpdateAsync(NodeModel model);

        Task DeleteAsync(Guid id);
    }
}