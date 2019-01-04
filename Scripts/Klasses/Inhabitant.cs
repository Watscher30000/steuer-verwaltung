using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class Inhabitant : IDuke
{
    // Die Attribute
    protected string name;
    protected int alter;
    protected float basetax;
    protected float tax;
    protected string rang;
    protected int since;

    // Die Get- und Set-Methoden
    public string Name
    {
        set
        {
            this.name = value;
        }

        get
        {
            return this.name;
        }
    }
    public int Alter
    {
        set
        {
            this.alter = value;
        }

        get
        {
            return this.alter;
        }
    }
    public float BaseTax
    {
        set
        {
            this.basetax = value;
        }
    }
    public float Tax
    {
        get
        {
            return this.tax;
        }
    }
    public string Rang
    {
        set
        {
            this.rang = value;

            if (value == new Settings().rankleader)
            {
                this.LeaderSince = new Settings().currentYear;
            }
        }

        get
        {
            return this.rang;
        }
    }
    public int LeaderSince
    {
        set
        {
            this.since = value;
        }

        get
        {
            return new Settings().currentYear - this.since;
        }
    }

    // Die Methoden

    // Gibt die Rasse des Bewohners zurück
    public string GetRace()
    {
        return "" + this.GetType();
    }
}
