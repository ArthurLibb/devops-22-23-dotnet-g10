using Domain.Users;
using Shared.VirtualMachines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Projects
{
    public static class ProjectResponse
    {
        public class All
        {
            public List<ProjectDto.Index> Projects { get; set; }
            public int Total { get; set; }
        }

        public class App
        {
            public List<ProjectDto.App> Projects { get; set; }
        }

        public class Detail
        {
            public ProjectDto.Detail Project { get; set; }
        }

        public class Edit
        {
            public int ProjectId { get; set; }
        }

        public class Create
        {
            public int ProjectId { get; set; }
        }

        public class Delete
        {

        }
    }
}