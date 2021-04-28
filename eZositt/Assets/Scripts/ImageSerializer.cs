﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ImageSerializer : Singleton<ImageSerializer>
{
    public DataFile data;
    public GameObject serializationArea;
    public void LoadData()
    {
        string text = File.ReadAllText(@"d:\test.json");
        DataFile df = JsonUtility.FromJson<DataFile>(text);
        data = df;
    }
    public void SerializeScene()
    {
        GeneratedObject[] data = serializationArea.GetComponentsInChildren<GeneratedObject>();
        List<SerializedObject> objects = new List<SerializedObject>();
        foreach (GeneratedObject obj in data)
        {
            objects.Add(new SerializedObject(obj));
        }
        DataFile df = new DataFile();
        df.objects = objects.ToArray();
        df.SaveData();
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
        
        Texture2D tex = obj.img.sprite.texture;
        texX = tex.width;
        texY = tex.height;
        sizeDelta = obj.rectTransform.sizeDelta;
        texbytes = ImageConversion.EncodeToPNG(tex);
        if(obj is ClickableObject)
        {
            ClickableObject co = (ClickableObject)obj;
            additionalTextures = new List<SerializableTexture>();
            foreach (Sprite s in co.imgFace)
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
}

[System.Serializable]
public class SerializableTexture
{
    public SerializableTexture(Sprite s)
    {
        Texture2D tex = s.texture;
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
    public void SaveData()
    {
        string text = JsonUtility.ToJson(this);
        File.WriteAllText(@"d:\test.json", text);
    }
}