using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    public bool isHome = false;
    // Start is called before the first frame update
    void Start()
    {

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

}
