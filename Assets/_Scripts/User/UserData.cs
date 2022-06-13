using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class UserInfosData : Saveable<UserInfosData>
{
    public string Name = " ";
    public string Surname = " ";
    public int Age;
    public float Weight;
    public float Height;
    public int carbPerDay = 2000;
}
