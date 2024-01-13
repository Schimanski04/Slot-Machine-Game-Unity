using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Symbol_Display : MonoBehaviour
{
    private SpriteRenderer SpriteRenderer;
    private TextMeshPro value_text;

    [SerializeField] Sprite empty_sprite;

    public int symbol_round_value;
    private Symbol mysymbol;

    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        value_text = GetComponentInChildren<TextMeshPro>();
    }

    public void Assign_Symbol(Symbol symbol)
    {
        mysymbol = symbol;
        symbol_round_value = symbol.base_value;
        symbol.current_position = new int[] { Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y) };
        symbol.current_base = this;

        Update_sprite();
    }

    public void Update_sprite()
    {
        if (mysymbol == null)
        {
            SpriteRenderer.sprite = empty_sprite;
            return;
        }
        SpriteRenderer.sprite = mysymbol.Sprite;
    }
}
