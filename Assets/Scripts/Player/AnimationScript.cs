using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Update animation properties on each fixed update;
public class AnimationScript : MonoBehaviour
{
    const string X_SPEED = "xSpeed";
    const string Y_SPEED = "ySpeed";
    const string ON_GROUND = "onGround";
    const string JUMP = "jump";
    const string ATTACK = "attack";
    const string ATTACKING = "attacking";
    const string HIT = "hit";

    [SerializeField] Animator _animator;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] FeetScript _feetScript;

    public float xSpeed { get => _animator.GetFloat(X_SPEED); private set => _animator.SetFloat(X_SPEED, Mathf.Abs(value));}
    public float ySpeed { get => _animator.GetFloat(Y_SPEED); private set => _animator.SetFloat(Y_SPEED, value);}
    public bool onGround { get => _animator.GetBool(ON_GROUND); private set => _animator.SetBool(ON_GROUND, value);}
    public bool attacking { get => _animator.GetBool(ATTACKING); private set => _animator.SetBool(ATTACKING, value);}
    public void TriggerJump() {
        _animator.SetTrigger(JUMP);
    }
    public void TriggerAttack() {
        _animator.SetTrigger(ATTACK);
        attacking = true;
    }
    public void TriggerHit() {
        _animator.SetTrigger(HIT);
    }
    public void StopAttack() {
        attacking = false;
    }

    public void TransitToIdle() {
        StopAttack();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Update on ground;
        onGround = _feetScript.isOnGround;

        // Update speed.
        xSpeed = _rb.velocity.x;
        ySpeed = _rb.velocity.y;
    }
}
