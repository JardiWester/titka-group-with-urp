using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class boatCubeFollow : MonoBehaviour
{
    public Transform cube;
    [SerializeField] private float smoothTime;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, cube.position, Time.deltaTime * smoothTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, cube.rotation, Time.deltaTime * smoothTime);
    }
}
