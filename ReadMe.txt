
# Login-Daten :
(
	Benutzername : K�nig
	Passwort     : Krasswort
)

+ Diagramme :
[
	- Klassendiagramm
	- Objektdiagramm
	- Klassendiagramm v2
]

+ Data :
[
	- Users.txt
	{
		// Beinhaltet den Nutzernamen und das Passwort von mindestem einen Nutzer
		// (Darf im Text-Editor ge�ffnet werden, direkte �nderungen der Datei im Editor kann zu Fehlern f�hren
		// in diesem Fall die Datei l�schen, das Programm erstellt eine neue)
	}

	- Data.txt
	{
		// Beinhaltet die Beispiel-Daten aus der Aufgabenstellung 
		// (Darf im Text-Editor ge�ffnet werden, direkte �nderungen der Datei im Editor kann zu Fehlern f�hren
		// in diesem Fall die Datei l�schen, das Programm erstellt eine neue)
	}
]

+ Scripts :
[
	// Beinhaltet den kommentierten Quellcode in C#

	+ Klassen :
	[
		// Beinhaltet die kommentierten Klassen in C#
	]
]

# Anleitung :
(
	Wenn auf einen Stamm geklickt wird werden alle dazugeh�rigen Mitglieder angezeigt.
	Wenn auf einen Bewohner geklickt wird kann der "Details"-Knopf genutzt werden um die Daten
	des Bewohners genauer anzuzeigen und zu bearbeiten.
	Eine rote Raute wird auf der Karte eines Bewohners gezeigt der den Stamm anf�hrt.

	In der Detailansicht des Zwerges k�nnen Name und Alter des Zwerges ge�ndert werden,
	die �nderung wird erst �bernommen wenn der "Schraubenschl�ssel"-Knopf neben dem Input-Feld
	gedr�ckt wird.
	Eine Waffe kann erst gegeben oder genommen werden wenn ein Titel und ein Macht-Wert (magischer Wert) gegeben sind.
	(Die Daten werden in der "Data.txt"-Datei gespeichert)

	In der Detailansicht des Elben k�nnen Name und Alter des Elben ge�ndert werden,
	die �nderung wird erst �bernommen wenn der "Schraubenschl�ssel"-Knopf neben dem Input-Feld
	gedr�ckt wird.
	Die Haarl�nge kann durch ein Eingabefeld und zwei Kn�pfe ver�ndert werden.
	(Die Daten werden in der "Data.txt"-Datei gespeichert)
)