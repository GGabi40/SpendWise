using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SpendWise.Core.Services
{
    public class ExchangeRateService
    {
        private readonly HttpClient _httpClient;

        public ExchangeRateService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ExchangeRateClient");
        }

        public async Task<(decimal? rate, decimal? converted)> ConvertUsdToArsAsync(decimal amount)
        {
            var response = await _httpClient.GetAsync("dolares/oficial");
            if (!response.IsSuccessStatusCode) return (null, null);

            using var stream = await response.Content.ReadAsStreamAsync();
            var json = await JsonDocument.ParseAsync(stream);

            var venta = json.RootElement.GetProperty("venta").GetDecimal();
            return (venta, amount * venta);
        }
    }
}
