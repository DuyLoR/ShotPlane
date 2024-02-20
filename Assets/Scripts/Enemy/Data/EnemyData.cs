using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/New Enemy Data")]
public class EnemyData : ScriptableObject
{
    public float movementSpeed = 4f;
    public int maxHp = 4;
}
