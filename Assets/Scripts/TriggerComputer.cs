using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerComputer : MonoBehaviour
{
    [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] GameObject _cameraPOV;
    [SerializeField] CameraManager _cameraManager;
    [SerializeField] FirstPersonController _playerMovement;
    private bool _isOnPC = false;
    private bool _inputCaught = false;
    private bool _inTriggerComputer = false;
    [SerializeField] private StarterAssetsInputs _input;

    private void OnTriggerEnter(Collider other)
    {
        _inTriggerComputer = true;
    }
    private void OnTriggerExit(Collider other)
    {
        _inTriggerComputer = false;
        _cameraManager.SetNormalCameraOn();
    }

    private void OnTriggerStay(Collider other)
    {
        float distanceToTarget = Vector3.Distance(transform.position, _cameraPOV.transform.position);

        Debug.DrawRay(_cameraPOV.transform.position, _cameraPOV.transform.forward * distanceToTarget, Color.red);
        if (_inTriggerComputer && Physics.Raycast(_cameraPOV.transform.position, _cameraPOV.transform.forward, distanceToTarget, playerInteraction.ComputerMask) &&
            !Physics.Raycast(_cameraPOV.transform.position, _cameraPOV.transform.forward, distanceToTarget, playerInteraction.ObstacleMask))
        {

            if (!_isOnPC)
                playerInteraction.SetTextInteractActive(true);
            if (_inputCaught != _input.interact)
            {
                if (!_isOnPC)
                {
                    _playerMovement.CanMove(false);
                    _cameraManager.SetComputerCameraOn();
                    playerInteraction.SetTextInteractActive(false);
                    _isOnPC = !_isOnPC;
                } else {
                    _playerMovement.CanMove(true);
                    _cameraManager.SetNormalCameraOn();
                    _isOnPC = !_isOnPC;
                }
                }
                _inputCaught = _input.interact;
            }
        }
}
