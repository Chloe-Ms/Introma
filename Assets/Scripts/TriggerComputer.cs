using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerComputer : MonoBehaviour
{
    [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] GameObject _cameraPOV;
    [SerializeField] CameraManager _cameraManager;

    private bool _inTriggerComputer = false;

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
        //_playerInput.actions["Interact"].IsPressed() //In interact
        Debug.DrawRay(_cameraPOV.transform.position, _cameraPOV.transform.forward * 10, Color.red);

        float distanceToTarget = Vector3.Distance(transform.position, _cameraPOV.transform.position);

        Debug.Log(Physics.Raycast(_cameraPOV.transform.position, _cameraPOV.transform.forward, distanceToTarget, playerInteraction.ComputerMask));
        Debug.Log("OBS" + Physics.Raycast(_cameraPOV.transform.position, _cameraPOV.transform.forward, distanceToTarget, playerInteraction.ObstacleMask));
        if (_inTriggerComputer && Physics.Raycast(_cameraPOV.transform.position, _cameraPOV.transform.forward, distanceToTarget, playerInteraction.ComputerMask) &&
            !Physics.Raycast(_cameraPOV.transform.position, _cameraPOV.transform.forward, distanceToTarget, playerInteraction.ObstacleMask))
        {
            playerInteraction.SetTextInteractActive(true);
            if (playerInteraction.InteractButtonIsPressed())
            {
                _cameraManager.SetComputerCameraOn();
                playerInteraction.SetTextInteractActive(false);
            }
        }
        else
        {
            playerInteraction.SetTextInteractActive(false);
        }
    }
}
