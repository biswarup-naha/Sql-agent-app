using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SqlAgent.Services
{
    public class AgentService
    {
        private HttpClient _http;
        public AgentService(HttpClient http)
        {
            _http = http;
        }

        public async Task<string> QueryAgent(string prompt)
        {
            var body = new
            {
                model = "llama3",
                prompt = prompt,
                stream = false
            };

            var response = await _http.PostAsync(
                "http://ollama:11434/api/generate",
                new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json")
                );

            var json = await response.Content.ReadAsStringAsync();
            var res = JsonSerializer.Deserialize<AgentResponse>(json);

            return res.Response;
        }
    }

    public class AgentResponse
    {
        [JsonPropertyName("response")]
        public string? Response { get; set; }
    }
}
