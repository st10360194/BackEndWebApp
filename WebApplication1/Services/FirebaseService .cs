using System.Text;
using System.Text.Json;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class FirebaseService:IFirebaseService
    {

        private readonly HttpClient _httpClient;
        private readonly string _databaseUrl;

        public FirebaseService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;

            _databaseUrl = configuration["Firebase:DatabaseUrl"]
                ?? throw new Exception("Firebase Database URL is missing.");
        }

        public async Task<List<Tasks>> GetAllTasks()
        {
            string url = $"{_databaseUrl}/Tasks.json";

            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json) || json == "null")
            {
                return new List<Tasks>();
            }

            var firebaseData =
                JsonSerializer.Deserialize<Dictionary<string, Tasks>>(json);

            if (firebaseData == null)
            {
                return new List<Tasks>();
            }

            foreach (var item in firebaseData)
            {
                item.Value.Id = item.Key;
            }

            return firebaseData.Values.ToList();
        }

        public async Task<Tasks?> GetTaskById(string id)
        {
            string url = $"{_databaseUrl}/Tasks/{id}.json";

            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(json) || json == "null")
            {
                return null;
            }

            var task = JsonSerializer.Deserialize<Tasks>(json);

            if (task != null)
            {
                task.Id = id;
            }

            return task;
        }


        public async Task<Tasks> CreateTask(Tasks task) {


            string url = $"{_databaseUrl}/Tasks.json";

            var json = JsonSerializer.Serialize(task);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(url, content);

            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<Dictionary<string, string>>(responseJson);

            if (result == null || !result.ContainsKey("name"))
            {
                throw new Exception("Firebase did not return a task ID.");
            }

            task.Id = result["name"];

            return task;



        }
    }
}
