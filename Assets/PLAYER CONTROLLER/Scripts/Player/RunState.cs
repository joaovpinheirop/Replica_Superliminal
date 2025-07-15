

using UnityEngine;

public class RunState : MoveState
{
 public RunState(ControllerPlayer controllerPlayer) 
 : base(controllerPlayer, controllerPlayer.speedMoviment * 3, "RunState"){}


}
