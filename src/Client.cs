using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JLOrdaz.ApiClient
{
    public class Client
    {
        private readonly HttpClient _httpClient;
        public Uri BaseEndPoint { get; set; }

        public Client(string baseEndPoint)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseEndPoint);
            BaseEndPoint = new Uri(baseEndPoint);
            Headers();
        }

        public Client(string baseEndPoint, string schema, string token)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseEndPoint);
            BaseEndPoint = new Uri(baseEndPoint);
            Headers();
            AddAuthenticationHeader(schema, token);
        }

        private void Headers()
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void AddAuthenticationHeader(string schema, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(schema, token);
        }

        public async Task<T> GetAsync<T>(string path)
        {
            return await _httpClient.GetFromJsonAsync<T>(path);
        }

        public async Task<T1> PostAsync<T1, T2>(string path, T2 content)
        {
            var response = await _httpClient.PostAsJsonAsync<T2>(path, content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T1>();
            }
            else
            {
                return default(T1);
            }
        }

        public async Task PostAsync<T>(string path, T content)
        {
            //var response = await _httpClient.PostAsJsonAsync<T>(path, content);
            await _httpClient.PostAsJsonAsync<T>(path, content);
        }

        public async Task PutAsync<T>(string path, T content)
        {
            await _httpClient.PutAsJsonAsync<T>(path, content);
        }

        public async Task<T1> PutAsync<T1, T2>(string path, T2 content)
        {
            var response = await _httpClient.PutAsJsonAsync<T2>(path, content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T1>();
            }
            else
            {
                return default(T1);
            }
        }

        public async Task DeleteAsync(string path)
        {
            await _httpClient.DeleteAsync(path);
        }

        public async Task<T> DeleteAsync<T>(string path)
        {
            var response = await _httpClient.DeleteAsync(path);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            else
            {
                return default(T);
            }
        }
    }
}
