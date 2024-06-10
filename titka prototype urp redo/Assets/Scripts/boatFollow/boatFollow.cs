using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatFollow : MonoBehaviour
{
    [SerializeField] private GameObject routeParent;
    [SerializeField] private List<Transform> routes;

    [SerializeField] private int routeToGo;

    [SerializeField] private float tParam;

    private Vector3 objectPosition;

    [SerializeField] private float speedModifier = 0.2f;

    public bool coroutineAllowed;
    private Vector3 oldPos;
    private float oldRotation;
    public bool moveAllowed;
    public GameObject tutorial;
    void Start()
    {
        oldRotation = gameObject.transform.rotation.y;
        routeToGo = 0;
        tParam = 0f;
        foreach(Transform Route in routeParent.transform.GetComponentsInChildren<Transform>())
        {
            if(Route.gameObject.tag == "routepart")
            {
                routes.Add(Route);
            }
        }
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

        while(tParam < 1.000000000001 && tParam >= 0 && moveAllowed)
        {
            float vertical = Input.GetAxisRaw("Vertical");
            tParam += Time.deltaTime * speedModifier * vertical;

            objectPosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;
            transform.position = objectPosition;
            if (vertical > 0)
            {
                tutorial.SetActive(false);
                transform.LookAt(new Vector3(oldPos.x, oldPos.y, oldPos.z));
            }else {
                float tempTParam = tParam - Time.deltaTime * speedModifier;
                Vector3 tempPossition = Mathf.Pow(1 - tempTParam, 3) * p0 + 3 * Mathf.Pow(1 - tempTParam, 2) * tempTParam * p1 + 3 * (1 - tempTParam) * Mathf.Pow(tempTParam, 2) * p2 + Mathf.Pow(tempTParam, 3) * p3;
                transform.LookAt(tempPossition);
            }
            oldPos = objectPosition;
            
            if (!moveAllowed)
            {
                coroutineAllowed = false;
                break;
            }else
            {
                yield return new WaitForEndOfFrame();
            }

        }
        if (moveAllowed){
            if (tParam > 1)    {
                tParam = 0f;

                routeToGo += 1;

                if(routeToGo > routes.Count - 1)
                {
                    routeToGo = 0;
                }
            }else{
                tParam = 1f;
                routeToGo -= 1;

                if(routeToGo < 0)
                {
                    routeToGo = routes.Count - 1;
                }
            }

            coroutineAllowed = true;
        } else
        {
            
        }
    }
}
