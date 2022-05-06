using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class PassengersSpecification : BaseSpecifcation<Passenger>
    {
        public PassengersSpecification(
            PassengerSpecParams passengerSpecParams
        ) :
            base(
                x =>
                    (
                    string.IsNullOrEmpty(passengerSpecParams.Search) ||
                    x.Name.ToLower().Contains(passengerSpecParams.Search) &&
                    string.IsNullOrEmpty(passengerSpecParams.Nat) ||
                    x.Name.ToLower().Contains(passengerSpecParams.Nat)
                    )
            )
        {
            AddOrderBy(x => x.Name);

            ApplyPaging(passengerSpecParams.PageSize *
            (passengerSpecParams.PageIndex - 1),
            passengerSpecParams.PageSize);

            if (!string.IsNullOrEmpty(passengerSpecParams.Sort))
            {
                switch (passengerSpecParams.Sort)
                {
                    case "name":
                        AddOrderBy(p => p.Name);
                        break;
                    case "phone":
                        AddOrderBy(p => p.Phone);
                        break;
                    case "gender":
                        AddOrderByDescending(p => p.Gender);
                        break;
                    default:
                        AddOrderBy(n => n.Nat);
                        break;
                }
            }
        }

        public PassengersSpecification(int id) :
            base(x => x.Id == id)
        {
        }
    }
}
