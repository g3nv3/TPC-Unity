using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private UnityEvent<Vector3> OnMoveInput = new UnityEvent<Vector3>();
    [SerializeField] private UnityEvent OnJumpInput = new UnityEvent();
    private IControllable _controllable;

    [SerializeField] private KeyCode _keyJump = KeyCode.Space;
    private void Start()
    {
        _controllable = GetComponent<IControllable>();
    }
    private void Update()
    { 
        _controllable.BaseUpdate();
        ReadMoveInput();
        ReadJumpInput();
    }

    private void ReadMoveInput()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        OnMoveInput.Invoke(new Vector3(moveX, 0f, moveZ));
    }

    private void ReadJumpInput()
    {
        if (Input.GetKeyDown(_keyJump))
            OnJumpInput.Invoke();
    }
}
