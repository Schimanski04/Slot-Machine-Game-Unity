using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    Slot_machine slot_Machine;
    Symbol Symbol;

    public List<string> symbols_that_multiply_me_by_2;

    private void Awake()
    {
        slot_Machine = FindObjectOfType<Slot_machine>();
        Symbol = GetComponent<Symbol>();
    }

    private void OnEnable()
    {
        slot_Machine.Activate_Synergy += Activate_Synergy;
    }

    private void OnDisable()
    {
        slot_Machine.Activate_Synergy -= Activate_Synergy;
    }

    void Activate_Synergy()
    {
        int i = Symbol.current_position[0];
        int j = Symbol.current_position[1];
        Symbol_Display current_base = Symbol.current_base.GetComponent<Symbol_Display>();

        for (int i1 = i - 1; i1 < i + 2; i1++)
        {
            if (i1 >= 0 && i1 < 5)
            {
                for (int j1 = j - 1; j1 < j + 2; j1++)
                {
                    if (j1 >= 0 && j1 < 4)
                    {
                        if (i1 != i || j1 != j)
                        {
                            foreach (var name in symbols_that_multiply_me_by_2)
                            {
                                if (slot_Machine.slot_Grid_symbols[i1, j1].name.Contains(name))
                                {
                                    current_base.symbol_round_value *= 2;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
