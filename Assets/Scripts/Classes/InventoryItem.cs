using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : ScriptableObject
{
    public Sprite sprite;
    public Vector3 Scale;
    public string ItemName;
    public int Cost;
    public int Strength;
    public int Defense;
}
