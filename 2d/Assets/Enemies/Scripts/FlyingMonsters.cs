using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMonsters : MonoBehaviour
{
    [SerializeField] private MonstersData _monsrerData;

    private Rigidbody2D _rigidbody;

    private bool _visiblePlayer;
    private Vector2 _lastPlayerPosition;
    private int _index;
    private bool _facingLeft = true;
    private Vector2 _raycastDirection;

    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _afterDeadPrefab;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _rayDistance = 3f;
    [SerializeField] private List<float> _patrolPoint;

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
            //int targetIndex = _patrolPoint.FindIndex(x => transform.position.x == x);

            //if (targetIndex != -1)
            //{
            //    _index = targetIndex > 0 ? 0 : 1;
            //    Flip();
            //}

            //Patrol(_patrolPoint[_index]);

            if (transform.position.x == _patrolPoint[0])
            {
                _monsrerData.isValid = true;
                _index = 1;
                Flip();
            }
            if (transform.position.x == _patrolPoint[1])
            {
                _monsrerData.isValid = false;
                _index = 0;
                Flip();
            }

            Patrol(_index);
        }
    }


    private void Patrol(int currentPointIndex)
    {
        var position = new Vector2(_patrolPoint[currentPointIndex], transform.position.y);
        var currentPosition = _rigidbody.position;
        Vector2 newPosition = Vector2.MoveTowards(currentPosition, position, _monsrerData.SpeedMonster * Time.deltaTime);
        _rigidbody.MovePosition(newPosition);
    }


    private void AttackPlayer()
    {
        if (_visiblePlayer)
        {
            _lastPlayerPosition = _player.position;
            Vector2 direction = (_player.position - transform.position).normalized;
            _rigidbody.velocity = new Vector2(direction.x * _monsrerData.AttackSpeed, _rigidbody.velocity.y);

        }
        else
        {
            if (transform.position.x == _lastPlayerPosition.x)
            {
                _rigidbody.velocity = Vector2.zero;
                return;
            }

            Vector2 direction = (_lastPlayerPosition - (Vector2)transform.position).normalized;
            _rigidbody.velocity = new Vector2(direction.x * _monsrerData.AttackSpeed, _rigidbody.velocity.y);
        }
    }

    private void Flip()
    {
        _facingLeft = !_facingLeft;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        _raycastDirection.x = - scale.x;
        transform.localScale = scale;
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
            Debug.Log("игрок слево");
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
