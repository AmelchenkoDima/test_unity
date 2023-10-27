using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class FlyingMonsters : MonoBehaviour
{
    [SerializeField] private MonstersData _monsrerData;

    private Rigidbody2D _rigidbody;

    private bool _visiblePlayer;
    private Vector2 _lastPlayerPosition;
    private int _index;
    //private bool _facingLeft = true;
    private Vector2 _raycastDirection;

    [SerializeField] private List<float> _patrolPoint;

    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _afterDeadPrefab;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _rayDistance = 3f;

    private Transform _parrent;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _raycastDirection = Vector2.zero;

    }

    private void Update()
    {
        _visiblePlayer = Physics2D.Raycast(transform.position, _raycastDirection, _rayDistance, _layerMask);
       
    }


    private void FixedUpdate()
    {
        ChoisePatrolPoint();
        AttackPlayer();     
    }


    private void ChoisePatrolPoint()
    {
        if (!_visiblePlayer)
        {
            int targetIndex = _patrolPoint.FindIndex(x => transform.position.x == x);

            if (targetIndex != -1)
            {
                _index = targetIndex > 0 ? 0 : 1;
                UnityEngine.Debug.Log(_patrolPoint[_index]);
            }

            Patrol(_patrolPoint[_index]);

        }
        Flip(_patrolPoint[_index]);
    }

    private void Flip(float target)
    {
        if (!_visiblePlayer)
        {
            Vector3 scale = transform.localScale;

            scale.x = transform.position.x > target ? 1 : -1;

            _raycastDirection.x = -scale.x;
            transform.localScale = scale;

        }
    }

    private void Patrol(float pointCoordinates)
    {
        var position = new Vector2(pointCoordinates, transform.position.y);
        var currentPosition = _rigidbody.position;
        Vector2 newPosition = Vector2.MoveTowards(currentPosition, position, _monsrerData.SpeedMonster * Time.deltaTime);
        _rigidbody.MovePosition(newPosition);
        //UnityEngine.Debug.Log(newPosition);
    }


    private void AttackPlayer()
    {
        if (_visiblePlayer)
        {
            _lastPlayerPosition = _player.position;
            Vector2 direction = (_player.position - transform.position).normalized;
            _rigidbody.velocity = new Vector2(direction.x * _monsrerData.AttackSpeed, _rigidbody.velocity.y);

        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 collPosition = collision.transform.position;

        if (collPosition.y > transform.position.y)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                var coin = Instantiate(_afterDeadPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        if (collPosition.x < transform.position.x)
        {
            //Debug.Log("игрок слево");
        }

    }


    private void TakeDamage(int damage)
    {
        _monsrerData.Health -= damage;
        if(_monsrerData.Health <= 0)
        {
            Die();
        }
    }


    protected virtual void Die()
    {
        Destroy(gameObject);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, _raycastDirection * _rayDistance);
    }
}
