using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Gun : MonoBehaviour, IWeapon
{
    [Header("Weapon Specs")]

    [SerializeField] private WeaponType weaponType;

    [SerializeField] private float damage;
    [SerializeField] private float fireRatePerSecond;

    [SerializeField] BulletFactory bulletFactory;
    [SerializeField] private BulletTypes bulletType;


    private bool canShoot; 
    private int waitBetweenBulletsMilli = 0; 


    public WeaponType WeaponType
    {
        get { return weaponType; }
        private set { weaponType = value; }
    }

    private void OnEnable()
    {
        canShoot = true;
    }

    private void Start()
    {
        waitBetweenBulletsMilli = (int)(1000 / fireRatePerSecond);
    }

    public void Shoot()
    {
        if (canShoot)
        {
            bulletFactory.CreateBullet(bulletType).Shoot(transform.forward);
            Debug.Log($"SHOOT {weaponType} \nDamage : {damage}\nFire Rate : {fireRatePerSecond}");
            WaitForFireRate();
        }
    }

    private async void WaitForFireRate()
    {
        canShoot = false;
        await Task.Delay(waitBetweenBulletsMilli);
        canShoot = true;
    }


}
