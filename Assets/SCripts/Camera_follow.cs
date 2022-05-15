using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_follow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;
    [SerializeField] float chase_factor=0.7f;

    private void FixedUpdate()
    {
        track_target();
    }

    void track_target()
    {
        Vector3 target_pos = target.position + offset;
        Vector3 smooth = Vector3.Lerp(transform.position, target_pos, chase_factor * Time.deltaTime);
        transform.position = smooth;
    }
}
