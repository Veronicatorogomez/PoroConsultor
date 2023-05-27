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
    public partial class Index
    {
        private bool easterEgg;
        private const int NUMERO_POROS = 10;
        private string video = "./Recursos/video/videoLol.mp4";
        private IJSObjectReference module;
        private bool audio;
        private string[] poritos = new string[NUMERO_POROS];
        protected override async Task OnInitializedAsync()
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Index.js");
            await module.InvokeVoidAsync("cargarAlerta");
            await module.InvokeVoidAsync("checkScroll");
            Random random = new Random();
            for (int i = 0; i < poritos.Length; i++)
            {
                poritos[i] = random.Next(1, 17).ToString() + ".png";
            }
        }

        public void Redirigir1()
        {
            NavigationManager.NavigateTo("/campeones");
        }

        public void Redirigir2()
        {
            NavigationManager.NavigateTo("/Consultor");
        }

        public void Redirigir3()
        {
            NavigationManager.NavigateTo("/Ranking");
        }

        protected override async Task OnParametersSetAsync()
        {
            easterEgg = await ObtenerValorBooleano();
            await base.OnParametersSetAsync();
        }

        //protected override async Task OnAfterRenderAsync(bool firstRender)
        //{
        //    if (firstRender)
        //    {
        //        module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Index.js");
        //    }
        //}
        //protected override async Task OnParametersSetAsync()
        //{
        //    //module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Index.js");
        //    //await module.InvokeVoidAsync("checkScroll");
        //    await base.OnParametersSetAsync();
        //}
        private void QuitarPoro()
        {
        }

        public async Task CerrarAlerta()
        {
            await module.InvokeVoidAsync("cerrarAlerta");
        }

        public async Task QuitarSonido()
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Index.js");
            await module.InvokeVoidAsync("quitarSonido");
            if (audio)
            {
                audio = false;
            }
            else
            {
                audio = true;
            }
        }

        private async Task<bool> ObtenerValorBooleano()
        {
            module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Index.js");
            return await module.InvokeAsync<bool>("existeEasteregg");
        }
    }
}