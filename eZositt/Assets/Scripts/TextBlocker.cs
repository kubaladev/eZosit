using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextBlocker : MonoBehaviour
{
    private void Awake()
    {
        TMP_InputField input = GetComponent<TMP_InputField>();
        input.interactable = false;
        input.text = GetComponent<ItemSlot>().text;
    }
}
