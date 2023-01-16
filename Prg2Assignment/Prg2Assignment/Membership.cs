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

    }
}
