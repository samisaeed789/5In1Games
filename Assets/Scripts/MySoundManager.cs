﻿using UnityEngine;
using System.Collections;

public class MySoundManager : MonoBehaviour {

	

	public static MySoundManager instance = null;



	[Range(0f, 1f)]
	public float musicValue = 1f;
	
	
	[Range(0f, 1f)]
	public float soundValue = 1f;




	[Header("AudioSource")]
	public AudioSource BGM;
	public AudioSource Effectsource;
	





    [Header("AudioClips")]
    public AudioClip ss;
    public AudioClip mm;
    public AudioClip bgm;
    public AudioClip busbgm;
    public AudioClip eurotruckbgm;
    public AudioClip jeepbgm;
    public AudioClip policebgm;
    public AudioClip Indi;
    public AudioClip HornBus;
    public AudioClip HornCar;
    public AudioClip Excellent;
    public AudioClip DiscourageS;
    public AudioClip click;
    public AudioClip busclick;
    public AudioClip eurotrckclick;
    public AudioClip Jeepclick;
    public AudioClip scificlick;
    public AudioClip policeclick;
	public AudioClip FillerPanel;
    public AudioClip panelpop;
    public AudioClip complete;
    public AudioClip fail;
    public AudioClip popup;
    public AudioClip pop;
    public AudioClip emoji;
    public AudioClip cash;
    public AudioClip coin;
    public AudioClip CP;
    public AudioClip RampSound;
    public AudioClip parkedSound;
    public AudioClip FireWork;
    public AudioClip Applaud;
    public AudioClip GreatJob;
    public AudioClip Splash;
    public AudioClip Hit;
    public AudioClip PlayEngine;
    public AudioClip Collect;
    public AudioClip CollectCoin;
    public AudioClip Beep;
    public AudioClip Revv;
    public AudioClip Revv1;
    public AudioClip Revv5;
    public AudioClip Unlock;
    public AudioClip UhOh;
    public AudioClip PoliceChatter;
    public AudioClip PoliceSiren;
    public AudioClip TypingSound;
    public AudioClip WaterSplash;


  

	[Header("Booleans")]
	public bool levelcompleted;
	public bool levelfailed;

	void Awake()
	{
		
		if(instance==null)
			instance=this;

		
		BGM = this.GetComponent<AudioSource>();
		Effectsource = transform.GetChild(0).GetComponent<AudioSource>();
	}

	public void musicValueChanged (float val)
	{

		musicValue = val;
        BGM.volume = musicValue;
		ValStorage.SetMVolume(val);
	
	}

	public void soundValueChanged (float val)
	{
		soundValue = val;
		Effectsource.volume = soundValue;
		ValStorage.SetSVolume(val);
	}

	public void SetMainMenuMusic (bool check, float val)
	{
		if (check) {
			BGM.clip = mm;
			musicValue = val;
			BGM.volume = musicValue;
			BGM.Play ();
		} else {
			BGM.Pause ();
		}
	}
	public void PlayLevelCompleteSound(bool check,float val)
	{
		if (check) {
			//BGM.clip = complete;
			//musicValue = val;
			//BGM.volume = musicValue;
			BGM.clip = null;
			BGM.PlayOneShot(complete);
		} else {
			BGM.Pause ();
		}
	}

	public void PlayCompleteSound(bool check)
	{
		if (check)
		{
			BGM.clip = complete;
			BGM.Play();
		}
		else
		{
			BGM.Pause();
		}
	}
	public void PlayChatterSound(bool val)
	{
		if (val)
			Effectsource.PlayOneShot(PoliceChatter);

		else
			Effectsource.Stop();
	}
	public void PlayPoliceSiren(bool effect) 
	{
		if (effect) 
		{
			Effectsource.loop = true;
			Effectsource.clip = PoliceSiren;
			Effectsource.Play();
		}
        else 
		{
			Effectsource.Stop();
		}
	}
	


	public void PlayLevelFailSound()
	{
			Effectsource.PlayOneShot(fail);
			BGM.Pause();
	}

