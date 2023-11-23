using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    public void UpdatePlayerHealth(float max, float current)
    {
        _slider.value = current / max;
    }
}
