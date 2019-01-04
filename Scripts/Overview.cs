using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Overview : MonoBehaviour
{
    // Dieses Array wird genutzt um die Detailansicht aufzubauen
    private string[] details = new string[2] {"", ""};

    // Das sind die Unity GameObjecte die dieses Script in die Listen einfügt
    private GameObject preClan;
    private GameObject preDwarf;

    // Das sind die Unity GameObjecte die dieses Script manipuliert
    private GameObject header;
    private GameObject value;
    private GameObject contentA;
    private GameObject contentB;
    private GameObject valuetax;
    private Button detbut;

    // Die Listen die in der Overview angezeigt werden
    private List<GameObject> clans = new List<GameObject>();
    private List<GameObject> bewohner = new List<GameObject>();

    // Die Methode die die Daten dieses Scriptes setzt und vom Controller einmal aufgerufen werden sollte
    public void Initial()
    {
        preClan = Resources.Load<GameObject>("Clan");
        preDwarf = Resources.Load<GameObject>("Dwarf");

        header = this.gameObject.GetComponentInChildren<Text>().gameObject;
        value = this.gameObject.GetComponentsInChildren<Text>()[2].gameObject;
        contentA = this.gameObject.GetComponentsInChildren<RectTransform>()[6].gameObject;
        contentB = this.gameObject.GetComponentsInChildren<RectTransform>()[12].gameObject;

        valuetax = this.gameObject.GetComponentInChildren<InputField>().gameObject;

        detbut = this.gameObject.GetComponentInChildren<Button>();

        SetTotalGold();
        SetBaseTax();
        DisplayClans();
    }

    // Diese Methode ist eine Unity-Methode die einmal pro Frame aufgerufen wird
    public void Update()
    {
        if (details[0] != "Dwarf" && details[0] != "Elb" && detbut.interactable == true)
        {
            detbut.interactable = false;
        }
        else if ((details[0] == "Dwarf" || details[0] == "Elb") && detbut.interactable == false)
        {
            detbut.interactable = true;
        }
    }

    // Setzt den String der Anrede auf der Overview
    public void SetCurrentUser()
    {
        header.GetComponent<Text>().text = "Willkommen,\n" + this.GetComponentInParent<Controller>().username;
    }

    // Zeigt die gesamten Steuereinzahlungen an
    public void SetTotalGold()
    {
        value.GetComponent<Text>().text = TaxToString(Mathf.RoundToInt(this.GetComponentInParent<Controller>().TotalTax() * 100f) / 100f) + " Gold";
    }

    public void SetBaseTax()
    {
        valuetax.GetComponent<InputField>().text = this.gameObject.GetComponentInParent<Controller>().settings.baseTax + " Gold";
    }

    // Hier wird die Stammes-Liste gefüllt
    public void DisplayClans()
    {
        foreach (GameObject element in this.clans)
        {
            GameObject.Destroy(element);
        }

        foreach (GameObject element in this.bewohner)
        {
            GameObject.Destroy(element);
        }

        this.clans.Clear();

        foreach (Clan clan in this.GetComponentInParent<Controller>().GetClans())
        {
            this.clans.Add(GameObject.Instantiate(preClan));
            this.clans[this.clans.Count - 1].transform.SetParent(contentA.transform);
            this.clans[this.clans.Count - 1].name = clan.Name;
            this.clans[this.clans.Count - 1].GetComponentsInChildren<Text>()[0].text = clan.Name;
            this.clans[this.clans.Count - 1].GetComponentsInChildren<Text>()[1].text = "" + clan.GetBewohner().Count;
            this.clans[this.clans.Count - 1].GetComponentsInChildren<Text>()[2].text = "" + (Mathf.RoundToInt(clan.Tax * 100f) / 100f) + " Gold";
            this.clans[this.clans.Count - 1].GetComponentsInChildren<Text>()[3].text = clan.Seit;
            this.clans[this.clans.Count - 1].GetComponent<Button>().onClick.AddListener(delegate {DisplayBewohner(clan.Name);});
        }
    }

    // Hier wird die Zwergen-Liste gefüllt
    public void DisplayBewohner(string name)
    {
        int index = 0;

        for (int i = 0; i < this.GetComponentInParent<Controller>().GetClans().Count; i++)
        {
            if (name == this.GetComponentInParent<Controller>().GetClans()[i].Name)
            {
                index = i;
                break;
            }
        }

        foreach (GameObject element in this.bewohner)
        {
            GameObject.Destroy(element);
        }

        this.bewohner.Clear();

        foreach (Inhabitant bewohner in this.GetComponentInParent<Controller>().GetClans()[index].GetBewohner())
        {
            this.bewohner.Add(GameObject.Instantiate(preDwarf));
            this.bewohner[this.bewohner.Count - 1].transform.SetParent(contentB.transform);
            this.bewohner[this.bewohner.Count - 1].name = bewohner.Name;
            this.bewohner[this.bewohner.Count - 1].GetComponentsInChildren<Text>()[0].text = bewohner.Name + " (" + bewohner.Alter + ")";

            if (bewohner.GetRace() == "Dwarf")
            {
                this.bewohner[this.bewohner.Count - 1].GetComponentsInChildren<Text>()[1].text = "" + ((Dwarf)bewohner).GetWaffen().Count;
                this.bewohner[this.bewohner.Count - 1].GetComponentsInChildren<Text>()[2].text = (Mathf.RoundToInt(((Dwarf)bewohner).Tax * 100f) / 100f) + " Gold";
            }
            else
            {
                this.bewohner[this.bewohner.Count - 1].GetComponentsInChildren<Text>()[1].text = "" + ((Elb)bewohner).Haar + " " + ((Elb)bewohner).Einheit;
                this.bewohner[this.bewohner.Count - 1].GetComponentsInChildren<Text>()[2].text = (Mathf.RoundToInt(((Elb)bewohner).Tax * 100f) / 100f) + " Gold";
                this.bewohner[this.bewohner.Count - 1].GetComponentsInChildren<Text>()[3].text = "Haarlänge:";
            }

            if (bewohner.Rang == new Settings().rankleader)
            {
                this.bewohner[this.bewohner.Count - 1].GetComponentsInChildren<Image>()[1].color = new Color(0.92f, 0f, 0f, 0.82f);
            }
            else
            {
                this.bewohner[this.bewohner.Count - 1].GetComponentsInChildren<Image>()[1].color = new Color(0.92f, 0f, 0f, 0f);
            }

            this.bewohner[this.bewohner.Count - 1].GetComponent<Button>().onClick.AddListener(delegate{SetDetails(bewohner.Name);});
        }

        details = new string[2] {"Clan", name};
    }

    // Setzt das "details"-Array
    public void SetDetails(string name)
    {
        for (int i = 0; i < this.gameObject.GetComponentInParent<Controller>().GetClans().Count; i++)
        {
            for (int j = 0; j < this.gameObject.GetComponentInParent<Controller>().GetClans()[i].GetBewohner().Count; j++)
            {
                if (name == this.gameObject.GetComponentInParent<Controller>().GetClans()[i].GetBewohner()[j].Name)
                {
                    details = new string[2]{this.gameObject.GetComponentInParent<Controller>().GetClans()[i].GetBewohner()[j].GetRace(), name};
                }
            }
        }
    }

    // Diese Methode ruft die Detailansicht-Methode im Controller auf
    public void GetDetails()
    {
        switch (this.details[0])
        {
            case "Clan":
                break;

            case "Dwarf":
                this.gameObject.GetComponentInParent<Controller>().Dwarfdetails(this.details[1]);
                break;

            case "Elb":
                this.gameObject.GetComponentInParent<Controller>().Elbdetails(this.details[1]);
                break;
        }
    }

    // Diese Methode modifiziert die Grundsteuer
    public void EditBaseTax()
    {
        if (float.Parse(valuetax.GetComponent<InputField>().text) >= 0f)
        {
            this.gameObject.GetComponentInParent<Controller>().BaseTax = float.Parse(valuetax.GetComponent<InputField>().text);
        }

        SetTotalGold();
        SetBaseTax();
        DisplayClans();
    }

    // Gibt einen String einer Zahl mit tausender Punkten zurück
    private string TaxToString(float num)
    {
        string text = "";
        string value = "" + num;

        if (num >= 1000000000)
        {
            if (num >= 100000000000)
            {
                text = "" + value[0] + value[1] + value[2] + "." + value[3] + value[4] + value[5] + "." + value[6] + value[7] + value[8] + "." + value[9] + value[10] + value[11];
            }
            else if (num >= 10000000000)
            {
                text = "" + value[0] + value[1] + "." + value[2] + value[3] + value[4] + "." + value[5] + value[6] + value[7] + "." + value[8] + value[9] + value[10];
            }
            else
            {
                text = "" + value[0] + "." + value[1] + value[2] + value[3] + "." + value[4] + value[5] + value[6] + "." + value[7] + value[8] + value[9];
            }
        }
        else if (num >= 1000000)
        {
            if (num >= 100000000)
            {
                text = "" + value[0] + value[1] + value[2] + "." + value[3] + value[4] + value[5] + "." + value[6] + value[7] + value[8];
            }
            else if (num >= 10000000)
            {
                text = "" + value[0] + value[1] + "." + value[2] + value[3] + value[4] + "." + value[5] + value[6] + value[7];
            }
            else
            {
                text = "" + value[0] + "." + value[1] + value[2] + value[3] + "." + value[4] + value[5] + value[6];
            }
        }
        else if (num >= 1000)
        {
            if (num >= 100000)
            {
                text = "" + value[0] + value[1] + value[2] + "." + value[3] + value[4] + value[5];
            }
            else if (num >= 10000)
            {
                text = "" + value[0] + value[1] + "." + value[2] + value[3] + value[4];
            }
            else
            {
                text = "" + value[0] + "." + value[1] + value[2] + value[3];
            }
        }
        else
        {
            text = "" + value;
        }

        return text;
    }
}
