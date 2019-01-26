using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    private float attackSpeed = 20f;
    private bool isAttacking = false;
    private float attackTimer = 0f;
    private float attackTime = 1f;

    Rigidbody2D rb;

    private GameObject home;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        home = GameObject.Find("StartRoom");
    }

    // Update is called once per frame
    void Update()
    {
        /*rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * moveSpeed,
                                Input.GetAxis("Vertical") * moveSpeed).normalized,
                                ForceMode2D.Impulse);
        */ 
        /*transform.Translate(new Vector2(Input.GetAxis("Horizontal"),
                                Input.GetAxis("Vertical")).normalized * moveSpeed);  
        */

        if(Input.GetButtonDown("Fire1")) {
            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pz.z = 0;
            Attack(pz); 
        }

        //Debug.Log(attackTimer);
        if(isAttacking) {
            if(attackTimer > attackTime) {
                isAttacking = false;
                attackTimer = 0f;
            } else {
                attackTimer += Time.deltaTime;
            }
        }

        if(Input.GetButtonDown("Jump")) {
            ReturnHome();
        }
    }

    void FixedUpdate() {
        /*rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * moveSpeed,
                                Input.GetAxis("Vertical") * moveSpeed).normalized,
                                ForceMode2D.Impulse);
        */
        transform.Translate(new Vector2(Input.GetAxis("Horizontal"),
                                Input.GetAxis("Vertical")).normalized * moveSpeed);  
        
    }

    private void Attack(Vector3 attackPos) {
        isAttacking = true;
        Vector2 attackDir = attackPos - transform.position;
        rb.AddForce(attackDir.normalized * attackSpeed, ForceMode2D.Impulse);
    }

    private void ReturnHome() {
        if(home != null) {
            transform.position = new Vector3(home.transform.position.x, home.transform.position.y, -1);
        }
    }

    void OnTriggerEnter2D(Collider2D coll) {
        //Debug.Log(coll.tag);
        if(coll.tag == "Enemy") {
            if(isAttacking) {
                coll.gameObject.GetComponent<ImpController>().Kill();
            }
        } else if (coll.tag == "Room") {
                Debug.Log(coll.gameObject.name);
        }
    }

    public bool IsAttacking() {
        return isAttacking;
    }

}
