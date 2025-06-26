using System.Text.Json;
using InterfaceAdapterAdapters;
using InterfaceAdapterAdapters.DTO;

namespace FrameworkDriversExternalServices;

public class PostService:IExternalService<PostServiceDTO>
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _options;

    public PostService(HttpClient httpClient)
    {
        
        _httpClient = httpClient;
        _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
    }
    public async Task<IEnumerable<PostServiceDTO>> GetContentAsync()
    {
        var responseMessage = await _httpClient.GetAsync(_httpClient.BaseAddress);
        responseMessage.EnsureSuccessStatusCode();
        
        var responseData = await responseMessage.Content.ReadAsStringAsync();
        
        return  JsonSerializer.Deserialize<IEnumerable<PostServiceDTO>>(responseData, _options);
    }
}