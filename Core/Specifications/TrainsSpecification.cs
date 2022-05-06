using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class TrainsSpecification : BaseSpecifcation<Train>
    {
        public TrainsSpecification(TrainSpecParams trainsSpecParams) :
            base(
                x =>
                    (
                    string.IsNullOrEmpty(trainsSpecParams.Search) ||
                    x.Name.ToLower().Contains(trainsSpecParams.Search) &&
                    (
                    !trainsSpecParams.TravelId.HasValue ||
                    x.Travel.Id == trainsSpecParams.TravelId
                    )
                    )
            )
        {
            AddOrderBy(x => x.Name);
            ApplyPaging(trainsSpecParams.PageSize *
            (trainsSpecParams.PageIndex - 1),
            trainsSpecParams.PageSize);

            if (!string.IsNullOrEmpty(trainsSpecParams.Sort))
            {
                switch (trainsSpecParams.Sort)
                {
                    case "status":
                        AddOrderBy(p => p.Status);
                        break;
                    case "capacityAsc":
                        AddOrderBy(p => p.Capacity);
                        break;
                    case "capacityDesc":
                        AddOrderByDescending(p => p.Capacity);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }

        public TrainsSpecification(int id) :
            base(x => x.Id == id)
        {
            AddInclude(x => x.Travel);
        }
    }
}
