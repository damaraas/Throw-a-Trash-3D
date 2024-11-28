using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class NewBehaviourScript : MonoBehaviour
{
    public float MoveSpeed = 10;
    public Transform Paper;
    public Transform PosPlay;
    public Transform PosHands;
    public Transform Hands;
    public Transform Target;
    public GameObject objectToSpawn;
    private GameObject spawnedObject;

    // variables
    private bool IsPaperInHands = true;
    private bool IsPaperFlying = false;
    private float T = 0;

    // Update is called once per frame
    void Update()
    {
        // berjalan
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transform.position += direction * MoveSpeed * Time.deltaTime;
        transform.LookAt(transform.position + direction);

        // ball in hands
        if (IsPaperInHands)
        {

            // hold in hands
            if (CrossPlatformInputManager.GetButtonDown("Shoot"))
            {
                Paper.position = PosHands.position;
                Hands.localEulerAngles = Vector3.right * 270;

                // look towards the target
                transform.LookAt(Target.position);
            }

            // melemparkan ke atas
            else
            {
                Paper.position = PosPlay.position + Vector3.up * Mathf.Abs(Mathf.Sin(Time.time * 5));
                Hands.localEulerAngles = Vector3.right * 270;
            }

            // throw paper
            if (CrossPlatformInputManager.GetButtonDown("Shoot"))
            {
                IsPaperInHands = false;
                IsPaperFlying = true;
                T = 0;
            }
        }

        // ball in the air
        if (IsPaperFlying)
        {
            T += Time.deltaTime;
            float duration = 0.66f;
            float t01 = T / duration;

            // move to target
            Vector3 A = PosHands.position;
            Vector3 B = Target.position;
            Vector3 pos = Vector3.Lerp(A, B, t01);

            // move in arc
            Vector3 arc = Vector3.up * 5 * Mathf.Sin(t01 * 3.14f);

            Paper.position = pos + arc;

            // moment when ball arrives at the target
            if (t01 >= 1)
            {
                ScoreManager.scoreCount += 1;

                IsPaperFlying = false;
                Paper.GetComponent<Rigidbody>().isKinematic = false;

                Vector3 randomPosition = new Vector3(Random.Range(5, -8), 1, Random.Range(-8, 5));
                spawnedObject = Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (!IsPaperInHands && !IsPaperFlying)
        {
            IsPaperInHands = true;
            Paper.GetComponent<Rigidbody>().isKinematic = true;

            // Destroy the spawned object when a collision occurs
            if (spawnedObject != null)
            {
                Destroy(spawnedObject);
            }
        }
    }
}