using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public List<AudioClip> audioClips;
    public void PlayFootstepSound()
    {
        int r = Random.Range(0, audioClips.Count);
        audioClips[r].PlayClip2D(this, null, 0.2f);
    }
}
