using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour {

	// Use this for initialization

	public AudioSource myFx;
	public AudioClip hoverFx;
	public AudioClip clickFx;

	public void HoverSound()
	{
		myFx.PlayOneShot(hoverFx);
	}

	public void ClickSound()
	{
		myFx.PlayOneShot(clickFx);
	}
}
