using UnityEngine;

public class PlayerGravitation : MonoBehaviour
{
    [SerializeField] private bool _isGravitation = true;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float _gravityForce = -9.8f;
    [SerializeField] private float _playerMass;
    [SerializeField] private float _currentFallingSpeed;
    public float CurrentFallingSpeed { set { _currentFallingSpeed = value; }  }

    public void Gravitation()
    {
        if (_isGravitation)
        {
            _playerController.CharacterController.Move(new Vector3(0f, _currentFallingSpeed * Time.deltaTime, 0f));
            CalculateFallingSpeed();
        }
    }

    private void CalculateFallingSpeed()
    {
        if (_playerController.CharacterController.isGrounded)
        {
            _currentFallingSpeed = _gravityForce;
            return;
        }

        _currentFallingSpeed += _gravityForce * _playerMass * Time.deltaTime;
    }
}
