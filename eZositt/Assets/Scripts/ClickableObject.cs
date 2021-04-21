using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableObject : GeneratedObject, IPointerDownHandler
{
    public List<Sprite> imgFace;
    public int correctId = 0;
    private int currentId;
    public void SwitchImage()
    {
        if (currentId < imgFace.Count - 1)
        {
            currentId++;
        }
        else
        {
            currentId = 0;   
        }
        img.sprite = imgFace[currentId];
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Initialize();
        //SwitchImage();
    }
}
