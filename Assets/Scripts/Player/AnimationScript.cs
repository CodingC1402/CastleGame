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

    [SerializeField] Animator _animator;
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] FeetScript _feetScript;

    private float xSpeed { get => _animator.GetFloat(X_SPEED); set => _animator.SetFloat(X_SPEED, Mathf.Abs(value));}
    private float ySpeed { get => _animator.GetFloat(Y_SPEED); set => _animator.SetFloat(Y_SPEED, value);}
    private bool onGround { get => _animator.GetBool(ON_GROUND); set => _animator.SetBool(ON_GROUND, value);}
    public void triggerJump() {
        _animator.SetTrigger(JUMP);
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
