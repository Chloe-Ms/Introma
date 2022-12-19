using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cameraPOVPlayer;
    [SerializeField] private CinemachineVirtualCamera _cameraComputer;
    [SerializeField] private CinemachineVirtualCamera _cameraEnemyScreamer;

    // Start is called before the first frame update
    void Start()
    {
        SetNormalCameraOn();
    }

    public void SetCameraScreamerOn()
    {
        _cameraPOVPlayer.Priority = 1;
        if (_cameraComputer != null)
            _cameraComputer.Priority = 1;
        if (_cameraEnemyScreamer != null)
            _cameraEnemyScreamer.Priority = 10;
    }
    public void SetNormalCameraOn()
    {
        _cameraPOVPlayer.Priority = 10;
        if (_cameraEnemyScreamer != null)
            _cameraEnemyScreamer.Priority = 1;
        if (_cameraComputer != null)
            _cameraComputer.Priority = 1;
    }

    public void SetComputerCameraOn()
    {
        _cameraPOVPlayer.Priority = 1;
        if (_cameraEnemyScreamer != null)
            _cameraEnemyScreamer.Priority = 1;
        if (_cameraComputer != null)
            _cameraComputer.Priority = 10;
    }
}
