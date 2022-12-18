using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cameraPOVPlayer;
    [SerializeField] private CinemachineVirtualCamera _cameraEnemyScreamer;

    // Start is called before the first frame update
    void Start()
    {
        _cameraPOVPlayer.Priority = 10;
        _cameraEnemyScreamer.Priority = 1;
    }

    public void SetCameraScreamerOn()
    {
        _cameraPOVPlayer.Priority = 1;
        _cameraEnemyScreamer.Priority = 10;
    }
}
