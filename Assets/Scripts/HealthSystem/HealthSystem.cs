using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private Stats _stats; 
    [SerializeField] private float _currentHealth;

    void Start()
    {
        _currentHealth = _stats.GetStatValue(StatType.Health);
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        Debug.Log($"{this.gameObject.name} get {damage} damage");

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log($"{this.gameObject.name} died");
        this.gameObject.SetActive(false);
    }
}
