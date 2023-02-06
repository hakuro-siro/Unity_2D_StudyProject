using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using System;

/// <summary>
/// 플레이어에 대한 input의 이벤트 트리거 관리용 스크립트
/// </summary>
public class PlayerInput : MonoBehaviour
{

    /// <summary>
    /// 이하 inspector
    /// </summary>
    [field: SerializeField]
    public  Vector2 MovementVector { get; private set; }
    
    // 인수 전달이 필요하지 않은 이벤트
    public event Action OnAttack,OnJumpPressed, OnJumpReleased, OnWeaponChange;
    
    // 인수전달이 필요한 이벤트
    public event Action<Vector2> OnMovement;
    
    public KeyCode jumpKey, attackKey, menuKey;
    
    public UnityEvent OnMenuKeyPressed;

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            GetMovementInput();
            GetJumpInput();
            GetAttackInput();
            GetWeaponSwapInput();
        }
        

        GetMenuInput();
    }

    private void GetMenuInput()
    {
        if (Input.GetKeyDown(menuKey))
        {
            OnMenuKeyPressed?.Invoke();
        }
    }

    private void GetWeaponSwapInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnWeaponChange?.Invoke();
        }
    }

    private void GetAttackInput()
    {
        if (Input.GetKeyDown(attackKey))
        {
            OnAttack?.Invoke();
        }
    }

    private void GetJumpInput()
    {
        if (Input.GetKeyDown(jumpKey))
        {
            OnJumpPressed?.Invoke();
        }

        if (Input.GetKeyUp(jumpKey))
        {
            OnJumpReleased?.Invoke();
        }
    }

    private void GetMovementInput()
    {
        MovementVector = GetMovementVector();
        OnMovement?.Invoke(MovementVector);
        
    }

    protected Vector2 GetMovementVector()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
