using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public int width, height;
    public Grid placementGrid;
    private GameObject _temporaryStructure = null;
    public bool buildPermanent;
    private bool _alreadyPlaced;

    private Dictionary<Vector3Int, StructureModel> _temporaryRoadObjects = new Dictionary<Vector3Int, StructureModel>();
    private Dictionary<Vector3Int, StructureModel> _structureDictionary = new Dictionary<Vector3Int, StructureModel>();

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

    internal void PlaceObjectOnTheMap(Vector3Int position, GameObject structurePrefab, Cell cell, int structureWidth = 1, int structureHeight = 1)
    {
        StructureModel structure = CreateANewStructureModel(position, structurePrefab, cell);
        for (var x = 0; x < structureWidth; x++)
        {
            for (var z = 0; z < structureHeight; z++)
            {
                var newPosition = position + new Vector3Int(x, 0, z);
                placementGrid[newPosition.x, newPosition.z] = cell;
                //Debug.Log(placementGrid[newPosition.x, newPosition.z]);
                _structureDictionary.Add(newPosition, structure);
                DestroyNatureAt(newPosition);
            }
        }
    }
    public Vector3Int? CheckFreeResidence()
    {
        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                if (GetTypeOfPosition(new Vector3Int(i, 0, j)) == typeof(ResidenceCell))
                {
                    var cell = (ResidenceCell)placementGrid[i, j];
                
                    // Check if cell is not null
                    if (cell != null)
                    {
                        //Debug.Log(cell.NumberOfResidentsCapacity + " " + cell.PersonList.Count);

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
        var cell = (ResidenceCell)placementGrid[residencePosition.x, residencePosition.z];
        cell.PersonList.Add(person);
    }
    
    public bool IsStructureCellNotOnFire(Cell cell)
    {
        if (cell is StructureCell structureCell)
        {
            
            return !structureCell.IsOnFire;
        }
        return false;
    }
    public Vector3Int? GetRandomPositionOfTypeCellSatisfying(Type cellType, Func <Cell, bool> predicate = null)
    {
        List<Vector3Int> positionsOfType = new List<Vector3Int>();
    
        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                Type currentType = GetTypeOfPosition(new Vector3Int(i, 0, j));
                Vector3Int position = new Vector3Int(i, 0, j);

                // Check if the current type is the specified type or derived from it
                if (cellType.IsAssignableFrom(currentType))
                {
                    Debug.Log("Found a cell of type " + currentType + " at position " + position);
                    if (predicate != null)
                    {
                        Debug.Log("Checking predicate");
                        if (predicate(GetCellAtPosition(position)))
                        {
                            Debug.Log("Predicate satisfied");
                            positionsOfType.Add(new Vector3Int(i, 0, j));
                            
                        }
                        
                    }
                    else
                    {
                        positionsOfType.Add(new Vector3Int(i, 0, j));
                    }
                }
            }
        }

        if (positionsOfType.Count > 0)
        {
            return positionsOfType[UnityEngine.Random.Range(0, positionsOfType.Count)];
        }

        return null;
    }
    public bool CheckIfCellIsOfBuildingType(Cell cell, BuildingType buildingType)
    {
        if (cell is StructureCell structureCell)
        {
            return structureCell.BuildingType == buildingType;
        }
        return false;
    }
        

    public void RemovePeopleFromResidence(Vector3Int residencePosition)
    {
        var residenceCell = (ResidenceCell)placementGrid[residencePosition.x, residencePosition.z];
        residenceCell.PersonList.Clear();
    }
    public void RemovePersonFromResidence(Vector3Int residencePosition, Person person)
    {
        var residenceCell = (ResidenceCell)placementGrid[residencePosition.x, residencePosition.z];
        residenceCell.PersonList.Remove(person);
    }
    
    public void RemovePersonFromJob(Vector3Int jobPosition, Person person)
    {
        var jobCell = (StructureCell)placementGrid[jobPosition.x, jobPosition.z];
        jobCell.EmployeeList.Remove(person);
    }
    
    
    public Vector3Int? CheckFreeJob()
    {
        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                if (GetCellAtPosition(new Vector3Int(i, 0, j)) is StructureCell structureCell && structureCell.GetType() != typeof(ResidenceCell))
                {
                    var cell = (StructureCell)placementGrid[i, j];
                    // Check if cell is not null
                    if (cell != null)
                    {
                        //Debug.Log("employees capacity: " + cell.NumberOfEmployeesCapacity);
                        //Debug.Log("employees list: " + cell.EmployeeList.Count);
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

        int structureLayer = LayerMask.NameToLayer("Structure");
        
        if (Physics.Raycast(ray, out hit, maxDistance, 1 << structureLayer) )
        {
            // Destroy the game object if a collider is hit
            Destroy(hit.collider.gameObject);
            Cell cellToDestroy = placementGrid[position.x, position.z];
            int widthStructureToDestroy = 1;
            int heightStructureToDestroy = 1;
            if(cellToDestroy is StructureCell)
            {
                StructureCell structureCellToDestroy = (StructureCell)cellToDestroy;
                var (structureWidth, structureHeight, cost) = Cell.GetAttributesForBuildingType(structureCellToDestroy.BuildingType);
                //Debug.Log("Structure width  :" + structureWidth );
                //Debug.Log("Structure height  : "+ structureHeight );
                widthStructureToDestroy = structureWidth;
                heightStructureToDestroy = structureHeight;
            }
            
            for (var x = 0; x < widthStructureToDestroy; x++)
            {
                for (var z = 0; z < heightStructureToDestroy; z++)
                {
                    var newPosition = position + new Vector3Int(x, 0, z);
                
                    if( CheckIfPositionInBound(newPosition) && placementGrid[newPosition.x, newPosition.z] ==  cellToDestroy)
                    {
                        //Debug.Log("Destroying structure at " + newPosition);
                        placementGrid[newPosition.x, newPosition.z] = new EmptyCell();
                        _structureDictionary.Remove(newPosition);
                    }
                    newPosition = position - new Vector3Int(x, 0, z);
                    
                    //check if new position is in bounds
                    if( CheckIfPositionInBound(newPosition) && placementGrid[newPosition.x, newPosition.z] ==  cellToDestroy)
                    {
                        //Debug.Log("Destroying structure at " + newPosition);
                        placementGrid[newPosition.x, newPosition.z] = new EmptyCell();
                        _structureDictionary.Remove(newPosition);
                    }
                    newPosition = position + new Vector3Int(-x, 0, z);
                
                    if(CheckIfPositionInBound(newPosition) && placementGrid[newPosition.x, newPosition.z] ==  cellToDestroy)
                    {
                        //Debug.Log("Destroying structure at " + newPosition);
                        placementGrid[newPosition.x, newPosition.z] = new EmptyCell();
                        _structureDictionary.Remove(newPosition);
                    }
                    newPosition = position + new Vector3Int(x, 0, -z);
                
                    if(CheckIfPositionInBound(newPosition) && placementGrid[newPosition.x, newPosition.z] ==  cellToDestroy)
                    {
                        //Debug.Log("Destroying structure at " + newPosition);
                        placementGrid[newPosition.x, newPosition.z] = new EmptyCell();
                        _structureDictionary.Remove(newPosition);
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
    
    internal void PlaceTemporaryStructureWithButton(Vector3Int position, GameObject structurePrefab, BuildingType buildingType)
    {
        var (structureWidth, structureHeight, cost) = Cell.GetAttributesForBuildingType(buildingType);
        if(structureManager.CheckBigStructure(position, structureWidth , structureHeight))
        {
            if (_alreadyPlaced == false)
            {
                _temporaryStructure = new GameObject("TemporaryStructure");
                _temporaryStructure.transform.SetParent(transform);
                _temporaryStructure.transform.localPosition = position;
                var structureModel = _temporaryStructure.AddComponent<StructureModel>();
                structureModel.CreateModel(structurePrefab);
                _alreadyPlaced = true;
            }

            _temporaryStructure.transform.position = position;
            if (buildPermanent)
            {
                DestroyTemporaryStructure();
            }
        }
    }

    public void DestroyTemporaryStructure()
    {
        Destroy(_temporaryStructure);
        _alreadyPlaced = false;
        buildPermanent = false;
    }


    internal void PlaceTemporaryStructure<T>(Vector3Int position, GameObject structurePrefab) where T : Cell, new()
    {
        T cellType = new T();
        placementGrid[position.x, position.z] = cellType;
        StructureModel structure = CreateANewStructureModel(position, structurePrefab, cellType);
        _temporaryRoadObjects.Add(position, structure);
    }

    public List<Vector3Int> GetNeighboursOfTypeFor<T>(Vector3Int position) where T : Cell, new()
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
        foreach (var structure in _temporaryRoadObjects.Values)
        {
            var position = Vector3Int.RoundToInt(structure.transform.position);
            EmptyCell emptyCell = new EmptyCell();
            placementGrid[position.x, position.z] = emptyCell;
            Destroy(structure.gameObject);
        }
        _temporaryRoadObjects.Clear();
    }

    internal void AddTemporaryStructuresToStructureDictionary()
    {
        foreach (var structure in _temporaryRoadObjects)
        {
            _structureDictionary.Add(structure.Key, structure.Value);
            DestroyNatureAt(structure.Key);
        }
        _temporaryRoadObjects.Clear();
    }

    public void ModifyStructureModel(Vector3Int position, GameObject newModel, Quaternion rotation)
    {
        if (_temporaryRoadObjects.ContainsKey(position))
            _temporaryRoadObjects[position].SwapModel(newModel, rotation);
        else if (_structureDictionary.ContainsKey(position))
            _structureDictionary[position].SwapModel(newModel, rotation);
    }
}
