﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpeSpeed;

    [SerializeField] private LayerCheck _groundCheck;

    private Rigidbody2D _rigidbody;
    private Vector2 _direction;
    private Animator _animator;
    private SpriteRenderer _sprite;

    private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
    private static readonly int IsRunning = Animator.StringToHash("is-running");
    private static readonly int VerticaVelocity = Animator.StringToHash("vertical-velocity");

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }
    public void SetDirection(Vector2 direction)
    {
        _direction = direction;
    }
    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_direction.x * _speed, _rigidbody.velocity.y);

        var isJumping = _direction.y > 0;
        var isGrounded = IsGrounded();
        if (isJumping)
        {
            if (IsGrounded() && _rigidbody.velocity.y <= 0)
            {
                _rigidbody.AddForce(Vector2.up * _jumpeSpeed, ForceMode2D.Impulse);
            }
            
        }
        else if (_rigidbody.velocity.y > 0)
        {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);
        }
        _animator.SetBool(IsGroundKey, isGrounded);
        _animator.SetBool(IsRunning, _direction.x != 0);
        _animator.SetFloat(VerticaVelocity, _rigidbody.velocity.y);

        UpdateSpriteDirection();

    }

    private void UpdateSpriteDirection()
    {
        if (_direction.x > 0)
        {
            _sprite.flipX = false;
        }
        else if (_direction.x < 0)
        {
            _sprite.flipX = true;
        }
    }

    private bool IsGrounded()
    {
        return _groundCheck.IsTouchingLayer;
    }

    public void SaySomething()
    {
        Debug.Log("Something");
    }
}
