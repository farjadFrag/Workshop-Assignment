using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


public enum BulletTypes
{
    normal,
    Rocket,
    Grenade
}

[CreateAssetMenu(fileName ="BulletFactory", menuName ="Bullet Factory")]
public class BulletFactory : ScriptableObject
{
    [SerializeField] List<Bullet> bullets;

    Dictionary<BulletTypes, IBullet> bulletsDictionary = new Dictionary<BulletTypes, IBullet>();

    private void OnEnable()
    {
        bulletsDictionary = bullets.ToDictionary(x => x.BulletTypes, y => y as IBullet);
    }

    public  IBullet CreateBullet(BulletTypes bulletType)
    {
        var bullet = Instantiate(bulletsDictionary[bulletType] as Bullet);
        return bullet;
    }
}
