using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public CoinCounter coinCounterScript;
    // Start is called before the first frame update
    void Start()
    {
        OnActivation();
    }

    // Update is called once per frame
    void Update()
    {
         //OnActivation();
    }

    public int OnActivation()
    {
        return 1;
    }
   
}
