//S10244400 Nathan Farrel Lukito

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prg2Assignment
{
    class StandardRoom : Room
    {
        public bool requireWifi { get; set; }
        public bool requireBreakfast { get; set; }

        public StandardRoom(int roomNumber, string bedConfiguration, double dailyRate, bool isAvailbool, bool requireWifi, bool requireBreakfast) : base(roomNumber, bedConfiguration, dailyRate, isAvailbool)
        {
            this.requireWifi = requireWifi;
            this.requireBreakfast = requireBreakfast;
        }

        public override double CalculateCharges(Guest guest)
        {
            Console.WriteLine("Standard: " + roomNumber + "  " + dailyRate * (guest.hotelStay.checkoutDate - guest.hotelStay.checkinDate).Days);
            return dailyRate * (guest.hotelStay.checkoutDate - guest.hotelStay.checkinDate).Days;
        }

        public override string ToString()
        {
            return base.ToString() + "\tRequire WiFi: " + requireWifi + "\tRequire Breakfast: " + requireBreakfast + "\tAdditional bed: " + "NULL";
        }
    }
}
