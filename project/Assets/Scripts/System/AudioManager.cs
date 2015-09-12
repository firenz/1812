using UnityEngine;
using System;
using System.Collections;

public static class AudioManager {

	public static void PlayMusic(string name, bool loop = false){
		AudioClip _clip = null;
		GameObject _audioGO = null;
		AudioSource _source = null;

		_audioGO = GameObject.Find("Music_" + name);

		if(_audioGO != null){
			MonoBehaviour.Destroy(_audioGO);
			_audioGO = null;
		}

		try{
			_clip = Resources.Load<AudioClip>("Sound/Music/" + name);
		}
		catch(NullReferenceException){
			Debug.LogError("Can't find Music audio clip");
			return;
		}
		
		_audioGO = new GameObject("Music_" + _clip.name);
		_audioGO.tag = "Music";
		_source = _audioGO.AddComponent<AudioSource>();
		_source.clip = _clip;
		_source.volume = GameState.SystemData.AudioVolumeSettings.music / 100f;
		_source.playOnAwake = false;
		_source.loop = loop;

		_source.Play();
		if(!loop){
			MonoBehaviour.Destroy(_audioGO, _clip.length);
		}
	}

	public static void PauseMusic(string name){
		AudioSource _source = null;
		
		try{
			_source = GameObject.Find("Music_" + name).GetComponent<AudioSource>();
		}
		catch(NullReferenceException){
			Debug.LogError("Can't find Music audio clip");
			return;
		}

		if(_source.loop){
			if(_source.isPlaying){
				_source.Pause();
			}
		}
	}

	public static void UnpauseMusic(string name){
		AudioSource _source = null;
		
		try{
			_source = GameObject.Find("Music_" + name).GetComponent<AudioSource>();
		}
		catch(NullReferenceException){
			Debug.LogError("Can't find Music audio clip");
			return;
		}

		if(_source.loop){
			if(!_source.isPlaying){
				_source.UnPause();
			}
		}
	}

	public static void StopMusic(string name){
		AudioSource _source = null;

		try{
			_source = GameObject.Find("Music_" + name).GetComponent<AudioSource>();
		}
		catch(NullReferenceException){
			Debug.LogError("Can't find Music audio clip");
			return;
		}

		_source.Stop();
		MonoBehaviour.Destroy(_source.gameObject);
	}

	public static void StopAllMusic(){
		GameObject[] _musics = GameObject.FindGameObjectsWithTag("Music");
		
		foreach(GameObject music in _musics){
			MonoBehaviour.Destroy(music);
		}
	}

	public static void PlaySFX(string name, bool loop = false){
		AudioClip _clip = null;
		GameObject _audioGO = null;
		AudioSource _source = null;
		
		_audioGO = GameObject.Find("SFX_" + name);
		if(_audioGO != null){
			MonoBehaviour.Destroy(_audioGO);
			_audioGO = null;
		}

		try{
			_clip = Resources.Load<AudioClip>("Sound/SFX/" + name);
		}
		catch(NullReferenceException){
			Debug.LogError("Can't find SFX audio clip with name " + name);
			return;
		}
		
		_audioGO = new GameObject("SFX_" + _clip.name);
		_audioGO.tag = "SFX";
		_source = _audioGO.AddComponent<AudioSource>();
		_source.clip = _clip;
		_source.volume = GameState.SystemData.AudioVolumeSettings.sfx / 100f;
		_source.playOnAwake = false;
		_source.loop = loop;

		_source.Play();
		if(!loop){
			MonoBehaviour.Destroy(_audioGO, _clip.length);
		}
		
	}

	public static void StopSFX(string name){
		AudioSource _source = null;
		
		try{
			_source = GameObject.Find("SFX_" + name).GetComponent<AudioSource>();
		}
		catch(NullReferenceException){
			Debug.LogError("Can't find SFX audio clip with name " + name);
			return;
		}
		
		_source.Stop();
		MonoBehaviour.Destroy(_source.gameObject);
	}

	public static bool IsPlayingMusic(string name){
		AudioSource _source;

		try{
			_source = GameObject.Find("Music_" + name).GetComponent<AudioSource>();
		}catch(NullReferenceException){
			return false;
		}

		bool _isPlaying = _source.isPlaying;

		return _isPlaying;
	}

	public static bool IsPlayingSFX(string name){
		AudioSource _source;
		
		try{
			_source = GameObject.Find("SFX_" + name).GetComponent<AudioSource>();
		}catch(NullReferenceException){
			return false;
		}
		
		bool _isPlaying = _source.isPlaying;
		
		return _isPlaying;
	}

	public static void ChangeVolumeAllCurrentMusics(int newMusicVolume){
		GameState.SystemData.AudioVolumeSettings.music = newMusicVolume;
		SettingsFileManager.Instance.SaveSettingsFile();
		GameObject[] _musics = GameObject.FindGameObjectsWithTag("Music");

		foreach(GameObject music in _musics){
			music.GetComponent<AudioSource>().volume = GameState.SystemData.AudioVolumeSettings.music / 100f;
		}
	}

	public static void ChangeVolumeAllCurrentSFX(int newSFXVolume){
		GameState.SystemData.AudioVolumeSettings.sfx = newSFXVolume;
		SettingsFileManager.Instance.SaveSettingsFile();
		GameObject[] _musics = GameObject.FindGameObjectsWithTag("SFX");
		
		foreach(GameObject music in _musics){
			music.GetComponent<AudioSource>().volume = GameState.SystemData.AudioVolumeSettings.sfx / 100f;
		}
	}
}
