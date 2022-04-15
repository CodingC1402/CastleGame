using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirFriction : MonoBehaviour
{
    [SerializeField] float _friction = 0;
    float _speedRetain = 0;
    Rigidbody2D rb = null;
    // Start is called before the first frame update
    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        _speedRetain = 1 - _friction;
    } 

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(rb.velocity.x * _speedRetain, rb.velocity.y);
    }
}
