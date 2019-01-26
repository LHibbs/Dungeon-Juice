using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    private PlayerStatus ps;

    public Slider healthSlider;
    public Image healthColor;

    private float r = 125f;
    private float g = 125f;
    private float b = 125f;

    private Color health = new Color(125f, 125f, 125f, 255f);

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

    public void UpdateHealthColor()
    {
        health = new Color(r += 25f, g += 25f, b += 25f, 255f);
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.maxValue = ps.GetMaxHealth();
        healthSlider.value = ps.GetCurrentHealth();
        healthColor.color = health;
        //Debug.Log("Health Bar's current health: " + currentHealth);
    }
}
