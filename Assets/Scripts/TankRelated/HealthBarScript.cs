using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{

    public Image healthCircle;

    public void UpdateHealthCircle(float currentValue, float maxValue)
    {
        healthCircle.fillAmount = currentValue / maxValue;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
