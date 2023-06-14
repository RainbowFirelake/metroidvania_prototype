using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IControllable))]
public class NewPlayerInput : MonoBehaviour
{
    private IControllable _controllable = null;
    private bool _isEnabled = true;
    private PlayerInputSystem _inputSystem;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        _inputSystem = new PlayerInputSystem();

        _inputSystem.Player.Jump.performed += context => _controllable.Jump(KeyState.KeyDown);
        _inputSystem.Player.Jump.canceled += context => _controllable.Jump(KeyState.KeyUp);
        _inputSystem.Player.Dash.performed += context => _controllable.Dash();
        _inputSystem.Player.Attack.performed += context => _controllable.Attack();
    }

    // Start is called before the first frame update
    void Start()
    {
        _controllable = GetComponent<IControllable>();
         }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        _inputSystem.Enable();
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        _inputSystem.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveDirection = _inputSystem.Player.Move.ReadValue<Vector2>();
        _controllable.MoveControllable(moveDirection.x, moveDirection.y);
    }
}
