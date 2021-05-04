using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    public RectTransform[] shuffleArray;
    public int contextPoints = 0;
    public int contextMaxPoints = 0;
    private void Awake()
    {
        Cursor.SetCursor(CursorTexture,new Vector2(50f,20f), CursorMode.Auto);
    }
    private void Start()
    {
        foreach(RectTransform rc in shuffleArray)
        {
            int random = Random.Range(0, shuffleArray.Length);
            Vector3 helpPos = rc.position;
            rc.position = shuffleArray[random].position;
            shuffleArray[random].position = helpPos;

        }
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
        if (dragObj != selectedObject)
        {
            UnselectObject();
            selectedObject = dragObj;
            selectedObject?.GetComponent<DragDrop>().OnUnselectObject();
        }

        
    }
    public void UnselectObject()
    {
        if (selectedObject != null)
        {
            selectedObject.OnUnselectObject();
            selectedObject = null;
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            UnselectObject();
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void CheckComplete()
    {
        SoundManager.Instance.PlaySound(5);
        if (contextPoints >= contextMaxPoints)
        {
            
            Invoke("NextTask", 1f);
        }
        else
        {
            Invoke("Restart", 1f);
        }
    }
    public void NextTask()
    {
        //TODOFIX
        if (SceneManager.GetActiveScene().buildIndex + 1 < 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}

