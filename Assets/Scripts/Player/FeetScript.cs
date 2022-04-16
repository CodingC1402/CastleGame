using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetScript : MonoBehaviour
{
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] Vector2 _size = new Vector2(10, 10);
    [SerializeField] float _scanDistance = 10;

    bool _isOnGround = false;
    public bool isOnGround {get => _isOnGround;}

    // Update is called once per frame
    void FixedUpdate()
    {
        var collisions = Physics2D.BoxCastAll(gameObject.transform.position, _size, 0, Vector3.down, _scanDistance, _groundLayer);
        _isOnGround = collisions.Length > 0;
    }

    void OnDrawGizmos()
    {
        var center = gameObject.transform.position;
        var size = _size;

        size.y += _scanDistance;
        center.y -= _scanDistance / 2;

        Gizmos.color = _isOnGround ? Color.green : Color.white;
        Gizmos.DrawWireCube(center, size);
    }
}
