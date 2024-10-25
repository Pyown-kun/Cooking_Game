using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedConuterChangedEventArgs> OnSelectedConuterChanged;
    public class OnSelectedConuterChangedEventArgs : EventArgs
    {
        public ClearCounter SelectedCounter;
    }

    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _rotationSpeed = 150f;
    [SerializeField]
    private GameInput _gameInput;
    [SerializeField] private Transform _KitchenObjectHoldPoint;
    private KitchenObject _kitchenObject;


    private bool isWalking;
    private CharacterController _characterController;
    private Vector3 _velocity;
    private Vector3 _moveDir;
    private Vector3 _lastDirection;
    private ClearCounter _selectedCounter;

    public void Awake()
    {
        if (Instance = null)
        {
            Debug.LogError("there is more tahn one player instance");
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();

        _gameInput.OnInterevtAction += _gameInput_OnInterevtAction;
    }

    private void _gameInput_OnInterevtAction(object sender, EventArgs e)
    {
        if (_selectedCounter != null)
        {
            _selectedCounter.Interact(this);
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
                if (_clearCounter != _selectedCounter)
                {
                    SetSelectedCounter(_clearCounter);


                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);

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

    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        this._selectedCounter = selectedCounter;

        OnSelectedConuterChanged?.Invoke(this, new OnSelectedConuterChangedEventArgs
        {
            SelectedCounter = _selectedCounter
        });


    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return _KitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject _kitchenObject)
    {
        this._kitchenObject = _kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }
}
