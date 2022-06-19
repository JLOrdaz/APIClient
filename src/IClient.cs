namespace JLOrdaz.ApiClient;

public interface IClient
{
    void AddAuthenticationHeader(string schema, string token);
    Task<T?> GetAsync<T>(string path);
    Task<T1?> PostAsync<T1, T2>(string path, T2 content);
    Task<string> PostReadStringAsync<T>(string path, T content);
    Task<string> PostReadStringAsync(string path);
    Task PostAsync<T>(string path, T content);
    Task PutAsync<T>(string path, T content);
    Task<T?> PutAsync<T, U>(string path, U content);
    Task DeleteAsync(string path);
    Task<T> DeleteAsync<T>(string path);

}