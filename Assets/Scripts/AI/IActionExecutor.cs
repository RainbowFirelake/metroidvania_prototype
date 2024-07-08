using Metroidvania.AI.Actions;

namespace Metroidvania.AI
{
    public interface IActionExecutor
    {
        IAction Action { get; }

        public void SetAction(IAction action);

        public void StopAction();
    }
}
