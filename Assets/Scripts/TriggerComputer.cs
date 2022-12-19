using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

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

    [SerializeField] private GameObject screen;
    [SerializeField] GameObject _pointer;

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
                    Cursor.visible = true;
                    screen.SetActive(true);
                    _pointer.SetActive(false);
                    Cursor.lockState = CursorLockMode.None;
                } else {
                    _playerMovement.CanMove(true);
                    _cameraManager.SetNormalCameraOn();
                    _isOnPC = !_isOnPC;
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    screen.SetActive(false);
                    _pointer.SetActive(true);
                }
                }
                _inputCaught = _input.interact;
            }
        }
    public void GetOffComputer()
    {
        if (_isOnPC)
        {
            _playerMovement.CanMove(true);
            _cameraManager.SetNormalCameraOn();
            _isOnPC = !_isOnPC;
            screen.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            _pointer.SetActive(true);
        }
    }
}

