using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

using Player;
public class Agents : Agent
{
    private playerCon _playerCon;

    void Start()
    {
        _playerCon = this.GetComponent<playerCon>();
    }
    
    public override void OnEpisodeBegin()
    {

    }
    public Transform clearPoint;

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(clearPoint.position);
        sensor.AddObservation(this.gameObject.transform.position);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        var index = Mathf.FloorToInt(actions.ContinuousActions[0]);
        var actionNum = index % 3;
        if(index == 0)
        {
            _playerCon.Shot();
        }
        else if(index == 1)
        {
            _playerCon.Jump();
        }
        else if(index == 2)
        {
            _playerCon.Move();
        }

        float dist = Vector2.Distance(this.gameObject.transform.position, clearPoint.position);

        if(dist < 60F)
        {
            SetReward(0.01F);
        }
        else if(dist < 50F)
        {
            SetReward(0.02F);
        }
        else if(dist < 40F)
        {
            SetReward(0.05F);
        }
        else if(dist < 30F)
        {
            SetReward(0.08F);
        }
        else if(dist <20F)
        {
            SetReward(0.1F);
        }
        else if(dist < 10F)
        {
            SetReward(0.5F);
        }
        else if (dist < 0.5F)
        {
            SetReward(3.0F);
            EndEpisode();
        }
        else if(dist > 62F)
        {
            SetReward(-0.5F);
            EndEpisode();
        }
    }

}
