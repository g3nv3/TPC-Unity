


public class StateJump : IState
{
    private PlayerController _playerController;
    private PlayerGravitation _playerGravitation;
    public StateJump(PlayerController playerController, PlayerGravitation playerGravitation)
    {
        _playerController = playerController;
        _playerGravitation = playerGravitation;
    }
    public void Enter()
    {
        _playerController.EnumStates = PlayerController.States.Jump;

        _playerGravitation.CurrentFallingSpeed = _playerController.JumpForce;
    }
    public void Update()
    {

    }
    public void Exit()
    {

    }
}
