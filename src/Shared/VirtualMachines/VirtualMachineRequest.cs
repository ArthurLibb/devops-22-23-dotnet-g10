using Domain.VirtualMachines.VirtualMachine;

namespace Shared.VirtualMachines
{
    public static class VirtualMachineRequest
    {
        public class GetIndex
        {
            public string? SearchTerm { get; set; }
            public VirtualMachineMode? Status { get; set; }
            public OperatingSystemEnum? OperatingSystem { get; set; }
            public int? MinStorage { get; set; }
            public int? MaxStorage { get; set; }
            public int? MinMemory { get; set; }
            public int? MaxMemory { get; set; }
            public int? MinAmountCPU { get; set; }
            public int? MaxAmountCPU { get; set; }

        }

        public class GetDetail
        {
            public int VirtualMachineId { get; set; }
        }

        public class Delete
        {
            public int VirtualMachineId { get; set; }
        }

        public class Create
        {
            public VirtualMachineDto.Mutate VirtualMachine { get; set; }
            public int CustomerId { get; set; }
        }

        public class Edit
        {
            public int VirtualMachineId { get; set; }

            public VirtualMachineDto.Edit VirtualMachine { get; set; }

        }
    }
}