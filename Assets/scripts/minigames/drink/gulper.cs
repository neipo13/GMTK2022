using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gulper : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;

    public float sourceTime;
    public Vector2 pitchRange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.StartsWith("liq") && (!source.isPlaying || source.time > sourceTime))
        {
            source.clip = clip;
            source.volume = 1f;
            source.pitch = UnityEngine.Random.Range(pitchRange.x, pitchRange.y);
            source.Play();
        }
    }
}
