using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerArea : MonoBehaviour
{
    // Start is called before the first frame update
    public abstract void OnTriggerEnter(Collider other);
    public abstract void OnTriggerExit(Collider other);

    public abstract bool GetValue();
    public abstract Transform GetTargetPosition();
    public abstract Collider[] GetAllColliders();
}
