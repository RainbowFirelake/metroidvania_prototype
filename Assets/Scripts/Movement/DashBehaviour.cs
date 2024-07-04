using System;
using System.Collections;
using UnityEngine;

namespace Metroidvania.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DashBehaviour : MonoBehaviour, IFixedUpdate
    {
        public event Action OnStartDash;
        public event Action OnEndDash;

        public bool CanDash;
        public bool IsDashing { get; private set; }
        public float LastDirectionX { get; set; }

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
            if (CanDash)
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
            CanDash = true;
        }

        private IEnumerator DashCoroutine(float directionX)
        {
            CanDash = false;
            IsDashing = true;
            float originalGravity = _rigidbody2D.gravityScale;
            _rigidbody2D.gravityScale = 0f;
            _rigidbody2D.velocity = new Vector2(
                (directionX != 0
                    ? directionX
                    : LastDirectionX) * _moveParams.DashStrength,
                0f);

            OnStartDash?.Invoke();

            yield return new WaitForSeconds(_moveParams.DashTime);
            _rigidbody2D.gravityScale = originalGravity;

            yield return new WaitForSeconds(_moveParams.DashCooldown);
            IsDashing = false;

            CanDash = true;
            OnEndDash?.Invoke();
        }
    }
}
