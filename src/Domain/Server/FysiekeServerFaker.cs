using Bogus;
using Domain.Common;
using Domain.VirtualMachines;
using Bogus;
using Domain.Common;
using Domain.VirtualMachines.VirtualMachine;
using System.Linq;
using System;

namespace Domain.Server
{
    public class FysiekeServerFaker : Faker<FysiekeServer>
    {
        public FysiekeServerFaker()
        {

            CustomInstantiator(e => {
                return new FysiekeServer("Server ", e.Internet.DomainName() + "." + "hogent.be");
            });


            RuleFor(e => e.VirtualMachines, _ => VirtualMachineFaker.Instance.Generate(12));

        }

    }
}