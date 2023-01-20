//StudentID: S10243254B
//Student name: Ervin Wong Yong Qi

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prg2Assignment
{
    class Stay
    {
        public DateTime checkinDate { get; set; }
        public DateTime checkoutDate { get; set;}

        public List<Room> roomlist { get; set; } = new List<Room>();

        //Constructors
        public Stay() { }

        public Stay (DateTime cid, DateTime cod)
        {
            checkinDate= cid;
            checkoutDate= cod;
            
        }


        //functions
        public override string ToString()
        {
            return "Check in date: " + DateOnly.FromDateTime(checkinDate) + "\tCheck out date: " + DateOnly.FromDateTime(checkoutDate);
        }

    }
}
