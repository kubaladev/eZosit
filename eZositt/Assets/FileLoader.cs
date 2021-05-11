using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFileBrowser;
using System;
using System.Text.RegularExpressions;
using System.IO;

public class FileLoader : Singleton<FileLoader>
{
    void Start()
    {
        FileBrowser.Filter[] filters=new FileBrowser.Filter[1];
        filters[0] = new FileBrowser.Filter("JSON files", ".json");
        FileBrowser.SetFilters(true, filters);
        FileBrowser.SetDefaultFilter(".json");
        FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");
        FileBrowser.AddQuickLink("Resources", @"Assets\Resources\", null);
    }
    public void OpenBrowser()
    {
        StartCoroutine(ShowLoadDialogCoroutine(@"Assets\Resources\","Načítaj úlohu","Úloha"));
    }
    public void OpenImageBrowser()
    {
        StartCoroutine(ShowLoadImageCoroutine(@"Assets\Resources\", "Načítaj obrázok", "obrázok"));
    }
    public void SaveJsonObject(string data)
    {
        StartCoroutine(SaveJsonCoroutine(data));
    }
    IEnumerator ShowLoadDialogCoroutine(string path, string tooltip,string type)
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: file, Initial path: default (Documents), Title: "Load File", submit button text: "Load"
        FileBrowser.Filter[] filters = new FileBrowser.Filter[1];
        filters[0] = new FileBrowser.Filter("JSON files", ".json");
        FileBrowser.SetFilters(true, filters);
        FileBrowser.SetDefaultFilter(".json");
        yield return FileBrowser.WaitForLoadDialog(false, path,tooltip,"Otvor");

        // Dialog is closed
        // Print whether a file is chosen (FileBrowser.Success)
        // and the path to the selected file (FileBrowser.Result) (null, if FileBrowser.Success is false)
        Debug.Log(FileBrowser.Success + " " + FileBrowser.Result);

        if (FileBrowser.Success)
        {
            // If a file was chosen, read its bytes via FileBrowserHelpers
            // Contrary to File.ReadAllBytes, this function works on Android 10+, as well
            //byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result)
            string fileName = FileBrowserHelpers.GetFilename(FileBrowser.Result);
            ImageSerializer.Instance.LoadData(FileBrowser.Result);
        
        }
    }
    IEnumerator ShowLoadImageCoroutine(string path, string tooltip, string type)
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: file, Initial path: default (Documents), Title: "Load File", submit button text: "Load"
        FileBrowser.Filter[] filters = new FileBrowser.Filter[2];
        filters[0] = new FileBrowser.Filter("PNG", ".png");
        filters[1] = new FileBrowser.Filter("JPG", ".jpg");
        FileBrowser.SetFilters(true, filters);
        FileBrowser.SetDefaultFilter(".png");
        yield return FileBrowser.WaitForLoadDialog(false, path, tooltip, "Nahraj");

        // Dialog is closed
        // Print whether a file is chosen (FileBrowser.Success)
        // and the path to the selected file (FileBrowser.Result) (null, if FileBrowser.Success is false)
        Debug.Log(FileBrowser.Success + " " + FileBrowser.Result);

        if (FileBrowser.Success)
        {
            ObjectModificator.Instance.LoadTexture(File.ReadAllBytes(FileBrowser.Result));

        }
    }
    IEnumerator SaveJsonCoroutine(string data)
    {
        FileBrowser.Filter[] filters = new FileBrowser.Filter[1];
        filters[0] = new FileBrowser.Filter("JSON files", ".json");
        FileBrowser.SetFilters(true, filters);
        FileBrowser.SetDefaultFilter(".json");
        yield return FileBrowser.WaitForSaveDialog(false, @"Assets\Resources\", "Uloz ulohu", "Uloz");
        if (FileBrowser.Success)
        {
            string path = FileBrowser.Result;
            string fileName = FileBrowserHelpers.GetFilename(FileBrowser.Result);
            File.WriteAllText(path, data);           
        }
    }


}
