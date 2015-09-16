using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Clase estatica que gestiona y controla los audios del juego.
/// </summary>
public static class AudioManager {

	/// <summary>
	/// Reproduce una cancion.
	/// </summary>
	/// <param name="name">Nombre de la cancion</param>
	/// <param name="loop">Reproduce en bucle la cancion si se indica el valor <c>true</c>, por defecto solo se reproduce una vez</param>
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

	/// <summary>
	/// Pausa la cancion si se esta reproduciendo en bucle.
	/// </summary>
	/// <param name="name">Nombre de la cancion.</param>
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

	/// <summary>
	/// Termina de pausar una cancion en bucle pausada que no se esta reproduciendo.
	/// </summary>
	/// <param name="name">Nombre de la cancion.</param>
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

	/// <summary>
	/// Para de reproducir la cancion.
	/// </summary>
	/// <param name="name">Nombre de la cancion.</param>
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

	/// <summary>
	/// Para de reproducir todas las canciones.
	/// </summary>
	public static void StopAllMusic(){
		GameObject[] _musics = GameObject.FindGameObjectsWithTag("Music");
		
		foreach(GameObject music in _musics){
			MonoBehaviour.Destroy(music);
		}
	}

	/// <summary>
	/// Reproduce un efecto sonoro.
	/// </summary>
	/// <param name="name">Nombre del efecto sonoro.</param>
	/// <param name="loop">Reproduce en bucle el efecto sonoro si se indica el valor <c>true</c>, por defecto solo se reproduce una vez</param>
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

	/// <summary>
	/// Para de reproducir el efecto sonoro.
	/// </summary>
	/// <param name="name">Nombre del efecto sonoro.</param>
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

	/// <summary>
	/// Determina si una cancion con el nombre especificado se esta reproduciendo.
	/// </summary>
	/// <returns>Devuelve <c>true</c> si la cancion especificada se esta reproduciendo; en otro caso, <c>false</c>.</returns>
	/// <param name="name">Nombre de la cancion.</param>
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

	/// <summary>
	/// Determina si un efecto sonoro con el nombre especificado se esta reproduciendo.
	/// </summary>
	/// <returns>Devuelve <c>true</c> si la cancion especificada se esta reproduciendo; en otro caso, <c>false</c>.</returns>
	/// <param name="name">Nombre del efecto sonoro.</param>
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

	/// <summary>
	/// Cambia el volumen de todas las canciones que hay en escena.
	/// </summary>
	/// <param name="newMusicVolume">Nuevo volumen para las canciones.</param>
	public static void ChangeVolumeAllCurrentMusics(int newMusicVolume){
		GameState.SystemData.AudioVolumeSettings.music = newMusicVolume;
		SettingsFileManager.Instance.SaveSettingsFile();
		GameObject[] _musics = GameObject.FindGameObjectsWithTag("Music");

		foreach(GameObject music in _musics){
			music.GetComponent<AudioSource>().volume = GameState.SystemData.AudioVolumeSettings.music / 100f;
		}
	}

	/// <summary>
	/// Cambia el volumen de todos los efectos sonoros que hay en escena.
	/// </summary>
	/// <param name="newSFXVolume">Nuevo volumen para los efectos sonoros.</param>
	public static void ChangeVolumeAllCurrentSFX(int newSFXVolume){
		GameState.SystemData.AudioVolumeSettings.sfx = newSFXVolume;
		SettingsFileManager.Instance.SaveSettingsFile();
		GameObject[] _musics = GameObject.FindGameObjectsWithTag("SFX");
		
		foreach(GameObject music in _musics){
			music.GetComponent<AudioSource>().volume = GameState.SystemData.AudioVolumeSettings.sfx / 100f;
		}
	}
}
