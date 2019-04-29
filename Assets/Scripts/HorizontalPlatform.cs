using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPlatform : MonoBehaviour
{
    private float useSpeed;
    public float directionSpeed = 1f;
    float origX;
    public float distance = 1f;
    private GameObject target = null;
    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        origX = transform.position.x;
        useSpeed = -directionSpeed;
        target = null;
    }

    // Update is called once per frame


void OnCollisionStay2D(Collision2D col) { 
     target = col.gameObject;
     offset = target.transform.position - transform.position;
 }
void OnCollisionExit2D(Collision2D collision)
{
        target = null;
}
 void LateUpdate(){
     if (target != null) {
         target.transform.position = transform.position+offset;
     }
 }
    void Update()
    {
        if (origX - transform.position.x > distance)
        {
            useSpeed = directionSpeed; //flip direction
        }
        else if (origX - transform.position.x < -distance)
        {
            useSpeed = -directionSpeed; //flip direction
        }
        transform.Translate(useSpeed * Time.deltaTime,0 , 0);
    }
}
