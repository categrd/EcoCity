using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TransportManager : MonoBehaviour
{
    public GameObject personPrefab;
    private List<Person> _pedestrianMovingList = new List<Person>();
    private List<Person> _peopleArrivedAtDestination = new List<Person>();
    
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
            agent.SetDestination(targetPosition);
            _pedestrianMovingList.Add(person);
        }
        else
        {
            Debug.LogError("NavMeshAgent component not found on the personPrefab.");
        }
    }

    private void Update()
    {
        foreach (var person in _pedestrianMovingList)
        { 
            if(person.personPrefab != null)
            {
                //let's check if person is idle by checking  if he's in a small radius from his starting position
                
                if (person.startingPosition.HasValue && Vector3.Distance(person.personPrefab.transform.position, person.startingPosition.Value) < 0.1f)
                {
                    person.idleTime += Time.deltaTime;
                    if (person.idleTime >= 10)
                    {
                        Destroy(person.personPrefab);
                        personPrefab = null;
                        person.busyTime = 10;
                        person.startingPosition = null;
                        person.targetPosition = null;
                        person.currentPosition = person.housePosition;
                        person.idleTime = 0;
                    }
                }
                else person.idleTime = 0;
                //let's check if person has reached his destination in a small radius
                if (person.targetPosition.HasValue && Vector3.Distance(person.personPrefab.transform.position, person.targetPosition.Value) < 0.5f)
                {
                    _peopleArrivedAtDestination.Add(person);
                
                }
            }
            
        }
        foreach (var person in _peopleArrivedAtDestination)
        {
            person.busyTime = 100;
            person.currentPosition = person.targetPosition;
            Destroy(person.personPrefab);
            person.personPrefab = null;
            _pedestrianMovingList.Remove(person);  
        }
        _peopleArrivedAtDestination.Clear();
    }
}
