using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Interactor : MonoBehaviour
{
    public event Action<Transform, string> OnInteractablePositionDetected;
    public event Action OnInteractableMissing;

    [SerializeField] private float _updateInteractablesAround = 0.3f;
    [SerializeField] private float _interactRadius = 2f;
    [SerializeField] private LayerMask _interactableMask;
    
    private IWorldInteractable nearestInteractable;
    private float _timeAfterLastCheckAround = Mathf.Infinity;

    void Update()
    {
        FindNearestInteraction();
        Interaction();

        UpdateTimers();
    }

    private void Interaction()
    {
        if (nearestInteractable != null)
            OnInteractablePositionDetected?.Invoke(
                nearestInteractable.t, nearestInteractable.InteractionName);
        else OnInteractableMissing?.Invoke();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interaction");
            if (nearestInteractable == null) return;

            nearestInteractable.Interact(this.gameObject);
        }
    }

    private void FindNearestInteraction()
    {
        if (_timeAfterLastCheckAround > _updateInteractablesAround)
        {
            Collider2D[] interactables = Physics2D.OverlapCircleAll(
                this.transform.position,
                _interactRadius,
                _interactableMask
            );
            if (interactables.Length == 0)
            {
                nearestInteractable = null;
                _timeAfterLastCheckAround = 0;
                return;
            }
            if (interactables.Length == 1)
            {
                _timeAfterLastCheckAround = 0;
                nearestInteractable = interactables[0].GetComponent<IWorldInteractable>();
                return;
            }

            var nearest = interactables[0];
            var minDistance = float.MaxValue;
            foreach (var item in interactables)
            {
                var distance = Vector3.Distance(
                    item.transform.position,
                    nearest.transform.position);
                if (distance < minDistance)
                {
                    nearest = item;
                    minDistance = distance;
                }
            }
            nearestInteractable = nearest.GetComponent<IWorldInteractable>();
            _timeAfterLastCheckAround = 0;
        }
    }

    private void UpdateTimers()
    {
        _timeAfterLastCheckAround += Time.deltaTime;
    }   

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _interactRadius);
    }
}
