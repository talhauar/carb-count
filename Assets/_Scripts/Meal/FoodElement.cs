using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FoodElement : MonoBehaviour
{
    [SerializeField] private Image foodImage;
    [SerializeField] private TMP_Text foodName;
    [SerializeField] private TMP_InputField foodAmountInput;
    [SerializeField] private TMP_Dropdown amountTypeDropDown;
    [SerializeField] private List<GameObject> mealDisables;

    private string _amountTextAdd = "g";
    private int _amount = 100;
    private Food _food;


    public void SetupFood(Food food)
    {
        _food = food;
        foodImage.sprite = food.FoodImage;
        foodName.text = food.FoodName;
    }

    public void SetupMeal(FoodMeal meal)
    {
        _food = meal.food;
        _amount = meal.amount;
        amountTypeDropDown.value = (int)meal.amountType;
        OnAmountTypeChanged((int)meal.amountType);
        OnAmountChanged(meal.amount.ToString());
        foodAmountInput.interactable = false;
        amountTypeDropDown.interactable = false;
        foreach (var mealDisable in mealDisables) mealDisable.SetActive(false);
    }

    public void OnAmountChanged(string newAmount)
    {
        _amount = int.Parse(GetNumbers(newAmount));
        foodAmountInput.text = _amount.ToString() + " " + _amountTextAdd;
    }

    private static string GetNumbers(string input)
    {
        return new string(input.Where(c => char.IsDigit(c)).ToArray());
    }

    public void OnAmountTypeChanged(int type)
    {
        amountTypeDropDown.template.gameObject.SetActive(false);
        switch ((FoodAmountType)type)
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
    }

    public void AddToHistory()
    {
        MealData.Data.FoodHistory.Add(new FoodMeal(_amount, (FoodAmountType)amountTypeDropDown.value, _food));
        MealData.Data.Save();
    }
}
