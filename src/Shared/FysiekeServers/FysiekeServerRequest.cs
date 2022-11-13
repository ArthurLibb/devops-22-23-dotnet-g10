﻿using Domain.Common;
using Shared.VirtualMachines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.FysiekeServers
{
    public static class FysiekeServerRequest
    {
        public class Order
        {
            public DateTime StartDay { get; set; }
            public DateTime EndDate { get; set; }
            public Hardware HardwareNeeded { get; set; }

        }
        public class Approve
        {
            public VirtualMachineDto.Detail VirtualMachine { get; set; }  // virtual machine contains the server
        }
        public class Detail
        {
            public int ServerId { get; set; }
        }

    }
}