using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAnimationController : MonoBehaviour
{
    const string ATTACK = "attack";
    const string HIT = "hit";
    const string DIE = "die";
    const string DEAD = "dead";
    [SerializeField] Animator _animator;

    public void Attack() {
        _animator.SetTrigger(ATTACK);
    }
    public void Hit() {
        _animator.SetTrigger(HIT);
    }
    public void Die() {
        _animator.SetTrigger(DIE);
        _animator.SetBool(DEAD, true);
    }
}
