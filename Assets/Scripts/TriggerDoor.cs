using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] GameObject _cameraPOV;
    private bool _isDoorOpen = false;

    private bool _inTriggerDoor = false;

    private void OnTriggerEnter(Collider other)
    {
        _inTriggerDoor = true;
    }
    private void OnTriggerExit(Collider other)
    {
        _inTriggerDoor = false;
        playerInteraction.SetTextInteractActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        float distanceToTarget = Vector3.Distance(transform.position, _cameraPOV.transform.position);
        if (_inTriggerDoor && Physics.Raycast(_cameraPOV.transform.position, _cameraPOV.transform.forward, distanceToTarget, playerInteraction.DoorMask) &&
            !Physics.Raycast(_cameraPOV.transform.position, _cameraPOV.transform.forward, distanceToTarget, playerInteraction.ObstacleMask) && !_isDoorOpen)
        {
            if (!playerInteraction.GetTextNeedKeyActive())
            {
                playerInteraction.SetTextInteractActive(true);
            }
            if (playerInteraction.InteractButtonIsPressed()) //If the player press the f key
            {
                playerInteraction.SetTextInteractActive(false);
                if (playerInteraction.IsKeyPicked)
                {
                    _isDoorOpen = true;
                    //START OPEN DOOR
                } else
                {
                    StartCoroutine(DisplayTextNeedKey());
                }
                
            }
        }
        else
        {
            playerInteraction.SetTextInteractActive(false);
        }
    }

    private IEnumerator DisplayTextNeedKey()
    {
        playerInteraction.SetTextNeedKeyActive(true);
        yield return new WaitForSeconds(3f);
        playerInteraction.SetTextNeedKeyActive(false);
    }
}
