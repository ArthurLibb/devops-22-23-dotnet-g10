﻿using Domain.Common;
using Domain.Projecten;
using Domain.Server;
using Domain.Users;
using Domain.VirtualMachines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.VirtualMachines;

public static class VirtualMachineDto
{
    public class Index
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public VirtualMachineMode Mode { get; set; }

    }
    public class Detail : Index  
    {
        public Hardware Hardware { get; set; }
        public OperatingSystemEnum OperatingSystem { get; set; }
        public VMContract? Contract { get; set; }
        public Backup BackUp { get; set; }
    }
    public class Edit
    {
        public String Name { get; set; }
        public VirtualMachineMode Mode { get; set; }
        public Backup Backup { get; set; }
        public Project Project { get; set; }

    }
    public class Mutate
    {
        public String Name { get; set; }   
        public VirtualMachineMode Mode { get; set; }
        public Hardware Hardware { get; set; }
        public OperatingSystemEnum OperatingSystem { get; set; }
        public VMContract Contract { get; set; }
        public Backup Backup { get; set; }
        public Project Project { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }


    }
}
