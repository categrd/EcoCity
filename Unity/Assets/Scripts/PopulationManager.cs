using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using Random = System.Random;

public class PopulationManager : MonoBehaviour
{
    public GameObject femaleCasual;
    public GameObject femaleDress;
    public GameObject maleCasual;
    public GameObject maleSuit;
    GameObject prefab;
    
    public TransportManager transportManager;
    public PlacementManager placementManager;
    public List<Person> joblessPeople = new List<Person>();
    public List<Person> peopleList = new List<Person>();

    private void Update()
    {
        //Debug.Log("number of person in peopleList " + peopleList.Count);
        foreach (var person in peopleList)
        {
            if(person.busyTime > 0)
            {
                person.busyTime -= Time.deltaTime;
            }

            if (person.busyTime <= 0)
            {
                person.busyTime = 0;
                person.isPersonFree = true;
            }
        }
    }
    public void HandlePeopleMovement()
    {
        foreach (Person person in peopleList)
        {
            if (person.isPersonFree && person.personPrefab == null && person.currentPosition != null)
            {
                
                Vector3Int personCurrentPosition = (Vector3Int) person.currentPosition;
                Vector3Int? targetPosition = null;
                List<Vector3Int> neighboursRoads = placementManager.GetNeighboursOfTypeFor<RoadCell>(personCurrentPosition);
                // if there's at least a road next to the person, we continue with the code
                if (neighboursRoads.Count > 0)
                {
                    Vector3Int startingPosition = neighboursRoads[0];
                    if (placementManager.GetCellAtPosition(personCurrentPosition) is ResidenceCell)
                    {
                        Random random = new Random();
                        int randomValue = random.Next(2);
                        if (randomValue == 0)
                        {
                            targetPosition = placementManager.GetRandomPositionOfTypeCellSatisfying(typeof(EntertainmentCell));
                            if(person.sex == 0)
                                prefab = maleCasual;
                            else
                                prefab = femaleCasual;
                        }
                        else
                        {
                            targetPosition = person.jobPosition;
                            if(person.sex == 0)
                                prefab = maleSuit;
                            else
                                prefab = femaleDress;
                        }
                    }

                    if (placementManager.GetCellAtPosition(personCurrentPosition) is JobCell)
                    {
                        targetPosition = person.housePosition;

                    }

                    if (targetPosition != null)
                    {
                        Vector3Int? targetRoad = GetTargetRoadPosition((Vector3Int)targetPosition);
                        if (targetRoad != null)
                        {
                            transportManager.MovePersonToPosition(person, startingPosition, (Vector3Int)targetPosition, prefab);
                        }

                    }
                }
            }
        }
    }
    private Vector3Int? GetTargetRoadPosition(Vector3Int targetPosition)
    {
        Cell targetCell = placementManager.GetCellAtPosition((Vector3Int)targetPosition);
        for (var x = 0; x < targetCell.StructureWidth; x++)
        {
            for (var z = 0; z < targetCell.StructureHeight; z++)
            {
                var newPosition = targetPosition + new Vector3Int(x, 0, z);
                
                if(placementManager.CheckIfPositionInBound(newPosition) && placementManager.GetCellAtPosition((Vector3Int)newPosition) ==  targetCell)
                {
                    List<Vector3Int>  neighboursTargetRoads = placementManager.GetNeighboursOfTypeFor<RoadCell>((Vector3Int)targetPosition);
                    if (neighboursTargetRoads.Count > 0)
                    {
                        return neighboursTargetRoads[0];
                    }
                }
            }
        }

        return null;
    }

    public void CreateNewPerson()
    {
        Vector3Int? residencePosition = placementManager.CheckFreeResidence();
        if(residencePosition == null)
        {
            //Debug.Log("No free residence");
            return;
        }
        Person newPerson = new Person();
        joblessPeople.Add(newPerson);
        peopleList.Add(newPerson);
        
        newPerson.housePosition = residencePosition;
        newPerson.currentPosition = residencePosition;
        newPerson.sex = UnityEngine.Random.Range(0, 2);
        placementManager.AddNewPersonInResidence((Vector3Int)residencePosition, newPerson);
        
        //Debug.Log("new residence position " + residencePosition);
    }

    public void RemoveRandomPerson()
    {
        Vector3Int? residencePosition = null;
        Vector3Int? jobPosition = null;
        Person person = null;
        if(peopleList.Count > 0)
        {
            person = peopleList[0];
        }
        if (person != null)
        {
            residencePosition = person.housePosition;
            jobPosition = person.jobPosition;
        }
        else return;
        if(residencePosition != null)
        {
            placementManager.RemovePersonFromResidence((Vector3Int)residencePosition, person);
        }
        if(jobPosition != null)
        {
            placementManager.RemovePersonFromJob((Vector3Int)jobPosition, person);
        }
        peopleList.Remove(person);
        
    }
    public void RemovePeopleAt(Vector3Int residencePosition)
    {
        Vector3Int? jobPosition = null;
        List<Person> peopleAtPosition = null;
        if(residencePosition != null)
        {
            peopleAtPosition = placementManager.GetPeopleAtPosition((Vector3Int)residencePosition);
            foreach (Person person in peopleAtPosition)
            {
                jobPosition = placementManager.CheckPersonJob(person);
                if(jobPosition != null)
                {
                    placementManager.RemovePersonFromJob((Vector3Int)jobPosition, person);
                }
                else joblessPeople.Remove(person);
            }
            placementManager.RemovePeopleFromResidence((Vector3Int)residencePosition);
            peopleList.RemoveAll(person => peopleAtPosition.Contains(person));
        }
    }
    
    public void FindJob()
    {
        Vector3Int? jobPosition = placementManager.CheckFreeJob();
        //g.Log("job position is" + jobPosition);
        if(jobPosition == null)
        {
            //Debug.Log("No free job");
            return;
        }
        if(joblessPeople.Count == 0)
        {
            //Debug.Log("No jobless people");
            return;
        }
        Person person = joblessPeople[0];
        person.jobPosition = jobPosition;
        joblessPeople.Remove(person);
        placementManager.AddPersonToJob((Vector3Int)jobPosition, person);
    }

    public void RemovePeopleJob(Vector3Int jobPosition)
    {
        foreach (Person person in placementManager.CheckPeopleAtJob(jobPosition))
        {
            person.jobPosition = null;
            joblessPeople.Add(person);
        }
    }
}
