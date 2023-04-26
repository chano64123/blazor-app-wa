namespace BlazorAPP.Models;

public class Product{
    public int id { get; set; }
    public string title { get; set; } = String.Empty;
    public double price { get; set; }
    public string description { get; set; } = String.Empty;
    public int categoryId { get; set; }
    public Category category { get; set; }
    public List<string> images { get; set; } = new List<string>();
    public string image { get; set; } = String.Empty;
}