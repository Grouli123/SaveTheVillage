using System;
using UnityEngine;
using UnityEngine.UI;
public class ImageTimer : MonoBehaviour 
{
    [SerializeField] private float _maxTime;
    [SerializeField] private bool _tick;
    public bool Tick => _tick;
    private Image _imgHurvestTime;
    private float _currentTime;

     private void Start()
    {
        _imgHurvestTime = GetComponent<Image>();
        _currentTime = _maxTime;
    }

    private void Update()
    {
        _tick = false;
        _currentTime -= Time.deltaTime;
        if (_currentTime <= 0)
        {
            _tick = true;
            _currentTime = _maxTime;
        }
        _imgHurvestTime.fillAmount = _currentTime / _maxTime;
    }
}