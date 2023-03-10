using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : State
{
    [SerializeField]
    protected MovementData movementData;
    public State IdleState;

    public float acceleration, deacceleration, maxSpeed;


    private void Awake()
    {
        movementData = GetComponentInParent<MovementData>();

    }

    protected override void EnterState()
    {
        agent.animationManager.PlayAnimation(AnimationType.run);

        movementData.horizontalMovementDirection = 0;
        movementData.currentSpeed = 0;
        movementData.currentVelocity = Vector2.zero;


    }

    public override void StateUpdate()
    {
        base.StateUpdate();
        CalculateVelocity();
        SetPlayerVelocity();

        if (Mathf.Abs(agent.rb2d.velocity.x)<0.01f)
        {
            agent.TransitionToState(IdleState);

        }
    }

    private void SetPlayerVelocity()
    {
        agent.rb2d.velocity = movementData.currentVelocity;
    }

    private void CalculateVelocity()
    {
        CalculateSpeed(agent.agentInput.MovementVector,movementData);
        CalculateHorizontalDirection(movementData);
        movementData.currentVelocity = Vector3.right * movementData.horizontalMovementDirection*movementData.currentSpeed;
        movementData.currentVelocity.y = agent.rb2d.velocity.y;

    }

    private void CalculateHorizontalDirection(MovementData movementData)
    {
        if (agent.agentInput.MovementVector.x>0) {
            movementData.horizontalMovementDirection = 1;
        }
        else if(agent.agentInput.MovementVector.x < 0)
        {
            movementData.horizontalMovementDirection = -1;
        }
    }

    private void CalculateSpeed(Vector2 movementVector, MovementData movementData)
    {

        if (MathF.Abs(movementVector.x) > 0)
        {
            movementData.currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            movementData.currentSpeed -= acceleration * Time.deltaTime;
        }
        movementData.currentSpeed = Math.Clamp(movementData.currentSpeed, 0, maxSpeed);


    }
}
