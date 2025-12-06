using EMPIRIAN.Database.Signalling;

public interface ISignallingBridgesService
{
    Task<SignallingBridge> CreateBridge(Guid initiatorUserId);
    Task<SignallingBridge?> GetBridgeByID(Guid bridgeId);
    Task DeactivateBridge(SignallingBridge bridge);
    Task DeactivateBridge(Guid bridgeId);
}