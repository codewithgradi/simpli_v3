using System.ComponentModel;
using ModelContextProtocol.Server;
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
    }
}