using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace MoviesLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private static readonly string API_KEY = "1f69302a";

        [HttpGet]
        public async Task<IActionResult> CallAPI(string imdbId="all", string? searchTitle="game")
        {

            // http://www.omdbapi.com/?apikey=1f69302a&r=json&type=movie&page=1
            // http://www.omdbapi.com/?apikey=1f69302a&i=tt2084970
            //call get API
            
            int page = 1;
            using (var client = new HttpClient())
            {
                string queryString;
                string searchParams = $"?apikey={API_KEY}&r=json&type=movie&s={searchTitle}&page={page}";
                string imdbIdParam = $"?apikey={API_KEY}&i={imdbId}";
                if(imdbId == "all")
                {
                    queryString = searchParams;
                }
                else
                {
                    queryString = imdbIdParam;
                }

                client.BaseAddress = new Uri("http://www.omdbapi.com/");
                using (HttpResponseMessage response = await client.GetAsync(queryString))
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    response.EnsureSuccessStatusCode();

                    return Ok(responseContent);
                }
            }
        }
      

    }
}
