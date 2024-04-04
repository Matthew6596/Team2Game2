using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Positioning / Angle")]
    public Transform cameraTarget;
    public float distanceFromTarget;
    public float angleAroundTarget;
    public float verticalOffset;
    [Header("Camera Smoothing")]
    public float movementSmoothing;

    Vector3 targetPosition;

    // Update is called once per frame
    void Update()
    {
        setTargetPosition();

        float dist = Vector3.Distance(transform.position, targetPosition);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSmoothing * dist);

        transform.LookAt(cameraTarget);
    }

    void setTargetPosition()
    {
        targetPosition = cameraTarget.position;
        float ang = Mathf.Deg2Rad*angleAroundTarget;
        Vector3 orbitPos = new Vector3(Mathf.Cos(ang) * distanceFromTarget, verticalOffset, Mathf.Sin(ang) * distanceFromTarget);
        targetPosition += orbitPos;
    }
}
