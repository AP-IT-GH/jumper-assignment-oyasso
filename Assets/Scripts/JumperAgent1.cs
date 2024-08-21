using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class JumperAgent1 : Agent
{
    public GameObject Obstacle;
    public GameObject Wall;
    public float jumpForce = 1.0f;
    public float obstacleSpeed = -1;
    bool grounded = true;
    bool hit = false; // check if obstacle hit agent

    public override void OnEpisodeBegin()
    {
        hit = false;
        Obstacle.transform.localPosition = new Vector3(0.7f, 0.5f, 9.3f);
        obstacleSpeed = Random.Range(-4.0f, -2.0f);
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        // Target en Agent posities
        sensor.AddObservation(Obstacle.transform.localPosition);
        sensor.AddObservation(this.transform.localPosition);
    }
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // if jumps
        if (actionBuffers.DiscreteActions[0] == 1 && grounded)
        {
            Jump();
            grounded = false;
        }
        // if standing still
        if (actionBuffers.DiscreteActions[0] == 0 && grounded)
        {
            AddReward(0.1f * Time.deltaTime);
        }

        Obstacle.transform.Translate(new Vector3(0, 0, obstacleSpeed) * Time.deltaTime);
        float distanceObstacleWall = Vector3.Distance(Obstacle.transform.localPosition, Wall.transform.localPosition);
        float distanceToObstacle = Vector3.Distance(Obstacle.transform.localPosition, this.transform.localPosition);

        // if obstacle hits the wall
        if (distanceObstacleWall <= 0.8f)
        {
            AddReward(1.0f);
            Obstacle.transform.localPosition = new Vector3(0.7f, 0.5f, 6);
            obstacleSpeed = Random.Range(-4.0f, -2.0f);
        }

        // only jump when on the ground
        if ( this.transform.localPosition.y < 0.51f && 
        this.transform.localPosition.y > 0.49f)
        {
            grounded = true;
        }

        if (hit)
        {
            EndEpisode();
        }

        Debug.Log(GetCumulativeReward());
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discActionsOut = actionsOut.DiscreteActions;
        discActionsOut[0] = 0;
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            discActionsOut[0] = 1;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Obstacle")
        {
            hit = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            ResetAgent();
        }
    }

    private void Jump()
    {
        this.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetAgent()
    {
        this.transform.localPosition = new Vector3(1, 0.5f, 0);
        this.transform.localRotation = Quaternion.identity;
    }

}
