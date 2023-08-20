using Domain.Common;
using Domain.Projecten;
using Domain.Server;
using Domain.Users;
using Domain.VirtualMachines.BackUp;
using Domain.VirtualMachines.Contract;
using Domain.Statistics;
using Domain.VirtualMachines.VirtualMachine;
using FluentValidation;
using Shared.VMContracts;
using Shared.Servers;
using Shared.VMConnection;
using System.ComponentModel.DataAnnotations;

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
        public VMContractDto.Index Contract { get; set; }
        public Backup BackUp { get; set; }
        public FysiekeServerDto.Index? FysiekeServer { get; set; }
        public VMConnectionDto.Index? VMConnection { get; set; }
    }

    public class Rapportage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Statistic Statistics { get; set; }

    }
    public class Edit
    {
        public String Name { get; set; }
        public Backup Backup { get; set; }
    }
    public class Mutate
    {
        [Required(ErrorMessage = "Je moet een naam ingeven.")]
        [StringLength(20, ErrorMessage = "Naam is te lang")]
        public String Name { get; set; }
        [Required(ErrorMessage = "Alles van hardware moet ingegeven zijn.")]
        public Hardware Hardware { get; set; }
        [Required(ErrorMessage = "Er moet een operatingsysteem zijn opgegeven.")]
        public OperatingSystemEnum OperatingSystem { get; set; }
        [Required(ErrorMessage = "Je moet een back type aanduiden.")]
        public Backup Backup { get; set; }
        [Required(ErrorMessage = "Selecteer een project.")]
        public int ProjectId { get; set; }
        [Required(ErrorMessage = "Geef een startdatum.")]
        public DateTime Start { get; set; }
        [Required(ErrorMessage = "Geef een einddatum..")]
        public DateTime End { get; set; }

    }
}