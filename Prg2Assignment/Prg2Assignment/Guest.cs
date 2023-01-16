//StudentID: S10243254B
//Student name: Ervin Wong Yong Qi

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prg2Assignment
{
    class Guest
    {
        public string name { get; set; }
        public string passportNum { get; set; }

        public Stay hotelStay { get; set; }
        
        public Membership membership { get; set; }

        public bool iSCheckedin { get; set; }

        //Constructor
        public Guest() { }

        public Guest(string n, string pass, Stay hs, Membership mem)
        {
            name = n;
            passportNum = pass;
            hotelStay = hs;
            membership = mem;
        } 

        public override string ToString()
        {
            return "Name: " + name + "\tPassport no: " + passportNum + "\tMembership: " + membership.ToString() + "Hotel stay: " + hotelStay.ToString();
        }
    }
}
