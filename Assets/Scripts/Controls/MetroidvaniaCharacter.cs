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
        private BasicMovementBehaviour _movementBehaviour;
        [SerializeField]
        private DashBehaviour _dashBehaviour;
        [SerializeField]
        private JumpBehaviour _jumpBehaviour;

        [SerializeField]
        private AttackerBehaviour _attackerBehaviour;

        private IFixedUpdate _currentFixedUpdatable;

        private bool _canMove = true;

        private void OnValidate()
        {
            _movementBehaviour = GetComponent<BasicMovementBehaviour>();
            _dashBehaviour = GetComponent<DashBehaviour>();
            _jumpBehaviour = GetComponent<JumpBehaviour>();
            _attackerBehaviour = GetComponent<AttackerBehaviour>();
        }

        private void Start()
        {
            _currentFixedUpdatable = _movementBehaviour;
        }

        private void OnEnable()
        {
            _attackerBehaviour.OnAttackStart += DisableMovement;
            _attackerBehaviour.OnAttackEnd += EnableMovement;

            _dashBehaviour.OnStartDash += SetDashBehaviourFixedUpdate;
            _dashBehaviour.OnEndDash += SetMovementBehaviourFixedUpdate;
        }

        private void OnDisable()
        {
            _attackerBehaviour.OnAttackEnd -= EnableMovement;
            _attackerBehaviour.OnAttackStart -= DisableMovement;

            _dashBehaviour.OnStartDash -= SetDashBehaviourFixedUpdate;
            _dashBehaviour.OnEndDash -= SetMovementBehaviourFixedUpdate;
        }

        private void FixedUpdate()
        {
            _currentFixedUpdatable?.HandleFixedUpdate();

            if (_movementBehaviour.MovementDirection.x != 0)
            {
                _dashBehaviour.LastDirectionX = _movementBehaviour.MovementDirection.x;
            }
        }

        public void MoveControllable(float horizontalInput, float verticalInput)
        {
            if (!_canMove)
            {
                return;
            }

            _movementBehaviour.SetDirection(horizontalInput, verticalInput);
        }

        public void Attack()
        {
            if (!_attackerBehaviour.CanAttack)
            {
                return;
            }

            if (!_jumpBehaviour.IsGrounded)
            {
                _movementBehaviour.InterruptMovementDuringGivenSeconds(
                    _attackerBehaviour.GetNextAttackAnimationDurationInSeconds());
            }

            if (_dashBehaviour.IsDashing)
            {
                _dashBehaviour.InterruptDash();
            }

            _attackerBehaviour.Attack();
        }

        // TODO: блок
        public void Block()
        {

        }

        public void Dash()
        {
            _attackerBehaviour.InterruptAttack();
            _movementBehaviour.ResetMovementState();
            _dashBehaviour.DashIfCan(_movementBehaviour.MovementDirection.x);
        }

        public void Jump(KeyState state = KeyState.Default)
        {
            _jumpBehaviour.Jump(_movementBehaviour.MovementDirection, state);
        }

        private void EnableMovement()
        {
            _canMove = true;
            Debug.Log(_canMove);
        }

        private void DisableMovement(int hash)
        {
            _canMove = false;
            _movementBehaviour.Stop();
        }

        private void SetDashBehaviourFixedUpdate() => _currentFixedUpdatable = _dashBehaviour;

        private void SetMovementBehaviourFixedUpdate() => _currentFixedUpdatable = _movementBehaviour;
    }
}

