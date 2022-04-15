using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterValues : MonoBehaviour
{
    [SerializeField] protected StatValue _health = new StatValue();
    
    public StatValue health {get => _health;}
}
