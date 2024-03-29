using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    public BulletData bulletData;

    private float bulletActiveTimer = 0;
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletActiveTimer = Time.time;
    }

    private void Update()
    {
        if (Time.time >= bulletActiveTimer + bulletData.activeTime)
        {
            bulletActiveTimer = Time.time;
            BulletSpawnPool.instance.AddToPool(gameObject);
        }
    }
    public void SetVelocity(Vector2 velocity)
    {
        rb.velocity = (velocity).normalized * bulletData.bulletSpeed;
    }
    public void SetBulletData(BulletData bulletData)
    {
        this.bulletData = bulletData;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            if (collision.CompareTag("Enemy"))
            {
                damageable.Damage(bulletData.damage);
                BulletSpawnPool.instance.AddToPool(gameObject);
            }
        }
    }
}
