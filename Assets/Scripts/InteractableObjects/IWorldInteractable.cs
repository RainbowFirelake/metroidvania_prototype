using UnityEngine;

public interface IWorldInteractable
{
    public string InteractionName { get; }
    public Transform t { get; }
    
    void Interact(GameObject Interactor);
}
