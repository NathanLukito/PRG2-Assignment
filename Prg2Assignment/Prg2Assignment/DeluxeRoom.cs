//S10244400 Nathan Farrel Lukito

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prg2Assignment
{
    class DeluxeRoom : Room
    {
        public bool additionalBed { get; set; }

        public DeluxeRoom(int roomNumber, string bedConfiguration, double dailyRate, bool isAvailbool, bool additionalBed) : base(roomNumber, bedConfiguration, dailyRate, isAvailbool)
        {
            this.additionalBed = additionalBed;
        }

        public override double CalculateCharges(Guest guest)
        {

            Console.WriteLine("Deluxe: " + roomNumber + "  " + dailyRate + "  " + dailyRate*(guest.hotelStay.checkoutDate - guest.hotelStay.checkinDate).Days);
            return dailyRate * (guest.hotelStay.checkoutDate - guest.hotelStay.checkinDate).Days;
        }

        public override string ToString()
        {
            return base.ToString() + "\tRequire WiFi: " + "NULL" + "\tRequire Breakfast: " + "NULL" + "\tAdditional bed: " + additionalBed;
        }
    }
}
