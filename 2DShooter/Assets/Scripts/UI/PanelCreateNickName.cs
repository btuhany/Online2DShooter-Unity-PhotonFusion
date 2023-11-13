using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelCreateNickName : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Button _buttonConfirm;
    [Header("Config")]
    [SerializeField] private int _minNickNameLenght = 2;
    [SerializeField] private int _maxNickNameLenght = 15;

    public event System.Action OnNickNameConfirmed;
    public void InitPanel()
    {
        _buttonConfirm.interactable = false;
        _buttonConfirm.onClick.AddListener(OnClickConfirm);
        _inputField.onValueChanged.AddListener(OnInputValueChanged);
    }
    private void OnClickConfirm()
    {
        string nickName = _inputField.text;
        OnNickNameConfirmed?.Invoke();
    }
    private void OnInputValueChanged(string text)
    {
        _buttonConfirm.interactable = text.Length >= _minNickNameLenght && text.Length <= _maxNickNameLenght;
    }
}