	public void SetSelectionScreenMusic (bool check, float val)
	{
		if (check) {
			    BGM.clip = ss;
				musicValue = val;
				BGM.volume = musicValue;
				BGM.Play ();
			
		} else {
			if (BGM) {
				BGM.Pause ();
			}
		}
	}	
	
	public void SetModeScreenMusic (bool check, float val)
	{
		if (check) {
			    BGM.clip = ss;
				musicValue = val;
				BGM.volume = musicValue;
				BGM.Play ();
			
		} else {
			if (BGM) {
				BGM.Pause ();
			}
		}
	}
	public void SetBGM (bool check)
	{
		if (check) {
			    BGM.clip = bgm;
				BGM.Play ();
			
		}
		else {
			if (BGM) {
				BGM.Pause ();
			}
		}
	}
	
	public void SoundMute (bool check)
	{
		if (check) {
			Effectsource.volume = 0;
			
		}
		else {
			Effectsource.volume = 1;

		}
	}
	
	public void MusicMute (bool check)
	{
		if (check) {
			BGM.Pause();
			
		}
		else {
			BGM.Play();


		}
	}
	public void SetBusBGM (bool check)
	{
		if (check) {
			    BGM.clip = busbgm;
				BGM.Play ();
			
		}
		else {
			if (BGM) {
				BGM.Pause ();
			}
		}
	}
	
	public void SetEuroTruckBGM (bool check)
	{
		if (check) {
			    BGM.clip = eurotruckbgm;
				BGM.Play ();
			
		}
		else {
			if (BGM) {
				BGM.Pause ();
			}
		}
	}
	
	public void SetJeepBGM (bool check)
	{
		if (check) {
			    BGM.clip = jeepbgm;
				BGM.Play ();
			
		}
		else {
			if (BGM) {
				BGM.Pause ();
			}
		}
	}
	
	public void SetPoliceBGM (bool check)
	{
		if (check) {
			    BGM.clip = policebgm;
				BGM.Play ();
			
		}
		else {
			if (BGM) {
				BGM.Pause ();
			}
		}
	}
	
	public void PanelFiller ()
	{
		Effectsource.PlayOneShot(FillerPanel);
	}

	public void PlayHorn(string Vehicle)
	{
        if (Vehicle == "Bus") 
		{
			Effectsource.clip = HornBus;
		}
		if (Vehicle == "Car")
		{
			Effectsource.clip = HornCar;
		}


		Effectsource.loop = true;  // Set looping to true while button is held down
		Effectsource.Play();
	}
	
	public void StopHorn()
	{
		if (Effectsource.isPlaying)  // Stop the sound if it's playing
		{
		Effectsource.loop = false;

			Effectsource.Stop();
		}
	}
	
	public void ExcellentSound()
	{
		Effectsource.PlayOneShot(Excellent);

	}
	
	public void SplashSound()
	{
		Effectsource.PlayOneShot(WaterSplash);
	}
	
	public void DiscourageSound()
	{
		Effectsource.PlayOneShot(DiscourageS);

	}

	public void PlayEngineSound()
	{
		Effectsource.PlayOneShot(PlayEngine);
	} 
	public void PlayHitSound()
	{
		Effectsource.PlayOneShot(Hit);
	} 
	
	public void PlayUIPopSound(float val)
	{
		soundValue = val;
		Effectsource.volume = soundValue;
		Effectsource.PlayOneShot(pop);
	}

	public void PlayCollectSound()
	{
		
		Effectsource.PlayOneShot(Collect);
	}

	public void PlayCollectCoin()
	{
	
		Effectsource.PlayOneShot(CollectCoin);
	}
	 public void PlayEmojiSound(float val)
	{
		soundValue = val;
		Effectsource.volume = soundValue;
		Effectsource.PlayOneShot(emoji);
	}

	public void PlayCashSound(float val)
	{
		soundValue = val;
		Effectsource.volume = soundValue;
		Effectsource.PlayOneShot(cash);
	}
	
	public void PlayTypeSound(bool val)
	{
		if (val)
		{
			BGM.clip = TypingSound;
			BGM.Play();

		}
		else
		{
			if (BGM)
			{
				BGM.Pause();
			}
		}
	}

