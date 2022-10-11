using UnityEngine;
using UnityEngine.UI;

public class ChangeSound : MonoBehaviour
{
    [SerializeField] private AudioClip _clip;
    private AudioSource _backgroundAudio;

    private void Start()
    {
        _backgroundAudio = GetComponent<AudioSource>();
        _backgroundAudio.Play();
    }

    /// <summary>
    /// Changing the sound of the start game
    /// </summary>
    public void ChangeSoundBacground()
    {
        _backgroundAudio.clip = _clip;
        _backgroundAudio.Play();
    }
}
