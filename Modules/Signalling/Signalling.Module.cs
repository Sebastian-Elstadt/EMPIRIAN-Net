using EMPIRIAN.Modules.Signalling.Hubs;

namespace EMPIRIAN.Modules.Signalling;

public static class SignallingModule
{
    public static void AddSignallingModule(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ISignallingBridgesService, SignallingBridgesService>();
        builder.Services.AddSignalR();
    }

    public static void MapSignalRHubs(this WebApplication app)
    {
        app.MapHub<P2PSignallingHub>("/signalling/exchange");
    }
}