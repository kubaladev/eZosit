using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableObject : GeneratedObject, IPointerDownHandler
{
    public List<Texture2D> imgFace;
    public int correctId = 0;
    public int currentId = 0;
    public bool editor=false;
    public override void Initialize(SerializedObject data)
    {
        base.Initialize(data);
        imgFace = new List<Texture2D>();
        foreach (SerializableTexture sex in data.additionalTextures)
        {
            Texture2D tex = new Texture2D(sex.texX, sex.texY);
            ImageConversion.LoadImage(tex, sex.texbytes);
            //Sprite mySprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), Vector2.one);
            imgFace.Add(tex);
        }
        img.texture = imgFace[currentId];

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
            img.texture = imgFace[currentId];
        }

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!editor)
        {
            SwitchImage();
        }
        else
        {
            if (eventData.button.ToString().Equals("Right"))
            {
                SwitchImage();
                ObjectModificator.Instance.klikaciPanel.UpdatePanel();
            }
        }

    }
    public void AddImage(Texture2D image)
    {
        imgFace.Add(image);
        img.texture = image;
    }
    public void RemoveImage(int id)
    {
        imgFace.RemoveAt(id);
        img.texture = imgFace[imgFace.Count - 1];
    }
}
