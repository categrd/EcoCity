using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationManager : MonoBehaviour
{
    public PlacementManager placementManager;
    public List<Person> joblessPeople = new List<Person>();
    public void CreateNewPerson()
    {
        Vector3Int? residencePosition = placementManager.CheckFreeResidence();
        if(residencePosition == null)
        {
            Debug.Log("No free residence");
            return;
        }
        Person newPerson = new Person();
        joblessPeople.Add(newPerson);
        
        newPerson.housePosition = residencePosition;
        
        placementManager.AddNewPersonInResidence((Vector3Int)residencePosition, newPerson);
        
        Debug.Log("new residence position " + residencePosition);
    }

    public void DestroyRandomPerson()
    {
        Vector3Int? residencePosition = placementManager.CheckOccupiedResidence();
        Vector3Int? jobPosition = null;
        Person person = null;
        if(residencePosition != null)
        {
            person = placementManager.CheckPersonAtPosition((Vector3Int)residencePosition);
            placementManager.RemovePersonFromResidence((Vector3Int)residencePosition, person);
            jobPosition = placementManager.CheckPersonJob(person);
        }
        if(jobPosition != null)
        {
            placementManager.RemovePersonFromJob((Vector3Int)jobPosition, person);
        }
        
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
        }
    }
    
    public void FindJob()
    {
        Vector3Int? jobPosition = placementManager.CheckFreeJob();
        Debug.Log("job position is" + jobPosition);
        if(jobPosition == null)
        {
            Debug.Log("No free job");
            return;
        }
        if(joblessPeople.Count == 0)
        {
            Debug.Log("No jobless people");
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
