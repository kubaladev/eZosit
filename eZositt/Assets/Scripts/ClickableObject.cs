using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableObject : GeneratedObject, IPointerDownHandler
{
    public List<Sprite> imgFace;
    public int correctId = 0;
    private int currentId;
    public override void Initialize(SerializedObject data)
    {
        base.Initialize(data);
        imgFace = new List<Sprite>();
        foreach (SerializableTexture sex in data.additionalTextures)
        {
            Texture2D tex = new Texture2D(sex.texX, sex.texY);
            ImageConversion.LoadImage(tex, sex.texbytes);
            Sprite mySprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), Vector2.one);
            imgFace.Add(mySprite);
        }

    }
    public void SwitchImage()
    {
        if (imgFace != null)
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

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        
        SwitchImage();
    }
}
