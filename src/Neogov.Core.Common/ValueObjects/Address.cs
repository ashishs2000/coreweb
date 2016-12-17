using Neogov.Core.Common.Extensions;
using Neogov.Core.Common.Wrappers;

namespace Neogov.Core.Common.ValueObjects
{
    public class Address : ValueObject<Address>
    {
        public string Street { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }

        public Address(string street, string city, int stateId)
        {
            Street = street;
            City = city;

            stateId.Ensure(p => p > 0, "State information is missing", val => StateId = val);
        }
    }
}