using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MLAgent : Agent
{
    public float moveSpeed = 5f;
    public float turnSpeed = 180f;

    private MLArea MLArea;
    new private Rigidbody rigidbody;
    private GameObject Trashbin;
    private bool isFull; // If true, trashbin is full

    /// Initial setup, called when the agent is enabled
    public override void Initialize()
    {
        base.Initialize();
        MLArea = GetComponentInParent<MLArea>();
        Trashbin = MLArea.Trashbin;
        rigidbody = GetComponent<Rigidbody>();
    }

    /// Perform actions based on a vector of numbers
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Convert the first action to forward movement
        float forwardAmount = actionBuffers.DiscreteActions[0];

        // Convert the second action to turning left or right
        float turnAmount = 0f;
        if (actionBuffers.DiscreteActions[1] == 1f)
        {
            turnAmount = -1f;
        }
        else if (actionBuffers.DiscreteActions[1] == 2f)
        {
            turnAmount = 1f;
        }

        // Apply movement
        rigidbody.MovePosition(transform.position + transform.forward * forwardAmount * moveSpeed * Time.fixedDeltaTime);
        transform.Rotate(transform.up * turnAmount * turnSpeed * Time.fixedDeltaTime);

        // Apply a tiny negative reward every step to encourage action
        if (MaxStep > 0) AddReward(-1f / MaxStep);
    }

    /// Read inputs from the keyboard and convert them to a list of actions
    /// This is called only when the player wants to control the agent and has set
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        int forwardAction = 0;
        int turnAction = 0;
        if (Input.GetKey(KeyCode.W))
        {
            // move forward
            forwardAction = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            // turn left
            turnAction = 1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // turn right
            turnAction = 2;
        }

        // Put the actions into the array
        actionsOut.DiscreteActions.Array[0] = forwardAction;
        actionsOut.DiscreteActions.Array[1] = turnAction;
    }

    // When a new episode begins, reset the agent and are
    public override void OnEpisodeBegin()
    {
        isFull = false;
        MLArea.ResetArea();
    }

    /// Collect all non-Raycast observations
    public override void CollectObservations(VectorSensor sensor)
    {
        // Whether the character has taken a paper (1 float = 1 value)
        sensor.AddObservation(isFull);

        // Distance to the trashbin (1 float = 1 value)
        sensor.AddObservation(Vector3.Distance(Trashbin.transform.position, transform.position));

        // Direction to trashbin (1 Vector3 = 3 values)
        sensor.AddObservation((Trashbin.transform.position - transform.position).normalized);

        // Direction character is facing (1 Vector3 = 3 values)
        sensor.AddObservation(transform.forward);

        // 1 + 1 + 3 + 3 = 8 total values
    }

    /// When the agent collides with something, take action
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("paper"))
        {
            // Try to take the paper
            TakePaper(collision.gameObject);
        }
        else if (collision.transform.CompareTag("trashbin"))
        {
            // Try to throw the paper to trashbin
            RegurgitateFish();
        }
    }
}