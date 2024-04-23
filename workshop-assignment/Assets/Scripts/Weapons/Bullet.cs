using System.Collections;
using System.Threading.Tasks;
using UnityEngine;


public class Bullet : MonoBehaviour, IBullet
{
    [SerializeField] private BulletTypes bulletType;

    [SerializeField] private float bulletSpeed;

    [SerializeField] Rigidbody rigidBody;
    public BulletTypes BulletTypes
    {
        get { return bulletType; }
        private set { bulletType = value; }
    }

    public void Shoot(Vector3 direction)
    {
        rigidBody.AddForce(direction * bulletSpeed);
        DestroyBullet();
    }

    private async void DestroyBullet()
    {
        await Task.Delay(3 * 1000);
        Destroy(gameObject);
    }
}
