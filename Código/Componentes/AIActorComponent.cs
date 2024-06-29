using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIActorComponent : MonoBehaviour
{
    public AIComponent manager;
    private UnityEngine.AI.NavMeshAgent agent;
    private ShipComponent player;
    void Start()	{
        player = manager.player;
	    agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	}

    void Update(){
        agent.SetDestination(player.gameObject.transform.position);
    }
}
