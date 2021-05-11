using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPanel : MonoBehaviour
{
    public Toggle toggle;
    public ObjectT objectT;
    public ItemSlot textObject;
    public void SetupPanel(ObjectT ot,  ItemSlot text)
    {
        objectT = ot;
        textObject = text;
        Color c = textObject.img.color;

        if (c.a == 0)
        {
            toggle.isOn = false;
        }
        else
        {
            toggle.isOn = true;
        }


    }
    public void SetBckground()
    {
        if (toggle.isOn)
        {
            Color c = textObject.img.color;
            c.a = 1;
            textObject.img.color = c;
        }
        else
        {
            Color c = textObject.img.color;
            c.a = 0;
            textObject.img.color = c;
        }

    }
}
