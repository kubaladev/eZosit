using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InteractionManager : Singleton<InteractionManager>
{
    public float scaleSpeed=0.04f;
    public float rotationSpeed = 0.04f;
    public bool scale=false;
    bool rotate = false;
    public void Upscale()
    {
        if (scaleSpeed < 0)
        {
            scaleSpeed *= -1;
        }
        scale = true;
        

    }
    public void Downscale()
    {
        if (scaleSpeed > 0)
        {
            scaleSpeed *= -1;
        }
        scale = true;
    }
    public void RotateRight()
    {
        if (rotationSpeed > 0)
        {
            rotationSpeed *= -1;
        }
        rotate = true;
    }
    public void RotateLeft()
    {
        if (rotationSpeed < 0)
        {
            rotationSpeed *= -1;
        }
        rotate = true;
    }
    public void StopScale()
    {
        scale = false;
    }
    public void StopRotate()
    {
        rotate = false;
    }


    private void FixedUpdate()
    {
        if (scale)
        {
            if (LevelManager.Instance.selectedObject != null)
                LevelManager.Instance.selectedObject.Scale(scaleSpeed);
        }
        if (rotate)
        {
            if (LevelManager.Instance.selectedObject != null)
                LevelManager.Instance.selectedObject.Rotate(rotationSpeed);
        }
    }
    public void Duplicate()
    {
        if(LevelManager.Instance.selectedObject != null)
        {
            if (LevelManager.Instance.selectedObject.rectTransform.localPosition.x + LevelManager.Instance.selectedObject.rectTransform.sizeDelta.x * LevelManager.Instance.selectedObject.rectTransform.localScale.x < 280)
            {
                GameObject go = Instantiate(LevelManager.Instance.selectedObject.gameObject, LevelManager.Instance.selectedObject.transform.parent);
                DragDrop dg = go.GetComponent<DragDrop>();       
                Vector3 scaledGO = dg.rectTransform.localScale;
                dg.rectTransform.localScale = Vector3.zero;
                dg.rectTransform.DOMoveX(dg.rectTransform.position.x + (dg.rectTransform.sizeDelta.x / 43 * this.gameObject.transform.localScale.x), 0.5f);
                dg.rectTransform.DOScale(scaledGO,0.5f);
                dg.original = false;
                LevelManager.Instance.SelectObject(dg);
                SoundManager.Instance.PlaySound(0);
            }
            
        }
    }
    public void Delete()
    {
        if (LevelManager.Instance.selectedObject != null)
        {
            LevelManager.Instance.selectedObject.FadeOut();
            LevelManager.Instance.UnselectObject();
            SoundManager.Instance.PlaySound(1);
        }
    }

}
