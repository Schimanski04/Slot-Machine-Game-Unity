using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerniManazer : MonoBehaviour
{
    public interface IIcon
    {
        int OnActivation();
    }

    //public List<object> inventory;
    //public [][] gameBoard;
    public CoinCounter coinCounterScript;
    public List<IIcon> inventory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        foreach (IIcon item in inventory) 
        {
            Debug.Log(item);
            int tempInt = item.OnActivation();
            coinCounterScript.coinCounter.text = (Int32.Parse(coinCounterScript.coinCounter.text) + tempInt).ToString();
        }
    }

}
