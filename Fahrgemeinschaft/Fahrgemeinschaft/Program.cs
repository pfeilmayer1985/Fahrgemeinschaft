using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Fahrgemeinschaft
{
    public class Program
    {
        static void Main(string[] args)
        {
            UDrivers offers = new UDrivers();
            UPassengers requests = new UPassengers();
            //requests.AddRequest();
            //requests.ListAllRequests();
            offers.AddOffer();
            offers.ListAllOffers();
            Console.ReadLine();
        }
    }
}
