using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data
{
    public class TrainRepository : ITrainRepository
    {
        private readonly StoreContext context;

        public TrainRepository(StoreContext context)
        {
            this.context = context;
        }

        public async Task<Train> GetTrainByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Train>> GetTrainsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
