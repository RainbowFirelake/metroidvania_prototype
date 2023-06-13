using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IControllable))]
public class PlayerInput : MonoBehaviour
{
    private IControllable _controllable = null;
    private bool _isEnabled = true;

    private void Start()
    {
        _controllable = GetComponent<IControllable>();
    }

    private void Update()
    {   
        if (!_isEnabled) return;

        float directionX = Input.GetAxisRaw("Horizontal");
        float directionY = Input.GetAxisRaw("Vertical");
        _controllable.MoveControllable(directionX, directionY);

        if (Input.GetKey(KeyCode.Space))
        {
            _controllable.Jump(KeyState.KeyDown);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _controllable.Jump(KeyState.KeyUp);
        }

        if (Input.GetMouseButtonDown(0))
        {
            _controllable.Attack();
        }

        if (Input.GetMouseButtonDown(1))
        {
            _controllable.Block();
        }
        else 
        {

        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _controllable.Dash();
        }
    }
}
