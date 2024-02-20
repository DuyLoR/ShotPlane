using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected float startTime;
    protected Vector2 rawMovement;
    protected bool normalAttack;
    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        Docheck();
    }
    public virtual void Exit()
    {

    }
    public virtual void LogicUpdate()
    {
        rawMovement = player.inputHandle.rawMovementInput;
        normalAttack = player.inputHandle.normalAttackInput;
        if (rawMovement != Vector2.zero)
        {
            player.stateMachine.ChangeState(player.moveState);
        }
        else
        {
            player.stateMachine.ChangeState(player.idleState);
        }

    }
    public virtual void PhysicsUpdate()
    {
        Docheck();
    }
    public virtual void Docheck()
    {

    }
}
