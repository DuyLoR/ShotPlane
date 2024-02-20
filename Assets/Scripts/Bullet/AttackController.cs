using UnityEngine;

public class AttackController : MonoBehaviour
{
    public BulletData bulletData;
    private Transform bulletSpawnPoint;
    private float startTime;
    private bool isAttack;
    private void Start()
    {
        startTime = Time.time;
    }
    private void Update()
    {
        if (Time.time >= startTime + bulletData.rechargeTime && isAttack)
        {
            startTime = Time.time;
            isAttack = false;
            FireBullet();
        }
    }

    private void FireBullet()
    {
    }

    public void SetAttackInput(bool AttackInput)
    {
        isAttack = AttackInput;
    }
}
