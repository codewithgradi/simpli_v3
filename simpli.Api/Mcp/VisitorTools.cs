using System.ComponentModel;
using ModelContextProtocol.Server;
using simpli.Application.Services;
using simpli.Domain.Entities;

namespace simpli.Api.Mcp
{
    [McpServerToolType]
    public class VisitorTools
    {
        private readonly IServiceProvider _provider;

        public VisitorTools(IServiceProvider provider)
        {
            _provider = provider;
        }
        [McpServerTool(Name ="get_all_visitors"), Description("Returns a list of all visitors")]
        public async Task<List<VisitorDto>> GetAllVisitors(
            [Description("this is the company id for which company the visitor is checked in at")]
            int companyId)
        {
            await using var scope = _provider.CreateAsyncScope();
            var service = scope.ServiceProvider.GetRequiredService<VisitorTools>();
            var visitors = await service.GetAllVisitors(companyId);
            return visitors;
        }
        
        [McpServerTool(Name="get_one_visitor"), Description("Returns on visitor from visitors table")]
        public async Task<VisitorDto> GetVisitor([Description("This is visitor id")]int id)
        {
            using var scope = _provider.CreateAsyncScope();
            var service = scope.ServiceProvider.GetRequiredService<VisitorService>();
            var visitor = await service.GetVisitor(id);
            return visitor;
        }
    }
}