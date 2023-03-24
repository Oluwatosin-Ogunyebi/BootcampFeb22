using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCube : MonoBehaviour
{
    [SerializeField] private Transform playerPosition;
    [SerializeField] private Vector3 offset;

    private void Update()
    {
        //transform.position = playerPosition.position + offset;
    }
}
