using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class Dwarf : Inhabitant
{
    // Die Attribute
    private float macht;
    private List<Weapon> waffen = new List<Weapon>();

    // Der Konstruktor
    public Dwarf(string name, int alter, float basetax, string rang = "")
    {
        this.Name = name;
        this.Alter = alter;
        this.BaseTax = basetax;
        this.Rang = (rang == "" ? new Settings().ranknormal : rang);
    }

    // Die Get- und Set-Methoden
    public new float Tax
    {
        get
        {
            return this.basetax * this.Macht;
        }
    }
    public float Macht
    {
        get
        {
            this.macht = 0;

            for (int i = 0; i < this.waffen.Count; i++)
            {
                this.macht += this.waffen[i].Macht;
            }

            return this.macht;
        }
    }

    // Die Methoden

    // Diese Methode fügt eine Waffe dem Zwerg hinzu.
    public void AddWaffe(Weapon input)
    {
        this.waffen.Add(input);
    }
    // Diese Methode nimmt dem Zwerg eine Waffe an einem bestimmten Index weg.
    public void RemoveWaffe(int index)
    {
        if (index < 0 || index >= this.waffen.Count)
        {
            return;
        }

        this.waffen.RemoveAt(index);
    }
    // Gibt die Waffen diese Zwerges zurück in einer Liste zurück.
    public List<Weapon> GetWaffen()
    {
        return this.waffen;
    }

    // Gibt die Rasse des Bewohners zurück
    public new string GetRace()
    {
        return "" + this.GetType();
    }
}
