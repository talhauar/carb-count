using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class UserManager : AutoSingleton<UserManager>
{
    [SerializeField] private TMPro.TMP_InputField nameText;
    [SerializeField] private TMPro.TMP_InputField surNameText;
    [SerializeField] private TMPro.TMP_InputField ageText;
    [SerializeField] private TMPro.TMP_InputField weightText;
    [SerializeField] private TMPro.TMP_InputField heightText;
    [SerializeField] private GameObject maleSelectedImage;
    [SerializeField] private GameObject femaleSelectedImage;


    public void Start()
    {
        if (UserInfosData.Data.Name != "")
        {
            nameText.text = UserInfosData.Data.Name;
        }
        if (UserInfosData.Data.Surname != "")
        {
            surNameText.text = UserInfosData.Data.Surname;
        }
        if (UserInfosData.Data.Age != 0)
        {
            ageText.text = UserInfosData.Data.Age.ToString();
        }
        if (UserInfosData.Data.Weight != 0)
        {
            weightText.text = UserInfosData.Data.Weight.ToString();
        }
        if (UserInfosData.Data.Height != 0)
        {
            heightText.text = UserInfosData.Data.Height.ToString();
        }

        maleSelectedImage.SetActive(UserInfosData.Data.isMale);
        femaleSelectedImage.SetActive(!UserInfosData.Data.isMale);
    }

    public void SetUserData(UserInfosData userData)
    {
        UserInfosData.Data.Name = userData.Name;
        UserInfosData.Data.Surname = userData.Surname;
        UserInfosData.Data.Age = userData.Age;
        UserInfosData.Data.Weight = userData.Weight;
        UserInfosData.Data.Height = userData.Height;
        UserInfosData.Data.Save();
    }

    public void DeleteUserData()
    {
        UserInfosData.DeleteSave();
    }

    public void SetUserName(string name)
    {
        if (name == "") return;
        UserInfosData.Data.Name = name;
        UserInfosData.Data.Save();
    }

    public void SetUserSurname(string surname)
    {
        if (surname == "") return;        
        UserInfosData.Data.Surname = surname;
        UserInfosData.Data.Save();
    }
    
    public void SetUserAge(string age)
    {
        if (age == "") return;
        UserInfosData.Data.Age = int.Parse(age);
        UserInfosData.Data.Save();
    }

    public void SetUserWeight(string weight)
    {
        if (weight == "") return;
        UserInfosData.Data.Weight = int.Parse(weight);
        UserInfosData.Data.Save();
    }

    public void SetUserHeight(string height)
    {
        if (height == "") return;
        UserInfosData.Data.Height = int.Parse(height);
        UserInfosData.Data.Save();
    }

    public void OnMale()
    {
        UserInfosData.Data.isMale = true;
        UserInfosData.Data.Save();
        femaleSelectedImage.SetActive(false);
        maleSelectedImage.SetActive(true);
    }

    public void OnFemale()
    {
        UserInfosData.Data.isMale = false;
        UserInfosData.Data.Save();
        femaleSelectedImage.SetActive(true);
        maleSelectedImage.SetActive(false);
    }
}
