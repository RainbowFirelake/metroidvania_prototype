namespace Metroidvania.AI.BehaviorTree
{
    public interface IBehaviourStrategy
    {
        Node.Status Process();
        void Reset();
    }
}
