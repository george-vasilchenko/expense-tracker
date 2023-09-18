using Microsoft.AspNetCore.Builder;

namespace WebApi.Plugin;

public interface IWebPlugin
{
    void AddServices(WebApplicationBuilder webApplicationBuilder);
    void Bootstrap(WebApplication webApplication);
}