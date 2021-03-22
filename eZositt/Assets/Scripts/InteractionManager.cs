using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : Singleton<InteractionManager>
{
    public float scaleSpeed=0.04f;
    public bool scale=false;
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
    public void StopScale()
    {
        scale = false;
    }

    private void Update()
    {
        if (scale)
        {
            if (LevelManager.Instance.selectedObject != null)
                LevelManager.Instance.selectedObject.Scale(scaleSpeed);
        }
    }

}
