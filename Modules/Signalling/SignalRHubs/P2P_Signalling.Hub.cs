using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace EMPIRIAN.Modules.Signalling.Hubs;

public class P2PSignallingHub : Hub
{
    private readonly ISignallingBridgesService signallingBridgesService;

    private static ConcurrentDictionary<Guid, HashSet<string>> bridges = new();

    public P2PSignallingHub(ISignallingBridgesService signallingBridgesService)
    {
        this.signallingBridgesService = signallingBridgesService;
    }

    public override async Task OnConnectedAsync()
    {
        // Verify Bridge
        string? bridgeIdString = Context.GetHttpContext()?.Request.Query["bridgeId"];
        if (bridgeIdString == null || !Guid.TryParse(bridgeIdString, out Guid bridgeId))
        {
            Context.Abort();
            return;
        }

        var bridge = await signallingBridgesService.GetBridgeByID(bridgeId);
        if (bridge == null || !bridge.IsActive)
        {
            Context.Abort();
            return;
        }

        // Add Connection to Bridge
        bridges.AddOrUpdate(bridgeId, id => new HashSet<string> { Context.ConnectionId }, (id, connections) =>
        {
            connections.Add(Context.ConnectionId);
            return connections;
        });

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        string? bridgeIdString = Context.GetHttpContext()?.Request.Query["bridgeId"];
        if (bridgeIdString != null && Guid.TryParse(bridgeIdString, out Guid bridgeId) && bridges.TryGetValue(bridgeId, out var connections))
        {
            connections.Remove(Context.ConnectionId);
            if (connections.Count == 0)
            {
                await signallingBridgesService.DeactivateBridge(bridgeId);
                bridges.TryRemove(bridgeId, out _);
            }
        }

        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendSignal(string signal, string data)
    {
        var bridge = bridges.FirstOrDefault(x => x.Value.Contains(Context.ConnectionId));
        if (bridge.Key == Guid.Empty) return;

        // Broadcast to other connections in the bridge
        var connections = bridge.Value.Where(x => x != Context.ConnectionId);
        foreach (var connectionId in connections)
        {
            await Clients.Client(connectionId).SendAsync("receive_signal", signal, data);
        }
    }
}