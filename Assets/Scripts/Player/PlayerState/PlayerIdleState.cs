using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData) : base(player, stateMachine, playerData)
    {
    }

    public override void Docheck()
    {
        base.Docheck();
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(Vector2.zero);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (rawMovement != Vector2.zero)
        {
            player.stateMachine.ChangeState(player.moveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
