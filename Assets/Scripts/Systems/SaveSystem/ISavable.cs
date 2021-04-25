using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

interface ISavable
{
    SaveDataBase SaveData();
    void LoadData(SaveDataBase saveData);
}

[Serializable]
public class SaveDataBase
{
    public string Name;
}