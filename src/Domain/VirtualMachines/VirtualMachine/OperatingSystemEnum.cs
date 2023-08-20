using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.VirtualMachines.VirtualMachine
{
    public enum OperatingSystemEnum
    {
        WINDOWS_10,
        WINDOWS_SERVER2019,
        KALI_LINUX,
        UBUNTU_22_04,
        FEDORA_36,
        FEDORA_35
    }

    public static class OperatingSystemEnumExtensions
    {
        public static string ToFriendlyString(this OperatingSystemEnum os)
        {
            switch (os)
            {
                case OperatingSystemEnum.WINDOWS_10:
                    return "Windows 10";
                case OperatingSystemEnum.WINDOWS_SERVER2019:
                    return "Windows Server 2019";
                case OperatingSystemEnum.KALI_LINUX:
                    return "Kali Linux";
                case OperatingSystemEnum.UBUNTU_22_04:
                    return "Ubuntu 22.04";
                case OperatingSystemEnum.FEDORA_36:
                    return "Fedora 36";
                case OperatingSystemEnum.FEDORA_35:
                    return "Fedora 35";
                default:
                    return os.ToString(); // Default to the default enum string representation
            }
        }
    }
}

