using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyData enemyData;
    private int maxHp;
    private int currentHp;
    private Vector2 targetPosition;
    private bool isSetTargetPosition;

    private void Awake()
    {
        maxHp = enemyData.maxHp;
        currentHp = maxHp;
    }
    private void Update()
    {
        if (isSetTargetPosition && (Vector2)transform.position != targetPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, enemyData.movementSpeed * Time.deltaTime);
        }
    }
    public void SetTargetPosition(Vector2 targetPosition)
    {
        this.targetPosition = targetPosition;
        isSetTargetPosition = true;
    }

    public void Damage(int damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        Score.Instance.UpdateScore();
    }
}
