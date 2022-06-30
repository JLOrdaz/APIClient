using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace JLOrdaz.ApiClient;

public class Client : IClient
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

    public async Task<T?> GetAsync<T>(string path) 
    {
        var response = await _httpClient.GetAsync(path); 
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<T>() ?? default;
        }
        else
        {
            return default;
        }
    }

    public async Task<T1?> PostAsync<T1, T2>(string path, T2 content) 
    {
        var response = await _httpClient.PostAsJsonAsync<T2>(path, content);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<T1>() ?? default;
        }
        else
        {
            return default;
        }
    }

    public async Task<string> PostReadStringAsync<T>(string path, T content)
    {
        var response = await _httpClient.PostAsJsonAsync<T>(path, content);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync() ?? string.Empty;
        }
        else
        {
            return response.ReasonPhrase ?? string.Empty;
        }
    }


    public async Task<string> PostReadStringAsync(string path)
    {
        var response = await this._httpClient.PostAsync(path, null);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        else
        {
            return response.ReasonPhrase ?? string.Empty;
        }
    }

    public async Task PostAsync<T>(string path, T content)
    {
        await _httpClient.PostAsJsonAsync<T>(path, content);
    }

    public async Task PutAsync<T>(string path, T content)
    {
        await _httpClient.PutAsJsonAsync<T>(path, content);
    }

    public async Task<T?> PutAsync<T, U>(string path, U content)
    {
        var response = await _httpClient.PutAsJsonAsync<U>(path, content);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<T>() ?? default;
        }
        else
        {
            return default;
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
            return await response.Content.ReadFromJsonAsync<T>() ?? default(T)!;
        }
        else
        {
            return default(T)!;
        }
    }

    public async Task<string> GetStringAsync(string path)
    {
        var response = await _httpClient.GetStringAsync(path); 
        return string.IsNullOrEmpty(response) ? string.Empty : response;
    }
}
