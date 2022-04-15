using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public struct HitInfo
{
    public Hitbox hitbox;
    public Collider2D collider;

    public HitInfo(Hitbox hitbox, Collider2D collider)
    {
        this.hitbox = hitbox;
        this.collider = collider;
    }
}

public class Hurtbox : MonoBehaviour
{
    const string HIT_BOX = "Hitbox";
    [SerializeField] HitboxMask _mask = 0;
    [SerializeField] bool _singleUse = false;
    [SerializeField] Collider2D[] _colliders;
    [SerializeField] LayerMask _layerMask = 0;
    [SerializeField] bool _repeatHit = false;
    Dictionary<Hitbox, HitInfo> _hitInfos = new Dictionary<Hitbox, HitInfo>();
    List<Hitbox> _excludeHitboxes = new List<Hitbox>();

    public HitboxMask mask { get => _mask; }
    private readonly EventHandler<Hurtbox, HitInfo> _callbacks = new EventHandler<Hurtbox, HitInfo>();
    public event EventHandler<Hurtbox, HitInfo>.Callback hit {add => _callbacks.Add(value); remove => _callbacks.Remove(value);}

    void Collide(Collider2D other)
    {
        var hitbox = other.gameObject.GetComponent<Hitbox>();
        if (!hitbox 
        || (hitbox.mask & _mask) == 0 
        || (_singleUse && _hitInfos.Count > 0) 
        || (!_repeatHit && _excludeHitboxes.IndexOf(hitbox) >= 0)) return;

        HitInfo info;
        bool found = _hitInfos.TryGetValue(hitbox, out info);

        if (found)
        {
            int compareResult = hitbox.ComparePriority(info.collider, other);
            if (compareResult < 0)
            {
                info.collider = other;
            }
        }
        else
        {
            _hitInfos.Add(hitbox, new HitInfo(hitbox, other));
        }
    }
    void FixedUpdate()
    {
        foreach(var collider in _colliders) {
            List<Collider2D> collideWith = new List<Collider2D>();
            ContactFilter2D filter = new ContactFilter2D();
            filter.layerMask = _layerMask;
            filter.useLayerMask = true;

            collider.OverlapCollider(filter, collideWith);
            foreach(var other in collideWith) {
                Collide(other);
            }
        }

        if (_hitInfos.Count == 0) return;
        foreach (var info in _hitInfos)
        {
            _callbacks.Invoke(this, info.Value);
        }

        if (_singleUse) {
            enabled = false;
        } else if (!_repeatHit) {
            _excludeHitboxes.AddRange(_hitInfos.Keys);
        }

        _hitInfos.Clear();
    }
}
