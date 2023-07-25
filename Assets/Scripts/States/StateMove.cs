using UnityEngine;

public class StateMove : IState
{
    private PlayerController _playerController;
    private Vector3 movementDirection;
    public StateMove(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public void Enter()
    {
        _playerController.EnumStates = PlayerController.States.Move;
    }
    public void Update()
    {
        Move();
    }
    private void Move()
    {
        GetLocalMovementDirection();
        RotatePlayer();
        _playerController.CharacterController.Move(movementDirection * Time.deltaTime);
    }

    private void GetLocalMovementDirection() 
    {
        movementDirection = Vector3.ClampMagnitude(_playerController.MoveDirection * _playerController.MoveSpeed, _playerController.MoveSpeed);

        Quaternion tempCameraRotation = _playerController.CameraTransform.rotation;
        _playerController.CameraTransform.rotation = Quaternion.Euler(0f, _playerController.CameraTransform.eulerAngles.y, 0f);
        movementDirection = _playerController.CameraTransform.TransformDirection(movementDirection);
        _playerController.CameraTransform.rotation = tempCameraRotation;
    }

    private void RotatePlayer()
    {
        _playerController.Transform.rotation = Quaternion.Slerp(_playerController.Transform.rotation,
            Quaternion.LookRotation(movementDirection), _playerController.RotationSpeed * Time.deltaTime);
    }

    public void Exit()
    {

    }
}
