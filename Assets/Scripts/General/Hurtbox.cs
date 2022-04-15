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
    [SerializeField] HitboxMask _mask = 0;
    [SerializeField] Collider2D _hurtCollider;
    [SerializeField] bool _singleUse = false;

    Dictionary<Hitbox, HitInfo> _hitInfos = new Dictionary<Hitbox, HitInfo>();

    public HitboxMask mask { get => _mask; }
    public readonly EventHandler<Hurtbox, HitInfo> callbacks = new EventHandler<Hurtbox, HitInfo>();

    void OnTriggerEnter2D(Collider2D other)
    {
        var hitbox = other.gameObject.GetComponent<Hitbox>();
        if (!hitbox || (_singleUse && _hitInfos.Count > 0)) return;

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
        StartCoroutine(ExecuteCallbacks());
    }
    IEnumerator ExecuteCallbacks()
    {
        yield return new WaitForFixedUpdate();

        if (_hitInfos.Count == 0) yield break;

        foreach (var info in _hitInfos)
        {
            callbacks.Invoke(this, info.Value);
        }

        _hitInfos.Clear();
    }
}
