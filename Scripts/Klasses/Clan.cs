using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class Clan
{
    // Die Attribute
    private string name;
    private string seit;
    private float tax;
    private List<Inhabitant> bewohner = new List<Inhabitant>();
    private int leader = 0;

    // Der Konstruktor
    public Clan(string name, int seit)
    {
        this.Name = name;
        this.Seit = "" + seit;
    }

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
    public string Seit
    {
        set
        {
            this.seit = value + new Settings().calender;
        }

        get
        {
            return this.seit;
        }
    }
    public float Tax
    {
        get
        {
             this.tax = 0f;

            foreach (Inhabitant bewohner in this.bewohner)
            {
                switch (bewohner.GetRace())
                {
                    case "Dwarf":
                        this.tax += ((Dwarf)bewohner).Tax;
                        break;

                    case "Elb":
                        this.tax += ((Elb)bewohner).Tax;
                        break;

                    default:
                        this.tax += bewohner.Tax;
                        break;
                }

            }

            return this.tax;
        }
    }

    // Die Methoden

    // Fügt dem Stamm einen Zwerg hinzu
    public void AddBewohner(Inhabitant input)
    {
        this.bewohner.Add(input);
    }
    // Entfernt einen Zwerg aus dem Stamm nach Index
    public void RemoveBewohner(int index)
    {
        if (index < 0 || index >= this.bewohner.Count)
        {
            return;
        }

        this.bewohner.RemoveAt(index);
    }
    // Gibt eine Liste der Zwerge dieses Stammes zurück
    public List<Inhabitant> GetBewohner()
    {
        return this.bewohner;
    }

    // Setzt den index des Häuptlings des Stammes
    public void SetLeader(int index)
    {
        this.bewohner[leader].Rang = new Settings().ranknormal;
        this.leader = index;
        this.bewohner[leader].Rang = new Settings().rankleader;
    }
    // Gibt den Zwerg zurück der mit dem Häuptlings-Index übereinstimmt, wenn dies nicht der Fall ist wird nach dem Häupling gesucht
    public Inhabitant GetLeader()
    {
        if (this.bewohner[leader].Rang != new Settings().rankleader)
        {
            for (int i = 0; i < this.bewohner.Count; i++)
            {
                if (this.bewohner[i].Rang == new Settings().rankleader)
                {
                    this.SetLeader(i);
                    break;
                }
            }
        }

        return this.bewohner[leader];
    }
}
