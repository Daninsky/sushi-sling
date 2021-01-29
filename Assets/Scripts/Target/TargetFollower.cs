using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    private void Start()
    {
    }

    private void LateUpdate()
    {
        Debug.Log(transform.position);
        Debug.Log(target.position);
    }
}
