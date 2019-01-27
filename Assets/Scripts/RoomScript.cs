using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public bool isHome = false;
    // Start is called before the first frame update
    public int numberOfImps;

    public GameObject Imp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void SpawnEnemies() {
        for (int i = 0; i < numberOfImps; i++){
            Instantiate(Imp, new Vector3(Random.Range(transform.position.x-2.5f, transform.position.x+2.5f),  
                Random.Range(transform.position.y-2f, transform.position.y+2f), -2f), 
                Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetHomeStatus() {
        return isHome;
    }

    public void SetHomeStatus(bool status) {
        isHome = status;
    }

    private void OnTriggerEnter2D(Collider2D coll) {
        if (coll.tag == "Player" && !isHome) {
            Debug.Log("Player detected.");
            SpawnEnemies();
        }
    }

}
