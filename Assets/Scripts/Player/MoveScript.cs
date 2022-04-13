using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    [SerializeField] float _speed = 15f;
    [SerializeField] float _jumpStrength = 5f;
    [SerializeField] Rigidbody2D _rb = null;
    [SerializeField] AnimationScript _anim = null;
    [SerializeField] FeetScript _feet = null;
    [SerializeField] float _delayBeforeJump = 0.25f;
    
    bool _isFlipped = false;
    float _jumpCounter = 0;

    public Rigidbody2D rb { get => _rb; }

    void Update()
    {
        if (_feet.isOnGround && Input.GetKeyDown(KeyCode.Space) && _jumpCounter <= 0)
        {
            _anim.triggerJump();
            _jumpCounter = _delayBeforeJump;
        }

        if (_jumpCounter > 0) {
            _jumpCounter -= Time.deltaTime;
            if (_jumpCounter <= 0) {
                rb.AddForce(Vector3.up * _jumpStrength, ForceMode2D.Impulse);
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.left * _speed, ForceMode2D.Force);
            if (!_isFlipped) Flip();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right * _speed, ForceMode2D.Force);
            if (_isFlipped) Flip();
        }
    }

    void Flip() {
        _isFlipped = !_isFlipped;
        gameObject.transform.eulerAngles = new Vector3(0, _isFlipped ? 180 : 0, 0);
    }
}