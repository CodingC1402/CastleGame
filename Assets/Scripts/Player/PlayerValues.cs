using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerValues : CharacterValues
{
    [SerializeField] AnimationScript _animController;
    [SerializeField] MoveScript _moveScript;
    [SerializeField] float _hitStunTime = 0.0f;
    [SerializeField] Vector2 _hitForce = Vector2.zero;
    [SerializeField] GameObject _hitBoxContainer = null;
    void Start()
    {
        health.changed += (statValue, change) => {
            // health decrease but not max health
            if ((change & StatValue.Change.DECREASED) > 0 && (change & StatValue.Change.MAX_DECREASED) == 0) {
                _moveScript.Stun(_hitStunTime);
                if (statValue.value == 0) {
                    _animController.Die();
                    _hitBoxContainer.SetActive(false);
                } else {
                    _moveScript.rb.AddForce(new Vector2(_hitForce.x * (_moveScript.isFlipped ? -1 : 1), _hitForce.y), ForceMode2D.Impulse);
                    _animController.TriggerHit();
                    StartCoroutine(DisableHitStun());
                }
            }
        };
    }

    IEnumerator DisableHitStun() {
        yield return new WaitForSeconds(_hitStunTime);
        _animController.StopHitStun();
    }
}
