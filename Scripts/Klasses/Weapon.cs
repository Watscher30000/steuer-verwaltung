using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class Weapon
{
    // Die Attribute
    private string title = "";
    private int macht = 0;

    // Der Konstruktor
    public Weapon(string title, int macht)
    {
        this.Title = title;
        this.Macht = macht;
    }

    // Die Get- und Set-Methoden
    public string Title
    {
        set
        {
            this.title = value;
        }

        get
        {
            return this.title;
        }
    }
    public int Macht
    {
        set
        {
            this.macht = value;
        }

        get
        {
            return this.macht;
        }
    }
}
