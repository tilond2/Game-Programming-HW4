using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    // Start is called before the first frame update
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
        if (origX - transform.position.x > distance)
        {
            useSpeed = directionSpeed; //flip direction
            this.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (origX - transform.position.x < -distance)
        {
            useSpeed = -directionSpeed; //flip direction
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        transform.Translate(useSpeed * Time.deltaTime, 0, 0);
    }
}
