using Fusion;
using System.Collections;
using UnityEngine;

public class PlayerController : NetworkBehaviour, IBeforeUpdate
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rollForce = 5f;
    private Rigidbody2D _rb;
    private float _horizontalInput;
    private float _verticalInput;
    private bool _canMove = true;
    [Networked] private NetworkButtons _buttonsPrev { get; set; }
    private enum PlayerInputButtons
    {
        None,
        Roll
    }
    public override void Spawned()
    {
        _rb = GetComponent<Rigidbody2D>();
        _canMove = true;
    }
    public override void FixedUpdateNetwork()
    {
        if(Runner.TryGetInputForPlayer(Object.InputAuthority, out PlayerData input))
        {
            Vector2 dir = new Vector2(input.HorizontalInput, input.VerticalInput).normalized;
            
            if (_canMove)
                _rb.velocity = dir * _moveSpeed;
            
            CheckRollInput(input, dir);
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
        data.NetworkButtons.Set(PlayerInputButtons.Roll, Input.GetKey(KeyCode.Space));
        return data;
    }
    private void CheckRollInput(PlayerData input, Vector2 dir)
    {
        NetworkButtons networkButtons = input.NetworkButtons.GetPressed(_buttonsPrev);
        if (networkButtons.WasPressed(_buttonsPrev, PlayerInputButtons.Roll))
        {
            //TODO: Set to look direction
            if (dir == Vector2.zero)
                dir = Vector2.right;

            _rb.AddForce(dir * _rollForce, ForceMode2D.Impulse);
            StartCoroutine(MoveCooldown());
        }

        _buttonsPrev = input.NetworkButtons;
    }

    private IEnumerator MoveCooldown()
    {
        _canMove = false;
        yield return new WaitForSeconds(0.5f);
        _canMove = true;

    }
}
