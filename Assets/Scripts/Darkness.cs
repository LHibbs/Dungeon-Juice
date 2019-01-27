using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkness : MonoBehaviour
{
    private GameObject player;
    public float scaleFactor = 0.0025f;

    private float multiplier = 1f;
    public int darknessHeight;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scale = gameObject.GetComponent<Transform>().localScale;
        scale += new Vector3(scaleFactor, scaleFactor, 0);
        gameObject.GetComponent<Transform>().localScale = scale;  
    
    }

    public void AddMultiplier(float add)
    {
        multiplier += add;
    }

    void OnTriggerEnter2D(Collider2D coll) {
        //Debug.Log("Darkness Entered");
        if(coll.tag == "Player") {
            coll.gameObject.GetComponent<PlayerStatus>().TakeDamage(.1f*multiplier);
        }
    }
 
    void OnTriggerStay2D(Collider2D coll){
        if(coll.tag == "Player") {
            coll.gameObject.GetComponent<PlayerStatus>().TakeDamage(.1f*multiplier);
        }
    } 

}
