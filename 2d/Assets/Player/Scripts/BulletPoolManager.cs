using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _pullSize = 10;

    private List<GameObject> _bulletPool;

    private void Start()
    {
        InitilaizePool();
    }

    private void InitilaizePool()
    {
        _bulletPool = new List<GameObject>();

        for (int i = 0 ; i < _pullSize;  i++)
        {
            GameObject bullet = Instantiate(_bulletPrefab, transform);
            bullet.SetActive(false);
            _bulletPool.Add(bullet);
        }
    }

    public GameObject GetBullet() 
    {
        foreach (GameObject bullet in _bulletPool)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                return bullet;
            }
        }
        return null;
    }    
}
