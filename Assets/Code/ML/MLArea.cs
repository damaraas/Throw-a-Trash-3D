using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using TMPro;

public class MLArea : MonoBehaviour
{
    public MLAgent MLAgent;
    public GameObject Trashbin;
    public TextMeshPro cumulativeRewardText;
    public Paper paperPrefab;

    private List<GameObject> paperList;

    /// Reset the area, including paper, and character placement
    public void ResetArea()
    {
        RemoveAllPaper();
        PlaceCharacter();
        PlaceTrashbin();
        SpawnPaper(20);
    }

    /// Remove a specific paper from the area
    public void RemoveSpecificPaper(GameObject paperObject)
    {
        paperList.Remove(paperObject);
        Destroy(paperObject);
    }

    /// The number of paper remaining
    public int PaperRemaining
    {
        get { return paperList.Count; }
    }

    /// Remove all paper from the area
    private void RemoveAllPaper()
    {
        if (paperList != null)
        {
            for (int i = 0; i < paperList.Count; i++)
            {
                if (paperList[i] != null)
                {
                    Destroy(paperList[i]);
                }
            }
        }

        paperList = new List<GameObject>();
    }

    /// Place the character in the area
    private void PlaceCharacter()
    {
        Rigidbody rigidbody = MLAgent.GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        Vector3 fixedPosition = new Vector3(3f, 1.887f, -8f);
        MLAgent.transform.position = fixedPosition;
    }

    /// Place the trashbin in the area
    private void PlaceTrashbin()
    {
        Rigidbody rigidbody = Trashbin.GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        Vector3 fixedPosition = new Vector3(-3f, 1.425f, 9f);
        Trashbin.transform.position = fixedPosition;
    }

    /// Spawn some number of paper in the area
    private void SpawnPaper(int count)
    {
        for (int i = 0; i < count; i++)
        {
            // Spawn and place the paper
            GameObject paperObject = Instantiate<GameObject>(paperPrefab.gameObject);
            Vector3 randomPosition = new Vector3(Random.Range(5, -8), 1, Random.Range(-8, 5));
            paperObject.transform.position = randomPosition;

            // Keep track of the paper
            paperList.Add(paperObject);
        }
    }

    /// Called when the game starts
    private void Start()
    {
        ResetArea();
    }

    /// Called every frame
    private void Update()
    {
        /// Update the cumulative reward text
        cumulativeRewardText.text = MLAgent.GetCumulativeReward().ToString("0.00");
    }
}