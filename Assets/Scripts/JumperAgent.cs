using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class JumperAgent : Agent
{
    public GameObject Obstacle;
    public GameObject Wall;
    public float jumpForce = 2.0f;
    public float obstacleSpeed = -1;
    bool grounded = true;
    Rigidbody rb;
    public override void OnEpisodeBegin()
    {
        this.transform.localPosition = new Vector3(1, 0.5f, 3);
        this.transform.localRotation = Quaternion.identity;
        Obstacle.transform.localPosition = new Vector3(0.7f, 0.5f, 9.3f);
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
            GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, jumpForce, 0.0f), ForceMode.Impulse);
            grounded = false;
            AddReward(-1f);
        }
        // if standing still
        if (actionBuffers.DiscreteActions[0] == 0 && grounded)
        {
            AddReward(1f * Time.deltaTime);
        }

        Obstacle.transform.Translate(new Vector3(0, 0, obstacleSpeed) * Time.deltaTime);
        float distanceObstacleWall = Vector3.Distance(Obstacle.transform.localPosition, Wall.transform.localPosition);
        float distanceToObstacle = Vector3.Distance(Obstacle.transform.localPosition, this.transform.localPosition);

        // if obstacle hits the wall
        if (distanceObstacleWall <= 0.8f)
        {
            AddReward(5f);
            Obstacle.transform.localPosition = new Vector3(0.7f, 0.5f, 6);
            obstacleSpeed = Random.Range(-5.0f, -0.5f);
        }

        // if obstacle hits the agent
        if (distanceToObstacle <= 0.6f)
        {
            AddReward(-10.0f);
            EndEpisode();
        }

        if (this.transform.localPosition.y < 0.3)
        {
            this.transform.localPosition = new Vector3(1, 0.5f, 3);
            this.transform.localRotation = Quaternion.identity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            grounded = true;
            Debug.Log(Time.deltaTime);
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discActionsOut = actionsOut.DiscreteActions;
        discActionsOut[0] = 0;
        if (Input.GetKey(KeyCode.Space))
        {
            discActionsOut[0] = 1;
        }
    }
}
