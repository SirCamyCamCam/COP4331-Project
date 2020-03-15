//By: Amin Kavehzadeh 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeValue : MonoBehaviour {

	private new AudioSource audio; 

	private float musicVal = 1f; 
	// Use this for initialization
	void Start () {

			audio = GetComponent<AudioSource>(); 
	}
	
	// Update is called once per frame
	void Update () {
		audio.volume = musicVal; 
	}

	public void SetVolume(float vol)
	{
		musicVal = vol; 
	}
}
