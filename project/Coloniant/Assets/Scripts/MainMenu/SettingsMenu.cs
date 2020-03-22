//By: Amin Kavehzadeh
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.Audio; 

public class SettingsMenu : MonoBehaviour {

	Resolution[] resolutions; 
	public Dropdown resolutionDropdown; 

	public AudioMixer audioMixer; 
	void Start()
	{
		resolutions = Screen.resolutions;
		resolutionDropdown.ClearOptions(); 

		List<string> options = new List<string>(); 

		int currentResolutionIndex = 0; 

		for (int i = 0; i < resolutions.Length; i++)
		{
			string option = resolutions[i].width + " x " + resolutions[i].height; 
			options.Add(option);

			if(resolutions[i].width == Screen.currentResolution.width && 
			  resolutions[i].height == Screen.currentResolution.height)
			{
				currentResolutionIndex = i; 
			} 
		}
		resolutionDropdown.AddOptions(options); 
		resolutionDropdown.value = currentResolutionIndex; 
		resolutionDropdown.RefreshShownValue(); 
	}

	public void SetResolution (int resolutionIndex)
	{
		Resolution resolution = resolutions[resolutionIndex]; 
		Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen); 
	}
	public void SetFullScreen(bool isFull)
	{
		Screen.fullScreen = isFull; 
	}

	public void SetMasterVolume(float volume)
	{
		audioMixer.SetFloat("volume",volume); 
	}

	public void SetQuality (int qIndex)
	{
		QualitySettings.SetQualityLevel(qIndex);
	}


	
	
}
