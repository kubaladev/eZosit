using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextChanger : MonoBehaviour
{
    public TMP_InputField input;
    ItemSlot itemslot;
    public void Start()
    {
        itemslot = GetComponent<ItemSlot>();
    }
   public void SetText()
    {
        if (itemslot != null)
        {
            itemslot.text = input.text;
        }
    }
}
