using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IControllable
{
    public enum States
    {
        Idle,
        Move,
        Jump
    }

    [Header("Base")]
    [SerializeField] private bool isGrounded;
    [SerializeField] private States _states;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private PlayerGravitation _gravitation;

    private Dictionary<string, IState> _statesMap;
    private IState _currentState;
    private Transform _transform;

    public States EnumStates { set { _states = value; } }
    public Transform Transform => _transform;
    public Transform CameraTransform => _cameraTransform;

    [Header("Move")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _rotationSpeed = 15f;
    public CharacterController CharacterController => _characterController;
    public Vector3 MoveDirection { get; set; }
    public float RotationSpeed => _rotationSpeed;
    public float MoveSpeed => _moveSpeed;   

    [Header("Jump")]
    [SerializeField] private float _jumpForce = 15f;
    public float JumpForce => _jumpForce;

    private void OnValidate()
    {
        _cameraTransform = Camera.main.transform;
        _gravitation = GetComponent<PlayerGravitation>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Awake()
    {
        InitStates();
    }
    private void Start()
    {
        _transform = transform;
        SwitchState(typeof(StateIdle).Name);
    }

    private void InitStates()
    {
        _statesMap = new Dictionary<string, IState>();

        _statesMap[typeof(StateIdle).Name] = new StateIdle(this);
        _statesMap[typeof(StateMove).Name] = new StateMove(this);
        _statesMap[typeof(StateJump).Name] = new StateJump(this, _gravitation);
    }

    public void SwitchState(string state)
    {
        if(_currentState != _statesMap[state])
        {
            if(_currentState != null)
                _currentState.Exit();
            _currentState = _statesMap[state];
            _currentState.Enter();
        }
    }
    public void BaseUpdate()
    {
        _currentState.Update();
        _gravitation.Gravitation();
        isGrounded = _characterController.isGrounded;
    }

    public void Jump()
    {
        if (_characterController.isGrounded)
            SwitchState(typeof(StateJump).Name);
    }

    public void Move(Vector3 direction)
    {
        MoveDirection = direction;
        if (direction == Vector3.zero)
        {
            SwitchState(typeof(StateIdle).Name);
            return;
        }

        SwitchState(typeof(StateMove).Name);
    }
}
