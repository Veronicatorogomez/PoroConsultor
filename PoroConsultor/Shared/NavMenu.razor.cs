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

namespace PoroConsultor.Shared
{
    public partial class NavMenu
    {
        private string[] poritos = new string[16];
        public bool saltar { get; set; }

        protected override async Task OnInitializedAsync()
        {
            for (int i = 0; i < poritos.Length; i++)
            {
                poritos[i] = (i + 1).ToString() + ".png";
            }
        }

        private async Task PoritosSaltan()
        {
            saltar = true;
        }
    }
}