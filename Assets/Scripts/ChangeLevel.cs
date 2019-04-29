using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public string scene;
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Player touched");
        if (coll.gameObject.name == "Player")
        {

            SceneManager.LoadScene(scene);

        }
    }
}

