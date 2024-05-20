using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [SerializeField] private Transform[] routes;
    [SerializeField] private cameraTransitions transitionScript;

    private int routeToGo;

    private float tParam;

    private Vector3 objectPosition;

    private float speedModifier;

    public bool coroutineAllowed;
    private Vector3 oldPos;
    private float oldRotation;


    void Start()
    {
        oldRotation = gameObject.transform.rotation.y;
        routeToGo = 0;
        tParam = 0f;
        speedModifier = 0.2f;
    }

    void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }
    }

    private IEnumerator GoByTheRoute(int routeNum)
    {
        coroutineAllowed = false;

        Vector3 p0 = routes[routeNum].GetChild(0).position;
        Vector3 p1 = routes[routeNum].GetChild(1).position;
        Vector3 p2 = routes[routeNum].GetChild(2).position;
        Vector3 p3 = routes[routeNum].GetChild(3).position;

        while(tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            objectPosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;
            transform.position = objectPosition;
            transform.LookAt(new Vector3(oldPos.x, oldPos.y, oldPos.z));
            oldPos = objectPosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;

        routeToGo += 1;

        if(routeToGo > routes.Length - 1)
        {
            //transitionScript.switchCameras();
        }else{
            coroutineAllowed = true;
        }

        
    }
}
