//StudentID: S10243254B
//Student name: Ervin Wong Yong Qi

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prg2Assignment
{
    class Membership
    {
        public string status { get; set; }

        public int points { get; set; }

        //Constructor
        public Membership() { }
        public Membership(string s, int p)
        {
            status = s;
            points = p;
        }

        public double EarnPoints(Guest guest)
        {
            return guest.hotelStay.CalculateTotal(guest)/10;
        }


        public override string ToString()
        {
            return "Member Status: " + status + "\tPoints: " + points;
        }

    }
}
