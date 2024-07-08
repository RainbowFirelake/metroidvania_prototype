namespace Metroidvania.AI.BehaviorTrees
{
    public interface IBehaviourStrategy
    {
        Node.Status Process();
        void Reset();
    }
}
