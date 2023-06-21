using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class FireLighting : MonoBehaviour
{
    [SerializeField] private Light2D _light;

    [SerializeField] private float _maxReduction = 0.2f;
    [SerializeField] private float _maxIncrease = 0.2f;
    [SerializeField] private float _rateDamping = 0.1f;
    [SerializeField] private float _strength = 300;

    private float _baseIntensity;
    private bool _flickering;

    void Start()
    {
        _baseIntensity = _light.intensity;
    }

    void Update()
    {
        if (!_flickering)
        {
            StartCoroutine(FireLight());
        }
    } 

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        StopCoroutine(FireLight());
    }

    private IEnumerator FireLight()
    {
        _flickering = true;
        while (true)
        {
            _light.intensity = Mathf.Lerp(
                _light.intensity, Random.Range(
                    _baseIntensity - _maxReduction,
                    _baseIntensity + _maxIncrease),
                    _strength * Time.deltaTime);
            yield return new WaitForSeconds(_rateDamping);
        }

        yield return null;
    }
}
