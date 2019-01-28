using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    public GameObject darkness;
    public GameObject playerStatus;

    void OnTriggerEnter2D(Collider2D coll) {
        if(coll.tag == "Player") {
            playerStatus.GetComponent<PlayerStatus>().Freeze();
            darkness.GetComponent<Darkness>().Shrink();

        }

    }
}
