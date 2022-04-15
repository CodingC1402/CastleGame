using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float _force = 0;
    [SerializeField, Min(0)] float _delayCleanUp = 0;
    [SerializeField] float _forceVar = 0;
    [SerializeField] Vector2 _dirVar = Vector2.zero;
    Rigidbody2D _rb = null;
    public void Fire(Vector2 direction) { 
        if (direction.x < 0) {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        var force = Random.Range(_force - _forceVar, _force + _forceVar);
        direction.x = Random.Range(direction.x - _dirVar.x, direction.x + _dirVar.x);
        direction.y = Random.Range(direction.y - _dirVar.y, direction.y + _dirVar.y);

        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(direction * force, ForceMode2D.Impulse);

        this.enabled = true;
    }

    void FixedUpdate() {
        var rot = _rb.transform.rotation;
        rot.eulerAngles = new Vector3(0, rot.eulerAngles.y, Mathf.Atan2(_rb.velocity.y, Mathf.Abs(_rb.velocity.x)) * Mathf.Rad2Deg);
        _rb.transform.rotation = rot;
    }

    void OnTriggerEnter2D() {
        _rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        Destroy(gameObject, _delayCleanUp);
        enabled = false;
    }
}
