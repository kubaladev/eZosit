using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TvarPanel : MonoBehaviour
{
    public void SetupPanel(RawImage img, RectTransform rect,ObjectT objt)
    {
        this.objT = objt;
        tvar = img;
        this.rect = rect;
        SetButtonColor(colorBtn, img.color);
    }
    public Button colorBtn;
    public Texture2D[] tvary;
    private RawImage tvar;
    private RectTransform rect;
    private ObjectT objT;
    public ColorSelector colorSelector;

    public void UpdateColorBtn()
    {
        SetButtonColor(colorBtn, tvar.color);
    }
    public void SetButtonColor(Button b, Color c)
    {
        ColorBlock cb = b.colors;
        cb.normalColor = c;
        b.colors = cb;
    }
    public void OpenColorMenu()
    {
        colorSelector.SetupPanel(tvar);
    }
    public void SetTvar(int id)
    {
        tvar.texture = tvary[id];
    }
}
