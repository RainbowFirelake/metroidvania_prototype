using Metroidvania.AllyAndEnemy;
using Metroidvania.CharacterControllers;
using UnityEngine;

namespace Metroidvania.AI.Actions
{
    public class AttackAction : IAction
    {
        private readonly MetroidvaniaCharacter _character;
        private readonly AllyAndEnemySystem _target;

        public AttackAction(MetroidvaniaCharacter character, AllyAndEnemySystem target)
        {
            _character = character;
            _target = target;
        }

        public void Execute()
        {
            if (Vector3.Distance(_character.Position, _target.transform.position) > 1f)
            {
                return;
            }
        }
    }
}
