using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pay_Rent_Activator : MonoBehaviour
{
    [SerializeField] GameObject payRentPanel;
    [SerializeField] GameObject pay_button;
    [SerializeField] TextMeshProUGUI rent_due_text;
    [SerializeField] GameObject game_over_text;
    [SerializeField] GameObject You_Win_text;
    [SerializeField] GameObject Restart_button;

    public Money_display money_Display;

    [SerializeField] int[] prices;

    int price_index = 0;

    private void Start()
    {
        price_index = 0;
        rent_due_text.text = prices[price_index].ToString();
    }

    public void ActivatePayRent()
    {
        payRentPanel.SetActive(true);
        pay_button.GetComponentInChildren<TextMeshProUGUI>().text = "Pay: " + prices[price_index].ToString();

        if (Slot_machine.money < prices[price_index])
        {
            pay_button.SetActive(false);
            game_over_text.SetActive(true);
            Restart_button.SetActive(true);
        }
    }

    public void PayRent()
    {
        Slot_machine.money -= prices[price_index];
        money_Display.Update_Money();
        price_index++;
        if (price_index == prices.Length)
        {
            You_Win();
            return;
        }

        rent_due_text.text = prices[price_index].ToString();
    }

    void You_Win()
    {
        Restart_button.SetActive(true);
        You_Win_text.SetActive(true);
    }

    public void Restart_Game()
    {
        SceneManager.LoadScene(0);
    }
}
