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
        _cameraPOVPlayer.enabled = true;
        _cameraEnemyScreamer.enabled = false;
    }

    public void SetCameraScreamerOn()
    {
        _cameraPOVPlayer.enabled = false;
        _cameraEnemyScreamer.enabled = true;
    }
}
