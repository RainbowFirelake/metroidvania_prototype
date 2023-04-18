using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyAndEnemySystem : MonoBehaviour
{
    [SerializeField] private CharacterSide _characterSide;
    public CharacterSide characterSide { get { return _characterSide; } }
}
