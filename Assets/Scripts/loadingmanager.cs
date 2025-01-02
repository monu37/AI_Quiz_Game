using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class loadingmanager : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] float Speed;
    [SerializeField] float min, max;
    bool isload;
    private void Awake()
    {
        slider.minValue = min;
        slider.maxValue = max;
        slider.value = min;
        isload = true;
    }

    private void Update()
    {
        if (isload)
        {
            if (slider.value <= max)
            {
                slider.value += Time.deltaTime * Speed;

                if (slider.value >= max)
                {
                    SceneManager.LoadScene("lobby");
                    isload = false;
                }
            }
        }
        
    }
}
