using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private Transform[] sensors;
    [SerializeField] private LayerMask groundCheckLayer;
    [SerializeField] private float groundCheckDistance = 0.2f;

    private Color groundHit = Color.green;
    private Color groundMiss = Color.red;

    [SerializeField] private bool isGrounded;
    public bool IsGrounded { get { return isGrounded; } }

    void Update()
    {
        isGrounded = RayCastFromAllSensors();
    }

    private bool RaycastFromSensor(Transform sensor) 
    {
        var origin = sensor.position;
        var direction = -sensor.up;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, groundCheckDistance, groundCheckLayer);
        if (hit.collider != null)
        {
            Debug.DrawRay(origin, direction * groundCheckDistance, groundHit);
            return true;
        }
        else 
        {
            Debug.DrawRay(origin, direction * groundCheckDistance, groundMiss);
        }
        return false;
    }

    private bool RayCastFromAllSensors() 
    {
        foreach (var sensor in sensors) 
        {
            if (RaycastFromSensor(sensor)) return true;
        }
        return false;
    }
}
