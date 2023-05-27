using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using PoroConsultor;
using PoroConsultor.Shared;
using PoroConsultor.Models;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Rendering;

namespace PoroConsultor.Pages
{
    public partial class Campeon
    {
        [Parameter]
        public string parametro { get; set; }

        private List<Estadisticas> campeonesStats;
        private List<Campeones> campeonesInfo;
        private List<Union> campeones = new List<Union>();
        private Union campeon;
        private IJSObjectReference jsModule;
        private int habilidad = 0;
        private int skin = 0;
        private string fondoVideo;
        protected override async Task OnInitializedAsync()
        {
            campeonesStats = await Http.GetFromJsonAsync<List<Estadisticas>>("apis/estadisticas.json");
            campeonesInfo = await Http.GetFromJsonAsync<List<Campeones>>("apis/campeones.json");
            foreach (var item in campeonesInfo)
            {
                foreach (var item2 in campeonesStats)
                {
                    if (item.Nombre == item2.Nombre.TrimStart(' ').ToUpper())
                    {
                        campeones.Add(new Union(item, item2));
                    }
                }
            }

            jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Campeon.js");
            await jsModule.InvokeVoidAsync("checkScroll");
            campeon = campeones.First(x => x.Nombre == parametro);
            Random random = new Random();
            fondoVideo = random.Next(1, 17).ToString() + ".png";
        }

        protected override async Task OnParametersSetAsync()
        {
            jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Campeon.js");
            await jsModule.InvokeVoidAsync("muteVideo");
            await base.OnParametersSetAsync();
        }

        private async Task DameNum(int i, string video)
        {
            habilidad = i;
            await jsModule.InvokeVoidAsync("loadAnotherVideo", video);
        }

        private void DameNumSkin(int i)
        {
            skin = i;
        }

        private async Task VideoBucle()
        {
            jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Campeon.js");
            await jsModule.InvokeVoidAsync("playVideoBucle");
        }
    }
}