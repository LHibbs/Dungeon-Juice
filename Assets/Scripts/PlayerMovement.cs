using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public AudioSource audio;
    private GameObject pentagram;
    private DungeonJuiceScript djs;
    private GameObject currentRoom;
    public float moveSpeed = 0.1f;
    private float attackSpeed = 20f;
    private bool isAttacking = false;
    private float attackTimer = 0f;
    private float attackTime = 1f;

    Rigidbody2D rb;

    private DungeonJuiceScript djs;

    private GameObject home;

    // Start is called before the first frame update
    void Start()
    {
        djs = GameObject.Find("PlayerStatsObject").GetComponent<DungeonJuiceScript>();
        rb = GetComponent<Rigidbody2D>();
        home = GameObject.Find("StartRoom");

        djs = GameObject.Find("PlayerStatsObject").GetComponent<DungeonJuiceScript>();
        if (djs == null)
        {
            Debug.Log("Error: PlayerStatus could not find object \"DungeonJuiceScript\".");
        }

        pentagram = GameObject.Find("Pentagram");
        if (pentagram == null)
        {
            Debug.Log("Error: PlayerStatus could not find object \"Pentagram\".");
        }
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

        if (Input.GetButtonDown("Home") && currentRoom.GetComponent<RoomScript>().GetHomeStatus() == false && djs.GetDungeonJuiceSliderValue() >= 75f) {
            SetHome();
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

    private void SetHome() 
    {
        //Debug.Log("Home Reset");
        djs.ChangeDungeonJuiceSliderValue(-75f);   
        home.GetComponent<RoomScript>().SetHomeStatus(false);
        currentRoom.GetComponent<RoomScript>().SetHomeStatus(true);
        home = currentRoom;
        pentagram.transform.position = new Vector3(currentRoom.transform.position.x, currentRoom.transform.position.y);
        
    }

    void OnTriggerEnter2D(Collider2D coll) {
        //Debug.Log(coll.tag);
        if(coll.tag == "Enemy") {
            if(isAttacking) {
                coll.gameObject.GetComponent<ImpController>().Kill();
                audio.Play();
            }
        } else if (coll.tag == "Room") {
                currentRoom = coll.gameObject;                
        } else if (coll.tag == "Juice") {
            djs.AddToDungeonJuice(10f);
            Destroy(coll.gameObject);
        }
    }

    public bool IsAttacking() {
        return isAttacking;
    }

}
