using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "Data/ New Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Bullet")]
    public BulletData bulletData;
}
