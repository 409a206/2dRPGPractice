using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : ScriptableObject {
    public string Name;
    [Range(10, 100)]
    public int Age;
    [Popup("Imperial", "Independant", "Evil")]
    public string Faction;
    [Popup("Mayor", "Wizard", "Layabout")]
    public string Occupation;
    [Range(1, 10)]
    public int Level = 1;
    public int Health = 2;
    public int Strength = 1;
    public int Magic = 0;
    public int Defense = 0;
    public int Speed = 1;
    public int Damage = 1;
    public int Armor = 0;
    public int NoOfAttacks = 1;
    public string Weapon;
    public Vector2 Position;

    public void TakeDamage(int Amount) {
        Health -= Mathf.Clamp((Amount - Armor), 0, int.MaxValue);
    }

    public void Attack(Entity entity) {
        entity.TakeDamage(Strength);
    }
}
