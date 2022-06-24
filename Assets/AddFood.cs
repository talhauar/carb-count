using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleFileBrowser;
using System.IO;
using System;
using UnityEngine.UI;
using TMPro;

public class AddFood : MonoBehaviour
{
    [SerializeField] TMP_InputField foodName;
    [SerializeField] TMP_InputField carbPerGram;
    [SerializeField] RawImage LoadedImage;

    [SerializeField] string popupHeader;
    [SerializeField] string popupBodyText;

    [SerializeField] string errorPopupHeader;
    [SerializeField] string errorPopupBodyText;

    private Texture2D loadedTexture = null;

    public void OnEnable()
    {
        ResetFields();
    }

    public void ResetFields()
    {
        loadedTexture = null;
        LoadedImage.texture = null;
        foodName.text = "";
        carbPerGram.text = "0";
    }

    public void OnLoadFood()
    {
        Debug.Log($"name: {foodName.text}, {float.Parse(carbPerGram.text)}");
        if (loadedTexture != null && foodName.text.Length > 1 && float.Parse(carbPerGram.text) > 0)
        {
            FoodData newFood = new FoodData();
            newFood.FoodName = foodName.text;
            newFood.CarbPerGram = float.Parse(carbPerGram.text) / 100f;
            newFood.CarbPerSession = float.Parse(carbPerGram.text);
            newFood.FoodImageEncrypted = Convert.ToBase64String(loadedTexture.EncodeToPNG());
            newFood.enabledAmountTypes = new List<FoodAmountType> { FoodAmountType.Gram, FoodAmountType.Kilogram }; 
            DatabaseManager.Instance.InsertRecord<FoodData>(TableNameManager.Instance.AllFoodsTable, newFood);
            Debug.Log("Food Loaded To Db");
            PopupManager.Instance.CreatePopup(popupHeader, popupBodyText, "Tamam", CloseAddFood);
        }
        else
        {
            PopupManager.Instance.CreatePopup(errorPopupHeader, errorPopupBodyText, "Tamam", null);
        }
    }

    public void CloseAddFood()
    {
        gameObject.SetActive(false);
    }

    public void OnChooseImage()
    {
        FileBrowser.SetFilters(false, new FileBrowser.Filter("Images", ".jpg", ".png",".jpeg",".webp"));
        FileBrowser.SetDefaultFilter("Images");
        if (FileBrowser.CheckPermission() != FileBrowser.Permission.Granted) FileBrowser.RequestPermission();
        if (FileBrowser.CheckPermission() != FileBrowser.Permission.Granted) return;
        FileBrowser.ShowLoadDialog(ImageLoaded, ImageLoadFail, FileBrowser.PickMode.Files);
    }

    public void ImageLoaded(string[] paths)
    {
        if (paths.Length < 1) return;
        string path = paths[0];
        Debug.Log(path);
        loadedTexture = LoadPNG(path);
        LoadedImage.texture = loadedTexture;
    }

    public void ImageLoadFail()
    {
        Debug.LogError("Failed Image Choose");
    }

    public static Texture2D LoadPNG(string filePath)
    {

        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }
}
