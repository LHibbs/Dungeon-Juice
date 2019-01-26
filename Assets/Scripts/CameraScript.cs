using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private GameObject player = null;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        if (player == null) 
        {
            Debug.Log("Error: Camera Script could object \"Player\".");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }
}
