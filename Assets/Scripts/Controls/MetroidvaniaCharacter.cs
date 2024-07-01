using Metroidvania.Combat;
using Metroidvania.Movement;
using UnityEngine;

namespace Metroidvania.CharacterControllers
{
    [RequireComponent(
    typeof(AttackerBehaviour),
    typeof(DashBehaviour),
    typeof(JumpBehaviour))]
    [RequireComponent(
    typeof(BasicMovementBehaviour))]
    public class MetroidvaniaCharacter : MonoBehaviour, IControllable
    {
        [SerializeField]
        private BasicMovementBehaviour _movement;
        [SerializeField]
        private DashBehaviour _dashBehaviour;
        [SerializeField]
        private JumpBehaviour _jumpBehaviour;

        [SerializeField]
        private AttackerBehaviour _attackerBehaviour;

        private bool _canMove = true;

        private void OnEnable()
        {
            _attackerBehaviour.OnAttackEnd += EnableMovement;
        }

        public void MoveControllable(float horizontalInput, float verticalInput)
        {
            if (!_canMove)
            {
                return;
            }

            _movement.SetDirection(horizontalInput, verticalInput);
        }

        public void Attack()
        {
            if (!_jumpBehaviour.IsGrounded || _dashBehaviour.IsDashing)
                return;

            _attackerBehaviour.Attack();
        }

        // TODO: блок
        public void Block()
        {

        }

        public void Dash()
        {
            _dashBehaviour.DashIfCan(_movement.MovementDirection.x);
        }

        public void Jump(KeyState state = KeyState.Default)
        {
            _jumpBehaviour.Jump(_movement.MovementDirection, state);
        }

        private void EnableMovement()
        {
            _canMove = true;
        }

        private void DisableMovement()
        {
            _canMove = false;
        }
    }
}

