using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectionTriggered : MonoBehaviour {
	gameManager gm;
	Text attemptsLabel;
	// Use this for initialization
	void Start () {
		GameObject manager = GameObject.Find ("GameManager");
		gm = manager.GetComponent<gameManager> ();
		GameObject attemptsObject = GameObject.Find ("Canvas/attempt time");
		attemptsLabel = attemptsObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		GameObject phone = collision.gameObject;
		AudioSource phoneSource = phone.GetComponent<AudioSource> ();
		if (phoneSource.clip.name == gm.correctTone.clip.name)
			gm.hasWon = true;
		else {
			gm.nAttempts++;
			attemptsLabel.text = "Attempts: " + gm.nAttempts;
		}
	}

}
