using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUi : MonoBehaviour
{
    [SerializeField] Slider healthSlider;


    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth.onHealthChange += UpdateHealthSlider;

    }

    void UpdateHealthSlider(int val)
    {
        if (healthSlider != null)
        {
            healthSlider.value = val;
        }
    }

    void OnDisable()
    {
        PlayerHealth.onHealthChange -= UpdateHealthSlider;
    }
}
