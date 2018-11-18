using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour {

    public void FootStep()
    {
        soundEngine.instance.PlaySoundAt(transform.position, soundEngine.SoundClip.Footstep);
    }
}
