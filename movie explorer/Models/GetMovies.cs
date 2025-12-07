using GoogleGson.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace movie_explorer.Models
{
    
     internal class GetMovies // a service that is used to download and operate  JSON file containing information avbout movies
    {
        public event EventHandler<string> DataAvailable;
        private readonly HttpClient _httpClient = new();
        public async Task<string> GetData() // downloads json file from the source and returns it as a string 
        {
            var moviejson = await _httpClient.GetStringAsync("https://raw.githubusercontent.com/DonH-ITS/jsonfiles/refs/heads/main/moviesemoji.json");
            DataAvailable?.Invoke(this, "Download Complete");
            return moviejson;
            
        }


    }
}
