using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float _bulletSpeed = 10f;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * _bulletSpeed * Time.deltaTime);
    }

    public void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
