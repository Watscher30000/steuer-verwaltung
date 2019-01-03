
# Login-Daten :
(
	Benutzername : König
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
		// (Darf im Text-Editor geöffnet werden, direkte Änderungen der Datei im Editor kann zu Fehlern führen
		// in diesem Fall die Datei löschen, das Programm erstellt eine neue)
	}

	- Data.txt
	{
		// Beinhaltet die Beispiel-Daten aus der Aufgabenstellung 
		// (Darf im Text-Editor geöffnet werden, direkte Änderungen der Datei im Editor kann zu Fehlern führen
		// in diesem Fall die Datei löschen, das Programm erstellt eine neue)
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
	Wenn auf einen Stamm geklickt wird werden alle dazugehörigen Mitglieder angezeigt.
	Wenn auf einen Bewohner geklickt wird kann der "Details"-Knopf genutzt werden um die Daten
	des Bewohners genauer anzuzeigen und zu bearbeiten.
	Eine rote Raute wird auf der Karte eines Bewohners gezeigt der den Stamm anführt.

	In der Detailansicht des Zwerges können Name und Alter des Zwerges geändert werden,
	die Änderung wird erst übernommen wenn der "Schraubenschlüssel"-Knopf neben dem Input-Feld
	gedrückt wird.
	Eine Waffe kann erst gegeben oder genommen werden wenn ein Titel und ein Macht-Wert (magischer Wert) gegeben sind.
	(Die Daten werden in der "Data.txt"-Datei gespeichert)

	In der Detailansicht des Elben können Name und Alter des Elben geändert werden,
	die Änderung wird erst übernommen wenn der "Schraubenschlüssel"-Knopf neben dem Input-Feld
	gedrückt wird.
	Die Haarlänge kann durch ein Eingabefeld und zwei Knöpfe verändert werden.
	(Die Daten werden in der "Data.txt"-Datei gespeichert)
)