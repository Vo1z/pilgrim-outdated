using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisionArea : TriggerArea
{
    private string _tag = "Player";
    private bool _value;
    private readonly List<Collider> _colliders = new List<Collider>();
    private Transform _target;
    public override void OnTriggerEnter(Collider other)
    {
        _colliders.Add(other);
        if (other.CompareTag(_tag))
        {
            _value = true;
            _target = other.transform;
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        _colliders.Remove(other);
        if (other.CompareTag(_tag))
        {
            _value = false;
            _target = null;
        }
    }

    public override bool GetValue()
    {
        Debug.Log(_value);
        return _value;
    }

    public override Transform GetTargetPosition()
    {
        return _target;
    }

    public override Collider[] GetAllColliders()
    {
        return _colliders.ToArray();
    }
}
