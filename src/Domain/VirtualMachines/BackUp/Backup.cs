using Ardalis.GuardClauses;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using Domain.Common;

namespace Domain.VirtualMachines.BackUp
{

    public class Backup : Entity
    {


        private BackUpType _type;

        public BackUpType Type { get { return _type; } set { _type = Guard.Against.Null(value, nameof(_type)); } }
        public DateTime? LastBackup { get; set; }  //lastBackup can be null

        public Backup() { }
        public Backup(BackUpType type, DateTime? lastBackup)
        {
            Type = type;
            LastBackup = lastBackup;
        }

    }
}