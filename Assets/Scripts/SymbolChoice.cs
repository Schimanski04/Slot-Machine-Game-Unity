using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SymbolChoice : MonoBehaviour
{
    public Image SpriteRenderer;
    public Text description;
    public Text name_text;

    private Slot_machine slot_Machine;
    [SerializeField] private Symbol symbol;

    private void Awake()
    {
        slot_Machine = FindObjectOfType<Slot_machine>();
    }

    public void GenerateSymbol()
    {
        int random = 0;
        bool unique = false;

        while (!unique)
        {
            random = Random.Range(1, slot_Machine.all_symbols.Count);
            
            for (int i = 0; i < slot_Machine.duplicate_protection.Length; i++)
            {
                if (slot_Machine.duplicate_protection[i] == random)
                {
                    unique = false;
                    break;
                }
                unique = true;
            }
        }

        slot_Machine.duplicate_protection[slot_Machine.dp_index] = random;
        slot_Machine.dp_index++;
        if (slot_Machine.dp_index == slot_Machine.duplicate_protection.Length)
        {
            slot_Machine.dp_index = 0;
        }

        symbol = slot_Machine.all_symbols[random];



        if (symbol.CompareTag("Jade Golem"))
        {
            JadeDescriptionUpdate();
        }
        else
        {

        }
    }
}
