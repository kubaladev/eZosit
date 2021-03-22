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

public class DragDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {

    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 startingPos;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("OnBeginDrag");
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        startingPos = rectTransform.position;
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
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        LevelManager.Instance.ChangeCursor(true);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        LevelManager.Instance.ChangeCursor(false);
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
}
