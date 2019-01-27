using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{   
    private float upgradeHealthValue = 100f;

    private float maxHealth = 100f;
    private float curHealth = 100f;

    private bool isAlive = true;

    private DungeonJuiceScript djs;
    
    private HealthBarScript hbs;
    public GameObject deathScreen;
    
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
        if(!isAlive) {
            if(Input.GetButtonDown("Restart")) {
                Scene loadedLevel = SceneManager.GetActiveScene ();
                SceneManager.LoadScene (loadedLevel.buildIndex);
            }
        }
        
        if(curHealth <= 0) {
            Kill();
        }
    }

    private void Kill() {
        isAlive = false;
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        deathScreen.SetActive(true);
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public void SetMaxHealth(float newMaxHealth)
    {
        maxHealth = newMaxHealth;
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
