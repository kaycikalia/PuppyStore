using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace PuppyStoreProxy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetStoreProxyController : ControllerBase
    {
        private readonly HttpClient _http;

        public PetStoreProxyController(IHttpClientFactory httpClientFactory)
        {
            _http = httpClientFactory.CreateClient();
        }

        // POST: /api/petstoreproxy/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] object request)
        {
            var url = "https://petstoreapitiny-ckemc9fjfdghg7fw.canadacentral-01.azurewebsites.net/psuser/insertPSUser";
            var response = await _http.PostAsJsonAsync(url, request);
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
        }

        // GET: /api/petstoreproxy/get?email=...
        [HttpGet("get")]
        public async Task<IActionResult> GetUser(string email)
        {
            var url = $"https://petstoreapitiny-ckemc9fjfdghg7fw.canadacentral-01.azurewebsites.net/GetPSUserByEmail?email={email}";
            var response = await _http.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, content);
        }
    }
}
