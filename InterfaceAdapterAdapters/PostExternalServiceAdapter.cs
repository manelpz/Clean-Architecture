using ApplicationBusinessLayer;
using EnterpriseBusinessLayer;
using InterfaceAdapterAdapters.DTO;

namespace InterfaceAdapterAdapters;

public class PostExternalServiceAdapter:IExternalServiceAdapter<Post>
{
    private readonly IExternalService<PostServiceDTO> _externalService;

    public PostExternalServiceAdapter(IExternalService<PostServiceDTO> externalService)
    => _externalService = externalService;
    
    public async Task<IEnumerable<Post>> GetDataAsync()
    {
        var postExternalService = await _externalService.GetContentAsync();
        var post = postExternalService.Select(b => new Post()
        {
            Id = b.Id,
            Body = b.Body,
            Title = b.Title,
        });

        return post;
    }
}