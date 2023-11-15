using Fusion;
using UnityEngine;

public class PlayerController : NetworkBehaviour, IBeforeUpdate
{
    [SerializeField] private float _moveSpeed = 5f;
    private Rigidbody2D _rb;
    private float _horizontalInput;
    private float _verticalInput;
    public override void Spawned()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public override void FixedUpdateNetwork()
    {
        if(Runner.TryGetInputForPlayer(Object.InputAuthority, out PlayerData input))
        {
            _rb.velocity = new Vector2(_horizontalInput, _verticalInput).normalized * _moveSpeed;
        }
    }
    public void BeforeUpdate()
    {
        if (Utils.IsLocalPlayer(Object))
        {
            _horizontalInput = Input.GetAxisRaw("Horizontal");
            _verticalInput = Input.GetAxisRaw("Vertical");
        }
    }
    public PlayerData GetPlayerNetworkInput()
    {
        PlayerData data = new PlayerData();
        data.HorizontalInput = _horizontalInput;
        data.VerticalInput = _verticalInput;
        return data;
    }
}
