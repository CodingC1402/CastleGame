using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherValues : CharacterValues
{
    [SerializeField] ArcherAnimationController _animation;  
    [SerializeField] Vector2 _hitForce = Vector2.zero;
    [SerializeField] EnemiesMoveScript _moveScript;
    [SerializeField] GameObject _hitBoxContainer;
    [SerializeField] ArcherAI _ai;
    void Awake()
    {
        _health.changed += (statValue, change) => {
            if ((change & StatValue.Change.DECREASED) != 0 && (change & StatValue.Change.MAX_DECREASED) == 0) {
                if (statValue.value > 0) {
                    _animation.Hit();
                    _moveScript.rb.AddForce(new Vector2(_hitForce.x * (_moveScript.isFlipped ? -1 : 1), _hitForce.y), ForceMode2D.Impulse);
                } else {
                    _animation.Die();
                    _ai.enabled = false;
                    _hitBoxContainer.SetActive(false);
                    Destroy(gameObject, 10);
                }
            }
        };
    }
}
