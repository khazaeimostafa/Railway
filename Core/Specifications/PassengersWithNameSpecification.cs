using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class PassengersWithNameSpecification : BaseSpecifcation<Passenger>
    {
        public PassengersWithNameSpecification(
            PassengerSpecParams passengerParams
        ) :
            base(
                x =>
                    (
                    string.IsNullOrEmpty(passengerParams.Search) ||
                    x.Name.ToLower().Contains(passengerParams.Search)
                    )
            )
        {
            AddOrderBy(x => x.Name);
            ApplyPaging(passengerParams.PageSize *
            (passengerParams.PageIndex - 1),
            passengerParams.PageSize);

            if (!string.IsNullOrEmpty(passengerParams.Sort))
            {
                switch (passengerParams.Sort)
                {
                    case "genre":
                        AddOrderBy(p => p.Gender);
                        break;
                    case "Nat":
                        AddOrderByDescending(p => p.Nat);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }

        public PassengersWithNameSpecification(int id) :
            base(x => x.Id == id)
        {
        }
    }
}
