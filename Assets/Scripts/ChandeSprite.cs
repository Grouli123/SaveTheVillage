using UnityEngine;
using UnityEngine.UI;

public class ChandeSprite : MonoBehaviour
{
    [SerializeField] private Sprite _spriteSoundOff;
    [SerializeField] private Sprite _spriteSoundOn;
    private Image _soundOn;
    private bool _change;

    private void Start()
    {
        _soundOn = GetComponent<Image>();
        _change = true;
    }

    /// <summary>
    /// Changing the sprite of the sound button
    /// </summary>
    public void ChangeSprite()
    {
        if (_change)
        {
            _change = false;
            _soundOn.sprite = _spriteSoundOff;
        }
        else
        {
            _change = true;
            _soundOn.sprite = _spriteSoundOn;
        }
    }
}
