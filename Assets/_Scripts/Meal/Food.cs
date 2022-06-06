using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Food
{
    public Sprite FoodImage;
    public string FoodName;
    public float CarbPerGram;
    public float CaloriePerGram;

    public List<FoodAmountType> enabledAmountTypes = new List<FoodAmountType>();
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