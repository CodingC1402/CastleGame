using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerValues : CharacterValues
{
    [SerializeField] AnimationScript _animController;
    [SerializeField] MoveScript _moveScript;
    [SerializeField] float _hitStunTime = 0.0f;
    void Start()
    {
        health.changed += (statValue, change) => {
            // health decrease but not max health
            if ((change & StatValue.Change.DECREASED) > 0 && (change & StatValue.Change.MAX_DECREASED) == 0) {
                _moveScript.Stun(_hitStunTime);
                _animController.TriggerHit();
            }
        };
    }
}
