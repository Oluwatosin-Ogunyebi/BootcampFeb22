using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _aimDirection;
    [SerializeField] private Animator _animDirection;
    [SerializeField] private float _throwSpeed;
    [SerializeField] private List<Rigidbody> _bowlingBallPrefabs;
    [SerializeField] private FollowTarget _followTarget;
    [SerializeField] private GameManager _gameManager;

    public bool wasBallThrown;

    private Rigidbody _bowlingBallPrefabInstance;

    private int ballIndex;
    private bool isBallSelected;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        if (FindObjectOfType<SelectBall>() != null)
        {
            ballIndex = FindObjectOfType<SelectBall>().ballIndex;
            isBallSelected = true;
        }
        else
            isBallSelected = false;
    }
    public void StartAiming()
    {
        _animDirection.SetBool("Aiming", true);
        wasBallThrown = false;
        _followTarget.playerPosition = transform;
    }
    // Update is called once per frame
    void Update()
    {
        TryThrowBall();

    }

    void TryThrowBall()
    {
#if UNITY_STANDALONE
        if (wasBallThrown || !Input.GetButtonDown("Fire1")) return;
#endif

#if UNITY_ANDROID || UNITY_IOS
        if (wasBallThrown || !DetectSwipeUp()) return;
#endif

        wasBallThrown = true;
        if (!isBallSelected) ballIndex = RandomNumber();
        _bowlingBallPrefabInstance = Instantiate(_bowlingBallPrefabs[ballIndex], transform.position, transform.rotation);
        _bowlingBallPrefabInstance.AddForce((_aimDirection.forward * -1) * _throwSpeed, ForceMode.Impulse);
        _followTarget.playerPosition = _bowlingBallPrefabInstance.transform;
        _gameManager.BallThrown(_bowlingBallPrefabInstance.GetComponent<Ball>());
        _animDirection.SetBool("Aiming", false);
    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           // _ballRb.AddForce((_aimDirection.forward * -1) * _throwSpeed, ForceMode.Impulse);
        }
    }

    private int RandomNumber()
    {
        int randomNum = Random.Range(0, _bowlingBallPrefabs.Count);
        return randomNum;
    }

#if UNITY_ANDROID || UNITY_IOS
    public bool DetectSwipeUp()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            if (touchDeltaPosition.y > 0)
            {
                if (touchDeltaPosition.magnitude > 50)
                {
                    return true;
                }
            }
        }

        return false;
    }
#endif
}
