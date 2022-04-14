using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] AnimationScript _anim;
    [SerializeField] FeetScript _feet;
    [SerializeField] MoveScript _moveScript;
    [SerializeField] float[] _attackForces;
    bool _haveQueueUp = false;
    bool _recovering = false;
    int _attackIndex = 0;

    void Update()
    {
        if (!_recovering && Input.GetKeyDown(KeyCode.U) && _feet.isOnGround) {
            if (_anim.attacking) {
                _haveQueueUp = true;
            } else {
                _moveScript.enabled = false;
                _anim.TriggerAttack();
                _attackIndex = 0;
            }
        }
    }

    void FixedUpdate()
    {
        if (_anim.attacking && !_feet.isOnGround) {
            StopAttack();
        }
    }

    void UpdateAnim() {
        if (!_haveQueueUp) {
            StopAttack();
            _recovering = true;
        } else {
            _haveQueueUp = false;
        }
    }

    void StopAttack() {
        _moveScript.enabled = true;
        _anim.StopAttack();
    }

    void FinishRecover() {
        _recovering = false;
    }

    private void MoveWithForce() {
        var force = _attackForces[_attackIndex];
        var rb = _moveScript.rb;
        
        rb.AddForce((_moveScript.isFlipped ? Vector2.left : Vector2.right) * force, ForceMode2D.Impulse);

        _attackIndex++;
    }
}
