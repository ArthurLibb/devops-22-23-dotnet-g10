﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Domain.Common;
using Domain.Projecten;
using Domain.Server;
using Domain.Users;
using Domain.VirtualMachines.BackUp;
using Domain.VirtualMachines.Contract;
using Domain.Statistics;
using Shared.Utility;

namespace Domain.VirtualMachines.VirtualMachine
{

    public class VirtualMachineFaker : Faker<VirtualMachine>
    {

        //private readonly IEnumerable<DateTime?> _dateOptions = GenerateRandomDatesIncNull();


        private List<VirtualMachine> _virtualMachines = new();


        private static VirtualMachineFaker instance = null;

        public static VirtualMachineFaker Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new VirtualMachineFaker();
                }
                return instance;
            }
        }


        public VirtualMachineFaker()
        {
            int id = 1;
            VMContract contract = VMContractFaker.Instance.GenerateOne();
            Hardware hardware = GenerateRandomHardware(false);

            CustomInstantiator(e => new VirtualMachine(
                e.Commerce.ProductName(),
                e.PickRandom<OperatingSystemEnum>(),
                e.PickRandom(hardware),
                e.PickRandom(GenerateRandomBackups())
                ));

            RuleFor(x => x.Id, _ => id++);
            RuleFor(x => x.Connection, _ => new Random().Next(0, 2) % 1 == 0 ? new VMConnection("MOCK-FQDN", GetRandomIpAddress(), "MOCK-USER", PasswordGenerator.Generate(20, 3, 3, 3, 3)) : null);
            RuleFor(x => x.Mode, x => x.PickRandom<VirtualMachineMode>());
            RuleFor(x => x.Contract, _ => VMContractFaker.Instance.GenerateOne());
            RuleFor(x => x.FysiekeServer, _ => new FysiekeServer("Mock Server", GenerateRandomHardware(true), "mock-server_adres.hogent.be"));
            RuleFor(x => x.Statistics, _ => new Statistic(contract.StartDate, contract.EndDate, hardware));

        }


        public override List<VirtualMachine> Generate(int count, string ruleSets = null)
        {
            List<VirtualMachine> output = new();
            if (_virtualMachines.Count() == 0)
            {
                _virtualMachines = base.Generate(count, ruleSets);
                output = _virtualMachines;
            }
            else if (_virtualMachines.Count < count)
            {
                output = base.Generate(count - _virtualMachines.Count());
                output.ForEach(e => _virtualMachines.Add(e));
                output = _virtualMachines.GetRange(0, count);
            }
            else
            {

                output = _virtualMachines.GetRange(0, count);

            }
            return output;
        }


        private static DateTime? GenerateRandomDatesIncNull()
        {
            List<DateTime?> res = new();
            DateTime min = new DateTime(2022, 09, 01);

            for (int i = 0; i < 10; i++)
            {
                res.Add(min.AddDays(new Random().Next((DateTime.Now - min).Days)));
            }
            res.Add(null);

            return res[RandomNumberGenerator.GetInt32(0, res.Count())];


        }

        private Hardware GenerateRandomHardware(bool server)
        {
            List<Hardware> res = new();


            int[] _memoryOptions = { 1, 2, 4, 8, 16, 32, 64 };
            int[] _storageOptions = { 30, 50, 75, 100, 150, 200, 250, 300, 500 };


            return new Hardware(_memoryOptions[new Random().Next(0, _memoryOptions.Count())] * (server ? 15 : 1), _storageOptions[new Random().Next(0, _storageOptions.Count())] * (server ? 15 : 1), new Random().Next(1, 13) * (server ? 15 : 1));
        }

        private Backup GenerateRandomBackups()
        {
            List<Backup> res = new();

            int r = new Random().Next(0, 10);
            Backup a;

            if (r == 1)
                a = new Backup(BackUpType.DAILY, DateTime.Now.Subtract(TimeSpan.FromMinutes(500)));
            else if (r == 2)
                a = new Backup(BackUpType.CUSTOM, DateTime.Now.Subtract(TimeSpan.FromHours(5)));
            else if (r <= 6)
                a = new Backup(BackUpType.WEEKLY, DateTime.Now.Subtract(TimeSpan.FromDays(new Random().NextDouble() * 7)));
            else
                a = new Backup(BackUpType.MONTHLY, DateTime.Now.Subtract(TimeSpan.FromDays(new Random().Next(30))));


            return a;

        }

        private IPAddress GetRandomIpAddress()
        {
            var random = new Random();
            var data = new byte[4];
            random.NextBytes(data);

            return new IPAddress(data);
        }



    }
}
