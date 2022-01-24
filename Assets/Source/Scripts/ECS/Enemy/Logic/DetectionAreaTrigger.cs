using System;
using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;
using Ingame.Enemy.ECS;
public class DetectionAreaTrigger : MonoBehaviour
{
    
    private EcsWorld ecs_; 
    private void OnTriggerEnter(Collider other)
    {
        //var 
        //todo
        //attack
        
    }

    private void OnTriggerExit(Collider other)
    {
        //todo
        //stop attacking
    }
}
