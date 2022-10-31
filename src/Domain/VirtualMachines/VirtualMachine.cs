﻿using Ardalis.GuardClauses;
using Bogus.DataSets;
using Domain.Common;
using Domain.Contract;
using Domain.Users;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.VirtualMachines
{
    public class VirtualMachine : Entity
    {

        private VMContract _vmContract;


        private string _name;
        private string _project;
        private OperatingSystemEnum _operatingSystem;
        private VirtualMachineMode _mode;
        private Klant _gebruiker;


        public String Name { get { return _name; } set {Guard.Against.NullOrEmpty(_name, nameof(_name)); } }
        public String Project { get { return _project; } set {Guard.Against.NullOrEmpty(_project, nameof(_project)); } }
        public OperatingSystemEnum OperatingSystem { get { return _operatingSystem; } set {Guard.Against.Null(_operatingSystem, nameof(_operatingSystem)); } }
        public VirtualMachineMode Mode { get { return _mode; } set { Guard.Against.Null(_mode, nameof(_mode)); } }
        public Hardware Hardware { get; set; }
        public VMConnection Connection { get; set; }
        public Backup BackUp { get; set; }
        public Klant Customer { get { return _gebruiker; } set { Guard.Against.Null(_gebruiker, nameof(_gebruiker)); } }
        public VMContract Contract { get { return _vmContract; } set { Guard.Against.Null(_vmContract, nameof(_vmContract)); } }

        //virtual machine used for templates.
        //builder will add: VMconnection
        public VirtualMachine(OperatingSystemEnum os, Hardware h, Backup b, Klant k, VMContract vmc)
        {
            this.Name = $"{os}-{h.Memory}Gb.{k.Project}";
            this.Project = k.Project;
            this.OperatingSystem = os;
            this.Hardware = h;
            this.BackUp = b;
            this.Customer = k;
            this.Contract = vmc;
            this.Mode = VirtualMachineMode.CREATED;

        }

        //virtual machine for custom (made with builder)
        //builder will add hardware, operating system, backup, VMConnection, Name
        public VirtualMachine(Klant g, VMContract vmc)
        {
            this.Project = g.Project;
            this.Customer = g;
            this.Contract = vmc;
            this.Mode = VirtualMachineMode.CREATED;

        }

   
    }
}
