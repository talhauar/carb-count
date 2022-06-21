using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class UserInfosData : Saveable<UserInfosData>
{
    public string Name = " ";
    public string Surname = " ";
    public int Age = 0;
    public float Weight = 0;
    public float Height = 0;
    public int carbPerDay = 0;
    public bool isMale = true;

    public bool InfosValid()
    {
        if (Name.Length < 2) return false;
        if (Surname.Length < 2) return false;
        if (Age < 1) return false;
        if (Weight < 1) return false;
        if (Height < 1) return false;
        return true;
    }
}
