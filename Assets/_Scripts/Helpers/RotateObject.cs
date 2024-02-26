using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    
    public float rotationSpeed = 30.0f;  // Velocidad de rotación en grados por segundo

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.left, rotationSpeed * Time.deltaTime);
    }
}
