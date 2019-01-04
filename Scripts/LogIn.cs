using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogIn : MonoBehaviour
{
    // Das sind die Unity GameObjecte die dieses Script manipuliert
    private GameObject user;
    private GameObject pass;
    private GameObject error;

    // Zwischenspeicher-String für das eingegebene Passwort
    private string word = "";

    // Die Methode die die Daten dieses Scriptes setzt und vom Controller einmal aufgerufen werden sollte
    public void Initial()
    {
        user = this.gameObject.GetComponentsInChildren<InputField>()[0].gameObject;
        pass = this.gameObject.GetComponentsInChildren<InputField>()[1].gameObject;
        error = this.gameObject.GetComponentsInChildren<Image>()[4].gameObject;

        Error();
    }

    // Diese Methode ist eine Unity-Methode die einmal pro Frame aufgerufen wird
    public void Update()
    { 
        word = pass.GetComponent<InputField>().text;
    }

    // Die Methode kontrolliert ob die eingegebenen Daten valide sind
    public void CheckData()
    {
        string message = "";

        if (user.GetComponent<InputField>().text == "")
        {
            message += "Benutzername muss gegeben sein.\n";
        }

        if (pass.GetComponent<InputField>().text == "")
        {
            message += "Passwort muss gegeben sein.";
        }

        if (message != "")
        {
            Error(message);
            return;
        }

        if (this.GetComponentInParent<Controller>().GetUser(user.GetComponent<InputField>().text) != null)
        {
            if (this.GetComponentInParent<Controller>().GetUser(user.GetComponent<InputField>().text)[1] == word)
            {
                this.GetComponentInParent<Controller>().UserLogin(user.GetComponent<InputField>().text);
                return;
            }
        }

        Error("Nutzername und Passwort stimmen nicht überein.");
    }

    // Diese Methode gibt die Error-Nachricht auf dem LogIn-Bildschirm aus
    public bool Error(string message = "")
    {
        error.GetComponentInChildren<Text>().text = message;

        if (message == "")
        {
            error.GetComponent<Image>().color = new Color(0.92f, 0f, 0f, 0f);
            return false;
        }

        error.GetComponent<Image>().color = new Color(0.92f, 0f, 0f, 0.82f);
        return true;
    }
}
