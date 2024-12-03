SETUP:
- Use the already prepared prefab named "UI Dialogue System" and drag it onto your scene.
** ONLY PUT 1 PREFAB PER SCENE**
** You can freely change canvas propietis as well as UI sprites**
- Add a Dialogue Manager in your scene.
- You can change what button you want to use in order to skip to the next line
- Ona a script you can use "Instantiate Dialogue(string)" and as input type the path of your dialogue.
** Dialogues must be on Resources folder **

How to Add Characters:
- On Resources/Characters/Database.json you can add a new character.
- A character object consists of:
	-Name		string
	-Color		string
- Upon start your new character will be automaticaly loaded

How to create a dialogue:
- You can create a dialogue using the example provided in Resources/Dialogues/DialogueExample.json
- A dialogue consists of an array of lines(object):
	-dialogue	string
	-character	string

How to display dialogue:
- On a script, use your reference of "Dialogue Manager" and use function "Start Dialogue".
** In "Dialogue Example" prefab you can see a basic usage of this with the onclick event**