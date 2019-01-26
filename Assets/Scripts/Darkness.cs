using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkness : MonoBehaviour
{
    private GameObject player;
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
        scale += new Vector3(0, 0.01F, 0);
        gameObject.GetComponent<Transform>().localScale = scale;  
    
    }

    void OnTriggerEnter2D(Collider2D coll) {
        Debug.Log("entered");
        if(coll.tag == "Player") {
            coll.gameObject.GetComponent<PlayerStatus>().TakeDamage(.1f);
        }
    }
 
    void OnTriggerStay2D(Collider2D coll){
        if(coll.tag == "Player") {
            coll.gameObject.GetComponent<PlayerStatus>().TakeDamage(.1f);
        }
    } 

}
