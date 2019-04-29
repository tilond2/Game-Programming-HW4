using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlatform : MonoBehaviour
{
    public bool check;
    GameObject player;
    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void kill()
    {

        player.GetComponent<Player>().hit();

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        kill();
    }
}
