using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings
{
    // Dies ist der Pfad auf dem nach den Daten gesucht wird
    public string defaultPath = "Assets/Data/";
    // Die Grundsteuer
    public float baseTax = 2f;
    // Das momentane Jahr
    public int currentYear = 1272;
    // Suffix des Datums
    public string calender = " ndK.";
    // Name der Position des Anführers eines Stammes
    public string rankleader = "Häuptling";
    // Name der Position des Mitglieds eines Stammes
    public string ranknormal = "Mitglied";

    // Die Farbe für die Messanzeige der Elbenhaare
    public Color grau = new Color(0.57f, 0.53f, 0.48f);
    public Color GetColor(string color)
    {
        switch (color)
        {
            case "grau":
                return this.grau;
        }

        return new Color(0f, 0f, 0f, 0f);
    }
}
