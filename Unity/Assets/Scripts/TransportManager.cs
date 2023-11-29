using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TransportManager : MonoBehaviour
{
    public GameObject personPrefab;
    
    public void MovePersonToPosition(Person person, Vector3Int startingPosition, Vector3Int targetPosition)
    {
        person.personPrefab = Instantiate(personPrefab, startingPosition + new Vector3(0,0.041f,-0.3f), Quaternion.identity);
        NavMeshAgent agent = person.personPrefab.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.SetDestination(targetPosition);
        }
        else
        {
            Debug.LogError("NavMeshAgent component not found on the personPrefab.");
        }
    }
}
