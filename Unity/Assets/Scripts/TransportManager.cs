using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TransportManager : MonoBehaviour
{
    
    //make a list of different prefabs for different types of people
    public GameObject fireTruckPrefab;
    public PlacementManager placementManager;
    private List<Person> _fireTruckList = new List<Person>();
    
    private List<Person> _pedestrianMovingList = new List<Person>();
    private List<Person> _peopleArrivedAtDestination = new List<Person>();
    private List<Person> _peopleToDestroy = new List<Person>();
    private List<Person> _carMovingList = new List<Person>();
    
    public void MovePersonToPosition(Person person, Vector3Int startingPosition, Vector3Int targetPosition, GameObject prefab)
    {
        Vector3 startingPositionOnStreet = startingPosition + new Vector3(0, 0.041f, -0.3f);
        person.personPrefab = Instantiate(prefab, startingPositionOnStreet , Quaternion.identity);
        // Get the Animator component from the instantiated person
        person.personAnimator = person.personPrefab.GetComponent<Animator>();
        // Check if the Animator component is present
        if (person.personAnimator == null)
        {
            Debug.LogError("Animator component not found on the instantiated person!");
        }
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
    public void MoveCarToPosition(Person person, Vector3Int startingPosition, Vector3Int targetPosition, GameObject prefab)
    {
        Vector3 startingPositionOnStreet = startingPosition + new Vector3(0, 0.021f, -0.1f);
        person.carPrefab = Instantiate(prefab, startingPositionOnStreet , Quaternion.identity);
        NavMeshAgent agent = person.carPrefab.GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            
            person.startingPosition = startingPositionOnStreet;
            person.targetPosition = targetPosition;
            person.isPersonFree = false;
            agent.SetDestination(targetPosition);
            _carMovingList.Add(person);
        }
        else
        {
            Debug.LogError("NavMeshAgent component not found on the personPrefab.");
        }
    }
    public void SendFireTruckToFire(Person person, Vector3Int startingPosition, Vector3Int targetPosition, StructureCell structureTarget ,GameObject prefab)
    {
        MoveCarToPosition(person, startingPosition, targetPosition, prefab);
        _fireTruckList.Add(person);
        person.structureToExtinguishFire = structureTarget;
        Debug.Log("structure to extinguish fire" + person.structureToExtinguishFire);
    }

    private bool destroyPerson;
    private bool destroyCar;
    private float _time;
    private void Update()
    {
        _time += Time.deltaTime;
        if (_time >= 1)
        {
            _time = 0;
            HandlePeopleMovement();
            HandleCarMovement();
        }
        
    }
    void HandlePeopleMovement()
    {
        foreach (var person in _pedestrianMovingList)
        { 
            if(person.personPrefab != null && person.carPrefab == null)
            {
                destroyPerson = false;
                //let's check if person has reached his destination in a small radius
                if (person.targetPosition.HasValue && Vector3.Distance(person.personPrefab.transform.position, person.targetPosition.Value) < 0.5f)
                {
                    _peopleArrivedAtDestination.Add(person);
                
                }
                HandlePeopleStayingAtStartingPosition(person);
                HandlePeopleStayingAtSamePosition(person);
                if (destroyPerson)
                {
                    _peopleToDestroy.Add(person);
                }
            }
        }
        foreach (var person in _peopleArrivedAtDestination)
        {
            person.busyTime = 100;
            person.currentPosition = person.targetPosition;
            Destroy(person.personPrefab);
            person.personPrefab = null;
            person.carPrefab = null;
            _pedestrianMovingList.Remove(person);  
        }
        _peopleArrivedAtDestination.Clear();
        foreach (var person in _peopleToDestroy)
        {
            DestroyPersonPrefab(person);
        }
        _peopleToDestroy.Clear();
    }
    void HandleCarMovement()
    {
        foreach (var person in _carMovingList)
        { 
            if(person.carPrefab != null && person.personPrefab == null)
            {
                destroyCar = false;
                //let's check if person has reached his destination in a small radius
                if (person.targetPosition.HasValue && Vector3.Distance(person.carPrefab.transform.position, person.targetPosition.Value) < 1f)
                {
                    Debug.Log("car arrived at destination");
                    _peopleArrivedAtDestination.Add(person);
                
                }
                //HandleCarStayingAtStartingPosition(person);
                //HandleCarStayingAtSamePosition(person);
                if (destroyCar)
                {
                    _peopleToDestroy.Add(person);
                }
            }
        }
        foreach (var person in _peopleArrivedAtDestination)
        {
            person.busyTime = 100;
            person.currentPosition = person.targetPosition;
            Debug.Log("How many firetrack" + _fireTruckList.Count);
            //if the person is a fire truck, let's make him extinguish the fire
            if (_fireTruckList.Contains(person))
            {
                Debug.Log("fire truck arrived at destination");
                
                if (person.jobPosition.HasValue)
                {
                    Debug.Log("fire truck extinguishing fire");
                    Vector3Int fireTruckStructure = person.jobPosition.Value;
                    PublicServiceCell fireTruckStructureCell = (PublicServiceCell) placementManager.GetCellAtPosition(fireTruckStructure);
                    fireTruckStructureCell.FireTrucks++;
                    // extinguish the fire after 5 seconds
                    StartCoroutine(DelayedExtinguishFire(person, 5f));
                }
            }
            else
            {
                Destroy(person.carPrefab);
                person.personPrefab = null;
                person.carPrefab = null;
                _carMovingList.Remove(person);
            } 
        }
        _peopleArrivedAtDestination.Clear(); 
        foreach (var person in _peopleToDestroy)
        {
            DestroyPersonPrefab(person);
        }
        _peopleToDestroy.Clear();
    }
    IEnumerator DelayedExtinguishFire(Person person, float delay)
    {
        Debug.Log("delayed extinguish fire");
        yield return new WaitForSeconds(delay);

        if (person != null)
        {
            // Your logic for ExtinguishFire using the provided Person instance
            ExtinguishFire(person);
        }
    }
    void ExtinguishFire(Person person)
    {
        Debug.Log("extinguish fire");
        if (person.structureToExtinguishFire != null)
        {
            Debug.Log("extinguish fire in " + person.structureToExtinguishFire);
            person.structureToExtinguishFire.IsOnFire = false;
            person.structureToExtinguishFire.TimeOnFire = 0;
            person.structureToExtinguishFire.IsFireTruckOnTheWay = false;
            person.structureToExtinguishFire.FirePrefab.SetActive(false);
            person.structureToExtinguishFire = null;
            person.isPersonFree = true;
            person.busyTime = 0;
            person.currentPosition = person.housePosition;
            person.targetPosition = null;
            person.startingPosition = null;
            _fireTruckList.Remove(person);
            Destroy(person.carPrefab);
            person.personPrefab = null;
            person.carPrefab = null;
            _carMovingList.Remove(person);
        }
    }
    
    void HandleCarStayingAtStartingPosition(Person person)
    {
        //let's check if person is idle by checking  if he's in a small radius from his starting position
        if (person.startingPosition.HasValue && Vector3.Distance(person.carPrefab.transform.position, person.startingPosition.Value) < 0.1f)
        {
            person.idleTimeAtStartingPosition +=1;
            if (person.idleTimeAtStartingPosition >= 10)
            {
                destroyCar = true;
            }
        }
        else person.idleTimeAtStartingPosition=0;  
    }
    void HandleCarStayingAtSamePosition(Person person)
    {
        //let's check if person is idle by checking  if he's in a small radius from the last position
        if (person.lastPosition.HasValue && Vector3.Distance(person.carPrefab.transform.position, person.lastPosition.Value) < 0.5f)
        {
            Debug.Log("car is staying at same position with idleTime:" + person.idleTime);
            person.idleTime += 1;
            if (person.idleTime >= 10)
            {
                destroyCar = true;
            }
        }
        else
        {
            person.idleTime = 0;
        }
        if (person.carPrefab != null)
        {
            person.lastPosition = person.carPrefab.transform.position;
        }
    }
    
    void HandlePeopleStayingAtStartingPosition(Person person)
    {
        //let's check if person is idle by checking  if he's in a small radius from his starting position
        if (person.startingPosition.HasValue && Vector3.Distance(person.personPrefab.transform.position, person.startingPosition.Value) < 0.1f)
        {
            person.idleTimeAtStartingPosition +=1;
            if (person.idleTimeAtStartingPosition >= 5)
            {
                destroyPerson = true;
            }
        }
        else person.idleTimeAtStartingPosition = 0;  
    }
    void HandlePeopleStayingAtSamePosition(Person person)
    {
        //let's check if person is idle by checking  if he's in a small radius from the last position
        if (person.lastPosition.HasValue && Vector3.Distance(person.personPrefab.transform.position, person.lastPosition.Value) < 0.01f)
        {
            person.idleTime +=1;
            person.personAnimator.SetBool("isWalking", false);
            if (person.idleTime >= 5)
            {
                destroyPerson = true;
            }
        }
        else
        {
            person.idleTime = 0;
            person.personAnimator.SetBool("isWalking", true);
        }
        if (person.personPrefab != null)
        {
            person.lastPosition = person.personPrefab.transform.position;
        }
    }

    void DestroyPersonPrefab(Person person)
    {
        if(person.personPrefab != null)
            Destroy(person.personPrefab);
        if(person.carPrefab != null)
            Destroy(person.carPrefab);
        _pedestrianMovingList.Remove(person);
        _carMovingList.Remove(person);
        person.personAnimator = null;
        person.personPrefab = null;
        person.carPrefab = null;
        person.busyTime = 20;
        person.startingPosition = null;
        person.targetPosition = null;
        person.currentPosition = person.housePosition;
        person.idleTime = 0;
        person.idleTimeAtStartingPosition = 0;
    }
    
}
