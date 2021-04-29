using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GeneralPanel : MonoBehaviour
{
    public TMP_InputField vyskaInp;
    public TMP_InputField sirkaInp;
    private RectTransform selectedRT;
    public void SetupPanel(RectTransform rect)
    {
        selectedRT = rect;
        selectedRT.rect.Set(0, 0, 1, 1);
        vyskaInp.text = Mathf.RoundToInt(rect.rect.width).ToString();
        sirkaInp.text = Mathf.RoundToInt(rect.rect.height).ToString();
    }
    public void UpdateRectSizeFromText()
    {
        if(vyskaInp.text.Length>0 && sirkaInp.text.Length > 0)
        {
            selectedRT.sizeDelta= new Vector2(float.Parse(sirkaInp.text), float.Parse(vyskaInp.text));
        }
    }
}
