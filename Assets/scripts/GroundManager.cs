using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    public GameObject groundPrefab;
    public GameObject firstGround;

    public bool finishMoveGround;
    private bool finishedRandomGround;
    private List<GameObject> listGroundForward = new List<GameObject>();
    private List<GameObject> listGroundback = new List<GameObject>();
    private Vector3 firstForwardPos;
    private Vector3 firstBackPos;
    private int numberOfGrounds = 5;

    private bool stopMoveGround = false;
    private float timeToMove = 1f;

    void Start()
    {
        firstForwardPos = firstGround.transform.position + Vector3.forward * firstGround.transform.localScale.z + new Vector3(0, -10, 0);
        firstBackPos = firstGround.transform.position + Vector3.back * firstGround.transform.localScale.z + new Vector3(0, -10, 0);
        StartCoroutine(RandomGroundForward(firstForwardPos, numberOfGrounds, listGroundForward));
        StartCoroutine(RandomGroundBack(firstBackPos, numberOfGrounds, listGroundForward));
    }

    // Update is called once per frame
    void Update()
    {
        //When all ground is created, start to move ground
        if (finishedRandomGround && !stopMoveGround)
        {
            for (int i = 0; i < listGroundForward.Count; i++)
            {
                StartCoroutine(MoveGround(listGroundForward[i], listGroundForward[i].transform.position, listGroundForward[i].transform.position + new Vector3(0, 10f, 0), timeToMove));
            }
            for (int i = 0; i < listGroundback.Count; i++)
            {
                StartCoroutine(MoveGround(listGroundback[i], listGroundback[i].transform.position, listGroundback[i].transform.position + new Vector3(0, 10f, 0), timeToMove));
            }
            stopMoveGround = true;
        }
    }

    IEnumerator MoveGround(GameObject ground, Vector3 startPos, Vector3 endPos, float timeToMove)
    {
        float t = 0;
        while (t < timeToMove)
        {
            float fraction = t / timeToMove;
            ground.transform.position = Vector3.Lerp(startPos, endPos, fraction);
            t += Time.deltaTime;
            yield return null;
        }
        ground.transform.position = endPos;
        finishMoveGround = true;
    }

    IEnumerator RandomGroundForward(Vector3 position, int number, List<GameObject> newlist)
    {
        finishedRandomGround = false;
        for (int i = 0; i < number; i++)
        {
            GameObject currentGround = Instantiate(groundPrefab, position, Quaternion.identity);
            newlist.Add(currentGround);
            currentGround.transform.SetParent(firstGround.transform.parent);
            position = currentGround.transform.position + Vector3.forward * currentGround.transform.localScale.z;
            yield return new WaitForSeconds(.1f);
        }
        finishedRandomGround = true;
    }

    IEnumerator RandomGroundBack(Vector3 position, int number, List<GameObject> newlist)
    {
        finishedRandomGround = false;
        for (int i = 0; i < number; i++)
        {
            GameObject currentGround = Instantiate(groundPrefab, position, Quaternion.identity);
            newlist.Add(currentGround);
            currentGround.transform.SetParent(firstGround.transform.parent);
            position = currentGround.transform.position + Vector3.back * currentGround.transform.localScale.z;
            yield return new WaitForSeconds(.1f);
        }
        finishedRandomGround = true;
    }


}
