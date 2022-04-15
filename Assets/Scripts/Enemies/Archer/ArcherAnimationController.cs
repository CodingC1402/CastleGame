using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAnimationController : MonoBehaviour
{
    const string ATTACK = "attack";
    const string HIT = "hit";

    [SerializeField] Animator _animator;

    public void Attack() {
        _animator.SetTrigger(ATTACK);
    }
    public void Hit() {
        _animator.SetTrigger(HIT);
    }
}
