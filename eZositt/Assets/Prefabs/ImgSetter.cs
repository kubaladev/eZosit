using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImgSetter : MonoBehaviour
{
    public Image img;
    public void SetImg(Sprite sprite)
    {
        img.sprite = sprite;
    }
}
