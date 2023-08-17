using ChartJs.Blazor;
using ChartJs.Blazor.Common;
using ChartJs.Blazor.Common.Axes;
using ChartJs.Blazor.Common.Enums;
using ChartJs.Blazor.Common.Handlers;
using ChartJs.Blazor.LineChart;
using ChartJs.Blazor.Util;
using Domain.Common;
using Domain.Statistics;
using Domain.Statistics.Datapoints;
using Domain.VirtualMachines.Statistics;
using Microsoft.AspNetCore.Components;
using Shared.VirtualMachines;
using System.Data;
using System.Drawing;
using Xamarin.Forms.Internals;

namespace Client.Servers
{
    public partial class Grafiek
    {
        [Inject] IVirtualMachineService VirtualMachineService { get; set; }
        [Parameter] public Statistic stats { get; set; }

        private Dictionary<DateTime, Hardware> _data = new();
        private VirtualMachineDto.Rapportage vm;

        private bool Loading = false;
        protected override async Task OnInitializedAsync()
        {
            Loading = true;
            _data.Add(key: stats.StartTime, value: stats.Hardware);
            _data.Add(key: stats.EndTime, value: stats.Hardware);
            Loading = false;
        }

    }

}
