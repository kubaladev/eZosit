using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ObjectT:MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GenObjectType typ;
    public PrefabType prefabTyp;
    public CanvasGroup canvasGroup;
    Canvas canvas;
    RectTransform rectTransform;
    Image icon;
    public RawImage img;
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
    public void FadeOut()
    {
        canvasGroup.DOFade(0, 0.4f);
        rectTransform.DOScale(Vector3.zero, 0.4f);
        Destroy(this.gameObject, 0.41f);
    }
    public void Duplicate()
    {
        if (rectTransform.localPosition.x < 250f)
        {
            GameObject go = Instantiate(this.gameObject, this.transform.parent);

            RectTransform gorc = go.GetComponent<RectTransform>();
            Vector3 scaledGO = gorc.localScale;
            gorc.localScale = Vector3.zero;
            gorc.DOMoveX(gorc.position.x + 1f, 0.5f);
            gorc.DOScale(scaledGO, 0.5f);

            ObjectModificator.Instance.SelectObject(go.GetComponent<GeneratedObject>(),go.GetComponent<ObjectT>());
            SoundManager.Instance.PlaySound(0);
        }

        
    }
}
