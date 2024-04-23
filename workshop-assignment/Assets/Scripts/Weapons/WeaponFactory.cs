
using UnityEngine;

public enum WeaponType
{
    AK47,
    M4,
    Famas,
    GrenadeLauncher,
    RocketLauncher
}

public abstract class WeaponFactory : MonoBehaviour
{
    public abstract IWeapon CreateWeapon(WeaponType weaponType);
}
