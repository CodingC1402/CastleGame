using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAI : MonoBehaviour
{
    [SerializeField] EnemiesMoveScript _moveScript = null;
    [SerializeField] EnemiesAttackScript _attackScript = null;
    [SerializeField] Vector2 _scanCenter = Vector2.zero;
    [SerializeField] Vector2 _scanSize = Vector2.zero;
    [SerializeField] LayerMask _playerMask = 0;

    private bool _foundPlayer = false;
    private Vector2 _currentPos = Vector2.zero;
    // Update is called once per frame
    void FixedUpdate()
    {
        _currentPos = new Vector2(transform.position.x, transform.position.y) + _scanCenter;

        var collide = Physics2D.BoxCastAll(
            _currentPos,
            _scanSize, 
            0, 
            Vector2.zero, 
            0,
            _playerMask
            );
        
        if (collide.Length == 0) {
            _foundPlayer = false;
            return;
        }

        _foundPlayer = true;
        float deltaX = collide[0].collider.transform.position.x - transform.position.x;
        if (deltaX < 0 && !_moveScript.isFlipped) {
            _moveScript.flip();
        } else if (deltaX > 0 && _moveScript.isFlipped) {
            _moveScript.flip();
        }

        _attackScript.Attack();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = _foundPlayer ? Color.red : Color.white;
        Gizmos.DrawWireCube(_currentPos, _scanSize);
    }
}
