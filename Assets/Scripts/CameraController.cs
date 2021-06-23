using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private float smoothing = 5f;

    private Vector3 offset;
   
    void Start()
    {
        offset = transform.position - player.position;
    }

    private void LateUpdate()
    {
        Vector3 PlayerCamPos = player.position + offset;

        transform.position = Vector3.Lerp(transform.position, PlayerCamPos, smoothing * Time.deltaTime);
    }

   
}
