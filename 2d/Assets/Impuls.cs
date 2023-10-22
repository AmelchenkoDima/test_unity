using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehaviour : MonoBehaviour
{
    private Rigidbody2D fireballBody;
    public float speed;

    void Start()
    {
        fireballBody = GetComponent<Rigidbody2D>();

        // AddForce is a function of RigidBody2D
        fireballBody.AddForce(new Vector2(speed, 0), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
