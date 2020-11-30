using Covid19_World.Shared.Services.Api.Model;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Covid_World.SharedData.Models
{
    public interface IUtilityFileReader
    {

    }
    public class UtilityFileReader : IUtilityFileReader
    {
        public IList<CountryPairs> CountryList { get; private set; }

        public UtilityFileReader()
        {
            using (StreamReader r = new StreamReader("CountryPairs.json"))
            {
                CountryList = JsonConvert.DeserializeObject<List<CountryPairs>>(r.ReadToEnd());
            }
        }

    }
}
