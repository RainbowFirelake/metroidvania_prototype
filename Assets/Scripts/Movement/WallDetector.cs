using UnityEngine;
using System;

public class WallDetector : MonoBehaviour
{
    public event Action OnWallEnter;
    public event Action OnWallExit;

    void OnTriggerEnter2D(Collider2D other)
    {
        OnWallEnter?.Invoke();
    }    

    void OnTriggerExit2D(Collider2D other)
    {
        OnWallExit?.Invoke();
    }
}
