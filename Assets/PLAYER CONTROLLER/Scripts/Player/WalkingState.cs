using UnityEngine;

public class WalkingState : MoveState
{
    public WalkingState(ControllerPlayer controllerPlayer) 
        : base(controllerPlayer, controllerPlayer.speedMoviment, "WalkingState") { }


}


