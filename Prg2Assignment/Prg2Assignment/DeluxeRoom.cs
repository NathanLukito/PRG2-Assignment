﻿using System;
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

        public override double CalculateCharges()
        {
            return;
        }
    }
}