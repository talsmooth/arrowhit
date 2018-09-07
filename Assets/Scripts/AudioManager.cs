using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public bool backGroundMusics;

    public bool soundMute;

    public static bool _soundMute;

    public AudioSource music;

    public AudioSource sound;

    public static AudioSource _sound;

    public AudioClip backGroundMusic;

    public AudioClip bananaFlight;

    public AudioClip shooting;

    public AudioClip hitFruit;

    public AudioClip hitDummy;

    public AudioClip trainerThrow;

    public AudioClip comboX2;

    public AudioClip comboX3;

    public static AudioClip _bananaFlight;

    public static AudioClip _shooting;

    public static AudioClip _hitFruit;

    public static AudioClip _hitDummy;

    public static AudioClip _trainerThrow;

    public static AudioClip _comboX2;

    public static AudioClip _comboX3;



	// Use this for initialization
	void Start () 
    {
        music.clip = backGroundMusic;

        _soundMute = soundMute;

        if (backGroundMusics)
        {
            music.Play();
        }

        _sound = sound;

        if (_soundMute)
        {
            _sound.mute = enabled;
        }

        _bananaFlight = bananaFlight;

        _shooting = shooting;

        _hitFruit = hitFruit;

        _hitDummy = hitDummy;

        _trainerThrow = trainerThrow;

        _comboX2 = comboX2;

        _comboX3 = comboX3;
	}
	
}
