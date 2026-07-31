using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.AI;
using simpli.Api.Mcp;
using simpli.Application.Dtos;

namespace simpli.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class ChatController:ControllerBase
{
    private readonly IChatClient _chatClient;
    private readonly McpToolRegistery _toolRegistery;
    private readonly ILogger<ChatController> _logger;

    public ChatController(
        IChatClient chatClient,
        McpToolRegistery registery,
        ILogger<ChatController> logger
         )
    {
        _chatClient = chatClient;
       _toolRegistery=registery;
        _logger = logger;
    }
    [HttpPost]
    public async Task<IActionResult> Chat([FromBody] ChatRequestDto request, CancellationToken cancellationToken)
    {
        // 1. Basic Request Validation
        if (string.IsNullOrWhiteSpace(request.message))
        {
            return BadRequest(new ChatResponseDto(
                Success: false,
                Reply: null,
                Error: "Message prompt cannot be empty."
            ));
        }

        _logger.LogInformation("Processing chat request with prompt: {Prompt}", request.message);

        // 2. Define System Role & Conversation Context
        List<ChatMessage> conversation = new()
        {
            new ChatMessage(ChatRole.System, """
                You are the system's AI assistant integrated with personal MCP tools.
                ALWAYS check and call your available
                tools (like 
                SoftDeleteCompanyProfile,
                GetAllRooms,
                CheckOut,
                GetAllVisitors,
                GetVisitor,
                ClearNotification
                ) 
                to find information about what you need to do before answering questions.
                Never state that you lack information without invoking your tools first.
                """),
            new ChatMessage(ChatRole.User, request.message)
        };

        var chatOptions = new ChatOptions
        {
            Tools = _toolRegistery.Gettools()
        };

        ChatResponse response = await _chatClient.GetResponseAsync(
            conversation,
            chatOptions,
            cancellationToken
        );

        _logger.LogInformation("Chat completion successful.");

        return Ok(new ChatResponseDto(
            Success: true,
            Reply: response.Text,
            Error: null
        ));
    }
}