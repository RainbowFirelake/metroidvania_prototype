using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DashBehaviour : MonoBehaviour
{
    public event Action OnStartDash;
    public event Action OnEndDash;

    [SerializeField]
    private Rigidbody2D _rigidbody2D;
    [SerializeField]
    private MoveParams _moveParams;

    public bool _canDash;
    public bool IsDashing;

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

    private IEnumerator DashCoroutine(float directionX)
    {
        _canDash = false;
        IsDashing = true;
        float originalGravity = _rigidbody2D.gravityScale;
        _rigidbody2D.gravityScale = 0f;
        _rigidbody2D.velocity = new Vector2(
            directionX *
            _moveParams.DashStrength, 0f);

        OnStartDash?.Invoke();

        yield return new WaitForSeconds(_moveParams.DashTime);
        _rigidbody2D.gravityScale = originalGravity;
        IsDashing = false;

        yield return new WaitForSeconds(_moveParams.DashCooldown);

        _canDash = true;
        OnEndDash?.Invoke();
    }
}
