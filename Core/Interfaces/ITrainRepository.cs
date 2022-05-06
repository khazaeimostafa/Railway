using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ITrainRepository
    {
        Task<Train> GetTrainByIdAsync(int id);

        Task<IReadOnlyList<Train>> GetTrainsAsync();
    }
}
