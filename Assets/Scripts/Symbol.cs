using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symbol : MonoBehaviour
{
    public Sprite Sprite;
    public string description;
    public int base_value;
    public bool marked_for_destruction;
    public int Value_when_destroyed;



    public int[] current_position = new int[2] { 0, 0 };

    public Symbol_Display current_base;

    private void OnDestroy()
    {
        if (current_base != null)
        {
            current_base.symbol_round_value += Value_when_destroyed;
        }
    }
}
