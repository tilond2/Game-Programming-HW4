using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPlatform : MonoBehaviour
{
    private float useSpeed;
    public float directionSpeed = 1f;
    float origX;
    public float distance = 1f;

    // Use this for initialization
    void Start()
    {
        origX = transform.position.x;
        useSpeed = -directionSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (origX - transform.position.y > distance)
        {
            useSpeed = directionSpeed; //flip direction
        }
        else if (origX - transform.position.y < -distance)
        {
            useSpeed = -directionSpeed; //flip direction
        }
        transform.Translate(0, useSpeed * Time.deltaTime, 0);
    }
}
