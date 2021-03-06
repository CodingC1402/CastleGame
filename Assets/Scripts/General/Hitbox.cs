using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

[Flags]
public enum HitboxMask {
    Player = 1 << 0,
    Enemy = 1 << 1,
    Object = 1 << 2
}
public class Hitbox : MonoBehaviour
{
    [SerializeField] CharacterValues _characterValues = null;
    [SerializeField] HitboxMask _mask = 0;
    [SerializeField] List<Collider2D> _colliders;

    public HitboxMask mask { get => _mask; }
    public CharacterValues characterValues { get => _characterValues; }

    public int ComparePriority(Collider2D collider1, Collider2D collider2) {
        var index1 = _colliders.IndexOf(collider1);
        var index2 = _colliders.IndexOf(collider2);

        if (index1 > index2) return 1;
        if (index1 < index2) return -1;

        return 0;
    }

    public int GetIndex(Collider2D collider) {
        int index = _colliders.IndexOf(collider);
        return index;
    }
}