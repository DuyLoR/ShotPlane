using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerInputHandle inputHandle { get; private set; }
    public AttackController attackController { get; private set; }


    public Rigidbody2D rb { get; private set; }
    public Transform bulletSpawnPoint;

    [SerializeField]
    private PlayerData playerData;
    private Vector2 boundary;
    private float startTime;

    private bool isAttack;
    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, playerData);
        moveState = new PlayerMoveState(this, stateMachine, playerData);
    }
    void Start()
    {
        inputHandle = GetComponent<PlayerInputHandle>();
        rb = GetComponent<Rigidbody2D>();
        attackController = GetComponentInChildren<AttackController>();


        stateMachine.Initialize(idleState);
        startTime = Time.time;
        boundary = new Vector2(Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize);
    }

    void Update()
    {
        stateMachine.currentState.LogicUpdate();
        isAttack = inputHandle.normalAttackInput;
        if (!BulletSpawnPool.instance.CheckIfBulletInPool(playerData.bulletData.bulletPrefab))
        {
            BulletSpawnPool.instance.AddBulletPrefabToPool(playerData.bulletData.bulletPrefab);
        }
        if (isAttack && Time.time >= startTime + playerData.bulletData.rechargeTime)
        {
            startTime = Time.time;
            FireBullet();
        }
    }

    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }
    private void FireBullet()
    {
        GameObject bullet = BulletSpawnPool.instance.GetFromPool(playerData.bulletData.bulletPrefab);
        bullet.transform.position = bulletSpawnPoint.position;
        bullet.GetComponent<Bullet>().SetBulletData(playerData.bulletData);
        bullet.GetComponent<Bullet>().SetVelocity(Vector2.up * playerData.bulletData.bulletSpeed);
    }
    public void SetVelocity(Vector2 velocity)
    {
        Vector2 currentPos = transform.position;
        bool isInsideCameraBounds = currentPos.x >= -boundary.x && currentPos.x <= boundary.x
            && currentPos.y >= -boundary.y && currentPos.y <= boundary.y;
        if (isInsideCameraBounds)
        {
            rb.velocity = velocity;

        }
        else
        {
            float clampedX = Mathf.Clamp(currentPos.x, -boundary.x, boundary.x);
            float clampedY = Mathf.Clamp(currentPos.y, -boundary.y, boundary.y);
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
            rb.velocity = Vector2.zero;
        }
    }

}
