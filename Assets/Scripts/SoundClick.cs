using UnityEngine;
using UnityEngine.UI;

public class SoundClick : MonoBehaviour
{
    [SerializeField] private AudioSource _click;

    private void Start()
    {
        _click.enabled = false;
    }
    /// <summary>
    /// Play click sound by click on button
    /// </summary>
    public void ClickSound()
    {
        _click.enabled = true;
        _click = GetComponent<AudioSource>();
        _click.Play();
    }
}
