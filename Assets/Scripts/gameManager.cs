
using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class gameManager : MonoBehaviour {
	public enum STATUS {START_SCREEN, PLAYING};
	
	private int nPhones = 6;
	protected GameObject player;
	protected CharacterController movement;
	protected GameObject highlightedPhone;
	protected GameObject lastHighlightedPhone;
	public float startTime;
	public float timeLeft = 50.0f;
	private float guess_penalty = 3.0f;
	private float score_time = 8.0f;
	private int correct;
	public STATUS stat = STATUS.START_SCREEN;
	public int nAttempts = 0;
	public bool hasLost = false;
	public bool waitingOnReset = false;
	private GameObject objText;
	private GameObject timerText;
	private GameObject statusText;
	public string[] PhoneNames = {"Defense", "Energy", "Justice", "Motor Vehicles", "Agriculture", "Tresury"};
	private string callToAction = "OBJECTIVE:\nTHE DEPARTMENT\nOF ";
	// Use this for initialization
	void applyTimerText(float t) {
		TextMesh mesh = timerText.GetComponent<TextMesh> ();
		int minutes = (int) t/60;
		int seconds = (int) t%60;
		mesh.text = "" + minutes + ":" + seconds;
	}

	void applyObjectiveText(int i) {
		TextMesh mesh = objText.GetComponent<TextMesh> ();
		mesh.text = callToAction + PhoneNames [i];
	}

	GameObject FindClosestPhone() {
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("phones");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = player.transform.position;
		foreach (GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
	}

	void selectNewObjective() {
		correct = Random.Range (0, nPhones);
		applyObjectiveText (correct);
	}

	void applyStatusText(string s) {
		TextMesh mesh = statusText.GetComponent<TextMesh> ();
		mesh.text = s;
		Color c = mesh.color;

		mesh.color = new Color(c.r, c.g, c.b, 1.0f);
	}

	void checkPhone() {
		if (highlightedPhone.name == PhoneNames [correct]) {
			timeLeft = timeLeft + score_time;
			applyStatusText ("GREAT!");
		} else {
			timeLeft = timeLeft - guess_penalty;
			applyStatusText("WRONG!");
		}
	}
	void Start () {
		Debug.Log("Start!");
		GameObject phones = GameObject.Find ("phones");
		objText = GameObject.Find ("ObjectiveText");
		timerText = GameObject.Find ("Timer");
		statusText = GameObject.Find ("statusText");
		player = GameObject.Find ("OVRPlayerController");
		movement = player.GetComponent<CharacterController> ();
		movement.enabled = false;
		highlightedPhone = FindClosestPhone ();
		Behaviour y = ((Behaviour) highlightedPhone.GetComponent("Halo"));
		y.enabled = true;
		lastHighlightedPhone = highlightedPhone;
	}

	// Update is called once per frame
	void Update () {
		if (stat == STATUS.START_SCREEN && Input.GetButtonDown("Desktop_Button A")) {
			stat = STATUS.PLAYING;
			selectNewObjective();
			movement.enabled = true;
			startTime = Time.time;
		} if (hasLost) {
			applyStatusText("YOU LOSE");
			waitingOnReset = true;

		} if (waitingOnReset == true && Input.GetButtonDown ("Desktop_Button A")) {
			Debug.Log("Ready to load...");
			Application.LoadLevel("OvalOffice");
		}
		highlightedPhone = FindClosestPhone();
		if (highlightedPhone != lastHighlightedPhone) {
			Debug.Log("cursor switch!");
			Behaviour x = ((Behaviour)lastHighlightedPhone.GetComponent("Halo"));
			x.enabled = false;
			Behaviour y = ((Behaviour) highlightedPhone.GetComponent("Halo"));
			y.enabled = true;
			lastHighlightedPhone = highlightedPhone;
		}


	}

	void FixedUpdate () {
		if (stat == STATUS.PLAYING) {
			//main loop
			timeLeft = timeLeft - 0.02f;
			applyTimerText(timeLeft);
			if(timeLeft <= 0.0f) hasLost = true;
			if(Input.GetButtonDown("Desktop_Button A")) {
				checkPhone();
			}
		}
	}



}
