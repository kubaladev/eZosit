/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {

    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 startingPos;
    public Image img;
    public bool active = true;
    private float maxScale;
    private float minScale;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        img = GetComponent<Image>();
        minScale = (rectTransform.localScale.x + rectTransform.localScale.y) * 0.25f;
        maxScale = (rectTransform.localScale.x + rectTransform.localScale.y) * 3.5f;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (active)
        {
            Debug.Log("OnBeginDrag");
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = false;
            startingPos = rectTransform.position;
            LevelManager.Instance.ChangeCursor(true);
            img.material = LevelManager.Instance.empty;
        }


    }

    public void OnDrag(PointerEventData eventData) {
        //Debug.Log("OnDrag");
        if (CheckBounds(eventData))
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        }
        else
        {
        }

    }

    public void OnEndDrag(PointerEventData eventData) {
        if (active)
        {
            Debug.Log("OnEndDrag");
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            img.material = LevelManager.Instance.wave;
        }


    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (active)
        {
            LevelManager.Instance.SelectObject(this);
        }

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (active)
        {
            LevelManager.Instance.ChangeCursor(false);
        }

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
    public void ResetLastPosition()
    {
        rectTransform.position = startingPos;
    }
    public void DisableMovement()
    {
        active = false;
        if (LevelManager.Instance.selectedObject == this)
        {
            LevelManager.Instance.UnselectObject();
        }
        img.material = LevelManager.Instance.empty;
    }
    public void Scale(float upscaleValue)
    {
        if ((upscaleValue < 0 || rectTransform.localScale.x + rectTransform.localScale.y < maxScale)&&
            (upscaleValue > 0 || rectTransform.localScale.x + rectTransform.localScale.y > minScale))
        {
            rectTransform.localScale = new Vector3(rectTransform.localScale.x + upscaleValue, rectTransform.localScale.y + upscaleValue, 1);
        }
       
      
    }
}
