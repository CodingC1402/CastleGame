using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] GameObject player;

    void LateUpdate()
    {
        var pos = player.transform.position;
        gameObject.transform.position = new Vector3(pos.x, pos.y, -1);
    }
}
