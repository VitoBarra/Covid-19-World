using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SharedLibrary.GeneralUse.RestService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Covid19_World.Client.Pages
{
    public partial class Map
    {
        Dictionary<string, int> pairs;
        string divID = "vmap";
        [Inject] JSRuntime JSRuntime { get; set; }


        protected async override Task OnInitializedAsync()
        {
            pairs = await GetCountryValue();
            await JSRuntime.InvokeVoidAsync("WriteMap",pairs,divID);
        }



        protected async Task<Dictionary<string, int>> GetCountryValue()
        {
            Dictionary<string, int> Data = new Dictionary<string, int>();
            string EndPoint = "[controller]";

            try
            {
                Data = await RestService.CallServiceAsync<Dictionary<string, int>>(EndPoint);
            }
            catch (Exception ex)
            {

            }

            return Data;
        }
    }
}
