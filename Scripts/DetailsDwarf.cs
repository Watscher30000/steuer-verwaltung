using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailsDwarf : MonoBehaviour
{   
    // Das sind die Unity GameObjecte die dieses Script in die Listen einfügt
    private GameObject preWeapon;
    private Sprite[] sprites;

    // Diese Liste speichert die Indexe des Stammes und des Zwerges für den Aufruf und die Speicherung der Daten
    private int[] current;

    // Die Waffen-Liste als variable
    private List<GameObject> weapons = new List<GameObject>();

    // Das sind die Unity GameObjecte die dieses Script manipuliert
    private GameObject n;
    private GameObject editName;
    private GameObject age;
    private GameObject editAge;
    private GameObject might;
    private GameObject rank;
    private GameObject content;
    private GameObject title;
    private GameObject power;
    private GameObject add;
    private GameObject remove;

    // Die Methode die die Daten dieses Scriptes setzt und vom Controller einmal aufgerufen werden sollte
    public void Initial()
    {
        preWeapon = Resources.Load<GameObject>("Weapon");
        sprites = Resources.LoadAll<Sprite>("WeaponSprites");

        n = this.gameObject.GetComponentsInChildren<InputField>()[0].gameObject;
        editName = n.GetComponentInChildren<Button>().gameObject;
        age = this.gameObject.GetComponentsInChildren<InputField>()[1].gameObject;
        editAge = age.GetComponentInChildren<Button>().gameObject;
        might = this.gameObject.GetComponentsInChildren<Text>()[4].gameObject;
        rank = this.gameObject.GetComponentsInChildren<Text>()[5].gameObject;
        content = this.gameObject.GetComponentsInChildren<RectTransform>()[15].gameObject;
        title = this.gameObject.GetComponentsInChildren<InputField>()[2].gameObject;
        power = this.gameObject.GetComponentsInChildren<InputField>()[3].gameObject;
        add = this.gameObject.GetComponentsInChildren<Button>()[2].gameObject;
        remove = this.gameObject.GetComponentsInChildren<Button>()[3].gameObject;
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

        if (title.GetComponent<InputField>().text == "" || power.GetComponent<InputField>().text == "")
        {
            if (add.GetComponent<Button>().interactable)
            {
                add.GetComponent<Button>().interactable = false;
            }

            if (remove.GetComponent<Button>().interactable)
            {
                remove.GetComponent<Button>().interactable = false;
            }
        }
        else if(title.GetComponent<InputField>().text != "" || power.GetComponent<InputField>().text != "")
        {
            if (!add.GetComponent<Button>().interactable)
            {
                add.GetComponent<Button>().interactable = true;
            }

            if (!remove.GetComponent<Button>().interactable)
            {
                remove.GetComponent<Button>().interactable = true;
            }
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
                    current = new int[2] {i, j};
                    break;
                }
            }
        }

        this.n.GetComponent<InputField>().text = this.gameObject.GetComponentInParent<Controller>().GetClans()[current[0]].GetBewohner()[current[1]].Name;
        this.age.GetComponent<InputField>().text = this.gameObject.GetComponentInParent<Controller>().GetClans()[current[0]].GetBewohner()[current[1]].Alter + " Years";
        this.rank.GetComponent<Text>().text = this.gameObject.GetComponentInParent<Controller>().GetClans()[current[0]].GetBewohner()[current[1]].Rang;
        DisplayWeapons();
    }

    // Füllt die Waffen-Liste
    public void DisplayWeapons()
    {
        this.might.GetComponent<Text>().text = ((Dwarf)this.gameObject.GetComponentInParent<Controller>().GetClans()[current[0]].GetBewohner()[current[1]]).Macht + " (Macht)";

        foreach (GameObject element in weapons)
        {
            GameObject.Destroy(element);
        }

        this.weapons.Clear();

        foreach (Weapon weapon in ((Dwarf)this.gameObject.GetComponentInParent<Controller>().GetClans()[current[0]].GetBewohner()[current[1]]).GetWaffen())
        {
            this.weapons.Add(GameObject.Instantiate(preWeapon));
            this.weapons[this.weapons.Count - 1].transform.SetParent(this.content.transform);
            this.weapons[this.weapons.Count - 1].name = weapon.Title;
            this.weapons[this.weapons.Count - 1].GetComponentsInChildren<Text>()[0].text = weapon.Title;
            this.weapons[this.weapons.Count - 1].GetComponentsInChildren<Text>()[1].text = weapon.Macht + " Macht";
            this.weapons[this.weapons.Count - 1].GetComponentsInChildren<Image>()[1].sprite = GetSprite(weapon.Title);
            this.weapons[this.weapons.Count - 1].GetComponent<Button>().onClick.AddListener(delegate{CloneWeapon(weapon);});
        }
    }

    // Modifiziert den Namen des Zwerges
    public void EditName()
    {
        this.gameObject.GetComponentInParent<Controller>().GetClans()[current[0]].GetBewohner()[current[1]].Name = n.GetComponent<InputField>().text;
    }

    // Modifiziert das Alter des Zwerges
    public void EditAlter()
    {
        this.gameObject.GetComponentInParent<Controller>().GetClans()[current[0]].GetBewohner()[current[1]].Alter = int.Parse(age.GetComponent<InputField>().text);
    }

    // Fügt eine Waffe hinzu
    public void AddWeapon()
    {
        ((Dwarf)this.gameObject.GetComponentInParent<Controller>().GetClans()[current[0]].GetBewohner()[current[1]]).AddWaffe(new Weapon(title.GetComponent<InputField>().text, int.Parse(power.GetComponent<InputField>().text)));
        DisplayWeapons();
    }

    // Nimmt eine Waffe wenn möglich
    public void RemoveWeapon()
    {
        for (int i = 0; i < ((Dwarf)this.gameObject.GetComponentInParent<Controller>().GetClans()[current[0]].GetBewohner()[current[1]]).GetWaffen().Count; i++)
        {
            if (((Dwarf)this.gameObject.GetComponentInParent<Controller>().GetClans()[current[0]].GetBewohner()[current[1]]).GetWaffen()[i].Title == this.title.GetComponent<InputField>().text && ((Dwarf)this.gameObject.GetComponentInParent<Controller>().GetClans()[current[0]].GetBewohner()[current[1]]).GetWaffen()[i].Macht == int.Parse(this.power.GetComponent<InputField>().text))
            {
                ((Dwarf)this.gameObject.GetComponentInParent<Controller>().GetClans()[current[0]].GetBewohner()[current[1]]).RemoveWaffe(i);
                DisplayWeapons();
                return;
            }
        }
    }

    // Setzt die Daten in der Waffe in die Input-Felder
    public void CloneWeapon(Weapon original)
    {
        title.gameObject.GetComponent<InputField>().text = original.Title;
        power.gameObject.GetComponent<InputField>().text = "" + original.Macht;
    }

    // Geht zurück in die Overview
    public void Back()
    {
        this.gameObject.GetComponentInParent<Controller>().SaveData();
        this.gameObject.GetComponentInParent<Controller>().Backleft();
    }

    // Gibt einen Sprite nach Namen zurück
    public Sprite GetSprite(string input)
    {
        foreach (Sprite sprite in this.sprites)
        {
            if (sprite.name == input)
            {
                return sprite;
            }
        }

        return this.sprites[0];
    }
}
