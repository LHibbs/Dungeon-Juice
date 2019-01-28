using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerStatus ps;
    private GameObject controls;
    private SpriteRenderer spriteRenderer;  
    private Darkness ds;
    private GameObject[] lightLevels;
    private int lightLevelIndex = 0;
    public AudioSource audioS;
    public AudioSource attackSound;
    public AudioSource pickup;
    private GameObject pentagram;
    private DungeonJuiceScript djs;
    private GameObject currentRoom;
    public float moveSpeed = 0.1f;
    private float attackSpeed = 30f;
    private bool isAttacking = false;
    private float attackTimer = 0f;
    private float attackTime = .50f;
    private bool justAttacked = false;
    private float attackCooldownTimer = 0f;
    private float attackCooldown = .45f;

    Rigidbody2D rb;

    private GameObject home;

    // Start is called before the first frame update
    void Start()
    {
        controls = GameObject.Find("Controls");

        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

        currentRoom = GameObject.Find("StartRoom");
        currentRoom.GetComponent<RoomScript>().SetHomeStatus(true);
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


        ds = GameObject.Find("Darkness").GetComponent<Darkness>();
        if (ds == null)
        {
            Debug.Log("Error: Darkness (script) could not find object \"Darkness\".");
        }

        lightLevels = new GameObject[5];

        lightLevels[0] = GameObject.Find("circle1");
        if (lightLevels[0] == null)
        {
            Debug.Log("Error: PlayerStatus could not find object \"circle1\".");
        }
        lightLevels[1] = GameObject.Find("circle2");
        if (lightLevels[1] == null)
        {
            Debug.Log("Error: PlayerStatus could not find object \"circle2\".");
        }
        lightLevels[2] = GameObject.Find("circle3");
        if (lightLevels[2] == null)
        {
            Debug.Log("Error: PlayerStatus could not find object \"circle3\".");
        }
        lightLevels[3] = GameObject.Find("circle4");
        if (lightLevels[3] == null)
        {
            Debug.Log("Error: PlayerStatus could not find object \"circle4\".");
        }
        lightLevels[4] = GameObject.Find("circle5");
        if (lightLevels[4] == null)
        {
            Debug.Log("Error: PlayerStatus could not find object \"circle5\".");
        }
        
        lightLevels[0].SetActive(false);
        lightLevels[1].SetActive(false);
        lightLevels[2].SetActive(false);
        lightLevels[3].SetActive(false);
        lightLevels[4].SetActive(false);
        
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
            if(!justAttacked){
                Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pz.z = 0;
                Attack(pz);
                justAttacked = true;
            }
        }

        if (Input.GetButtonDown("Escape")) {
            controls.SetActive(false);
        }

        if(isAttacking) {
            if(attackTimer > attackTime) {
                isAttacking = false;
                attackTimer = 0f;
            } else {
                attackTimer += Time.deltaTime;
            }
        }

        if(justAttacked){
            if(attackCooldownTimer > attackCooldown) {
                justAttacked = false;
                attackCooldownTimer = 0f;
            } else {
                attackCooldownTimer += Time.deltaTime;
            }
        }
        if(Input.GetButtonDown("Fire2")) {
            ReturnHome();
        }
        
        if (Input.GetButtonDown("Home") && !currentRoom.GetComponent<RoomScript>().GetHomeStatus() && djs.GetDungeonJuice() >= 75f) {
            SetHome();
        }
    }

    void FixedUpdate() {
        /*rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * moveSpeed,
                                Input.GetAxis("Vertical") * moveSpeed).normalized,
                                ForceMode2D.Impulse);
        */
        
        /*if(!isAttacking){
        transform.Translate(new Vector2(Input.GetAxis("Horizontal"),
                                Input.GetAxis("Vertical")).normalized * moveSpeed);  
        }*/
        if(!isAttacking){
            Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pz.z = -1;
            transform.Translate((transform.position - pz).normalized * -moveSpeed);

            //Flip player sprite towards mouse position
            if (Input.mousePosition.x > Screen.width/2)  {
                spriteRenderer.flipX = false;
            } else if (Input.mousePosition.x < Screen.width/2) {
                spriteRenderer.flipX = true;
            }
            
        }

    }

    private void Attack(Vector3 attackPos) {
        isAttacking = true;
        attackSound.Play();
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
        djs.AddToDungeonJuice(-75f);   
        home.GetComponent<RoomScript>().SetHomeStatus(false);
        currentRoom.GetComponent<RoomScript>().SetHomeStatus(true);
        home = currentRoom;
        pentagram.transform.position = new Vector3(currentRoom.transform.position.x, currentRoom.transform.position.y);

        ds.AddMultiplier(-.3f);

        lightLevels[lightLevelIndex].SetActive(true);
        lightLevelIndex++;

        if(lightLevelIndex == 5){
            ps.MusicFadeOut();
        }
        
    }

    void OnTriggerEnter2D(Collider2D coll) {
        if(coll.tag == "Imp") {
            if(isAttacking) {
                coll.gameObject.GetComponent<ImpController>().Kill();
                audioS.Play();
            }
        } else if(coll.tag == "Archer") {
            if(isAttacking) {
                coll.gameObject.GetComponent<ThrowerScript>().Kill();
                audioS.Play();
            }
        } else if (coll.tag == "Room") {
                currentRoom = coll.gameObject;                
        } else if (coll.tag == "Juice") {
            pickup.Play();
            djs.AddToDungeonJuice(10f);
            Destroy(coll.gameObject);
        }
    }

    public bool IsAttacking() {
        return isAttacking;
    }

}
