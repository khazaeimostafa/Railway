using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class TravelsSpecification : BaseSpecifcation<Travel>
    {
        public TravelsSpecification(TravelSpecParams travelsSpecParams) :
            base(
                x =>
                    (
                    string.IsNullOrEmpty(travelsSpecParams.Search) ||
                    x.Src.ToLower().Contains(travelsSpecParams.Search) &&
                    (
                    string.IsNullOrEmpty(travelsSpecParams.Search) ||
                    x.Dest.ToLower().Contains(travelsSpecParams.Search) &&
                    (
                    !travelsSpecParams.TrainId.HasValue ||
                    x.TrainId == travelsSpecParams.TrainId
                    ) &&
                    (
                    !travelsSpecParams.Cost.HasValue ||
                    x.Cost == travelsSpecParams.Cost.Value
                    )
                    )
                    )
            )
        {
            AddInclude(x => x.Reservation);

            AddOrderBy(x => x.Date);
            ApplyPaging(travelsSpecParams.PageSize *
            (travelsSpecParams.PageIndex - 1),
            travelsSpecParams.PageSize);

            if (!string.IsNullOrEmpty(travelsSpecParams.Sort))
            {
                switch (travelsSpecParams.Sort)
                {
                    case "status":
                        AddOrderBy(p => p.Src);
                        break;
                    case "capacityAsc":
                        AddOrderBy(p => p.Dest);
                        break;
                    case "capacityDesc":
                        AddOrderByDescending(p => p.Cost);
                        break;
                    default:
                        AddOrderBy(n => n.Date);
                        break;
                }
            }
        }

        public TravelsSpecification(int id) :
            base(x => x.Id == id)
        {
            AddInclude(x => x.Reservation);
        }
    }
}
