using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    private PlayerStatus ps;

    public Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        ps = GameObject.Find("Player").GetComponent<PlayerStatus>();
        if (ps == null)
        {
            Debug.Log("Error: HealthBarScript cannot find object \"Player\".");
        }

        healthSlider.maxValue = ps.GetMaxHealth();
        //Debug.Log("Health Bar's max health: " + maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.maxValue = ps.GetMaxHealth();
        healthSlider.value = ps.GetCurrentHealth();

        //Debug.Log("Health Bar's current health: " + currentHealth);
    }
}
