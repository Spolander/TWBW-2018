using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class soundEngine : MonoBehaviour {

    public static soundEngine instance;

    public enum SoundClip {Footstep, GunSound, RightAnswer, WrongAnswer, Death};


    [SerializeField]
    private AudioMixer mixer;


    [SerializeField]
    private AudioClip[] footSteps;

    [SerializeField]
    private AudioClip gunSound;

    [SerializeField]
    private AudioClip deathSound;

    private void Awake()
    {
        instance = this;
    }


    public void PlaySoundAt(Vector3 pos, SoundClip clip,float delay)
    {

        GameObject g = new GameObject("sound clip");
        AudioSource a = g.AddComponent<AudioSource>();

        a.outputAudioMixerGroup = mixer.FindMatchingGroups("FX")[0];
        a.spatialBlend = 1;

        a.maxDistance = 15;

        if (clip == SoundClip.Footstep)
        {
            a.clip = footSteps[Random.Range(0, footSteps.Length - 1)];
        }
        else if (clip == SoundClip.GunSound)
        {
            a.clip = gunSound;
        }
        else if (clip == SoundClip.Death)
        {
            a.clip = deathSound;
        }
        g.transform.position = pos;
        a.PlayDelayed(delay);

        if (a.clip)
            Destroy(g, a.clip.length);

    }
}
