using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Show_Inventory : MonoBehaviour
{
    Slot_machine slot_Machine;
    [SerializeField]
    public TextMeshProUGUI inventory;
    public TextMeshProUGUI inventory_heading;


    void Start()
    {
        slot_Machine = FindObjectOfType<Slot_machine>();
    }

    public void Display_symbol_list()
    {
        List<string> unique_symbol_list = new List<string>();
        List<int> symbol_frequency_list = new List<int>();

        inventory.text = "";

        foreach (Symbol symbol in slot_Machine.symbol_list)
        {
            if (!unique_symbol_list.Contains(symbol.symbol_name))
            {
                unique_symbol_list.Add(symbol.symbol_name);
                symbol_frequency_list.Add(1);
            }
            else
            {
                int i = unique_symbol_list.IndexOf(symbol.name);
                symbol_frequency_list[index]++;
            }
        }

        int j = 0;
        for (int i = 0; i < unique_symbol_list.Count; i++)
        {
            j++;

            inventory.text += unique_symbol_list[i] + " x" + symbol_frequency_list[i] + "\n";
            
            if (j == 6)
            {
                inventory.text += "  <voffset=0.35em><sprite name=\"" + unique_symbol_list[i].Remove(unique_symbol_list[i].Length-7) + "\"></voffset>X " + symbol_frequency_list[i] + ", ";
                j = 0;
            }
        }

        inventory_heading.text = "Inventory: (" + slot_Machine.symbol_list.Count + ")";
    }
}
