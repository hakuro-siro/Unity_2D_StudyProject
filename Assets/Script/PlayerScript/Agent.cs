using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public PlayerInput agentInput;
    public AgentAnimation animationManager;
    public AgentRender agentRender;

    public State currentState = null, previousState = null;
    public State IdleState;

    [Header("State  Debugging")]
    public string stateName= "";
    


    private void Awake()
    {
        agentInput = GetComponentInParent<PlayerInput>();
        rb2d = GetComponent<Rigidbody2D>();
        animationManager = GetComponentInChildren<AgentAnimation>();
        agentRender = GetComponentInChildren<AgentRender>();
        State[] states = GetComponentsInChildren<State>();
        foreach(var state in states)
        {
            state.InitalizeState(this);
        }
    }

    private void Start()
    {
        agentInput.OnMovement += agentRender.FaceDirection;
        TransitionToState(IdleState);
    }


    internal void TransitionToState(State desiredState)
    {
        if (desiredState == null)
            return;
        if(currentState != null)
        {
            currentState.Exit();
        }

        previousState = currentState;
        currentState = desiredState;
        currentState.Enter();

        displayState();
    }

    private void displayState()
    {
        if (previousState == null || previousState.GetType() != currentState.GetType())
        {
            stateName = currentState.GetType().ToString();
        }
    }


    private void Update()
    {
        currentState.StateUpdate();
    }
    private void FixedUpdate()
    {
        currentState.StateFixedUpdate();
    }

}

