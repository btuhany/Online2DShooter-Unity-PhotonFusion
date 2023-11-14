using Fusion;
using UnityEngine;

public class PlayerSpawnerController : NetworkBehaviour, IPlayerJoined, IPlayerLeft
{
    [SerializeField] private NetworkPrefabRef _playerNetworkPrefab = NetworkPrefabRef.Empty;
    [SerializeField] private Transform _spawnPoint;

    //Host is the first one to join the server, so there will be one active player anyway.
    public override void Spawned()
    {
        Debug.Log("Spawned");
        if (Runner.IsServer)
        {
            foreach (PlayerRef player in Runner.ActivePlayers)
            { 
                SpawnPlayer(player);
            }
        }
    }
    private void SpawnPlayer(PlayerRef playerRef)
    {
        if (Runner.IsServer)
        {
            Debug.Log(_spawnPoint.position);
            var playerObj = Runner.Spawn(_playerNetworkPrefab, _spawnPoint.position, _spawnPoint.rotation);
            _spawnPoint.position += Vector3.right * 2f;
            Runner.SetPlayerObject(playerRef, playerObj);
        }
    }
    private void DespawnPlayer(PlayerRef playerRef)
    {
        if (!Runner.IsServer) return;

        if (Runner.TryGetPlayerObject(playerRef, out NetworkObject networkObject))
        {
            if (networkObject != null)
                Runner.Despawn(networkObject);
        }
        Runner.SetPlayerObject(playerRef, null);
    }
    public void PlayerJoined(PlayerRef playerRef)
    {
        SpawnPlayer(playerRef);
    }

    public void PlayerLeft(PlayerRef playerRef)
    {
        DespawnPlayer(playerRef);
    }
}
