using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class gameManager : MonoBehaviour {
	public enum STATUS {LISTENING, PLAYING};
	private ArrayList allRingtoneSources;
	private ArrayList incorrectTones;
	public AudioSource correctTone;

	private ArrayList allPhones;
	private int nPhones = 6;
	public float startTime;

	public STATUS stat = STATUS.LISTENING;
	public int nAttempts = 0;
	public bool hasWon = false;
	public bool waitingOnReset = false;
	// Use this for initialization
	void Start () {
		Debug.Log("Start!");
		initializeRingtones ();
		initializePhones ();
		selectRingTones ();
		assignTonesToPhones ();
		correctTone.Play ();
	}

	void initializeRingtones() {
		allRingtoneSources = new ArrayList ();
		incorrectTones = new ArrayList ();
		GameObject ringtoneObject = GameObject.Find("Ringtones");
		Debug.Log ("Initalizing Ringtones from " + ringtoneObject.name);
		allRingtoneSources.AddRange(ringtoneObject.GetComponentsInChildren<AudioSource>());

		Debug.Log ("Number of ringtones: " + allRingtoneSources.Count);
	}

	void selectRingTones() {
		Random rand = new Random ();
		int correctRingToneIdx = Random.Range (0,allRingtoneSources.Count);
		correctTone = allRingtoneSources [correctRingToneIdx] as AudioSource;
		allRingtoneSources.RemoveAt (correctRingToneIdx);
		Debug.Log ("Correct audio source: " + correctTone);

		for (int i = 0; i < nPhones-1; i++) {
			int incorrectRingToneIdx = Random.Range(0, allRingtoneSources.Count);
			AudioSource incorrectTone = allRingtoneSources[incorrectRingToneIdx] as AudioSource;
			incorrectTones.Add (incorrectTone);
			allRingtoneSources.RemoveAt(incorrectRingToneIdx);
			Debug.Log ("Incorrect Audio Source: " + incorrectTones[i]);
		}
	}

	void initializePhones() {
		allPhones = new ArrayList ();

		GameObject phonesObject = GameObject.Find ("phones");
		allPhones.AddRange (phonesObject.GetComponentsInChildren<AudioSource> ());
		Debug.Log ("number of phones: " + allPhones.Count);
		nPhones = allPhones.Count;
	}

	void assignTonesToPhones() {
		//tones2phones inc
		bool correctToneAssigned = false;
		int j = 0;
		for (int i = 0; i < nPhones; i++) {
			if(Random.Range(0,3) > 1 || j >= nPhones-1) {
				AudioSource phone = allPhones[i] as AudioSource;
				phone.clip = correctTone.clip;
			}
			else {
				AudioSource phone = allPhones[i] as AudioSource;
				AudioSource tone = incorrectTones[j] as AudioSource;
				phone.clip = tone.clip;
				//phone.Play();
				j++;
			}
		}

	}

	// Update is called once per frame
	void Update () {
		if (stat == STATUS.LISTENING && Input.GetKeyDown (KeyCode.Space)) {
			stat = STATUS.PLAYING;
			correctTone.Stop ();
			for (int i = 0; i < allPhones.Count; i++) {
				AudioSource phone = allPhones [i] as AudioSource;
				phone.Play ();
			}
			startTime = Time.time;
		} if (hasWon) {
			waitingOnReset = true;

		} if (waitingOnReset == true && Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log("Ready to load...");
			Application.LoadLevel("phonePickup");
		}

	}

}
