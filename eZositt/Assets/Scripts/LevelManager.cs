using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public struct UIBounds
{
    public float n;
    public float p;
    public UIBounds(float n, float p)
    {
        this.n = n;
        this.p = p;
    }
}
public class LevelManager : Singleton<LevelManager>
{
    public UIBounds xb = new UIBounds(-300f, 300f);
    public UIBounds yb = new UIBounds(-160f, 160f);
    public Texture2D CursorTexture;
    public Texture2D HandCursor;
    public Material wave;
    public Material empty;
    public DragDrop selectedObject = null;
    private void Awake()
    {
        Cursor.SetCursor(CursorTexture,new Vector2(50f,20f), CursorMode.Auto);
    }
    public void ChangeCursor(bool hand)
    {
        if (hand)
        {
            Cursor.SetCursor(HandCursor, new Vector2(30f, 20f), CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(CursorTexture, new Vector2(50f, 20f), CursorMode.Auto);
        }
    }
    public void SelectObject(DragDrop dragObj)
    {
        selectedObject = dragObj;
        selectedObject.img.material = wave;
        
    }
    public void UnselectObject()
    {
        if (selectedObject != null)
        {
            selectedObject.img.material = empty;
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            UnselectObject();
        }
    }
}

