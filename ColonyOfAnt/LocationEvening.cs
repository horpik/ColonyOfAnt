using System.Collections.Generic;
namespace ColonyOfAnt
{
    public class LocationEvening : Location
    {
        public LocationEvening(List<Colony> colonies) : base(colonies)
        {
            nameLocation = "вечер";
        }
    }
}