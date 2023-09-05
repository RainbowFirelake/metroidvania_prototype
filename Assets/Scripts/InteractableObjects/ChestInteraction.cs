using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cainos.PixelArtPlatformer_VillageProps;

public class ChestInteraction : MonoBehaviour, IWorldInteractable
{
    [SerializeField]
    private Chest _chest;
    [field: SerializeField]
    public string InteractionName { get; private set; }
    
    public Transform t { get; private set; }

    public bool CanInteract { get; set; } = true;

    void Start()
    {
        t = this.transform;
    }

    public void Interact(GameObject Interactor)
    {
        if (!CanInteract) return;

        _chest.Open();
        CanInteract = false;
    }
}
