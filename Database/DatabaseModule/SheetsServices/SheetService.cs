namespace Database.Sheets.Services;

using Grpc.Core;
using DatabaseModule.Entities;
using DatabaseModule.Controllers;
using Database.Sheets;
using Google.Protobuf.WellKnownTypes;

public class SheetService : SpreadsheetService.SpreadsheetServiceBase
{
    private readonly ILogger<SheetService> _logger;
    private UserController _controller;

    public SheetService(ILogger<SheetService> logger)
    {
        _logger = logger;

        _controller = new UserController();
    }

    public override Task<UserResponse> GetUser(GetUserRequest request, ServerCallContext context)
    {
        Console.WriteLine($"'{context.Method}' request received from '{context.Host}': {request}");
        throw new RpcException(new Status(StatusCode.Unimplemented,$"{context.Method} unimplemented by server"));
    }
    public override Task<SheetResponse> GetSheet(GetSheetRequest request, ServerCallContext context)
    {
        Console.WriteLine($"'{context.Method}' request received from '{context.Host}': {request}");
        throw new RpcException(new Status(StatusCode.Unimplemented,$"{context.Method} unimplemented by server"));
    }
    
}
