using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonJuiceVileScript : MonoBehaviour
{
    private float timer = 0f;
    private float timeToReady = 0.25f;

    private BoxCollider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > timeToReady) {
            coll.enabled = true;
        } else {
            timer += Time.deltaTime;
        }
    }
    
}
