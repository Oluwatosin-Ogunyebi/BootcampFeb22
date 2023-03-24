using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform _aimDirection;
    [SerializeField] private Animator _animDirection;
    [SerializeField] private float _throwSpeed;
    [SerializeField] private List<Rigidbody> _bowlingBallPrefabs;

    public bool wasBallThrown;

    private Rigidbody _bowlingBallPrefabInstance;
    // Start is called before the first frame update
    void Start()
    {
        _animDirection.SetBool("Aiming", true);
    }

    // Update is called once per frame
    void Update()
    {
        TryThrowBall();
        if (Input.GetButtonDown("Fire1"))
        {
            _bowlingBallPrefabInstance = Instantiate(_bowlingBallPrefabs[RandomNumber()],transform.position,transform.rotation);
            _bowlingBallPrefabInstance.AddForce((_aimDirection.forward * -1) * _throwSpeed, ForceMode.Impulse);
        }
    }

    void TryThrowBall()
    {
        if (wasBallThrown || !Input.GetButtonDown("Fire1"))
        {
            wasBallThrown = true;
        }
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

}
