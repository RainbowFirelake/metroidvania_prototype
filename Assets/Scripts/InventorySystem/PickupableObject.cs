using UnityEngine;

namespace InventorySystem
{
    public class PickupableObject : MonoBehaviour
    {
        [SerializeField] private ItemType _type;
        [SerializeField] private int _amount; 

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                var inventory = other.GetComponent<Inventory>();
                if (inventory)
                {
                    inventory.AddItem(_type, _amount);
                }
                this.gameObject.SetActive(false);
            }
        }
    }
}
