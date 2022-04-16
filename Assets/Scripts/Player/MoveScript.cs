using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    [SerializeField] float _speed = 15f;
    [SerializeField] float _maxSpeed = 20f;
    [SerializeField] float _jumpStrength = 5f;
    [SerializeField] Rigidbody2D _rb = null;
    [SerializeField] AnimationScript _anim = null;
    [SerializeField] FeetScript _feet = null;
    [SerializeField] float _jumpDelay = 0.25f;
    [SerializeField] float _descendStun = 0.25f;

    bool _descending = false;
    bool _stunned = false;
    bool _isFlipped = false;
    Coroutine _stunCoroutine = null;

    public bool isFlipped { get => _isFlipped; }
    public Rigidbody2D rb { get => _rb; }

    void Update()
    {
        if (_feet.isOnGround && Input.GetKeyDown(KeyCode.Space) && !_stunned)
        {
            _anim.TriggerJump();
            StartCoroutine(ExecuteJump(_rb.velocity));
            Stun(_jumpDelay);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Control();

        if (_feet.isOnGround && _descending)
        {
            _descending = false;
            Stun(_descendStun);
        }
    }
    void Control()
    {
        if (_stunned) return;

        if (Input.GetKey(KeyCode.A) && Mathf.Abs(rb.velocity.x) < _maxSpeed)
        {
            if (rb.velocity.x > 0)
            {
                rb.velocity = Vector2.up * rb.velocity;
            }
            rb.AddForce(Vector3.left * _speed, ForceMode2D.Force);
            if (!_isFlipped) Flip();
        }
        else if (Input.GetKey(KeyCode.D) && rb.velocity.x < _maxSpeed)
        {
            if (rb.velocity.x < 0)
            {
                rb.velocity = Vector2.up * rb.velocity;
            }
            rb.AddForce(Vector3.right * _speed, ForceMode2D.Force);
            if (_isFlipped) Flip();
        }
    }

    public void Stun(float t)
    {
        if (_stunCoroutine != null) return;
        _stunCoroutine = StartCoroutine(ExecuteStun(t));
    }
    public void StopStun() {
        if (_stunCoroutine == null) return;

        StopCoroutine(_stunCoroutine);
        _stunned  = false;
    }
    IEnumerator ExecuteStun(float t)
    {
        rb.velocity = Vector2.zero;
        _stunned = true;

        yield return new WaitForSeconds(t);

        _stunCoroutine = null;
        _stunned = false;
    }

    IEnumerator ExecuteJump(Vector2 vBeforeJump)
    {
        yield return new WaitForSeconds(_jumpDelay);

        rb.velocity = vBeforeJump;
        rb.AddForce(Vector3.up * _jumpStrength, ForceMode2D.Impulse);
    }

    public void OnDescending()
    {
        _descending = true;
    }

    void Flip()
    {
        _isFlipped = !_isFlipped;
        gameObject.transform.eulerAngles = new Vector3(0, _isFlipped ? 180 : 0, 0);
    }
}