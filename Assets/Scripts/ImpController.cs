using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpController : MonoBehaviour
{
    public GameObject juicePrefab;

    private GameObject player;
    private float timer = 1f;
    private float attackTime = 1.5f;
    private Rigidbody2D rb;
    public float moveForce = 15;
    // Start is called before the first frame update
    void Start()
    {
        timer = Random.Range(0.5f, 1f);
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
            attackTime = Random.Range(1f, 1.5f);
        } else {
            timer += Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if(coll.tag == "Player") {
            coll.gameObject.GetComponent<PlayerStatus>().TakeDamage(10);
        }
    }

    public void Kill() {
        Instantiate(juicePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
