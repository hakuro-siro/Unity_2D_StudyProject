using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public PlayerInput PlayerInput;

    private void Awake()
    {
        PlayerInput = GetComponentInParent<PlayerInput>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        PlayerInput.OnMovement += HandleMovement;
    }

    private void HandleMovement(Vector2 input)
    {
        if (Math.Abs(input.x) > 0)
        {
            rb2d.velocity = new Vector2(input.x * 5, rb2d.velocity.y);
            
        }
        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
            
    }
}
