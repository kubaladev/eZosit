using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImgSetter : MonoBehaviour
{
    public Image img;
    Quaternion rotation;
    private void Awake()
    {
        rotation = transform.rotation;
    }
    public void SetImg(Sprite sprite)
    {
        img.sprite = sprite;
    }
    private void LateUpdate()
    {
        transform.rotation = rotation;
    }
}
