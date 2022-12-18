using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class TriggerKey : MonoBehaviour
{
    [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] GameObject _cameraPOV;


    private bool _inTriggerKey = false;

    private void OnTriggerEnter(Collider other)
    {
        _inTriggerKey = true;
    }
    private void OnTriggerExit(Collider other)
    {
        _inTriggerKey = false;
    }

    private void Update()
    {
        //_playerInput.actions["Interact"].IsPressed() //In interact
        Debug.DrawRay(_cameraPOV.transform.position, _cameraPOV.transform.forward * 10, Color.red);

        float distanceToTarget = Vector3.Distance(transform.position, _cameraPOV.transform.position);
        if (_inTriggerKey && Physics.Raycast(_cameraPOV.transform.position, _cameraPOV.transform.forward, distanceToTarget, playerInteraction.KeyMask) &&
            !Physics.Raycast(_cameraPOV.transform.position, _cameraPOV.transform.forward, distanceToTarget, playerInteraction.ObstacleMask))
        {
            playerInteraction.SetTextInteractActive(true);
            if (playerInteraction.InteractButtonIsPressed())
            {
                playerInteraction.IsKeyPicked = true;
                Destroy(transform.parent.gameObject);
            }
        }
        else
        {
            playerInteraction.SetTextInteractActive(false);
        }
    }
}
