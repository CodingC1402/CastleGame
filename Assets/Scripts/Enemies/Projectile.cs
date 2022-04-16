using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float _force = 0;
    [SerializeField, Min(0)] float _delayCleanUp = 0;
    [SerializeField] float _forceVar = 0;
    [SerializeField] Vector2 _dirVar = Vector2.zero;
    [SerializeField] float _damage = 0;
    [SerializeField] bool _rotateToVelocity = true;
    [SerializeField] Collider2D _projecttileCollider;
    [SerializeField] LayerMask _stopMask;
    [SerializeField] Hurtbox _hurtBox;
    [SerializeField] AudioManager _audioManager;
    Rigidbody2D _rb = null;

    void Start()
    {
        _hurtBox.hit += HitCharacter;
    }

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
        List<Collider2D> colliders = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.layerMask = _stopMask;
        filter.useLayerMask = true;

        int count = _projecttileCollider.OverlapCollider(filter, colliders);
        if (count > 0) {
            _rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            _audioManager.Play("hit");
            enabled = false;
            Destroy(gameObject, _delayCleanUp);
        }


        if (_rotateToVelocity) return;
        var rot = _rb.transform.rotation;
        rot.eulerAngles = new Vector3(0, rot.eulerAngles.y, Mathf.Atan2(_rb.velocity.y, Mathf.Abs(_rb.velocity.x)) * Mathf.Rad2Deg);
        _rb.transform.rotation = rot;
    }

    void HitCharacter(Hurtbox hurtBox, HitInfo info) {
        float result = (info.hitbox.characterValues.health.value -= _damage);
        Audio audio = _audioManager.GetAudio("hit");

        audio.Play();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        enabled = false;

        Debug.Log($"Health left: {result}");
        Destroy(gameObject, audio.clip.length);
    }

    void OnDestroy()
    {
        _hurtBox.hit -= HitCharacter;
    }
}
