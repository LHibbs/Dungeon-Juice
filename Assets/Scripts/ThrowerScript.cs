using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerScript : MonoBehaviour
{
    public GameObject swordPrefab;
    private GameObject swordInstance;
    private GameObject player;

    public GameObject juicePrefab;

    private float timer = 0f;
    private float attackTime;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        attackTime = Random.Range(0.5f, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < attackTime) {
            timer += Time.deltaTime;
        } else {
            swordInstance = Instantiate(swordPrefab, transform.position, Quaternion.identity) as GameObject;
            swordInstance.GetComponent<SwordScript>().SetVector(player.transform.position - transform.position);
            timer = 0f;
            attackTime = Random.Range(1.25f, 1.75f);
        }
        
    }

    public void Kill() {
        Instantiate(juicePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
