using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class loadingscript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI LoadingText;

    private void Awake()
    {
        if (LoadingText == null)
        {

            LoadingText = GetComponent<TextMeshProUGUI>();
        }
    }

    private void OnEnable()
    {
        StartCoroutine(load());
    }

    IEnumerator load()
    {
        yield return new WaitForSeconds(.2f);

        LoadingText.text = "Loading.";
        
        yield return new WaitForSeconds(.2f);

        LoadingText.text = "Loading..";
        
        yield return new WaitForSeconds(.2f);

        LoadingText.text = "Loading...";

        StartCoroutine(load());

    }
}
