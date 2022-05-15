using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform startPos;
    [SerializeField] Transform endPos;
    [SerializeField] float speed;

    private float startTime;
    private float length;
    private float fracLength;
    private bool facingRight = false;

    void Start()
    {
        startTime = Time.time;
        length = Vector2.Distance(startPos.position, endPos.position);
    }

    void Update()
    {
        float distCovered = (Time.time - startTime) * speed;
        fracLength = distCovered/length;

        if(facingRight == true){
            transform.position = Vector3.Lerp(startPos.position, endPos.position, fracLength);
        } else {
            transform.position = Vector3.Lerp(endPos.position, startPos.position, fracLength);
        }

        if(facingRight == true && fracLength > 0.999f){
            facingRight = false;
            startTime = Time.time;
            transform.Rotate(Vector3.up, 180f); //(0,1,0)
        } else if (facingRight == false && fracLength > 0.999f) {
            facingRight = true;
            startTime = Time.time;
            transform.Rotate(Vector3.up, 180f); //(0,1,0)
        }
    }
}
