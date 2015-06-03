#pragma strict

var impact : AudioClip;
	function OnCollisionEnter () {
		GetComponent.<AudioSource>().PlayOneShot(impact, 1f);
	}