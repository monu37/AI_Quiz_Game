using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseonoff : MonoBehaviour
{
    
    public void pointerenter()
    {
        transform.localScale += new Vector3(0.1f, .1f, .1f);
    }

    public void pointerexit()
    {
        transform.localScale -= new Vector3(0.1f, .1f, .1f);
    }


}
