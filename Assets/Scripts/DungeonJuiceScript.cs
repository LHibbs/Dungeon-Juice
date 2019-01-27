using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonJuiceScript : MonoBehaviour
{
    public Slider dungeonJuiceSlider;
    private float dungeonJuice;

    // Start is called before the first frame update
    void Start()
    {
        dungeonJuice = 0f;
        dungeonJuiceSlider.maxValue = 100f;
    }

    public void ChangeDungeonJuiceSliderValue(float newValue)
    {
        dungeonJuiceSlider.value = newValue;
    }

    public float GetDungeonJuiceSliderValue() 
    {
        return dungeonJuiceSlider.value;
    }

    public void AddToDungeonJuice(float changeAmount) {
        dungeonJuice += changeAmount;
        if(dungeonJuice < 0)
            dungeonJuice = 0;
        if(dungeonJuice > 100) {
            dungeonJuice = 100;
        }
        Debug.Log("Current DJ: " + dungeonJuice);

        ChangeDungeonJuiceSliderValue(dungeonJuice);
    }

    public float GetDungeonJuice() {
        return dungeonJuice;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AddToDungeonJuice(10f);
        }
    }
}
