using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TransportManager : MonoBehaviour
{
    public GameObject personPrefab;
    private List<Person> pedestrianMovingList = new List<Person>();
    
    public void MovePersonToPosition(Person person, Vector3Int startingPosition, Vector3Int targetPosition)
    {
        Vector3 startingPositionOnStreet = startingPosition + new Vector3(0, 0.041f, -0.3f);
        person.personPrefab = Instantiate(personPrefab, startingPositionOnStreet , Quaternion.identity);
        NavMeshAgent agent = person.personPrefab.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            person.startingPosition = startingPositionOnStreet;
            person.targetPosition = targetPosition;
            person.isPersonFree = false;
            person.busyTime = 100;
            agent.SetDestination(targetPosition);
            pedestrianMovingList.Add(person);
        }
        else
        {
            Debug.LogError("NavMeshAgent component not found on the personPrefab.");
        }
    }

    private void Update()
    {
        foreach (var person in pedestrianMovingList)
        { 
            if(person.personPrefab != null)
            {
                if (person.personPrefab.transform.position == person.startingPosition)
                {
                    person.idleTime += Time.deltaTime;
                    if (person.idleTime >= 10)
                    {
                        Destroy(person.personPrefab);
                        person.busyTime = 10;
                        person.startingPosition = null;
                        person.targetPosition = null;
                        person.currentPosition = person.housePosition;
                        person.idleTime = 0;
                    }
                }
                else person.idleTime = 0;
            }

            if (person.personPrefab.transform.position == person.targetPosition)
            {
                Destroy(person.personPrefab);
            }
        }
    }
    
}
