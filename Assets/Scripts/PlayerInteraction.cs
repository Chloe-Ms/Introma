using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] LayerMask doorMask;
    public LayerMask DoorMask
    {
        get { return doorMask; }
    }
    [SerializeField] LayerMask keyMask;
    public LayerMask KeyMask
    {
        get { return keyMask; }
    }

    [SerializeField] LayerMask obstacleMask;
    public LayerMask ObstacleMask
    {
        get { return obstacleMask; }
    }
    [SerializeField] GameObject _textInteract;
    private bool _isKeyPicked = false;
    public bool IsKeyPicked { 
        get { return _isKeyPicked; }
        set { _isKeyPicked = value; }
    }
    [SerializeField] PlayerInput _playerInput;
    
    public bool InteractButtonIsPressed()
    {
        return _playerInput.actions["Interact"].IsPressed();
    }

    public void SetTextInteractActive(bool isActive)
    {
        _textInteract.SetActive(isActive);
    }
}
