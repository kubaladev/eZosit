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
            if (LevelManager.Instance.selectedObject.rectTransform.localPosition.x < 250f)
            {
                GameObject go = Instantiate(LevelManager.Instance.selectedObject.gameObject, LevelManager.Instance.selectedObject.transform.parent);
                
                RectTransform gorc = go.GetComponent<RectTransform>();       
                Vector3 scaledGO = gorc.localScale;
                gorc.localScale = Vector3.zero;
                gorc.DOMoveX(gorc.position.x + 1f, 0.5f);
                gorc.DOScale(scaledGO,0.5f);

                LevelManager.Instance.SelectObject(go.GetComponent<DragDrop>());
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
