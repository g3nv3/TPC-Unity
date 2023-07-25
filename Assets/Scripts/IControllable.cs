
using UnityEngine;

public interface IControllable
{
    public void BaseUpdate();
    public void Move(Vector3 direction);
    public void Jump();
}
