using UnityEngine;

public interface IWeapon
{
    public void Rotate(Vector2 input);
    public void Shoot(bool isShooting);
}
