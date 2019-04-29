using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOffscreen : MonoBehaviour
{
    public float screenEdgeX = 9f, screenEdgeY = 5.06f;
    public bool check;
    GameObject player;
    private void Start()
    {
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        if (check)
        {
            wrap();
        }

    }
    public bool wrap()
    {

        
        if (transform.position.y < -screenEdgeY)
        {
            player.GetComponent<Player>().hit();
            return true;
        }
        return false;
    }
}
