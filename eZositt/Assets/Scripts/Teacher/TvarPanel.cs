using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TvarPanel : MonoBehaviour
{
    public void SetupPanel(Image img, RectTransform rect)
    {
        tvar = img;
        this.rect = rect;
    }
    public Sprite[] tvary;
    public Image tvar;
    public RectTransform rect;
    public void SetTvar(int id)
    {
        tvar.sprite = tvary[id];
    }
}
