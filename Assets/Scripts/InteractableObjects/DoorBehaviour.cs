using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InteractableObjects.Doors
{
    public class DoorBehaviour : MonoBehaviour
    {
        [SerializeField] private DoorTrigger _doorTrigger;
        [SerializeField] private float _openedDoorDelta = 5f;
        [SerializeField] private float _openingDoorSpeed = 1f;

        private bool _isOpened = false;
        private Vector2 _openedDoorPoint;
        private Transform _transform;

        void Start()
        {
            _transform = GetComponent<Transform>();
            _openedDoorPoint = new Vector2(
                _transform.position.x,
                _transform.position.y + _openedDoorDelta);
        }

        private void OnEnable()
        {
            _doorTrigger.OnDoorActivatorEnter += ActivateDoor;
        }

        private void Update()
        {
            if (!_isOpened) return;

            float step = _openingDoorSpeed * Time.deltaTime;
            _transform.position = Vector2.MoveTowards(
                _transform.position,
                _openedDoorPoint,
                step);
        }

        public void ActivateDoor()
        {
            _isOpened = true;
        }
    }
}
