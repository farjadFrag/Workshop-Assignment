using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GunFactory : WeaponFactory
{
    [SerializeField] List<Gun> guns;

    Dictionary<WeaponType, IWeapon> gunsDictionary;

    private void Awake()
    {
        gunsDictionary = guns.ToDictionary(x => x.WeaponType, y => y as IWeapon);
    }

    public override IWeapon CreateWeapon(WeaponType gunType)
    {
        var weapon = Instantiate(gunsDictionary[gunType] as Gun);
        return weapon;
    }
}
