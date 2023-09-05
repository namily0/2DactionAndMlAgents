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

    public override void CollectObservations(VectorSensor sensor)
    {

    }

    public override void OnActionReceived(ActionBuffers actions)
    {

    }

}
