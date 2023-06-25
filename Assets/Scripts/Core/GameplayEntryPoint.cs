using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField] private NewPlayerInput _player;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera _cinCamera;
    // TODO : UI initialization
    [SerializeField] private GameObject UI;

    void Start()
    {
        _player.Initialize();
        var camera = Instantiate(_cinCamera);
        camera.Follow = _player.transform;
    }
}
