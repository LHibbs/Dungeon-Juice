using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    public bool debug = false;

    private GameObject[] lightLevels;
    public AudioSource beginningMusic;
    public AudioSource endingMusic;
    public AudioSource deathSound;
    public AudioSource upgrade;
    public AudioSource oof;
    private int lightLevelIndex = 0;
    private float upgradeHealthValue = 100f;
    private bool deathSoundPlayed = false;
    private float maxHealth = 100f;
    private float curHealth = 100f;

    private bool isAlive = true;
    private bool isFrozen = false;

    private float musicFadeRate = .01f;
    private bool FadeOutBeginningMusic = false;
    private bool FadeInEndMusic = false;
    private DungeonJuiceScript djs;
    
    private HealthBarScript hbs;
    public GameObject deathScreen;
    
    // Start is called before the first frame update
    void Start()
    {
        beginningMusic.Play();
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
        if(debug) {
            curHealth = 100f;
        }
        if(!isAlive || isFrozen) {
            if(Input.GetButtonDown("Restart")) {
                Scene loadedLevel = SceneManager.GetActiveScene ();
                SceneManager.LoadScene (loadedLevel.buildIndex);
            }
        }
        if(!isFrozen) {
        
            if(curHealth <= 0) {
                Kill();
            }
        }
        if(FadeOutBeginningMusic)
        {
            beginningMusic.volume -= musicFadeRate;  
            if(beginningMusic.volume <= 0)
            {
                FadeOutBeginningMusic = false;
                FadeInEndMusic = true;
            }
        }
        if(FadeInEndMusic)
        {
            if(!endingMusic.isPlaying)
            {
                endingMusic.Play();
                endingMusic.volume = 0f;
            }
            endingMusic.volume += musicFadeRate;  
            if(endingMusic.volume >= 1)
            {
                FadeInEndMusic = false;
            }
        }
    }


    private void Kill() {
        if(!deathSoundPlayed){
            endingMusic.Stop();
            beginningMusic.Stop();
            deathSound.Play();
            deathSoundPlayed = true;
        }
        isAlive = false;
        gameObject.GetComponent<PlayerMovement>().enabled = false;
        deathScreen.SetActive(true);
    }

    public void Freeze() {
        isFrozen = true;
        gameObject.GetComponent<PlayerMovement>().enabled = false;
    }

    public void UnFreeze() {
        isFrozen = false;
        gameObject.GetComponent<PlayerMovement>().enabled = true;
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

    public void MusicFadeOut()
    {
        FadeOutBeginningMusic = true;
    }
    public void TakeDamage(float damage) {
        if(damage > 1){
            if(isAlive)
                oof.Play();
        }
        if(!gameObject.GetComponent<PlayerMovement>().IsAttacking() && !isFrozen || damage < 0)
            curHealth -= Mathf.Abs(damage);

    }
}
