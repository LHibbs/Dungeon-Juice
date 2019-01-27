using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private float upgradeHealthValue = 100f;

    private float maxHealth = 100f;
    private float curHealth = 100f;

    private DungeonJuiceScript djs;
    private HealthBarScript hbs;
    
    // Start is called before the first frame update
    void Start()
    {
        djs = GameObject.Find("PlayerStatsObject").GetComponent<DungeonJuiceScript>();
        if (djs == null)
        {
            Debug.Log("Error: PlayerStatus could not find object \"DungeonJuiceScript\".");
        }

        hbs = GameObject.Find("PlayerStatsObject").GetComponent<HealthBarScript>();
        if (hbs == null)
        {
            Debug.Log("Error: PlayerStatus could not find object \"HealthBarScript\".");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Health: " + curHealth);

        if (Input.GetMouseButtonDown(1))
        {
            Upgrade();
        }
    }

    private void Upgrade()
    {
        if (djs.GetDungeonJuice() >= 50f) 
        {
            djs.AddToDungeonJuice(-50f);
            maxHealth += upgradeHealthValue;
            curHealth += upgradeHealthValue;
        }
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetCurrentHealth()
    {
        return curHealth;
    }

    public void TakeDamage(float damage) {
        if(!gameObject.GetComponent<PlayerMovement>().IsAttacking())
            curHealth -= damage;

    }
}
