using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Utilities;

[CreateAssetMenu(menuName = "ScriptableObjects/Singletons/TableName")]
public class TableNameManager : ScriptableSingleton<TableNameManager>
{
    public string DatabaseTable;
    public string UserDataTable;
    public string GameSessionTable;
    public string MealHistoryTable;
    public string AllFoodsTable;
}