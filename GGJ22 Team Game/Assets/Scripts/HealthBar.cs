using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;

    public void SetMaxHealth(int h)
    {
        slider.maxValue = h;
        slider.value = h;
    }
    public void SetHealth(int h)
    {
        slider.value = h;
    }

    public void AddHealth(int h)
    {
        slider.value += h;
    }
}
