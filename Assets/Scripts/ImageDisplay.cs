using UnityEngine;
using System.Collections;

public class ImageDisplay : MonoBehaviour {
	public Texture splashImage;
	public Texture homeScreen;
	public float timeToDisplaySplash;
	public int nextLevelToLoad;
	public AudioClip themeMusic;
	
	private float timeForNextLevel;
	
	public void Start() {
		timeForNextLevel = Time.time + timeToDisplaySplash;
	}
	
	public void OnGUI() {
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), splashImage);
		if (Time.time >= timeForNextLevel) {
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), homeScreen);
			if (Input.GetKeyDown ("space"))
				Application.LoadLevel(nextLevelToLoad);
		}
	}
}