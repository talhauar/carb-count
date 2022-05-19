using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class UserData : Saveable<UserData>
{
    public string Name = " ";
    public string Surname = " ";
    public int Age;
    public float Weight;
    public float Height;
}
