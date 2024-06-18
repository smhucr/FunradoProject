using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTracker : MonoBehaviour
{
    public GameObject enemy;
    public Vector3 offset;
    private void FixedUpdate()
    {
        transform.position = enemy.transform.position + offset;
    }
}
