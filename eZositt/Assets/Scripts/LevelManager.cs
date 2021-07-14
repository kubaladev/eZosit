using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
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
    public UIBounds xb = new UIBounds(-306f, 306f);
    public UIBounds yb = new UIBounds(-169f, 169f);
    public Texture2D CursorTexture;
    public Texture2D HandCursor;
    public Material wave;
    public Material empty;
    public DragDrop selectedObject = null;
    public GameObject midPanel;
    public bool control=false;
    public EndPanel hurrayPanel;
    public EndPanel sadPanel;
    public EndPanel nekontrolPanel;
    public CanvasGroup hide;
    public Button deleteBtn;

    public void HideLevel()
    {
        hide.gameObject.SetActive(true);
        hide.DOFade(1, 0.75f);
    }
    public void ShowLevel()
    {
        hide.DOFade(0, 0.75f);
        Invoke("HideHide", 0.75f);
    }
    public void HideHide()
    {
        hide.gameObject.SetActive(false);
    }
    private void Awake()
    {
        Cursor.SetCursor(CursorTexture,new Vector2(50f,20f), CursorMode.Auto);
    }
    public void Randomize()
    {
        ClickableObject[] coObjects = midPanel.GetComponentsInChildren<ClickableObject>();
        if (coObjects != null)
            foreach (ClickableObject co in coObjects)
            {
                co.currentId = Random.Range(0, co.imgFace.Count);
                co.img.texture = co.imgFace[co.currentId];
            }
    }
    public void Shuffle()
    {
        DragDrop[] dgObjects = midPanel.GetComponentsInChildren<DragDrop>();
        if (dgObjects != null)
            foreach (DragDrop rc in dgObjects)
            {
                int random = Random.Range(0, dgObjects.Length);
                Vector3 helpPos = new Vector3(rc.rectTransform.position.x, rc.rectTransform.position.y, rc.rectTransform.position.z);
                Vector3 nPos = new Vector3(dgObjects[random].rectTransform.position.x, dgObjects[random].rectTransform.position.y, dgObjects[random].rectTransform.position.z);
                rc.rectTransform.position = nPos;
                dgObjects[random].rectTransform.position = helpPos;


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
            if (dragObj.original)
            {
                if(deleteBtn.enabled)
                    deleteBtn.interactable = false;
            }
            else
            {
                if (deleteBtn.enabled)
                    deleteBtn.interactable = true;
            }
        }

        
    }
    public void UnselectObject()
    {
        if (selectedObject != null)
        {
            selectedObject?.GetComponent<DragDrop>().OnUnselectObject();
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
        HideLevel();
        Invoke("ReloadScene", 1f);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ReloadLevel()
    {
        foreach (Transform child in midPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        HideLevel();
        Invoke("LoadLevel", 1f);

    }
    public void CheckComplete()
    {
        if (!control)
        {
            nekontrolPanel.SetupPanel(0, 0);
            return;
        }
        int contextPoints = 0;
        int contextMaxPoints = 0;
        ItemSlot[] itemSlots = midPanel.GetComponentsInChildren<ItemSlot>();
        if (itemSlots != null)
        {
            foreach(ItemSlot itemSlot in itemSlots)
            {
                if (!itemSlot.notCounted)
                {
                    contextMaxPoints++;
                    if (itemSlot.empty || itemSlot.negative)
                    {

                    }
                    else
                    {
                        contextPoints++;
                    }
                }

            }
        }
        ClickableObject[] clickableObjects = midPanel.GetComponentsInChildren<ClickableObject>();
        if (clickableObjects != null)
        {
            foreach (ClickableObject clickableObject in clickableObjects)
            {
                contextMaxPoints++;
                if (clickableObject.correctId == clickableObject.currentId)
                {
                    contextPoints++;
                }
            }
        }
        if (contextPoints >= contextMaxPoints)
        {
            hurrayPanel.SetupPanel(contextPoints, contextMaxPoints);
            SoundManager.Instance.PlaySound(5);
        }
        else
        {
            sadPanel.SetupPanel(contextPoints, contextMaxPoints);
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
    public void LoadLevel()
    {
        ObjectFactory.Instance.TestLoad();
    }
}

