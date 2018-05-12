using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour {

	public AudioSource audioSource;

	public AudioClip success;

	public void PlayAudio(SoundType type )
	{
		AudioClip clip = null;
		switch (type)
		{
			case SoundType.success:
				clip = success;
				break;
		}
		this.audioSource.PlayOneShot(clip);
	}
}

public enum SoundType
{
	success,
}