using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelLoadingScreen : MonoBehaviour
{
    [SerializeField] private Button _buttonCancel;
    public event System.Action OnCancelConnection;
    private void OnEnable()
    {
        _buttonCancel.onClick.AddListener(CancelButton);
    }
    private void CancelButton()
    {
        OnCancelConnection?.Invoke();
    }
}
