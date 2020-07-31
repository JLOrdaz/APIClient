using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JLOrdaz.ApiClient
{
    public class Client
    {
        private JsonSerializerOptions JsonSerOpions => new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        public Uri CreateRequestUri(string relativePath)
        {
            var endpoint = new Uri(BaseEndPoint, relativePath);
            var uriBuilder = new UriBuilder(endpoint);
            return uriBuilder.Uri;
        }

        private readonly HttpClient _httpClient;
        public Uri BaseEndPoint { get; set; }

        public Client()
        {
            _httpClient = new HttpClient();
            Headers();
        }

        public Client(string baseEndPoint)
        {
            _httpClient = new HttpClient();
            BaseEndPoint = new Uri(baseEndPoint);
            Headers();
        }

#region "GET Methods"

        public async Task<T> GetAsync<T>(Uri requestUrl)
        {
            var response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(data, JsonSerOpions);
        }

        public async Task<T> GetAsync<T>(string path)
        {
            Uri requestUrl = CreateRequestUri(path);
            var response = await _httpClient.GetAsync(requestUrl);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(data, JsonSerOpions);
        }
#endregion

#region "POST Method"

        /// <summary>
        /// Common method for making POST calls
        /// </summary>
        public async Task<T1> PostAsync<T1, T2>(Uri requestUrl, T2 content)
        {
            var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T2>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T1>(data, JsonSerOpions);
        }


        /// <summary>
        /// Common method for making POST calls
        /// </summary>
        public async Task<T1> PostAsync<T1, T2>(string path, T2 content)
        {
            Uri requestUrl = CreateRequestUri(path);
            var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T2>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T1>(data, JsonSerOpions);
        }

        public async Task PostAsync<T>(Uri requestUrl, T content)
        {
            var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
        }

        public async Task PostAsync<T>(string path, T content)
        {
            Uri requestUrl = CreateRequestUri(path);
            var response = await _httpClient.PostAsync(requestUrl.ToString(), CreateHttpContent<T>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
        }

#endregion

#region "PUT Methods"
        public async Task PutAsync<T>(Uri requestUrl, T content)
        {
            var response = await _httpClient.PutAsync(requestUrl.ToString(), CreateHttpContent<T>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
        }

        public async Task PutAsync<T>(string path, T content)
        {
            Uri requestUrl = CreateRequestUri(path);
            var response = await _httpClient.PutAsync(requestUrl.ToString(), CreateHttpContent<T>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
        }

        public async Task<T1> PutAsync<T1, T2>(Uri requestUrl, T2 content)
        {
            var response = await _httpClient.PutAsync(requestUrl.ToString(), CreateHttpContent<T2>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T1>(data, JsonSerOpions);
        }

        public async Task<T1> PutAsync<T1, T2>(string path, T2 content)
        {
            Uri requestUrl = CreateRequestUri(path);
            var response = await _httpClient.PutAsync(requestUrl.ToString(), CreateHttpContent<T2>(content));
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T1>(data, JsonSerOpions);
        }

#endregion

#region "Delete Methods"

        public async Task DeleteAsync(Uri requestUrl)
        {
            var response = await _httpClient.DeleteAsync(requestUrl.ToString());
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
        }


        public async Task DeleteAsync(string path)
        {
            Uri requestUrl = CreateRequestUri(path);
            var response = await _httpClient.DeleteAsync(requestUrl.ToString());
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
        }

        public async Task<T> DeleteAsync<T>(Uri requestUrl)
        {
            var response = await _httpClient.DeleteAsync(requestUrl.ToString());
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(data, JsonSerOpions);
        }


        public async Task<T> DeleteAsync<T>(string path)
        {
            Uri requestUrl = CreateRequestUri(path);
            var response = await _httpClient.DeleteAsync(requestUrl.ToString());
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(data, JsonSerOpions);
        }

#endregion


        public HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonSerializer.Serialize(content);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private void Headers()
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // _httpClient.DefaultRequestHeaders.Remove("userIP");
            // string hostname = Dns.GetHostName();
            // _httpClient.DefaultRequestHeaders.Add("userIP", Dns.GetHostEntry(hostname).AddressList[0].ToString());
        }

        public void AddAuthenticationHeader(string schema, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(schema, token);
        }

    }
}
