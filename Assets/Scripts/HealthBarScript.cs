using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarScript : MonoBehaviour
{
    private float maxHealth;

    private float currentHealth;

    private PlayerStatus ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.Find("Player").GetComponent<PlayerStatus>();
        if (ps == null)
        {
            Debug.Log("Error: HealthBarScript cannot find object \"Player\".");
        }

        maxHealth = ps.GetMaxHealth();
        //Debug.Log("Health Bar's max health: " + maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        maxHealth = ps.GetMaxHealth();
        currentHealth = ps.GetCurrentHealth();

        //Debug.Log("Health Bar's current health: " + currentHealth);
    }
}
