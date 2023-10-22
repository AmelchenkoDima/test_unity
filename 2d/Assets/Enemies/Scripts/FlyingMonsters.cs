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

    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _afterDeadPrefab;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _rayDistance = 3f;
    [SerializeField] private List<float> _patrolEnemy;

    private Transform _parrent;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        _visiblePlayer = Physics2D.Raycast(transform.position, Vector2.left, _rayDistance, _layerMask);

        if (_visiblePlayer)
        {
            Attack();
        }

    }

    private void FixedUpdate()
    {
        ChoisePatrolPoint();
    }

    private void ChoisePatrolPoint()
    {
        if (!_visiblePlayer)
        {
            if (transform.position.x == _patrolEnemy[0])
            {   
                _monsrerData.isValid = true;
                _index = 1;
                Flip();
            }
            if (transform.position.x == _patrolEnemy[1])
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
        var position = new Vector2(_patrolEnemy[currentPointIndex], transform.position.y);
        var currentPosition = _rigidbody.position;
        Vector2 newPosition = Vector2.MoveTowards(currentPosition, position, _monsrerData.SpeedMonster * Time.deltaTime);
        _rigidbody.MovePosition(newPosition);
    }

    private void Flip()
    {
        _facingLeft = !_facingLeft;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void Attack()
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


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.left * _rayDistance);
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
}
