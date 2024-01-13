using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Slot_machine : MonoBehaviour
{
    public GameObject symbol_base;
    public List<Symbol> all_symbols;

    [HideInInspector] public int[] duplicate_protection = new int[3];
    [HideInInspector] public int dp_index = 0;
    [HideInInspector] public int jade_count = 1;

    public TextMeshProUGUI roundNumbertext;

    public static int money;
    private int roundsLeft;

    // list of all symbols on deck, including empty, list is now a list of game objects
    List<Symbol> symbol_list;

    List<int> roundvalueList;

    // array of the top 20 symbols in their custom positions
    public Symbol[,] slot_Grid_symbols = new Symbol[5, 4];

    // array of the game objects that display the sprites from the grid
    // they contain the symbol display script
    public GameObject[,] slot_Grid_Base_objects = new GameObject[5, 4];


    public Money_display money_Display;

    int round_money = 0;

    void Awake()
    {
        symbol_list = new List<Symbol>();
        money = 0;
        roundsLeft = 5;
        roundNumbertext.text = roundsLeft.ToString();

        for (int i = 0; i < 5; i++)
        {
            symbol_list.Add(Instantiate(all_symbols[i + 1]));
        }

        for (int i = 0; i < 20; i++)
        {
            symbol_list.Add(Instantiate(all_symbols[0]));
        }

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                slot_Grid_Base_objects[i, j] = Instantiate(symbol_base, transform);
                slot_Grid_Base_objects[i, j].transform.position = new Vector3(i, j);
            }
        }
    }


    public delegate void Activate_Synergy_Handler();

    public event Activate_Synergy_Handler Activate_Synergy;

    public void Spin()
    {
        round_money = 0;
        Disable_all_symbols();
        Shuffle_List();
        Assign_Symbols_To_Positions();

        OnActivate_Synergy();
        DestroyMarked();
        StartCoroutine(DestroyMarkedVisual());
    }

    void Spin_part2()
    {
        roundvalueList = new List<int>();
        Count_Money();
        roundvalueList.Sort();
        StartCoroutine(DisplayMoney());
    }

    private void Disable_all_symbols()
    {
        foreach (var symbol in symbol_list)
        {
            symbol.gameObject.SetActive(false);
        }
    }

    private void Shuffle_List()
    {
        System.Random random = new System.Random();
        int n = symbol_list.Count;

        while (n > 1)
        {
            n--;
            int k = random.Next(n);
            Symbol temp = symbol_list[k];
            symbol_list[k] = symbol_list[n];
            symbol_list[n] = temp;
        }
    }

    private void Spin_Animation()
    {
        
    }

    private void Count_Money()
    {
        GameObject current_Symbol_base;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {

                current_Symbol_base = slot_Grid_Base_objects[i, j];
                int value = current_Symbol_base.GetComponent<Symbol_Display>().symbol_round_value;
                round_money += value;

                if (!roundvalueList.Contains(value))
                {
                    roundvalueList.Add(value);
                }
            }
        }

        money += round_money;
    }

    private void DestroyMarked()
    {
        for (int i = symbol_list.Count - 1; i >= 0; i--)
        {
            if (symbol_list[i].marked_for_destruction)
            {
                Remove_Symbol_From_List(symbol_list[i]);
            }
        }
    }

    private IEnumerator DestroyMarkedVisual()
    {
        yield return new WaitForSeconds(1f);
        Symbol_Display current_symbol_Display;

        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (slot_Grid_symbols[i, j] == null)
                {
                    current_symbol_Display = slot_Grid_Base_objects[i, j].GetComponent<Symbol_Display>();
                    current_symbol_Display.Update_sprite();
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
        Spin_part2();
    }

    IEnumerator DisplayMoney()
    {
        foreach (int value in roundvalueList)
        {
            if (value != 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {

                        if (slot_Grid_Base_objects[i, j].GetComponent<Symbol_Display>().symbol_round_value == value)
                        {
                            slot_Grid_Base_objects[i, j].GetComponent<Symbol_Display>().StartCoroutine("DisplayRoundValue");
                        }
                    }
                }
                yield return new WaitForSeconds(0.5f);
            }
        }

        money_Display.Update_Money();
        yield return new WaitForSeconds(2f);
        FindObjectOfType<Shop_Activator>().ActivateShop();
        PayRentCheck();
        yield return null;
    }

    private void PayRentCheck()
    {
        roundsLeft--;
        if (roundsLeft == 0)
        {
            FindObjectOfType<Pay_Rent_Activator>().ActivatePayRent();
            roundsLeft = 5;
        }
        roundNumbertext.text = roundsLeft.ToString();
    }

    private void Assign_Symbols_To_Positions()
    {
        int k = 0;
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    symbol_list[k].gameObject.SetActive(true);

                    slot_Grid_symbols[i, j] = symbol_list[k];
                    slot_Grid_Base_objects[i, j].GetComponent<Symbol_Display>().Assign_Symbol(symbol_list[k]);
                    k++;
                }
            }
        }
    }

    void OnActivate_Synergy()
    {
        Activate_Synergy?.Invoke();
    }
}
