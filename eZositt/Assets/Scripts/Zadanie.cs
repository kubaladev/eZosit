using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Zadanie : MonoBehaviour
{
    public TMP_InputField input;
    public void SetZadanie()
    {
        ImageSerializer.Instance.zadanie=input.text;
    }
}
