using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class UItextChanger : MonoBehaviour {
	gameManager gm;
	Text uiText;
	bool gameStarted = false;
	bool wonTextShown = false;
	// Use this for initialization
	void Start () {
		GameObject managerObject = GameObject.Find ("GameManager");
		gm = managerObject.GetComponent<gameManager>();
		uiText = gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gm.stat == gameManager.STATUS.PLAYING && !gameStarted) {
			uiText.text = "PICKUP YOUR PHONE\nTHROW IT ON THE PEDESTAL";
			gameStarted = true;
		} else if (gm.hasWon && !wonTextShown) {
			float time = Time.time- gm.startTime;
			int minutes = (int) time/60;
			int seconds = (int) time%60;
			uiText.text = "YOU WON IN " + minutes + ":" + seconds + "\nPRESS SPACE TO RESTART";
			wonTextShown = true;
		}
	}
}
