using Domain.Users;
using Domain.VirtualMachines.VirtualMachine;
using FluentValidation;
using Shared.Users;
using Shared.VirtualMachines;

namespace Shared.Projects;

public static class ProjectDto
{
    public class Index
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public KlantDto.Index Klant { get; set; }

    }
    public class Detail : Index
    {
        public List<VirtualMachineDto.Index> VirtualMachines { get; set; }

    }

    public class App
    {
        public int Id { get; set; } 
        public String Name { get; set; }
        public List<VirtualMachineDto.Index> VirtualMachines { get; set; }
    }

    public class Mutate
    {
        public String Name { get; set; }
        public Klant Klant { get; set; }


        public class Validator : AbstractValidator<Mutate>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty().Length(5, 50);
                RuleFor(x => x.Klant).NotNull();
                RuleFor(x => x.Klant.Id).NotNull();

            }
        }
    }
}