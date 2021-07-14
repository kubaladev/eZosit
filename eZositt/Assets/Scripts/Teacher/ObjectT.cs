using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

public class ObjectT:MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GenObjectType typ;
    public PrefabType prefabTyp;
    public CanvasGroup canvasGroup;
    Canvas canvas;
    RectTransform rectTransform;
    Image icon;
    public RawImage img;
    public Outline outline;
    public string velkost="";
    public void SetTyp(GenObjectType typ, Sprite iconSprite)
    {
        this.typ = typ;
        icon.sprite = iconSprite;
        
    }
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        icon = GetComponentInChildren<ImgSetter>().img;
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        img = GetComponent<RawImage>();
        outline = GetComponent<Outline>();
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
        if ((eventData.delta.x / canvas.scaleFactor + rectTransform.rect.xMax * rectTransform.localScale.x +rectTransform.anchoredPosition.x > LevelManager.Instance.xb.p) ||
                (eventData.delta.x / canvas.scaleFactor + rectTransform.rect.xMin * rectTransform.localScale.x + rectTransform.anchoredPosition.x < LevelManager.Instance.xb.n) ||
                (eventData.delta.y / canvas.scaleFactor + rectTransform.rect.yMax * rectTransform.localScale.x + rectTransform.anchoredPosition.y > LevelManager.Instance.yb.p) ||
                (eventData.delta.y / canvas.scaleFactor + rectTransform.rect.yMin * rectTransform.localScale.x + rectTransform.anchoredPosition.y < LevelManager.Instance.yb.n))
            return false;
        else
            return true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnUnselectObject()
    {
        outline.enabled = false;
        GeneratedObject GO=GetComponent<GeneratedObject>();
        if (GO.pair != null)
        {
            GO.pair.GetComponent<Outline>().enabled = false;
        }
  
    }
    public void OnSelectObject()
    {
        outline.enabled = true;
        UpdatePair();
    }
    public void UpdatePair()
    {

    }
    public void OnDrag(PointerEventData eventData)
    {
        if (CheckBounds(eventData))
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }
    public void FadeOut()
    {
        canvasGroup.DOFade(0, 0.4f);
        rectTransform.DOScale(Vector3.zero, 0.4f);
        if (typ.Equals(GenObjectType.Drag))
        {
            DragDrop dg = GetComponent<DragDrop>();
            if (dg != null)
            {
                ObjectSPawner.Instance.FindAndKillAllFriends(dg.contextID);
                Debug.Log("Removing");
            }
        }
        Destroy(this.gameObject, 0.45f);
    }
    public void Duplicate()
    {
        if (rectTransform.localPosition.x +rectTransform.sizeDelta.x*rectTransform.localScale.x<280)
        {
            GameObject go = Instantiate(this.gameObject, this.transform.parent);

            RectTransform gorc = go.GetComponent<RectTransform>();
            RawImage rawImage = go.GetComponent<RawImage>();
            if (rawImage != null)
            {
                rawImage.DOFade(0, 0f);
                rawImage.DOFade(1, 0.5f);
            }
            Vector3 scaledGO = gorc.localScale;
            //gorc.localScale = Vector3.zero;
            gorc.DOMoveX(gorc.position.x + (gorc.sizeDelta.x/43*this.gameObject.transform.localScale.x), 0.5f);
            //gorc.DOScale(scaledGO, 0.5f);
            GeneratedObject generatedObject = go.GetComponent<GeneratedObject>();
            ObjectT objectT = go.GetComponent<ObjectT>();
            if (generatedObject.type == "Drag")
            {
                ObjectSPawner.Instance.AssignId(generatedObject, objectT);
                generatedObject.pair = null;
            }
            
            if(generatedObject is ItemSlot)
            {
                gorc.transform.SetAsFirstSibling();
            }
            ObjectModificator.Instance.SelectObject(generatedObject,objectT);
            SoundManager.Instance.PlaySound(0);
        }
        UpdatePair();

        
    }
}
