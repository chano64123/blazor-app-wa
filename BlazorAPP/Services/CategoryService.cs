using System.Text.Json;
using BlazorAPP.Models;

namespace BlazorAPP.Services;

public class CategoryService : ICategoryService{
    public readonly HttpClient _client;
    private readonly JsonSerializerOptions _options;

    public CategoryService(HttpClient client)
    {
        _client = client;
        _options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
    }

    public async Task<List<Category>?> GetAll(){
        var response = await _client.GetAsync("v1/categories");
        var content = await response.Content.ReadAsStringAsync();
        if(!response.IsSuccessStatusCode){
            throw new ApplicationException(content);
        }
        return JsonSerializer.Deserialize<List<Category>>(content, _options);
    }

    public async Task<Category?> GetById(int id){
        var response = await _client.GetAsync($"v1/products/{id}");
        var content = await response.Content.ReadAsStringAsync();
        if(!response.IsSuccessStatusCode){
            throw new ApplicationException(content);
        }
        return JsonSerializer.Deserialize<Category>(content, _options);
    }
}

public interface ICategoryService {
    Task<List<Category>?> GetAll();
    Task<Category?> GetById(int id);
}