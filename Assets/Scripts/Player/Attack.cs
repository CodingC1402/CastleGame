using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct HurtBoxInfo {
    public GameObject hurtboxContainer;
    public float delay;
}

public class Attack : MonoBehaviour
{
    [SerializeField] AnimationScript _anim;
    [SerializeField] FeetScript _feet;
    [SerializeField] MoveScript _moveScript;
    [SerializeField] float[] _attackForces;
    [SerializeField] HurtBoxInfo[] _attackInfo;
    [SerializeField] Hurtbox _hurtBox;
    [SerializeField] float _damage;
    bool _haveQueueUp = false;
    bool _recovering = false;
    int _attackIndex = 0;
    int _hurtBoxIndex = 0;

    void Start()
    {
        _hurtBox.hit += HitCharacter;
    }
    void Update()
    {
        if (!_recovering && Input.GetKeyDown(KeyCode.U) && _feet.isOnGround) {
            if (_anim.attacking) {
                _haveQueueUp = true;
            } else {
                _moveScript.Stun(float.MaxValue);
                _anim.TriggerAttack();
                _attackIndex = 0;
                _hurtBoxIndex = 0;
            }
        }
    }

    void FixedUpdate()
    {
        if (_anim.attacking && !_feet.isOnGround) {
            StopAttack();
        }
    }
    void OnDestroy()
    {
        _hurtBox.hit -= HitCharacter;
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
        _moveScript.StopStun();
        _anim.StopAttack();
    }

    void FinishRecover() {
        _recovering = false;
    }

    private void ActivateHurtbox() {
        var hurtBoxContainer = _attackInfo[_hurtBoxIndex].hurtboxContainer;
        var delay = _attackInfo[_hurtBoxIndex].delay;

        hurtBoxContainer.SetActive(true);
        _hurtBox.ResetExcludeInfos();
        _hurtBoxIndex++;
        StartCoroutine(DeactivateHurtbox(hurtBoxContainer, delay));
    }
    private IEnumerator DeactivateHurtbox(GameObject hurtBoxContainer, float delay) {
        yield return new WaitForSeconds(delay);

        hurtBoxContainer.SetActive(false);
    }

    private void MoveWithForce() {
        var force = _attackForces[_attackIndex];
        var rb = _moveScript.rb;
        
        rb.AddForce((_moveScript.isFlipped ? Vector2.left : Vector2.right) * force, ForceMode2D.Impulse);

        _attackIndex++;
    }

    void HitCharacter(Hurtbox hurtBox, HitInfo info) {
        info.hitbox.characterValues.health.value -= _damage;
        Debug.Log($"Dealt: {_damage}");
    }
}
