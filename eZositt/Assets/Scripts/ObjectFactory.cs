using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFactory : MonoBehaviour
{
    public GameObject GenerationArea;
    public GameObject dragPref;
    public GameObject statPref;
    public GameObject clickPref;

    public void TestLoad()
    {
        GenerateScene(ImageSerializer.Instance.data.objects);
    }
    public void GenerateScene(SerializedObject[] data)
    {
        foreach(SerializedObject obj in data)
        {
            if (obj.type.Equals("Drag"))
            {
                GameObject go = Instantiate(dragPref, GenerationArea.transform);
                go.GetComponent<GeneratedObject>().Initialize(obj);
            }
            if (obj.type.Equals("Click"))
            {
                GameObject go = Instantiate(clickPref, GenerationArea.transform);
                go.GetComponent<GeneratedObject>().Initialize(obj);
            }
            if (obj.type.Equals("Stat"))
            {
                GameObject go = Instantiate(statPref, GenerationArea.transform);
                go.GetComponent<GeneratedObject>().Initialize(obj);
            }
        }
    }
}
