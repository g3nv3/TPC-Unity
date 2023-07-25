using UnityEngine;
public class StateIdle : IState
{
    private PlayerController _playerController;
    public StateIdle(PlayerController playerController)
    {
        _playerController = playerController;
    }
    public void Enter()
    {
        _playerController.EnumStates = PlayerController.States.Idle;
    }
    public void Update()
    {
        
    }
    public void Exit()
    {

    }

}
