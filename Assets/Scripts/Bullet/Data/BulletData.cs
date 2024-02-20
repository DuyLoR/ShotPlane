using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Data/New Bullet Data")]
public class BulletData : ScriptableObject
{
    public GameObject bulletPrefab;
    public int damage = 1;
    public float rechargeTime = 0.1f;
    public float activeTime = 2f;
    public float bulletSpeed = 4f;
}
