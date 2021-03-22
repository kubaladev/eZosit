using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
}

