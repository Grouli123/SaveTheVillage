using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ImageTimer HarvestTimter;
    [SerializeField] private ImageTimer EatingTimer;

    [SerializeField] private GameObject _loseGameOverScreen;
    [SerializeField] private GameObject _winGameOverScreen;
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _game;
    [SerializeField] private GameObject _music;
    [SerializeField] private GameObject _eatMusic;
    [SerializeField] private GameObject _enemyMusic;
    [SerializeField] private GameObject _wheatMusic;
    [SerializeField] private GameObject _doneMusic;

    [SerializeField] private Image _peasantTimterImage;
    [SerializeField] private Image _warriorTimterImage;
    [SerializeField] private Image _raidTimerImage;

    [SerializeField] private Button _peasantButton;
    [SerializeField] private Button _warriorButton;

    [SerializeField] private Text _nextRaidText;
    [SerializeField] private Text _resourceText;
    [SerializeField] private Text _peasantText;
    [SerializeField] private Text _warriorText;

    [SerializeField] private Text _loseNextRaidText;
    [SerializeField] private Text _loseResourceText;
    [SerializeField] private Text _losePeasantText;
    [SerializeField] private Text _loseWarriorText;

    [SerializeField] private Text _winNextRaidText;
    [SerializeField] private Text _winResourceText;
    [SerializeField] private Text _winPeasantText;
    [SerializeField] private Text _winWarriorText;

    private int _peasantCost;
    private int _warriorCost;

    private int _peasantCount;
    private int _warriorsCount;
    private int _wheatCount;

    private int _wheatPerPeasant;
    private int _wheatToWarriors;

    private int _raidIncrease;
    private int _nextRaid;

    private float _peasantCreateTime;
    private float _warriorCreateTime;
    private float _raidMaxTime;

    private float _peasantTimer = -2;
    private float _warriorTimer = -2;
    private float _raidTimer;

    private bool _turnOnOff;

    private bool _paused;

    private bool _eat;
    private bool _enemy;
    private bool _wheat;
    private bool _done;
    private bool _doneMusicSecond;

    private bool _startTimer;
  
    private void Start()
    {
        StartValue();
        OffStartTimer();
        _enemy = false;
        _turnOnOff = true;
        SetActiveMenuTrue();
        SetActiveGameFalse();
        UpdateText();
        _raidTimer = _raidMaxTime;
    }
    
    private void Update()
    {
        EnemyTimer();
        WheatTimer();
        EatTimer();
        PeasantTimer();
        WarriorTimer();
        Lose();
        Win();
        DoneMusicSoud();        
        UpdateText();
    }

    /// <summary>
    /// Algorithm (function) create peasant
    /// </summary>
    public void CreatePeasant()
    {        
        _wheatCount -= _peasantCost;
        _peasantTimer = _peasantCreateTime;
        PeasantButtonFalse();
    }

    /// <summary>
    /// Algorithm (function) create warriors
    /// </summary>
    public void CreateWarrior()
    {
        _wheatCount -= _warriorCost;
        _warriorTimer = _warriorCreateTime;
        WarriorButtonFalse();
    }

    /// <summary>
    /// Updating text fields in game menu, lose window and win window
    /// </summary>
    public void UpdateText()
    {
        _nextRaidText.text = _nextRaid.ToString();
        _resourceText.text = _wheatCount.ToString();
        _peasantText.text = _peasantCount.ToString();
        _warriorText.text = _warriorsCount.ToString();

        _loseNextRaidText.text = _nextRaid.ToString();
        _loseResourceText.text = _wheatCount.ToString();
        _losePeasantText.text = _peasantCount.ToString();
        _loseWarriorText.text = _warriorsCount.ToString();

        _winNextRaidText.text = _nextRaid.ToString();
        _winResourceText.text = _wheatCount.ToString();
        _winPeasantText.text = _peasantCount.ToString();
        _winWarriorText.text = _warriorsCount.ToString();
    }

    /// <summary>
    /// Initial indicators when you click on the play button 
    /// </summary>
    public void StartGame()
    {
        StartValue();
        Time.timeScale = 1;
        _startTimer = true;
        _menu.SetActive(false);
        _game.SetActive(true);
        LoseWinWindouFalse();
    }

    /// <summary>
    /// Closing extra windows before game start
    /// </summary>
    public void StartMenu()
    {
        SetActiveMenuTrue();
        SetActiveGameFalse();
        LoseWinWindouFalse();
    }

    /// <summary>
    /// Pause game with music saving
    /// </summary>
    public void PauseGame()
    {
        if (_paused)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
        _paused = !_paused;
    }
    
    /// <summary>
    /// Turn off or on the music sound
    /// </summary>
    public void MusicSound()
    {
        if (_turnOnOff)
        {
            _turnOnOff = false;
            _music.SetActive(false);
        }
        else
        {
            _turnOnOff = true;
            _music.SetActive(true);
        }
    }

    private void StartValue()
    {
        _peasantCost = 2;
        _warriorCost = 4;
        _peasantCount = 3;
        _warriorsCount = 2;
        _wheatCount = 15;
        _wheatPerPeasant = 2;
        _wheatToWarriors = 5;
        _peasantCreateTime = 6;
        _warriorCreateTime = 10;
        _raidMaxTime = 45f;
        _raidIncrease = 1;
        _nextRaid = 0;
    }
    private void OffStartTimer()
    {
        _startTimer = false;
    }

    private void SetActiveMenuTrue()
    {
        _menu.SetActive(true);
    }

    private void DoneMusicTrue()
    {
        _doneMusic.SetActive(true);
    }

    private void DoneMusicFalse()
    {
        _doneMusic.SetActive(false);
    }

    private void SetActiveGameFalse()
    {
        _game.SetActive(false);
    }

    private void WarriorButtonTrue()
    {
        _warriorButton.interactable = true;
    }

    private void WarriorButtonFalse()
    {
        _warriorButton.interactable = false;
    }

    private void PeasantButtonTrue()
    {
        _peasantButton.interactable = true;
    }

    private void PeasantButtonFalse()
    {
        _peasantButton.interactable = false;
    }

    private void LoseWinWindouFalse()
    {
        _loseGameOverScreen.SetActive(false);
        _winGameOverScreen.SetActive(false);
    }

    private void EnemyTimer()
    {
        if (_startTimer)
        {
            _raidTimer -= Time.deltaTime;
            _raidTimerImage.fillAmount = _raidTimer / _raidMaxTime;
        }
        if (_raidTimer <= 0)
        {
            _enemy = true;
            _raidTimer = _raidMaxTime;
            _warriorsCount -= _nextRaid;
            _nextRaid += _raidIncrease;

            _enemyMusic.SetActive(false);
        }
        if (_enemy)
        {
            _enemy = false;
            _enemyMusic.SetActive(true);
        }
    }

    private void WheatTimer()
    {
        if (HarvestTimter.Tick)
        {
            _wheat = true;
            _wheatCount += _peasantCount * _wheatPerPeasant;
            _wheatMusic.SetActive(false);
        }
        if (_wheat)
        {
            _wheat = false;
            _wheatMusic.SetActive(true);
        }
    }

    private void EatTimer()
    {
        if (EatingTimer.Tick)
        {
            _eat = true;
            _wheatCount -= _warriorsCount * _wheatToWarriors;
            _eatMusic.SetActive(false);
        }
        if (_eat)
        {
            _eat = false;
            _eatMusic.SetActive(true);
        }
    }

    private void PeasantTimer()
    {
        if (_peasantTimer > 0)
        {
            _done = false;
            _peasantTimer -= Time.deltaTime;
            _peasantTimterImage.fillAmount = _peasantTimer / _peasantCreateTime;
        }
        else if (_peasantTimer > -1)
        {
            _done = true;
            DoneMusicFalse();
            _peasantTimterImage.fillAmount = 1;
            PeasantButtonTrue();
            _peasantCount += 1;
            _peasantTimer = -2;
        }
        else if (_wheatCount < 2)
        {
            PeasantButtonFalse();
        }
        else if (_wheatCount >= 2)
        {
            PeasantButtonTrue();
        }
    }

    private void WarriorTimer()
    {
        if (_warriorTimer > 0)
        {
            _doneMusicSecond = false;
            _warriorTimer -= Time.deltaTime;
            _warriorTimterImage.fillAmount = _warriorTimer / _warriorCreateTime;
        }
        else if (_warriorTimer > -1)
        {
            _doneMusicSecond = true;
            DoneMusicFalse();
            _warriorTimterImage.fillAmount = 1;
            WarriorButtonTrue();
            _warriorsCount += 1;
            _warriorTimer = -2;
        }
        else if (_wheatCount < 4)
        {
            WarriorButtonFalse();
        }
        else if (_wheatCount >= 4)
        {
            WarriorButtonTrue();
        }
    }

    private void Lose()
    {
        if (_warriorsCount < 0)
        {
            Time.timeScale = 0;
            _loseGameOverScreen.SetActive(true);
            _eatMusic.SetActive(false);
            OffStartTimer();
        }
    }
    private void Win()
    {
        if (_warriorsCount >= 50 || _peasantCount >= 100)
        {
            Time.timeScale = 0;
            _winGameOverScreen.SetActive(true);
            _eatMusic.SetActive(false);
            OffStartTimer();
        }
    }

    private void DoneMusicSoud()
    {
        if (_done)
        {
            DoneMusicTrue();
        }
        if (_doneMusicSecond)
        {
            DoneMusicTrue();
        }
    }
}
