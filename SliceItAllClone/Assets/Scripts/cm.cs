using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cm : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float movementSpeed;
    private Vector3 targetOffset;
    void Start()
    {
        targetOffset = transform.position - target.position;
    }
    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + targetOffset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, movementSpeed);
    }
}
