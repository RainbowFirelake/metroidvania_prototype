using UnityEngine;

namespace Metroidvania.Animators
{
    public interface ICharacterMovementAnimator
    {
        public void SetMoveAnimation(Vector2 movementDirection);
    }
}
