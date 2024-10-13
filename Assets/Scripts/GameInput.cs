using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInterevtAction;

    private PlayerInputManager _playerInputManager;
    private void Awake()
    {
        _playerInputManager = new PlayerInputManager();
        _playerInputManager.Player.Enable();

        _playerInputManager.Player.Interect.performed += Interect_performed;
    }

    private void Interect_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //Two Same Things

        //    if (OnInterevtAction != null) 
        //    { 
        //        OnInterevtAction(this, EventArgs.Empty);
        //    }

            OnInterevtAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalize()
    {
        Vector2 inputVector = _playerInputManager.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
