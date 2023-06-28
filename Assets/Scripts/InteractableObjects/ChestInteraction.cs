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

    void Start()
    {
        t = this.transform;
    }

    public void Interact(GameObject Interactor)
    {
        _chest.Open();
    }
}
