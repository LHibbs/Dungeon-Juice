using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    private Vector2 attackDir;
    private float moveSpeed = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(attackDir * moveSpeed);
    }

    public void SetVector(Vector2 v) {
        attackDir = v.normalized;
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if(coll.tag == "Wall") {
            Destroy(gameObject);
        } else if(coll.tag == "Player") {
            coll.gameObject.GetComponent<PlayerStatus>().TakeDamage(10);
            Destroy(gameObject);
        }
    }

}
