using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    private GameObject[] lightLevels;
    private int lightLevelIndex = 0;
    private float upgradeHealthValue = 100f;

    private float maxHealth = 100f;
    private float curHealth = 100f;

    private bool isAlive = true;

    private DungeonJuiceScript djs;

    private Darkness ds;
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
        if(!isAlive) {
            if(Input.GetButtonDown("Restart")) {
                Scene loadedLevel = SceneManager.GetActiveScene ();
                SceneManager.LoadScene (loadedLevel.buildIndex);
            }
        }
        
        if(curHealth <= 0) {
            Kill();
        }

        if (Input.GetMouseButtonDown(1)) {
            Upgrade();
        }
    }

    private void Kill() {
        isAlive = false;
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        deathScreen.SetActive(true);
    }


    private void Upgrade()
    {
        if (djs.GetDungeonJuice() >= 50f && lightLevelIndex < 5) 
        {
            djs.AddToDungeonJuice(-50f);
            ds.AddMultiplier(-.3f);

            lightLevels[lightLevelIndex].SetActive(true);
            lightLevelIndex++;
        }
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
