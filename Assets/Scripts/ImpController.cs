using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpController : MonoBehaviour
{
    private GameObject player;
    private float timer = 0f;
    private float attackTime = 2.5f;
    private Rigidbody2D rb;
    public float moveForce = 15;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > attackTime) {
            Vector2 towardsPlayer = player.transform.position - transform.position;
            rb.AddForce(towardsPlayer.normalized * moveForce, ForceMode2D.Impulse);
            timer = 0f;
        } else {
            timer += Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if(coll.tag == "Player") {
            coll.gameObject.GetComponent<PlayerStatus>().TakeDamage();
        }
    } 
}
