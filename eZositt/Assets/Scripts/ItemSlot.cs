
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : GeneratedObject, IDropHandler {
    public float scaleOffsetX;
    public float scaleOffsetY;
    public int imageID=0;
    public int contextID = 0;
    RectTransform place;
    protected override void Awake()
    {
        base.Awake();
        place = this.GetComponent<RectTransform>();
        scaleOffsetX = 0.25f * place.localScale.x;
        scaleOffsetY = 0.25f * place.localScale.y;
    }
    public override void Initialize(SerializedObject data)
    {
        base.Initialize(data);
        imageID = data.imageID;
        contextID = data.contextID;
    }
    public void OnDrop(PointerEventData eventData) {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null) {
            RectTransform other = eventData.pointerDrag.GetComponent<RectTransform>();
            if (other.localScale.x <= place.localScale.x + scaleOffsetX &&
                other.localScale.y <= place.localScale.y + scaleOffsetY &&
                other.localScale.x >= place.localScale.x - scaleOffsetX &&
                other.localScale.y >= place.localScale.y - scaleOffsetY &&
                other.GetComponent<DragDrop>().imageID.Equals(imageID))
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                other.localScale = place.localScale;
                other.rotation = place.rotation;
                LevelManager.Instance.UnselectObject();
                DragDrop dg = other.GetComponent<DragDrop>();
                dg.DisableMovement();
                if (dg.contextID == contextID)
                {
                    LevelManager.Instance.contextPoints++;
                }
                SoundManager.Instance.PlaySound(2);
            }
            else
            {
                //other.GetComponent<DragDrop>().ResetLastPosition();
                //LevelManager.Instance.UnselectObject();
                SoundManager.Instance.PlaySound(4);

            }
           
           
        }
    }

}
