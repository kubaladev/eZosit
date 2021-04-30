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
    }
    public Texture2D[] tvary;
    private RawImage tvar;
    private RectTransform rect;
    private ObjectT objT;
    public void Delete()
    {
        objT.FadeOut();
        HidePanel();
    }
    public void Duplicate()
    {
        objT.Duplicate();
    }
    public void HidePanel()
    {
        this.gameObject.SetActive(false);
    }
    public void SetTvar(int id)
    {
        tvar.texture = tvary[id];
    }
}
