using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentScore;

    PlayerController playerController;

    private Pin[] _currentPins = new Pin[0];
    private Ball _currentBall;

    [SerializeField] private Transform _pinSetSpawnPosition;
    [SerializeField] private GameObject _pinSetPrefab;

    private bool _throwStarted;
    private int _throwNumber;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
        Invoke(nameof(SetupFrame), 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_throwStarted || !playerController.wasBallThrown)
            return;

        if (CheckIfPiecesAreStatic())
            FinishThrow();
    }

    public void PinKnockedDown()
    {
        currentScore++;
        Debug.Log("Current Score is: "+currentScore);
        //Called when each pin is hit and pin head comes in contact with floor
    }

    public void BallKnockedDown()
    {
        _currentBall = null;
    }

    public void BallThrown(Ball ball)
    {
        _currentBall = ball;
    }

    private  bool CheckIfPiecesAreStatic()
    {
        foreach (var pin in _currentPins)
        {
            if (pin != null && pin.DidPinMove())
            {
                return false;
            }
        }
        var ballStatus = _currentBall == null || !_currentBall.DidBallMove();
        return ballStatus; //Checks for Pin Movement
    }

    private void SetupFrame()
    {
        _throwNumber = 0;
        DisposeLastFrame();
        Instantiate(_pinSetPrefab, _pinSetSpawnPosition.position, _pinSetSpawnPosition.rotation);
        _currentPins = FindObjectsOfType<Pin>();

        SetupThrow();
    }

    private void FinishThrow()
    {
        _throwStarted = false;

        foreach (var pin in _currentPins)
        {
            if (pin != null && pin.DidPinFall)
            {
                currentScore++;
                Destroy(pin.gameObject);
            }
        }

        if (_throwNumber == 0 && currentScore < 10)
        {
            Invoke(nameof(SetupThrow), 1);
            _throwNumber++;
            return;
        }

        Invoke(nameof(SetupFrame), 1);
    }

    public void SetupThrow()
    {
        foreach (var pin in _currentPins)
        {
            if (pin != null)
                pin.ResetPosition();
        }

        if (_currentBall != null) Destroy(_currentBall.gameObject);

        playerController.StartAiming();
        _throwStarted = true;
    }

    public void DisposeLastFrame()
    {
        foreach (var pin in _currentPins)
        {
            if (pin != null)
            {
                Destroy(pin.gameObject);
            }
        }
    }

}
