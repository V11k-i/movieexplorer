using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace movie_explorer.Resources.Splash
{
    
     internal class MovieService
    {
        public event EventHandler<string> DataAvailable;
        private readonly HttpClient _httpClient = new();
        public async Task<string> GetData()
        {
            var moviejson = await _httpClient.GetStringAsync("https://raw.githubusercontent.com/DonH-ITS/jsonfiles/refs/heads/main/moviesemoji.json");
            DataAvailable?.Invoke(this, "Download Complete");
            return moviejson;
            
        }


    }
}
