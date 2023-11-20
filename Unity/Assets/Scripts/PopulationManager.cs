using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationManager : MonoBehaviour
{
    public PlacementManager placementManager;
    public List<Person> joblessPeople = new List<Person>();
    public void CreateNewPerson()
    {
        Vector3Int residencePosition = placementManager.CheckFreeResidence();
        if(residencePosition == new Vector3Int(-1, -1, -1))
        {
            Debug.Log("No free residence");
            return;
        }
        Person newPerson = new Person();
        joblessPeople.Add(newPerson);
        newPerson.housePosition = residencePosition;
        placementManager.AddNewPersonInResidence(residencePosition, newPerson);
        Debug.Log("new residence position " + residencePosition);
    }

    public void DestroyPerson()
    {
        Vector3Int residencePosition = placementManager.CheckOccupiedResidence();
        Person person = placementManager.CheckPersonAtPosition(residencePosition);
        Vector3Int jobPosition = placementManager.CheckPersonJob(person);
        if(residencePosition == new Vector3Int(-1, -1, -1) && jobPosition == new Vector3Int(-1, -1, -1))
        {
            Debug.Log("No occupied residence or job");
            return;
        }
        placementManager.RemovePersonFromResidenceAndJob(residencePosition, jobPosition, person);
    }
    
    public void FindJob()
    {
        Vector3Int jobPosition = placementManager.CheckFreeJob();
        Debug.Log("job position is" + jobPosition);
        if(jobPosition == new Vector3Int(-1, -1, -1))
        {
            Debug.Log("No free job");
            return;
        }
        if(joblessPeople.Count == 0)
        {
            Debug.Log("No jobless people");
            return;
        }
        Person person = joblessPeople[joblessPeople.Count - 1];
        person.jobPosition = jobPosition;
        joblessPeople.Remove(person);
        placementManager.AddPersonToJob(jobPosition, person);
    }

    public void RemovePeopleFromJob(Vector3Int jobPosition)
    {
        foreach (Person person in placementManager.CheckPeopleAtJob(jobPosition))
        {
            person.jobPosition = new Vector3Int(-1, -1, -1);
            joblessPeople.Add(person);
        }
    }
}
