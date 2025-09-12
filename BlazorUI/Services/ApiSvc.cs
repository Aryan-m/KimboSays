using BlazorUI.Models;
using System.Net.Http.Json;

namespace BlazorUI.Services
{
    public class ApiSvc : IApiSvc
    {
        private readonly HttpClient _httpClient;
        private const string TaskEndPointUrl = "kimbotasks";
        private const string EffortEndPointUrl = "effortoptions";

        public ApiSvc(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<KimboTask>> GetAllTasksAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<KimboTask>>(TaskEndPointUrl);
        }

        public async Task<KimboTask> GetTaskByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<KimboTask>($"{TaskEndPointUrl}/{id}");
        }

        public async Task<KimboTask> AddTaskAsync(KimboTask newTask)
        {
            var response = await _httpClient.PostAsJsonAsync(TaskEndPointUrl, newTask);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<KimboTask>();
        }

        public async Task UpdateTaskAsync(KimboTask updatedTask)
        {
            var response = await _httpClient.PutAsJsonAsync($"{TaskEndPointUrl}", updatedTask);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteTaskAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{TaskEndPointUrl}/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<TaskEffort>> GetEffortOptionsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TaskEffort>>(EffortEndPointUrl);
        }
    }
}
