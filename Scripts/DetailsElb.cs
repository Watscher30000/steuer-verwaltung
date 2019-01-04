using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailsElb : MonoBehaviour
{
    // Diese Liste speichert die Indexe des Stammes und des Zwerges für den Aufruf und die Speicherung der Daten
    private int[] current;

    // Das sind die Unity GameObjecte die dieses Script manipuliert
    private GameObject n;
    private GameObject editName;
    private GameObject age;
    private GameObject editAge;
    private GameObject hairlength;
    private GameObject rank;
    private GameObject modvalue;
    private GameObject add;
    private GameObject remove;
    private GameObject length;
    private GameObject a;
    private GameObject b;
    private GameObject c;

    // Die Methode die die Daten dieses Scriptes setzt und vom Controller einmal aufgerufen werden sollte
    public void Initial()
    {
        n = this.gameObject.GetComponentsInChildren<InputField>()[0].gameObject;
        editName = n.GetComponentInChildren<Button>().gameObject;
        age = this.gameObject.GetComponentsInChildren<InputField>()[1].gameObject;
        editAge = age.GetComponentInChildren<Button>().gameObject;
        hairlength = this.gameObject.GetComponentsInChildren<Text>()[4].gameObject;
        rank = this.gameObject.GetComponentsInChildren<Text>()[5].gameObject;
        modvalue = this.gameObject.GetComponentsInChildren<InputField>()[2].gameObject;
        add = this.gameObject.GetComponentsInChildren<Button>()[2].gameObject;
        remove = this.gameObject.GetComponentsInChildren<Button>()[3].gameObject;
        length = this.gameObject.GetComponentsInChildren<Image>()[11].gameObject;
        a = length.GetComponentsInChildren<Text>()[0].gameObject;
        b = length.GetComponentsInChildren<Text>()[1].gameObject;
        c = length.GetComponentsInChildren<Text>()[2].gameObject;
    }

    // Diese Methode ist eine Unity-Methode die einmal pro Frame aufgerufen wird
    public void Update()
    {
        if (n.GetComponent<InputField>().text == "" && editName.GetComponent<Button>().interactable)
        {
            editName.GetComponent<Button>().interactable = false;
        }
        else if (n.GetComponent<InputField>().text != "" && !editName.GetComponent<Button>().interactable)
        {
            editName.GetComponent<Button>().interactable = true;
        }

        if (age.GetComponent<InputField>().text == "" && editAge.GetComponent<Button>().interactable)
        {
            editAge.GetComponent<Button>().interactable = false;
        }
        else if (age.GetComponent<InputField>().text != "" && !editAge.GetComponent<Button>().interactable)
        {
            editAge.GetComponent<Button>().interactable = true;
        }

        if (modvalue.GetComponent<InputField>().text == "" && add.GetComponent<Button>().interactable)
        {
            add.GetComponent<Button>().interactable = false;
        }
        else if (modvalue.GetComponent<InputField>().text != "")
        {
            if (float.Parse(modvalue.GetComponent<InputField>().text) > ((Elb)this.gameObject.GetComponentInParent<Controller>().GetClans()[current[0]].GetBewohner()[current[1]]).Haar)
            {
                add.GetComponent<Button>().interactable = false;
            }
            else if (float.Parse(modvalue.GetComponent<InputField>().text) <= ((Elb)this.gameObject.GetComponentInParent<Controller>().GetClans()[current[0]].GetBewohner()[current[1]]).Haar)
            {
                add.GetComponent<Button>().interactable = true;
            }
        }

        if (modvalue.GetComponent<InputField>().text == "" && remove.GetComponent<Button>().interactable)
        {
            remove.GetComponent<Button>().interactable = false;
        }
        else if (modvalue.GetComponent<InputField>().text != "" && !remove.GetComponent<Button>().interactable)
        {
            remove.GetComponent<Button>().interactable = true;
        }
    }

    // Zeigt die ausgewählten Daten an
    public void SetDetails(string name)
    {

        for (int i = 0; i < this.gameObject.GetComponentInParent<Controller>().GetClans().Count; i++)
        {
            for (int j = 0; j < this.gameObject.GetComponentInParent<Controller>().GetClans()[i].GetBewohner().Count; j++)
            {
                if (name == this.gameObject.GetComponentInParent<Controller>().GetClans()[i].GetBewohner()[j].Name)
                {
                    current = new int[2] { i, j };
                    break;
                }
            }
        }

        this.n.GetComponent<InputField>().text = this.gameObject.GetComponentInParent<Controller>().GetClans()[current[0]].GetBewohner()[current[1]].Name;
        this.age.GetComponent<InputField>().text = this.gameObject.GetComponentInParent<Controller>().GetClans()[current[0]].GetBewohner()[current[1]].Alter + " Years";
        this.rank.GetComponent<Text>().text = this.gameObject.GetComponentInParent<Controller>().GetClans()[current[0]].GetBewohner()[current[1]].Rang;
        DisplayHair();
    }

    // Füllt die Messanzeige für das Haar des Elben
    public void DisplayHair()
    {
        float value = ((Elb)this.gameObject.GetComponentInParent<Controller>().GetClans()[current[0]].GetBewohner()[current[1]]).Umrechnen("cm");
        string einheit = " cm";

        float max = 100;
        float min = 0;

        while (value > max)
        {
            max += 100;
        }

        length.GetComponent<Image>().color = new Settings().GetColor(((Elb)this.gameObject.GetComponentInParent<Controller>().GetClans()[current[0]].GetBewohner()[current[1]]).Haarfarbe);
        length.GetComponent<Image>().fillAmount = (Mathf.Round((value * 100f) / max) / 100f);

        if ((max / 2) >= 100)
        {
            max = max / 100;
            einheit = " m";
        }

        a.GetComponent<Text>().text = min + einheit;
        b.GetComponent<Text>().text = (max / 2) + einheit;
        c.GetComponent<Text>().text = max + einheit;

        hairlength.GetComponent<Text>().text = ((Elb)this.gameObject.GetComponentInParent<Controller>().GetClans()[current[0]].GetBewohner()[current[1]]).Haar + " " + ((Elb)this.gameObject.GetComponentInParent<Controller>().GetClans()[current[0]].GetBewohner()[current[1]]).Einheit;
    }

    // Modifiziert den Namen des Elben
    public void EditName()
    {
        this.gameObject.GetComponentInParent<Controller>().GetClans()[current[0]].GetBewohner()[current[1]].Name = n.GetComponent<InputField>().text;
    }

    // Modifiziert das Alter des Elben
    public void EditAlter()
    {
        this.gameObject.GetComponentInParent<Controller>().GetClans()[current[0]].GetBewohner()[current[1]].Alter = int.Parse(age.GetComponent<InputField>().text);
    }

    // Verändert die Haarlänge
    public void ModHair(string mod)
    {
        float value = float.Parse(modvalue.GetComponent<InputField>().text);

        if (value < 0)
        {
            value = -value;
        }

        if (mod == "-")
        {
            ((Elb)this.gameObject.GetComponentInParent<Controller>().GetClans()[current[0]].GetBewohner()[current[1]]).Haar -= value;
        }
        else
        {
            ((Elb)this.gameObject.GetComponentInParent<Controller>().GetClans()[current[0]].GetBewohner()[current[1]]).Haar += value;
        }

        DisplayHair();
    }

    // Geht zurück in die Overview
    public void Back()
    {
        this.gameObject.GetComponentInParent<Controller>().SaveData();
        this.gameObject.GetComponentInParent<Controller>().Backright();
    }
}
