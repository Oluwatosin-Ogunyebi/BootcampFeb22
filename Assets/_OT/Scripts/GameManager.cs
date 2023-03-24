using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentScore;

    PlayerController playerController;

    private Pin[] _currentPins = new Pin[0];
    private Ball _currentBall;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.wasBallThrown)
        {
            //Player has not thrown ball
        }
    }

    public void PinKnockedDown()
    {
        currentScore++;
        Debug.Log("Current Score is: "+currentScore);
        //Called when each pin is hit and pin head comes in contact with floor
    }

    public void BallKnockedDown()
    {
        //Called when ball gets into pit
    }

    public void BallThrown(Ball ball)
    {
        //Reference ball that is thrown
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
        //Used to setup frame
    }

    private void FinishThrow()
    {
        //Called when we complete a throw
    }

    public void SetupThrow()
    {
        //Creates initial conditions to allow player throw ball
    }

}
