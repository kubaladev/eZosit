using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ImageSerializer : Singleton<ImageSerializer>
{
    public DataFile data;
    public GameObject serializationArea;
    public string zadanie="";
    public void LoadData(string path)
    {
        string text = File.ReadAllText(path);
        DataFile df = JsonUtility.FromJson<DataFile>(text);
        data = df;
        ObjectFactory.Instance.TestLoad();
    }
    public void SerializeScene()
    {
        GeneratedObject[] data = serializationArea.GetComponentsInChildren<GeneratedObject>();
        List<SerializedObject> objects = new List<SerializedObject>();
        Debug.Log("Number of objects serialized: " + data.Length);
        foreach (GeneratedObject obj in data)
        {
            objects.Add(new SerializedObject(obj));
        }
        DataFile df = new DataFile();
        df.objects = objects.ToArray();
        df.zadanie = zadanie;
        FileLoader.Instance.SaveJsonObject(df.SaveData());
    }
}
[System.Serializable]
public class SerializedObject
{
    public SerializedObject(GeneratedObject obj)
    {
        position = obj.rectTransform.position;
        scale = obj.rectTransform.localScale;
        rotation = obj.rectTransform.localRotation;
        type = obj.type;
        
        Texture2D tex = (Texture2D)obj.img.texture;
        texX = tex.width;
        texY = tex.height;
        rawRect = obj.img.uvRect;
        sizeDelta = obj.rectTransform.sizeDelta;
        texbytes = ImageConversion.EncodeToPNG(tex);
        if(obj is ClickableObject)
        {
            ClickableObject co = (ClickableObject)obj;
            additionalTextures = new List<SerializableTexture>();
            foreach (Texture2D s in co.imgFace)
            {
                additionalTextures.Add(new SerializableTexture(s));
            }
        }
        if (obj is DragDrop)
        {
            DragDrop dg = (DragDrop)obj;
            imageID = dg.imageID;
            contextID = dg.contextID;
            color = dg.img.color;
        }
        if (obj is ItemSlot)
        {
            ItemSlot dg = (ItemSlot)obj;
            imageID = dg.imageID;
            contextID = dg.contextID;
        }

    }
    [SerializeField]
    public string type;
    [SerializeField]
    public Vector3 position;
    [SerializeField]
    public Vector3 scale;
    [SerializeField]
    public Quaternion rotation;

    [SerializeField]
    public Vector2 sizeDelta;

    [SerializeField]
    public int texX;
    [SerializeField]
    public int texY;
    [SerializeField]
    public byte[] texbytes;
    [SerializeField]
    public List<SerializableTexture> additionalTextures;

    [SerializeField]
    public int contextID;
    [SerializeField]
    public int imageID;

    [SerializeField]
    public Color color;

    [SerializeField]
    public Rect rawRect;

}

[System.Serializable]
public class SerializableTexture
{
    public SerializableTexture(Texture2D s)
    {
        Texture2D tex = s;
        texX = tex.width;
        texY = tex.height;
        texbytes = ImageConversion.EncodeToPNG(tex);
    }
    [SerializeField]
    public int texX;
    [SerializeField]
    public int texY;
    [SerializeField]
    public byte[] texbytes;
}


[System.Serializable]
public class DataFile
{
    [SerializeField]
    public SerializedObject[] objects;
    [SerializeField]
    public string zadanie;
    public string SaveData()
    {
        return JsonUtility.ToJson(this);
    }
}