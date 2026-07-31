using ModelContextProtocol.Server;

namespace simpli.Api.Mcp
{
    [McpServerToolType]
    public class RoomTools
    {
        private readonly IServiceProvider _provider;

        public RoomTools(IServiceProvider provider)
        {
            _provider = provider;
        }
        [McpServerTool]
        public async 
    }
}