using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform Target { get; set; }

    [SerializeField] AnimationCurve curve;
    [SerializeField] float heightMultiplier = 1;

    Vector3 start;
    Vector3 lastPosition;
    float time;

    void Start()
    {
        start = transform.position;
    }


    void Update()
    {
        if (Target == null)
        {
            Debug.Log("Projectile has no target");
            return;
        }

        Vector3 headingTo = transform.position + (transform.position - lastPosition);
        transform.LookAt(new Vector3(headingTo.x, headingTo.y, headingTo.z));
        lastPosition = transform.position;

        time += Time.deltaTime;
        Vector3 lerpPosition = Vector3.Lerp(start, Target.position, time);
        lerpPosition.y += (curve.Evaluate(time) * heightMultiplier);
        transform.position = lerpPosition;
    }
}
