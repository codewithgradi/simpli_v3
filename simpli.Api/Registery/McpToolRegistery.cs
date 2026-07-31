using Microsoft.Extensions.AI;
using simpli.Api.Mcp;

public class McpToolRegistery
{
    private readonly List<AITool> _aiTools = new ();
    public McpToolRegistery( RoomTools roomTools,
        CompanyTools companyTools,
        VisitorTools visitorTools,
        NotificationTools notificationTools)
    {
        _aiTools.AddRange(AIFunctionFactory.Create(companyTools.SoftDeleteCompanyProfile));
        _aiTools.AddRange(AIFunctionFactory.Create(roomTools.GetAllRooms));
        _aiTools.AddRange(AIFunctionFactory.Create(visitorTools.CheckOut));
        _aiTools.AddRange(AIFunctionFactory.Create(visitorTools.GetAllVisitors));
        _aiTools.AddRange(AIFunctionFactory.Create(visitorTools.GetVisitor));
        _aiTools.AddRange(AIFunctionFactory.Create(notificationTools.ClearNotification));
    }
    public List<AITool> Gettools()=>_aiTools;
    
}