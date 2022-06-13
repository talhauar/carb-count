using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FoodElement : MonoBehaviour
{
    [SerializeField] private RawImage foodImage;
    [SerializeField] private TMP_Text foodName;
    [SerializeField] private TMP_Text carbs;
    [SerializeField] private TMP_InputField foodAmountInput;
    [SerializeField] private TMP_Dropdown amountTypeDropDown;
    [SerializeField] private List<GameObject> mealDisables;
    [SerializeField] private List<GameObject> mealEnables;
    [SerializeField] private Button addToHistoryButton;
    [SerializeField] private List<TMP_Dropdown.OptionData> dropdownOptions;

    private string _amountTextAdd = "g";
    private int _amount = 100;
    public Food _food;
    private FoodMeal _meal;


    public void SetupFood(Food food)
    {
        _food = food;
        foodImage.texture = food.GetTexture();
        foodName.text = food.FoodName;
        ResetDropDown();
    }

    public void SetupMeal(FoodMeal meal)
    {
        _meal = meal;
        _food = meal.food;
        _amount = meal.amount;
        foodName.text = _food.FoodName;
        foodImage.texture = _food.GetTexture();
        amountTypeDropDown.value = (int)meal.amountType;
        OnAmountTypeChanged((int)meal.amountType);
        OnAmountChanged(meal.amount.ToString());
        foodAmountInput.interactable = false;
        amountTypeDropDown.interactable = false;
        foreach (var mealDisable in mealDisables) mealDisable.SetActive(false);
        foreach (var mealEnable in mealEnables) mealEnable.SetActive(true);
    }

    public void OnAmountChanged(string newAmount)
    {
        _amount = int.Parse(GetNumbers(newAmount));
        foodAmountInput.text = _amount.ToString() + " " + _amountTextAdd;
        carbs.text = CarbCalculator.CarbCount(TextToAmountType(amountTypeDropDown.captionText.text), _amount, _food.CarbPerGram, _food.CarbPerSession).ToString() + " g";
    }

    public void ResetDropDown()
    {
        int counter = -1;
        amountTypeDropDown.options.Clear();
        foreach (var option in dropdownOptions)
        {
            counter++;
            if (!_food.enabledAmountTypes.Contains((FoodAmountType) counter)) continue;
            amountTypeDropDown.options.Add(option);
        }
        amountTypeDropDown.options.Add(dropdownOptions[dropdownOptions.Count-1]);
        amountTypeDropDown.value = dropdownOptions.Count;
        carbs.text = "-";
    }

    private static string GetNumbers(string input)
    {
        return new string(input.Where(c => char.IsDigit(c)).ToArray());
    }

    public void OnAmountTypeChanged(int type)
    {
        Debug.Log("OnAmountTypeChanged: " + type);
        amountTypeDropDown.template.gameObject.SetActive(false);

        if (type < amountTypeDropDown.options.Count-1)
        {
            foodAmountInput.gameObject.SetActive(true);
            addToHistoryButton.interactable = true;

            switch (TextToAmountType(amountTypeDropDown.captionText.text))
            {
                case FoodAmountType.Gram:
                    foodAmountInput.contentType = TMP_InputField.ContentType.IntegerNumber;
                    foodAmountInput.keyboardType = TouchScreenKeyboardType.NumberPad;
                    _amountTextAdd = " g";
                    foodAmountInput.text = "100 g";
                    _amount = 100;
                    break;
                case FoodAmountType.Kilogram:
                    foodAmountInput.contentType = TMP_InputField.ContentType.IntegerNumber;
                    foodAmountInput.keyboardType = TouchScreenKeyboardType.NumberPad;
                    _amountTextAdd = " kg";
                    foodAmountInput.text = "1 kg";
                    _amount = 1;
                    break;
                case FoodAmountType.Mililitre:
                    foodAmountInput.contentType = TMP_InputField.ContentType.IntegerNumber;
                    foodAmountInput.keyboardType = TouchScreenKeyboardType.NumberPad;
                    _amountTextAdd = " ml";
                    foodAmountInput.text = "100 ml";
                    _amount = 100;
                    break;
                case FoodAmountType.Litre:
                    foodAmountInput.contentType = TMP_InputField.ContentType.IntegerNumber;
                    foodAmountInput.keyboardType = TouchScreenKeyboardType.NumberPad;
                    _amountTextAdd = " L";
                    foodAmountInput.text = "1 L";
                    _amount = 1;
                    break;
                case FoodAmountType.Porsiyon:
                    foodAmountInput.contentType = TMP_InputField.ContentType.IntegerNumber;
                    foodAmountInput.keyboardType = TouchScreenKeyboardType.NumberPad;
                    _amountTextAdd = " ";
                    foodAmountInput.text = "1";
                    _amount = 1;
                    break;
                case FoodAmountType.Adet:
                    foodAmountInput.contentType = TMP_InputField.ContentType.IntegerNumber;
                    foodAmountInput.keyboardType = TouchScreenKeyboardType.NumberPad;
                    _amountTextAdd = " ";
                    foodAmountInput.text = "1";
                    _amount = 1;
                    break;
                case FoodAmountType.Bardak:
                    foodAmountInput.contentType = TMP_InputField.ContentType.IntegerNumber;
                    foodAmountInput.keyboardType = TouchScreenKeyboardType.NumberPad;
                    _amountTextAdd = " ";
                    foodAmountInput.text = "1";
                    _amount = 1;
                    break;
                default:
                    break;
            }
            carbs.text = CarbCalculator.CarbCount(TextToAmountType(amountTypeDropDown.captionText.text), _amount, _food.CarbPerGram, _food.CarbPerSession).ToString() + " g";
        }
        else
        {
            addToHistoryButton.interactable = false;
            foodAmountInput.gameObject.SetActive(false);
            carbs.text = "-";
        }
    }

    private FoodAmountType TextToAmountType(string text)
    {
        foreach (var option in dropdownOptions)
        {
            if (option.text == text)
            {
                return (FoodAmountType)dropdownOptions.IndexOf(option);
            }
        }
        return FoodAmountType.Gram;
    }

    public void AddToHistory()
    {
        MealData.Data.FoodHistory.Add(new FoodMeal(_amount, TextToAmountType(amountTypeDropDown.captionText.text), _food));
        MealData.Data.Save();
        ResetDropDown();
    }

    public void RemoveFromHistory()
    {
        MealData.Data.FoodHistory.Remove(_meal);
        MealData.Data.Save();
        GameObject.Destroy(gameObject);
    }
}
