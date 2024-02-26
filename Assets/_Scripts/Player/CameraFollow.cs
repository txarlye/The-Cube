using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    public Vector3 offset = new Vector3(0, 20, 0);
    protected Transform _transform;
 
    void Awake()
    {
        _transform = GetComponent<Transform>(); 
        target = GameObject.Find("Cube [Player](Clone)").transform; 
    }
 
    void Update()
    {
        if (target == null)
        {
            return;
        }
        _transform.position = target.position + offset;
        _transform.LookAt(target, Vector3.up);
    }
}

