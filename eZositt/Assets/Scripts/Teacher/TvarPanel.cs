using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TvarPanel : MonoBehaviour
{
    public void SetupPanel(RawImage img, RectTransform rect,ObjectT objt,GeneratedObject go)
    {
        this.objT = objt;
        tvar = img;
        this.rect = rect;
        SetButtonColor(colorBtn, img.color);
        objectName.text = go.objectName;
        this.GO = go;
    }
    public Button colorBtn;
    public Texture2D[] tvary;
    private RawImage tvar;
    private RectTransform rect;
    private ObjectT objT;
    public ColorSelector colorSelector;
    public TMP_Text objectName;
    public GeneratedObject GO;

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
        GO.imageID = id;
        if (GO.pair != null)
        {
            GeneratedObject other=GO.pair.GetComponent<GeneratedObject>();
            other.img.texture = tvary[id];
            other.imageID = id;
        }
    }
}
