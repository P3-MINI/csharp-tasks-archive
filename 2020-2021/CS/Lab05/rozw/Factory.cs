
namespace Vehicles
{
    public class Factory
    {
        public static Vehicle Manufacture(string type, string name)
        {
            switch (type)
            {
                case "car":
                    return new Car(name);
                case "bus":
                    return new Bus(name);
                case "truck":
                    return new Truck(name);
                default:
                    return null;
            }
        }
    }
}
