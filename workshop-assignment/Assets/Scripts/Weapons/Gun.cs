using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Gun : MonoBehaviour, IWeapon
{
    [Header("Weapon Specs")]

    [SerializeField] private WeaponType weaponType;

    [SerializeField] private int maxBulletsInMag = 30;
    [SerializeField] private int reloadTimeInSeconds = 0;
    [SerializeField] private bool canReload;

    [SerializeField] private float damage;
    [SerializeField] private float fireRatePerSecond;

    [SerializeField] BulletFactory bulletFactory;
    [SerializeField] private BulletTypes bulletType;


    private int currentBulletsInMag;
    private bool canShoot; 
    private int waitBetweenBulletsMilli = 0;

    public static event Action<int,int> onBulletUpdate;
    public WeaponType WeaponType
    {
        get { return weaponType; }
        private set { weaponType = value; }
    }

    private void Awake()
    {
        currentBulletsInMag = maxBulletsInMag;
    }

    private void OnEnable()
    {
        if(currentBulletsInMag == 0)
        {
            if(canReload)
            {
                Reload();
            }
            else
            {
                onBulletUpdate?.Invoke(currentBulletsInMag, maxBulletsInMag);
                return;
            }
        }
        else
        {
            canShoot = true;
            onBulletUpdate?.Invoke(currentBulletsInMag,maxBulletsInMag);
        }
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
            currentBulletsInMag--;
            onBulletUpdate?.Invoke(currentBulletsInMag, maxBulletsInMag);

            if(currentBulletsInMag == 0)
            {
                Reload();
                return;
            }

            WaitForFireRate();
        }
    }

    private async void WaitForFireRate()
    {
        canShoot = false;
        await Task.Delay(waitBetweenBulletsMilli);
        canShoot = true;
    }

    private async void Reload()
    {
        canShoot = false;

        if(!canReload)
        {
            return;
        }
        Debug.Log("Reloading");

        await Task.Delay(reloadTimeInSeconds * 1000);
        canShoot = true;
        currentBulletsInMag = maxBulletsInMag;

        onBulletUpdate?.Invoke(currentBulletsInMag,maxBulletsInMag);
    }


}
