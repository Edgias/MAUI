using System.Net.Http.Json;
using System.Text.Json;

namespace Rest.Data
{
    public static class PartsManager
    {
        // TODO: Add fields for BaseAddress, Url, and authorizationKey
        static readonly string _baseAddress = "https://mslearnpartsserver1090131895.azurewebsites.net";
        static readonly string _url = $"{_baseAddress}/api/";
        private static string _authorizationKey;
        static HttpClient _client;


        private static async Task<HttpClient> GetClient()
        {
            if (_client != null)
                return _client;

            _client = new HttpClient();

            if (string.IsNullOrEmpty(_authorizationKey))
            {
                _authorizationKey = await _client.GetStringAsync($"{_url}login");
                _authorizationKey = JsonSerializer.Deserialize<string>(_authorizationKey);
            }

            _client.DefaultRequestHeaders.Add("Authorization", _authorizationKey);
            _client.DefaultRequestHeaders.Add("Accept", "application/json");

            return _client;
        }

        public static async Task<IEnumerable<Part>> GetAll()
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                return new List<Part>();
            }

            HttpClient client = await GetClient();
            string result = await client.GetStringAsync($"{_url}parts");


            var data =  JsonSerializer.Deserialize<List<Part>>(result, new JsonSerializerOptions() 
            { 
                PropertyNameCaseInsensitive = true
            });

            return data;
        }

        public static async Task<Part> Add(string partName, string supplier, string partType)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                return new Part();
            }

            Part part = new()
            {
                PartName = partName,
                Suppliers = new List<string>(new[] { supplier }),
                PartID = string.Empty,
                PartType = partType,
                PartAvailableDate = DateTime.Now.Date
            };

            HttpRequestMessage msg = new(HttpMethod.Post, $"{_url}parts")
            {
                Content = JsonContent.Create(part)
            };

            HttpResponseMessage response = await _client.SendAsync(msg);
            response.EnsureSuccessStatusCode();

            string returnedJson = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Part>(returnedJson, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
                        
        }

        public static async Task Update(Part part)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                return;
            }

            HttpRequestMessage msg = new(HttpMethod.Put, $"{_url}parts/{part.PartID}")
            {
                Content = JsonContent.Create<Part>(part)
            };

            HttpClient client = await GetClient();

            HttpResponseMessage response = await client.SendAsync(msg);
            response.EnsureSuccessStatusCode();
        }

        public static async Task Delete(string partID)
        {
            if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            {
                return;
            }

            HttpRequestMessage msg = new(HttpMethod.Delete, $"{_url}parts/{partID}");

            HttpClient client = await GetClient();

            HttpResponseMessage response = await client.SendAsync(msg);
            response.EnsureSuccessStatusCode();
        }
    }
}
