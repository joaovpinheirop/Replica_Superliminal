 using UnityEngine;

public class JumpState : State
{
    public ControllerPlayer controller;

    public JumpState(ControllerPlayer controllerPlayer) : base("JumpState")
    {
        this.controller = controllerPlayer;
    }

    public override void Enter()
    {
        jump();
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
     controller.stateMachine.ChangeState(controller.idleState);
    }

    public void jump(){
         // Aplica a força de pulo apenas uma vez
        controller.rb.AddForce(Vector3.up * controller.forceJump, ForceMode.Impulse);
    }
}
