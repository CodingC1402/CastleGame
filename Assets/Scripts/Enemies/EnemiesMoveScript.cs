using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesMoveScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    public Rigidbody2D rb {get => _rb;}
    bool _isFlipped = false;
    public bool isFlipped { get => _isFlipped; }

    public bool flip() {
        _isFlipped = !_isFlipped;
        gameObject.transform.eulerAngles = new Vector3(0, _isFlipped ? 180 : 0, 0);

        return _isFlipped;
    }
}
