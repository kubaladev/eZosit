using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObjectT:MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GenObjectType typ;
    public PrefabType prefabTyp;
    Canvas canvas;
    RectTransform rectTransform;
    Image icon;
    public void SetTyp(GenObjectType typ, Sprite iconSprite)
    {
        this.typ = typ;
        icon.sprite = iconSprite;
        
    }
    private void Awake()
    {
        icon = GetComponentInChildren<ImgSetter>().img;
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        LevelManager.Instance.ChangeCursor(true);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        LevelManager.Instance.ChangeCursor(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ObjectModificator.Instance.SelectObject(GetComponent<GeneratedObject>(), this);
    }

    public bool CheckBounds(PointerEventData eventData)
    {
        if ((eventData.delta.x / canvas.scaleFactor + rectTransform.anchoredPosition.x > LevelManager.Instance.xb.p) ||
                (eventData.delta.x / canvas.scaleFactor + rectTransform.anchoredPosition.x < LevelManager.Instance.xb.n) ||
                (eventData.delta.y / canvas.scaleFactor + rectTransform.anchoredPosition.y > LevelManager.Instance.yb.p) ||
                (eventData.delta.y / canvas.scaleFactor + rectTransform.anchoredPosition.y < LevelManager.Instance.yb.n))
            return false;
        else
            return true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (CheckBounds(eventData))
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }
}
