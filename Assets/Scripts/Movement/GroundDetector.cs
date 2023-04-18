using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public event Action OnGroundEnter;
    public event Action OnGroundExit;

    // temporary solution
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            OnGroundEnter?.Invoke();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            OnGroundExit?.Invoke();
        }
    }
}
