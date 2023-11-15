using Fusion;

public static class Utils
{
    public static bool IsLocalPlayer(NetworkObject networkObj)
    {
        return networkObj.IsValid == networkObj.HasInputAuthority;
    }
}
