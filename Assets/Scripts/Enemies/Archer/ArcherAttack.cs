using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttack : EnemiesAttackScript
{
    [SerializeField] GameObject _arrow;
    [SerializeField] GameObject _firePoint;
    [SerializeField] EnemiesMoveScript _moveScript;
    [SerializeField] float _delay;
    [SerializeField] ArcherAnimationController _animController = null;
    bool _coolDown = false;

    public override void Attack()
    {
        if (_coolDown) return;
        
        _coolDown = true;
        _animController.Attack();
    }

    IEnumerator ExecuteAttack() {
        var arrow = Instantiate(_arrow, _firePoint.transform.position, Quaternion.identity);
        var script = arrow.GetComponent<Projectile>();
        script.Fire(_moveScript.isFlipped ? Vector2.left : Vector2.right);
        _coolDown = true;

        yield return new WaitForSeconds(_delay);

        _coolDown = false;
    }

    public void OnAttackAnimEnd() {
        StartCoroutine(ExecuteAttack());
    }
}
