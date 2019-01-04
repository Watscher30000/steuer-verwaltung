using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class Elb : Inhabitant
{
    // Die Attribute
    private float haar;
    private string haarfarbe;
    private string einheit;

    // Der Konstruktor
    public Elb(string name, int alter, float basetax, float haar, string einheit = "cm", string haarfarbe = "grau", string rang = "")
    {
        this.Name = name;
        this.Alter = alter;
        this.BaseTax = basetax;
        this.Haar = haar;
        this.Einheit = einheit;
        this.Haarfarbe = haarfarbe;
        this.Rang = (rang == "" ? new Settings().ranknormal : rang);
    }

    // Die Get- und Set-Methoden
    public float Haar
    {
        set
        {
            this.haar = value;
        }

        get
        {
            return this.haar;
        }
    }
    public string Haarfarbe
    {
        set
        {
            this.haarfarbe = value;
        }

        get
        {
            return this.haarfarbe;
        }
    }
    public string Einheit
    {
        set
        {
            this.einheit = value;
        }

        get
        {
            return this.einheit;
        }
    }
    public new float Tax
    {
        get
        {
            return (this.alter / this.basetax) + this.Umrechnen("cm");
        }
    }

    // Die Methoden

    // Gibt die Haarlänge in der angegebenen Einheit zurück
    public float Umrechnen(string einheit)
    {
        if (einheit == this.einheit)
        {
            return this.haar;
        }
        else if(einheit == "inch" || einheit == "cm")
        {
            switch (einheit)
            {
                case "inch":
                    return this.haar * 0.393700787402f;

                case "cm":
                    return this.haar * 2.54f;

                default:
                    return 0;
            }
        }
        else
        {
            return 0;
        }
    }

    // Verändert die Haarlänge um den gegebenen Wert
    public float HaareSchneiden(float value)
    {
        this.haar -= (value < 0 ? -value : value);
        return this.haar;
    }
    public float HaareWachsenLassen(float value)
    {
        this.haar += (value < 0 ? -value : value);
        return this.haar;
    }

    // Gibt die Rasse des Bewohners zurück
    public new string GetRace()
    {
        return "" + this.GetType();
    }
}
