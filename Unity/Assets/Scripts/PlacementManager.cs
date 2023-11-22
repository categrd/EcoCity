using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public int width, height;
    public Grid placementGrid;
    GameObject temporaryStructure = null;
    public bool buildPermanent;
    private bool alreadyPlaced;

    private Dictionary<Vector3Int, StructureModel> temporaryRoadobjects = new Dictionary<Vector3Int, StructureModel>();
    private Dictionary<Vector3Int, StructureModel> structureDictionary = new Dictionary<Vector3Int, StructureModel>();

    public StructureManager structureManager;
    private void Start()
    {
        placementGrid = new Grid(width, height);
    }

    internal Type[] GetNeighbourTypesFor(Vector3Int position)
    {
        return placementGrid.GetAllAdjacentCellTypes(position.x, position.z);
    }

    internal bool CheckIfPositionInBound(Vector3Int position)
    {
        if(position.x >= 0 && position.x < width && position.z >=0 && position.z < height)
        {
            return true;
        }
        return false;
    }

    internal void PlaceObjectOnTheMap(Vector3Int position, GameObject structurePrefab, Cell type, int width = 1, int height = 1)
    {
        StructureModel structure = CreateANewStructureModel(position, structurePrefab, type);
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                var newPosition = position + new Vector3Int(x, 0, z);
                placementGrid[newPosition.x, newPosition.z] = type;
                Debug.Log(placementGrid[newPosition.x, newPosition.z]);
                structureDictionary.Add(newPosition, structure);
                DestroyNatureAt(newPosition);
            }
        }
    }
    public Vector3Int? CheckFreeResidence()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (GetTypeOfPosition(new Vector3Int(i, 0, j)) == typeof(ResidenceCell))
                {
                    ResidenceCell cell = (ResidenceCell)placementGrid[i, j];
                
                    // Check if cell is not null
                    if (cell != null)
                    {
                        Debug.Log(cell.NumberOfResidentsCapacity + " " + cell.PersonList.Count);

                        // Check if PersonList is not null before accessing Count
                        if (cell.NumberOfResidentsCapacity > cell.PersonList?.Count)
                        {
                            return new Vector3Int(i, 0, j);
                        }
                    }
                    else
                    {
                        // Log an error or handle the case where cell is null
                        Debug.LogError("ResidenceCell is null at position: " + new Vector3Int(i, 0, j));
                    }
                }
            }
        }
        return null;
    }

    public void AddNewPersonInResidence(Vector3Int residencePosition, Person person)
    {
        ResidenceCell cell = (ResidenceCell)placementGrid[residencePosition.x, residencePosition.z];
        cell.PersonList.Add(person);
    }

    public Vector3Int? CheckOccupiedResidence()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (GetTypeOfPosition(new Vector3Int(i, 0, j)) == typeof(ResidenceCell))
                {
                    ResidenceCell cell = (ResidenceCell)placementGrid[i, j];
                
                    // Check if cell is not null
                    if (cell != null)
                    {
                        Debug.Log(cell.NumberOfResidentsCapacity + " " + cell.PersonList.Count);

                        // Check if PersonList is not null before accessing Count
                        if (cell.PersonList?.Count > 0)
                        {
                            return new Vector3Int(i, 0, j);
                        }
                    }
                    else
                    {
                        // Log an error or handle the case where cell is null
                        Debug.LogError("ResidenceCell is null at position: " + new Vector3Int(i, 0, j));
                    }
                }
            }
        }
        return null;
    }

    public void RemovePeopleFromResidence(Vector3Int residencePosition)
    {
        ResidenceCell residenceCell = (ResidenceCell)placementGrid[residencePosition.x, residencePosition.z];
        residenceCell.PersonList.Clear();
    }
    public void RemovePersonFromResidence(Vector3Int residencePosition, Person person)
    {
        ResidenceCell residenceCell = (ResidenceCell)placementGrid[residencePosition.x, residencePosition.z];
        residenceCell.PersonList.Remove(person);
    }
    
    public void RemovePersonFromJob(Vector3Int jobPosition, Person person)
    {
        StructureCell jobCell = (StructureCell)placementGrid[jobPosition.x, jobPosition.z];
        jobCell.EmployeeList.Remove(person);
    }
    
    
    public Vector3Int? CheckFreeJob()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (GetCellAtPosition(new Vector3Int(i, 0, j)) is StructureCell structureCell && structureCell.GetType() != typeof(ResidenceCell))
                {
                    StructureCell cell = (StructureCell)placementGrid[i, j];
                    // Check if cell is not null
                    if (cell != null)
                    {
                        Debug.Log("employees capacity: " + cell.NumberOfEmployeesCapacity);
                        Debug.Log("employees list: " + cell.EmployeeList.Count);
                        if (cell.NumberOfEmployeesCapacity > cell.EmployeeList.Count)
                        {
                            return new Vector3Int(i, 0, j);
                        }
                    }
                    else
                    {
                        // Log an error or handle the case where cell is null
                        Debug.LogError("Structure is null at position: " + new Vector3Int(i, 0, j));
                    }
                }
            }
        }
        return null;
    }
    public Vector3Int? CheckPersonJob(Person person)
    {
            return person.jobPosition;
    }
    public List<Person> CheckPeopleAtJob(Vector3Int jobPosition)
    {
        StructureCell cell = (StructureCell)placementGrid[jobPosition.x, jobPosition.z];
        return cell.EmployeeList;
    }
    public Person CheckPersonAtPosition(Vector3Int position)
    {
        ResidenceCell cell = (ResidenceCell)placementGrid[position.x, position.z];
        return cell.PersonList[cell.PersonList.Count - 1];
    }
    public List<Person> GetPeopleAtPosition(Vector3Int position)
    {
        ResidenceCell cell = (ResidenceCell)placementGrid[position.x, position.z];
        return cell.PersonList;
    }
    
    public void AddPersonToJob(Vector3Int jobPosition, Person person)
    {
        StructureCell cell = (StructureCell)placementGrid[jobPosition.x, jobPosition.z];
        cell.EmployeeList.Add(person);
    }
    
    private void DestroyNatureAt(Vector3Int position)
    {
        RaycastHit[] hits = Physics.BoxCastAll(position + new Vector3(0, 0.5f, 0), new Vector3(0.5f, 0.5f, 0.5f), transform.up, Quaternion.identity, 1f, 1 << LayerMask.NameToLayer("Nature"));
        foreach (var item in hits)
        {
            Destroy(item.collider.gameObject);
        }
    }

    public void DestroyGameObjectAt(Vector3Int position)
    {
        // Define the ray from above the position pointing downwards
        Ray ray = new Ray(position + new Vector3(0, 10f, 0), Vector3.down);

        // Set the maximum distance of the ray
        float maxDistance = 20f;

        // RaycastHit variable to store information about the hit
        RaycastHit hit;

        // Check if the ray hits any collider
        if (Physics.Raycast(ray, out hit, maxDistance, 1 << LayerMask.NameToLayer("Structure")))
        {
            // Destroy the game object if a collider is hit
            Destroy(hit.collider.gameObject);
            Cell cellToDestroy = placementGrid[position.x, position.z];
            for (int x = 0; x < cellToDestroy.Width; x++)
            {
                for (int z = 0; z < cellToDestroy.Height; z++)
                {
                    var newPosition = position + new Vector3Int(x, 0, z);
                
                    if(placementGrid[newPosition.x, newPosition.z] ==  cellToDestroy)
                    {
                        Debug.Log("Destroying structure at " + newPosition);
                        placementGrid[newPosition.x, newPosition.z] = new EmptyCell();
                        structureDictionary.Remove(newPosition);
                    }
                }
            }
        }
    }


    internal bool CheckIfPositionIsFree(Vector3Int position)
    {
        Type emptyCellType = typeof(EmptyCell);
        return CheckIfPositionIsOfType(position, emptyCellType);
    }


    public bool CheckIfPositionIsOfType(Vector3Int position, Type cellType)
    {
        return placementGrid[position.x, position.z].GetType() == cellType;
    }
    
    public bool CheckIfPositionIsBuildingType(Vector3Int position, BuildingType buildingType)
    {
        if (placementGrid[position.x, position.z] is StructureCell structureCell)
        {
            return structureCell.BuildingType == buildingType;
        }
        return false;
    }
    
    public Cell GetCellAtPosition(Vector3Int position)
    {
        return placementGrid[position.x, position.z];
    }


    public Type GetTypeOfPosition(Vector3Int position)
    {
        return placementGrid[position.x, position.z].GetType();
    }
    
    internal void PlaceTemporaryStructureWithButton(Vector3Int position, GameObject structurePrefab)
    {
        if(structureManager.CheckPositionBeforePlacement(position))
        {
            if (alreadyPlaced == false)
            {
                temporaryStructure = Instantiate(structurePrefab);
                alreadyPlaced = true;
            }

            temporaryStructure.transform.position = position;
            if (buildPermanent)
            {
                DestroyTemporaryStructure();
            }
        }
    }

    public void DestroyTemporaryStructure()
    {
        Destroy(temporaryStructure);
        alreadyPlaced = false;
        buildPermanent = false;
    }


    internal void PlaceTemporaryStructure<T>(Vector3Int position, GameObject structurePrefab) where T : Cell, new()
    {
        T cellType = new T();
        placementGrid[position.x, position.z] = cellType;
        StructureModel structure = CreateANewStructureModel(position, structurePrefab, cellType);
        temporaryRoadobjects.Add(position, structure);
    }

    internal List<Vector3Int> GetNeighboursOfTypeFor<T>(Vector3Int position) where T : Cell, new()
    {
        Type cellType = typeof(T);
        var neighbourVertices = placementGrid.GetAdjacentCellsOfType(position.x, position.z, cellType);
        List<Vector3Int> neighbours = new List<Vector3Int>();
        foreach (var point in neighbourVertices)
        {
            neighbours.Add(new Vector3Int(point.X, 0, point.Y));
        }
        return neighbours;
    }

    private StructureModel CreateANewStructureModel(Vector3Int position, GameObject structurePrefab, Cell cellType)
    {
        GameObject structure = new GameObject(cellType.ToString());
        structure.transform.SetParent(transform);
        structure.transform.localPosition = position;
        var structureModel = structure.AddComponent<StructureModel>();
        structureModel.CreateModel(structurePrefab);
        return structureModel;
    }

    internal List<Vector3Int> GetPathBetween(Vector3Int startPosition, Vector3Int endPosition)
    {
        var resultPath = GridSearch.AStarSearch(placementGrid, new Point(startPosition.x, startPosition.z), new Point(endPosition.x, endPosition.z));
        List<Vector3Int> path = new List<Vector3Int>();
        foreach (Point point in resultPath)
        {
            path.Add(new Vector3Int(point.X, 0, point.Y));
        }
        return path;
    }

    internal void RemoveAllTemporaryStructures()
    {
        foreach (var structure in temporaryRoadobjects.Values)
        {
            var position = Vector3Int.RoundToInt(structure.transform.position);
            EmptyCell emptyCell = new EmptyCell();
            placementGrid[position.x, position.z] = emptyCell;
            Destroy(structure.gameObject);
        }
        temporaryRoadobjects.Clear();
    }

    internal void AddtemporaryStructuresToStructureDictionary()
    {
        foreach (var structure in temporaryRoadobjects)
        {
            structureDictionary.Add(structure.Key, structure.Value);
            DestroyNatureAt(structure.Key);
        }
        temporaryRoadobjects.Clear();
    }

    public void ModifyStructureModel(Vector3Int position, GameObject newModel, Quaternion rotation)
    {
        if (temporaryRoadobjects.ContainsKey(position))
            temporaryRoadobjects[position].SwapModel(newModel, rotation);
        else if (structureDictionary.ContainsKey(position))
            structureDictionary[position].SwapModel(newModel, rotation);
    }
}
