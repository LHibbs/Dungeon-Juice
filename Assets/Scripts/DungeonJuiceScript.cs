using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonJuiceScript : MonoBehaviour
{
    public Slider dungeonJuiceSlider;

    // Start is called before the first frame update
    void Start()
    {
        dungeonJuiceSlider.maxValue = 100f;
    }

    public void ChangeDungeonJuiceValue(float newValue)
    {
        dungeonJuiceSlider.value += newValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dungeonJuiceSlider.value += 10f;
        }
    }
}