	public void PlaycoinSound()
	{
	
		Effectsource.PlayOneShot (coin);
		Effectsource.loop=true;
	}
	public void PlayUhoH()
	{
		Effectsource.PlayOneShot(UhOh);
	}

	public void StopcoinSound()
	{
		
		Effectsource.Stop();
		Effectsource.loop=false;
	}

	public void PlayButtonClickSound(bool scifi=false)
	{
		if(scifi==true)
			Effectsource.PlayOneShot(scificlick);

        else 
		{
			Effectsource.PlayOneShot(click);
		}
	}
	
	public void PlayBusClickSound()
	{
		
			Effectsource.PlayOneShot(busclick);
		
	}
	public void PlayEuroClickSound()
	{
		
			Effectsource.PlayOneShot(eurotrckclick);
		
	}
	
	public void PlayJeepClickSound()
	{
		Effectsource.PlayOneShot(Jeepclick);
	}
	public void PlaypoliceClickSound()
	{
		Effectsource.PlayOneShot(policeclick);
	}
	public void PlayPanelPop()
	{
		Effectsource.PlayOneShot(panelpop);
	}
	public void PlayBeepSound()
	{
		Effectsource.PlayOneShot(Beep);
	}
	public void PlayRevvSound()
	{
		Effectsource.PlayOneShot(Revv);
	}
	
	public void PlayRevv1Sound()
	{
		Effectsource.PlayOneShot(Revv1);
	}
	public void PlayRevv5Sound()
	{
		Effectsource.clip = Revv5;
		Effectsource.loop = true;
		Effectsource.Play();
	}
	public void StopRevv5Sound()
	{
		Effectsource.Stop();
		
	}

	public void PlayCPSound(float val)
	{
		soundValue = val;
		Effectsource.volume = soundValue;
		Effectsource.PlayOneShot(CP);
	}



	public void PlayRampSound(float val)
	{
		soundValue = val;
		Effectsource.volume = soundValue;
		Effectsource.PlayOneShot(RampSound);
	}
	
	public void StopRampSound()
	{
		Effectsource.clip = null;
		Effectsource.Stop();
	}

	public void PlayParkedSound(float val)
	{

		soundValue = val;
		Effectsource.volume = soundValue;
		Effectsource.PlayOneShot(parkedSound);
	}


	public void PlayFireworkSound(float val)
	{

		soundValue = val;
		Effectsource.volume = soundValue;
		Effectsource.clip=FireWork;
		Effectsource.loop = true;
		Effectsource.Play();
	}


	public void PlayApplaudSound(bool check,float val)
	{
		if (check)
		{
			BGM.clip = Applaud;
			musicValue = val;
			BGM.volume = musicValue;
			BGM.Play();
		}
		else
		{
			BGM.Pause();
		}
	}


	public void playindiSound(bool check) 
	{
		if (check)
		{
			Effectsource.clip = Indi;
			Effectsource.loop = true;
			Effectsource.Play();
		}
		else
		{
		
			Effectsource.loop = false;
			Effectsource.Stop();
		}
	}
	public void PlayVO(float val)
	{

		soundValue = val;
		Effectsource.volume = soundValue;
		Effectsource.PlayOneShot(GreatJob);

	}
	public void PlaySplash(float val)
	{

		soundValue = val;
		Effectsource.volume = soundValue;
		Effectsource.PlayOneShot(Splash);

	}

    public void OnVolumeChanged(float value)
    {
        // Set the volume based on the slider's value
        BGM.volume = value;
    }

	public void PauseSounds() 
	{
		SetBGM(false);
		playindiSound(false);

	}
	
	public void ResumeSounds() 
	{
		SetBGM(true);

  //      if (GameMngr.instance) S
		//{
		//	bool play = GameMngr.instance.IsIndsiactive();
		//	playindiSound(play);
		//}
	}
	public void CarUnlock() 
	{
		Effectsource.PlayOneShot(Unlock);
		
	}

	public void PlayRunningCar() 
	{
        BGM.clip = bgm;
        BGM.Play();
    }

	
}
