using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class Food
{
    public Texture2D FoodImage;
    public string FoodImageEncrypted;
    public string FoodName;
    
    // gram - mililitre - kilogram/1000 - lite / 1000
    public float CarbPerGram;
    
    // bardak - adet - porsiyon
    public float CarbPerSession;

    public List<FoodAmountType> enabledAmountTypes = new List<FoodAmountType>();

    public Texture2D GetTexture()
    {
        Texture2D tex = new Texture2D(256, 256);
        tex.LoadImage(Convert.FromBase64String(FoodImageEncrypted));
        return tex;
    }

    public FoodData ToFoodData()
    {
        FoodData foodData = new FoodData();

        string enc = Convert.ToBase64String(FoodImage.EncodeToPNG());

        FoodImageEncrypted = enc;
        foodData.Id = ObjectId.GenerateNewId();
        foodData.FoodImageEncrypted = enc;
        foodData.FoodName = FoodName;
        foodData.CarbPerGram = CarbPerGram;
        foodData.CarbPerSession = CarbPerSession;
        foodData.enabledAmountTypes = enabledAmountTypes;
        return foodData;
    }
}

[Serializable]
public class FoodData
{
    [BsonId]
    public ObjectId Id { get; set; }

    public string FoodName;
    public string FoodImageEncrypted;
    
    // gram - mililitre - kilogram/1000 - lite / 1000
    public float CarbPerGram;

    // bardak - adet - porsiyon
    public float CarbPerSession;

    public List<FoodAmountType> enabledAmountTypes = new List<FoodAmountType>();

    public Food ToFood()
    {
        Food food = new Food();
        Texture2D tex = new Texture2D(256, 256);
        tex.LoadImage(Convert.FromBase64String(FoodImageEncrypted));
        food.FoodImage = tex;
        food.FoodImageEncrypted = FoodImageEncrypted;
        food.FoodName = FoodName;
        food.CarbPerGram = CarbPerGram;
        food.CarbPerSession = CarbPerSession;
        food.enabledAmountTypes = enabledAmountTypes;
        return food;
    }
}

[Serializable]
public class FoodMeal
{
    public Food food;
    public FoodAmountType amountType;
    public int amount;

    public FoodMeal(int _amount, FoodAmountType _amountType, Food _food)
    {
        Debug.Log("FoodMeal: " + _amount + " " + _amountType);
        this.amount = _amount;
        this.amountType = _amountType;
        this.food = _food;
    }

    public string GetFoodUnitText()
    {
        switch (amountType)
        {
            case FoodAmountType.Gram:
                return "gr";
                break;
            case FoodAmountType.Kilogram:
                return "kg";
                break;
            case FoodAmountType.Mililitre:
                return "ml";
                break;
            case FoodAmountType.Litre:
                return "l";
                break;
            case FoodAmountType.Porsiyon:
                break;
            case FoodAmountType.Adet:
                break;
            case FoodAmountType.Bardak:
                break;
            default:
                break;
        }
        return "";
    }
}

public enum FoodAmountType
{
    Gram,
    Kilogram,
    Mililitre,
    Litre,
    Porsiyon,
    Adet,
    Bardak,
}