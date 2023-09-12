using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ModifiableActor : MonoBehaviour
{
    [field : SerializeField]
    public HealthSystem Health { get; private set; }

    private List<ModifierData> _modifiers;
    private Transform _transform;

    void Start()
    {
        _modifiers = new List<ModifierData>();
        _transform = GetComponent<Transform>();
    }

    public void AddModifier(BaseModifier modifier)
    {
        _modifiers.Add(new ModifierData(modifier));
    }

    private void Update()
    {
        UpdateTimers();
        if (_modifiers.Count == 0) return; 
    
        for (int i = 0; i < _modifiers.Count; i++)
        {
            if (_modifiers[i]._currentEffectTime > 1f)
            {
                _modifiers[i].Use(this);
                _modifiers[i].Modifier.CreateVFX(
                    transform.position, transform.rotation);
                    
                if (!_modifiers[i].IsActive)
                {
                    _modifiers.Remove(_modifiers[i]);
                    return;
                }
                _modifiers[i]._currentEffectTime = 0f;
            }
        }
    }

    private void UpdateTimers()
    {
        for (int i = 0; i < _modifiers.Count; i++)
        {
            _modifiers[i]._currentEffectTime += Time.deltaTime;
        }
    }

    [System.Serializable]
    public class ModifierData : IDisposable
    {
        public float _currentEffectTime;
        public int _currentTimes { get; private set; }
        public BaseModifier Modifier { get; private set; }
        public bool IsActive;

        public ModifierData(BaseModifier modifier)
        {
            _currentEffectTime = 0f;
            _currentTimes = 0;
            Modifier = modifier;
            IsActive = true;
        }

        public void Use(ModifiableActor actor)
        {
            Modifier.GiveEffect(actor);
            _currentTimes++;
            if (_currentTimes >= Modifier.EffectTimes)
            {
                IsActive = false;
            }
        }

        public void Dispose()
        {
            Modifier = null;
        }
    }
}