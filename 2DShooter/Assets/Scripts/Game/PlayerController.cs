using Fusion;
using UnityEngine;

public class PlayerController : NetworkBehaviour, IBeforeUpdate
{
    [SerializeField] private float _moveSpeed = 5f;
    private Rigidbody2D _rb;
    private float _horizontalInput;
    public override void Spawned()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public override void FixedUpdateNetwork()
    {
        if(Runner.TryGetInputForPlayer(Object.InputAuthority, out PlayerData input))
        {
            _rb.velocity = new Vector2(input.HorizontalInput * _moveSpeed, _rb.velocity.y);
        }
    }
    public void BeforeUpdate()
    {
        Debug.Log("before updt" + Object.IsValid + " " + Object.HasInputAuthority);
        if (Utils.IsLocalPlayer(Object))
        {
            _horizontalInput = Input.GetAxisRaw("Horizontal");
            Debug.Log("input detected");
        }
    }
    public PlayerData GetPlayerNetworkInput()
    {
        PlayerData data = new PlayerData();
        data.HorizontalInput = _horizontalInput;
        return data;
    }
}
