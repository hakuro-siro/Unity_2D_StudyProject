using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public State MoveState;

    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.idle);

    }

    protected override void HandleMovement(Vector2 input)
    {
        if (Math.Abs(input.x) > 0)
        {
            agent.TransitionToState(MoveState);

        }
    }



}
