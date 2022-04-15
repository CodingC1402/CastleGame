using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

[Serializable]
public class StatValue
{
    public enum Change
    {
        NOTHING = 0,
        DECREASED = 1 << 0,
        INCREASED = 1 << 1,
        MAX_DECREASED = 1 << 2,
        MAX_INCREASED = 1 << 3
    }

    const int MIN_VALUE = 0;
    [SerializeField, Min(MIN_VALUE)] float _maxValue = 0;
    [SerializeField, Min(MIN_VALUE)] float _value = 0;

    Change _stagingChanges = Change.NOTHING;
    EventHandler<StatValue, Change> _eventHandler = new EventHandler<StatValue, Change>();
    public event EventHandler<StatValue, Change>.Callback changed {add => _eventHandler.Add(value); remove => _eventHandler.Remove(value);}

    public float value { get => _value; set {
        UpdateValue(value);
        Invoke();
    }}
    public float maxValue {get => _maxValue; set {
        if (value > _maxValue) _stagingChanges |= Change.MAX_INCREASED;
        else if (value < _maxValue) _stagingChanges |= Change.MAX_DECREASED;

        _maxValue = value;
        UpdateValue(_value);
        Invoke();
    }}

    void Invoke() {
        if (_stagingChanges == Change.NOTHING) return;

        _eventHandler.Invoke(this, _stagingChanges);
        _stagingChanges = Change.NOTHING;
    }

    void UpdateValue(float value)
    {
        float oldValue = _value;
        _value = Mathf.Clamp(value, MIN_VALUE, _maxValue);
        
        if (_value > oldValue) _stagingChanges |= Change.INCREASED;
        else if (_value < oldValue) _stagingChanges |= Change.DECREASED;
    }
}
