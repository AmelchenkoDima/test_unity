using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   
    [HideInInspector]public float _moveX;
    public bool _isJamp;

    private Rigidbody2D _rigidbody;
    private Animator _animator;

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _jampHeght = 2f;

    [SerializeField] private int _extraJump = 2;

    private float _groundHeight = 1f;

    private bool _isEnemy;
    private bool _isGrounded;
    private bool _facingRight = true;
    private int _remainingJump;

    #region Animation
    public static int _animMoveId = Animator.StringToHash("move");
    #endregion

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _layerMask = LayerMask.GetMask("Ground");
        _remainingJump = _extraJump;
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        Movement();

    }

    private void Movement()
    {
        _rigidbody.velocity = new Vector2(_moveX * _moveSpeed, _rigidbody.velocity.y);

        if(_moveX > 0 && !_facingRight || _moveX < 0 && _facingRight) 
        {
            Flip();
        }

       
        if (_moveX != 0)
        {
             var moduleHorizontalMovement = Mathf.Abs(_moveX); 
             _animator.SetFloat(_animMoveId, moduleHorizontalMovement);
        }
        else
        {
            _animator.SetFloat(_animMoveId, 0);
        }      
    }

    private void Jamp()
    {
        if(_isJamp ) 
        { 
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jampHeght);
            _remainingJump--;
        }
    }


    private void Flip()
    {
        _facingRight = !_facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;   
    }


    private void GroundCheck()
    {
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, _groundHeight, _layerMask);
        if (_isGrounded)
        {
            _remainingJump = _extraJump;
        }

        if (_isGrounded || _remainingJump > 0)
        {
            Jamp();
        }
    }
}
