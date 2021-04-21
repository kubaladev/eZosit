using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ImageSerializer : Singleton<ImageSerializer>
{
    public SerializeTexture st;
    
}
[System.Serializable]
public class SerializeTexture
{
    public SerializeTexture(GeneratedObject obj)
    {
        position = obj.rectTransform.position;
        scale = obj.rectTransform.localScale;
        rotation = obj.rectTransform.localRotation;
        width = obj.rectTransform.rect.width;
        height = obj.img.rectTransform.rect.height;
        Texture2D tex = obj.img.sprite.texture;
        texX = tex.width;
        texY = tex.height;
        texbytes = ImageConversion.EncodeToPNG(tex);
        string text = JsonUtility.ToJson(this);
        File.WriteAllText(@"d:\test.json", text);

    }
    [SerializeField]
    public Vector3 position;
    [SerializeField]
    public Vector3 scale;
    [SerializeField]
    public Quaternion rotation;

    [SerializeField]
    public float width;
    [SerializeField]
    public float height;

    [SerializeField]
    public int texX;
    [SerializeField]
    public int texY;
    [SerializeField]
    public byte[] texbytes;
}