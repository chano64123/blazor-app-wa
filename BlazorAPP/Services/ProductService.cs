using System.Net.Http.Json;
using System.Text.Json;
using BlazorAPP.Models;

namespace BlazorAPP.Services;

public class ProductService : IProductService{
    public readonly HttpClient _client;
    private readonly JsonSerializerOptions _options;

    public ProductService(HttpClient client)
    {
        _client = client;
        _options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
    }

    public async Task<List<Product>?> GetAll(){
        var response = await _client.GetAsync("v1/products");
        var content = await response.Content.ReadAsStringAsync();
        if(!response.IsSuccessStatusCode){
            throw new ApplicationException(content);
        }
        return JsonSerializer.Deserialize<List<Product>>(content, _options);
    }

    public async Task<Product?> GetById(int id){
        var response = await _client.GetAsync($"v1/products/{id}");
        var content = await response.Content.ReadAsStringAsync();
        if(!response.IsSuccessStatusCode){
            throw new ApplicationException(content);
        }
        return JsonSerializer.Deserialize<Product>(content, _options);
    }

    public async Task Add (Product product){
        var response = await _client.PostAsync($"v1/products", JsonContent.Create(product));
        var content = await response.Content.ReadAsStringAsync();
        if(!response.IsSuccessStatusCode){
            throw new ApplicationException(content);
        }
    }

    public async Task Delete (int id){
        var response = await _client.DeleteAsync($"v1/products/{id}");
        var content = await response.Content.ReadAsStringAsync();
        if(!response.IsSuccessStatusCode){
            throw new ApplicationException(content);
        }
    }
}

public interface IProductService {
    Task<List<Product>?> GetAll();
    Task<Product?> GetById(int id);
    Task Add (Product product);
    Task Delete (int id);
}
