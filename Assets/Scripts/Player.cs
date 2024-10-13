using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float _speed = 5f;

    [SerializeField]
    private float _rotationSpeed = 150f;

    [SerializeField]
    private GameInput _gameInput;

    private Vector3 _velocity;

    private Vector3 _moveDir;

    private Vector3 _lastDirection;

    private bool isWalking;

    private CharacterController _characterController;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();

        _gameInput.OnInterevtAction += _gameInput_OnInterevtAction;
    }

    private void _gameInput_OnInterevtAction(object sender, EventArgs e)
    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormalize();

        _moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        float magnitudo = Mathf.Clamp01(_moveDir.magnitude) * _speed;
        _moveDir.Normalize();

        _velocity = _moveDir * magnitudo;

        if (_moveDir != Vector3.zero)
        {
            _lastDirection = _moveDir;
        }

        float interactionDistance = 2f;
        RaycastHit _rayCastHit;

        if (Physics.Raycast(transform.position, _lastDirection, out _rayCastHit, interactionDistance))
        {
            if (_rayCastHit.transform.TryGetComponent(out ClearCounter _clearCounter))
            {
                _clearCounter.Interact();
            }
        }
    }

    void Update()
    {
        HanddleMovements();
        HanddleInteraction();
    }


    public bool IsWalking()
    {
        return isWalking;
    }

    private void HanddleInteraction()
    {
        Vector2 inputVector = _gameInput.GetMovementVectorNormalize();

        _moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        float magnitudo = Mathf.Clamp01(_moveDir.magnitude) * _speed;
        _moveDir.Normalize();

        _velocity = _moveDir  * magnitudo;

        if (_moveDir != Vector3.zero)
        {
            _lastDirection = _moveDir;
        }

        float interactionDistance = 2f;
        RaycastHit _rayCastHit;

        if (Physics.Raycast(transform.position, _lastDirection, out _rayCastHit, interactionDistance))
        {
            if (_rayCastHit.transform.TryGetComponent(out ClearCounter _clearCounter))
            {
               // _clearCounter.Interact();
            }
        }
    }

    private void HanddleMovements()
    {

        _characterController.Move(_velocity * _speed * Time.deltaTime);

        isWalking = _velocity != Vector3.zero;

        if (_moveDir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(_moveDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}
