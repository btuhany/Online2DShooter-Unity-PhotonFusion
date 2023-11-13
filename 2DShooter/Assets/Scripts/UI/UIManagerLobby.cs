using Fusion;
using UnityEngine;

public class UIManagerLobby : MonoBehaviour
{
    [SerializeField] private PanelCreateNickName _panelCreateNickName;
    [SerializeField] private PanelJoinRoom _panelJoinRoom;
    [SerializeField] private PanelLoadingScreen _panelLoadingScreen;

    private void Awake()
    {
        
    }
    private void OnEnable()
    {
        _panelCreateNickName.InitPanel();
        _panelCreateNickName.OnNickNameConfirmed += HandleOnNickNameConfirmed;

        _panelJoinRoom.InitPanel();
        _panelJoinRoom.OnEnteredRoom += HandleOnEnteredRoom;

        _panelLoadingScreen.OnCancelConnection += HandleOnCancelConnection;

        NetworkRunnerController.Instance.OnPlayerJoinedEvent += HandleOnPlayerJoined;
    }
    private void HandleOnNickNameConfirmed()
    {
        _panelCreateNickName.gameObject.SetActive(false);
        _panelJoinRoom.gameObject.SetActive(true);
    }
    private void HandleOnEnteredRoom(string roomName, GameMode mode)
    {
        NetworkRunnerController.Instance.StartGame(mode, roomName, 4);
        _panelCreateNickName.gameObject.SetActive(false);
        _panelJoinRoom.gameObject.SetActive(false);
        _panelLoadingScreen.gameObject.SetActive(true);
    }
    private void HandleOnPlayerJoined()
    {
        //_panelLoadingScreen.gameObject.SetActive(false);
    }
    private void HandleOnCancelConnection()
    {
        NetworkRunnerController.Instance.ShutDownRunner();
    }
}
