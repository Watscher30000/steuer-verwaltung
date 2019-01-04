using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Controller : MonoBehaviour, IManagement
{
    // Die globale Settings-Klasse und Grundsteuer
    public Settings settings = new Settings();
    public float BaseTax
    {
        set
        {
            this.settings.baseTax = value;

            for (int i = 0; i < clans.Count; i++)
            {
                for (int j = 0; j < clans[i].GetBewohner().Count; j++)
                {
                    clans[i].GetBewohner()[j].BaseTax = this.settings.baseTax;
                }
            }
        }
    }

    // Dies ist die Liste der Benutzer
    private List<string[]> users = new List<string[]>();
    // Dies ist ein Platzhalter-String
    public string username = "";

    // Dies ist die Liste aus der die Daten ausgelesen werden
    private List<Clan> clans = new List<Clan>();

    // Dies sind die Variablen der anderen Scripts um den Aufruf zu vereinfachen
    private LogIn login;
    private Overview overview;
    private DetailsDwarf detailsdwarf;
    private DetailsElb detailselb;

    // Dies ist eine Main-Methode in Unity, sie wird als aller erstes und nur einmal ausgeführt
    // Die Methode setzt die Grund-Daten die das Programm benötigt um zu laufen
    public void Awake()
    {
        SetData();

        LoadUsers();
        LoadData();

        login = this.gameObject.GetComponentInChildren<LogIn>();
        overview = this.gameObject.GetComponentInChildren<Overview>();
        detailsdwarf = this.gameObject.GetComponentInChildren<DetailsDwarf>();
        detailselb = this.gameObject.GetComponentInChildren<DetailsElb>();

        login.Initial();
        overview.Initial();
        detailsdwarf.Initial();
        detailselb.Initial();
    }
    
    // Diese Methode sucht oder erstellt den Pfad und die Daten die das Programm braucht
    // Die Methode sollte nur einmal und als erstes aufgerufen werden
    private void SetData()
    {
        if (Directory.Exists("Assets"))
        {
            this.settings.defaultPath = "Assets/Data/";
        }
        else if (Directory.Exists("Data"))
        {
            this.settings.defaultPath = "Data/";
        }
        else
        {
            Directory.CreateDirectory("Data");
            this.settings.defaultPath = "Data/";
        }

        if (!File.Exists(this.settings.defaultPath + "Users.txt"))
        {
            FileStream u = File.Create(this.settings.defaultPath + "Users.txt");
            u.Close();

            this.users.Add(new string[2]{"König", "Krasswort"});

            SaveUsers();
        }

        if (!File.Exists(this.settings.defaultPath + "Data.txt"))
        {
            FileStream d = File.Create(this.settings.defaultPath + "Data.txt");
            d.Close();

            this.clans.Add(new Clan("Altobarden", 1247));
            this.clans.Add(new Clan("Elbknechte", 1023));
            this.clans.Add(new Clan("Murkpeak", 954));
            this.clans.Add(new Clan("Montzieu", 1168));

            this.clans[0].AddBewohner(new Dwarf("Gimli", 140, this.settings.baseTax));
            this.clans[0].AddBewohner(new Dwarf("Zwingli", 70, this.settings.baseTax));
            this.clans[1].AddBewohner(new Dwarf("Gumli", 163, this.settings.baseTax));

            this.clans[2].AddBewohner(new Elb("Elidyr", 318, this.settings.baseTax, 21, "inch"));
            this.clans[2].AddBewohner(new Elb("Iefyr", 214, this.settings.baseTax, 84));
            this.clans[2].AddBewohner(new Elb("Vulas", 96, this.settings.baseTax, 23));
            this.clans[3].AddBewohner(new Elb("Malon", 592, this.settings.baseTax, 145));

            this.clans[0].SetLeader(0);
            this.clans[0].GetBewohner()[0].LeaderSince = 1247;
            this.clans[2].SetLeader(1);
            this.clans[2].GetBewohner()[1].LeaderSince = 1260;
            this.clans[3].SetLeader(0);
            this.clans[3].GetBewohner()[0].LeaderSince = 1168;

            ((Dwarf) this.clans[0].GetBewohner()[0]).AddWaffe(new Weapon("Axt", 12));
            ((Dwarf) this.clans[0].GetBewohner()[0]).AddWaffe(new Weapon("Schwert", 15));
            ((Dwarf) this.clans[0].GetBewohner()[1]).AddWaffe(new Weapon("Zauberstab", 45));
            ((Dwarf) this.clans[0].GetBewohner()[1]).AddWaffe(new Weapon("Streithammer", 15));
            ((Dwarf) this.clans[1].GetBewohner()[0]).AddWaffe(new Weapon("Axt", 17));

            SaveData();
        }
    }

    // Hier werden die Daten der Nutzer aus der "users"-Liste in die "Users.txt"-Datei geschrieben
    public void SaveUsers()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(this.settings.defaultPath + "Users.txt", FileMode.Open);
        bf.Serialize(file, users);
        file.Close();
    }

    // Hier werden die Daten der Nutzer aus der "Users.txt"-Datei in die "users"-Liste geschrieben
    public void LoadUsers()
    {
        users.Clear();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(this.settings.defaultPath + "Users.txt", FileMode.Open);
        users = (List<string[]>)bf.Deserialize(file);
        file.Close();
    }

    // Hier werden die Daten der Nutzer aus der "clans"-Liste in die "Data.txt"-Datei geschrieben
    public void SaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(this.settings.defaultPath + "Data.txt", FileMode.Open);
        bf.Serialize(file, clans);
        file.Close();
    }

    // Hier werden die Daten der Nutzer aus der "Data.txt"-Datei in die "clans"-Liste geschrieben
    public void LoadData()
    {
        clans.Clear();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(this.settings.defaultPath + "Data.txt", FileMode.Open);
        clans = (List<Clan>)bf.Deserialize(file);
        file.Close();
    }

    // Gibt den User nach Namen zurück
    public string[] GetUser(string name)
    {
        foreach (string[] user in users)
        {
            if (user[0] == name)
            {
                return user;
            }
        }

        return null;
    }

    // Gibt die Daten als Liste zurück
    public List<Clan> GetClans()
    {
        return this.clans;
    }

    // Gibt die gesamten Steuereinnahmen aus
    public float TotalTax()
    {
        float output = 0f;

        foreach (Clan clan in this.clans)
        {
            output += clan.Tax;
        }

        return output;
    }

    // Gibt alle vom Steuerverwalter erfasste Bürger aus
    public List<IInhabitant> AllInhabitants()
    {
        List<IInhabitant> inhabitants = new List<IInhabitant>();

        for (int i = 0; i < this.clans.Count; i++)
        {
            for (int j = 0; j < this.clans[i].GetBewohner().Count; j++)
            {
                inhabitants.Add(this.clans[i].GetBewohner()[j]);
            }
        }

        return inhabitants;
    }

    // Gibt alle vom Steuerverwalter erfasste Häuptlinge aus aus
    public List<IDuke> AllDukes()
    {
        List<IDuke> dukes = new List<IDuke>();

        for (int i = 0; i < this.clans.Count; i++)
        {
            dukes.Add(this.clans[i].GetLeader());
        }

        return dukes;
    }

    // Diese Methode ist für den Übergang nach dem LogIn des Nutzers
    public void UserLogin(string name)
    {
        this.username = name;
        overview.SetCurrentUser();
        this.gameObject.GetComponent<Animation>().Play("Login");
    }

    // Diese Methode ist für den Übergang zur Detailansicht des Zwerges
    public void Dwarfdetails(string name)
    {
        detailsdwarf.SetDetails(name);
        this.gameObject.GetComponent<Animation>().Play("Dwarfdetails");
    }

    // Diese Methode ist für den Übergang zur Detailansicht des Elben
    public void Elbdetails(string name)
    {
        detailselb.SetDetails(name);
        this.gameObject.GetComponent<Animation>().Play("Elbdetails");
    }

    // Diese Methode ist für den Übergang aus Detailansicht des Zwerges
    public void Backleft()
    {
        overview.DisplayClans();
        overview.SetBaseTax();
        overview.SetTotalGold();
        this.gameObject.GetComponent<Animation>().Play("Backleft");
    }

    // Diese Methode ist für den Übergang aus Detailansicht des Elben
    public void Backright()
    {
        overview.DisplayClans();
        overview.SetBaseTax();
        overview.SetTotalGold();
        this.gameObject.GetComponent<Animation>().Play("Backright");
    }
}
