using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public int numberOfImps;

    public GameObject Imp;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfImps; i++){
            Instantiate(Imp, new Vector3(Random.Range(transform.position.x-3.5f, transform.position.x+3.5f),  
                Random.Range(transform.position.y-3f, transform.position.y+3f), -1f), 
                Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
