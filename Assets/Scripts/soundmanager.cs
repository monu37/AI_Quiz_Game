using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundmanager : MonoBehaviour
{
    public static soundmanager instance;

    [SerializeField] AudioSource audiosource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);

        }
        else
        {
            Destroy(gameObject);
        }

    }
    public void clicksound()
    {
        audiosource.Play();
    }
}
