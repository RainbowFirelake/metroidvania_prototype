using Metroidvania.Movement;
using System;
using System.Collections;
using UnityEngine;

namespace Metroidvania.Movement
{

}
[RequireComponent(typeof(Rigidbody2D))]
public class DashBehaviour : MonoBehaviour, IFixedUpdate
{
    public event Action OnStartDash;
    public event Action OnEndDash;

    public bool _canDash;
    public bool IsDashing;

    [SerializeField]
    private Rigidbody2D _rigidbody2D;
    [SerializeField]
    private MoveParams _moveParams;

    private void OnValidate()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void DashIfCan(float directionX)
    {
        if (_canDash)
        {
            StartCoroutine(DashCoroutine(directionX));
        }
    }

    public void HandleFixedUpdate()
    {

    }

    public void InterruptDash()
    {
        StopCoroutine(DashCoroutine(0));
        OnEndDash?.Invoke();
        IsDashing = false;
        _canDash = true;
    }

    private IEnumerator DashCoroutine(float directionX)
    {
        _canDash = false;
        IsDashing = true;
        float originalGravity = _rigidbody2D.gravityScale;
        _rigidbody2D.gravityScale = 0f;
        _rigidbody2D.velocity = new Vector2(
            directionX * _moveParams.DashStrength,
            0f);

        OnStartDash?.Invoke();

        yield return new WaitForSeconds(_moveParams.DashTime);
        _rigidbody2D.gravityScale = originalGravity;

        yield return new WaitForSeconds(_moveParams.DashCooldown);
        IsDashing = false;

        _canDash = true;
        OnEndDash?.Invoke();
    }
}
