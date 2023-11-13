using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelJoinRoom : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Button _buttonJoinRoom;
    [SerializeField] private Button _buttonJoinRandomRoom;
    [SerializeField] private Button _buttonCreateRoom;
    [SerializeField] private TMP_InputField _inputCreateRoomName;
    [SerializeField] private TMP_InputField _inputJoinRoomName;
    [Header("Config")]
    [SerializeField] private int _minRoomNameLenght = 2;
    [SerializeField] private int _maxRoomNameLenght = 20;

    public event System.Action<string, GameMode> OnEnteredRoom;

    public void InitPanel()
    {
        _buttonCreateRoom.interactable = false;

        _buttonJoinRoom.onClick.AddListener(JoinRoom);
        _buttonJoinRandomRoom.onClick.AddListener(JoinRandomRoom);
        _buttonCreateRoom.onClick.AddListener(CreateRoom);
        _inputCreateRoomName.onValueChanged.AddListener(OnInputCreateRoomValueChanged);
    }
    private void OnInputCreateRoomValueChanged(string text)
    {
        _buttonCreateRoom.interactable = text.Length >= _minRoomNameLenght && text.Length <= _maxRoomNameLenght;
    }
    private void CreateRoom()
    {
        OnEnteredRoom?.Invoke(_inputCreateRoomName.text, GameMode.Host);
    }
    private void JoinRoom()
    {
        OnEnteredRoom?.Invoke(_inputJoinRoomName.text, GameMode.Client);
    }
    private void JoinRandomRoom()
    {
        OnEnteredRoom?.Invoke(string.Empty, GameMode.AutoHostOrClient);
    }
}
