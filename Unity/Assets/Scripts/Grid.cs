using System;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor.Search;
using UnityEngine;
using Random = System.Random;

/// <summary>
/// Source https://github.com/lordjesus/Packt-Introduction-to-graph-algorithms-for-game-developers
/// </summary>
public class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        if (obj is Point)
        {
            Point p = obj as Point;
            return this.X == p.X && this.Y == p.Y;
        }
        return false;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 6949;
            hash = hash * 7907 + X.GetHashCode();
            hash = hash * 7907 + Y.GetHashCode();
            return hash;
        }
    }

    public override string ToString()
    {
        return "P(" + this.X + ", " + this.Y + ")";
    }
}

public enum BuildingType
{
    Clinic,
    Hospital,
    
    SolarPanel,
    WindTurbine,
    CarbonPowerPlant,
    NuclearPlant,
    
    House,
    HighDensityHouse,
    
    Shop,
    Restaurant,
    Bar,
    Cinema,
    
    University,
    FireStation,
    PoliceStation,
    
    Factory,
    Crop,
    Livestock,
    
    Landfill,
    IncinerationPlant,
    WasteToEnergyPlant,
    
    Park

    
}

public class Person
{
    public GameObject personPrefab = null;
    public int health;
    public int education;
    public int happiness;
    public int money;
    public Vector3Int? jobPosition = null;
    public Vector3Int? housePosition = null;
    public float busyTime = 0;
    public bool isPersonFree = true;
    public Vector3Int? currentPosition = null;
    public int crime;
    public int fire;
    public int entertainment;
    public int transport;
}

public class Cell
{
    private int _cost;
    private int _structureHeight;
    private int _structureWidth;
    public int Cost { get; set; }
    public int StructureHeight { get; set; }
    public int StructureWidth { get; set; }
    public static (int _structureWidth, int _structureHeight, int cost) GetAttributesForBuildingType(BuildingType buildingType)
    {
        // Provide the width, height, and cost based on the building type
        switch (buildingType)
        {
            case BuildingType.House:
                return (1, 1, 100000);
            case BuildingType.HighDensityHouse:
                return (1, 1, 300000);
            case BuildingType.Clinic:
                return (1, 1, 200000);
            case BuildingType.Hospital:
                return (1, 1, 1000000);
            case BuildingType.Cinema:
                return (1, 2, 400000);
            case BuildingType.Crop:
                return (2, 2, 300000);
            case BuildingType.PoliceStation:
                return (1, 1, 250000);
            case BuildingType.Factory:
                return (3, 3, 500000);
            case BuildingType.Bar:
                return (1, 1, 50000);
            case BuildingType.WindTurbine:
                return (1, 1, 50000);
            case BuildingType.Shop:
                return (1, 1, 100000);
            case BuildingType.Park:
                return (1, 1, 10000);
            
            // Add more cases for other building types
            default:
                return (0, 0, 0); // Default values if the building type is not recognized
        }
    }
    public Cell()
    {
        // Set the default value for _dimensions to 1
        StructureHeight = 1;
        StructureWidth = 1;
    }
}

public class EmptyCell : Cell
{
    
    // Properties and methods specific to empty cells
}

public class RoadCell : Cell
{
    
    // Properties and methods specific to road cells
}




    

public class StructureCell : Cell
{
    private BuildingType _buildingType;
    private int _maintenanceCost;
    private int _incomeGenerated;
    private float _beauty;
    private float _energyConsumption;
    private float _wasteProduction;
    private int _numberOfEmployeesCapacity;
    private List<Person> _employeeList;
    private float _carbonMonoxide;
    



    public BuildingType BuildingType { get; set; }
    public int MaintenanceCost { get; set; }
    public int IncomeGenerated { get; set; }
    public float Beauty { get; set; }
    public float EnergyConsumption { get; set; }
    public float WasteProduction { get; set; }
    public int NumberOfEmployeesCapacity { get; set; }
    public List<Person> EmployeeList { get; set; }
    public float CarbonMonoxideProduction { get; set; }

}

public class JobCell : StructureCell
{
    
}
public class SanityCell : JobCell
{
    private int _patientCapacity;
    public int PatientCapacity { get; set; }
    // Properties and methods specific to hospitals
    
    public SanityCell(BuildingType buildingType)
    {
        EmployeeList = new List<Person>();
        if(buildingType == BuildingType.Clinic)
        {
            Cost = 200000;
            MaintenanceCost = 200;
            IncomeGenerated = 0;
            Beauty = 0;
            EnergyConsumption = 50;
            WasteProduction = 20;
            NumberOfEmployeesCapacity = 20;
            PatientCapacity = 50;
            CarbonMonoxideProduction = 1;
        }

        if (buildingType == BuildingType.Hospital)
        {
            Cost = 1000000;
            MaintenanceCost = 2000;
            IncomeGenerated = 0;
            Beauty = 0;
            EnergyConsumption = 400;
            WasteProduction = 150;
            NumberOfEmployeesCapacity = 150;
            PatientCapacity = 500;
            CarbonMonoxideProduction = 3;
            
        }
    }

}

public class ResidenceCell : StructureCell
{
    private int _numberOfResidentsCapacity;
    private float _comfortLevel;
    private List<Person> _personList;
    public int NumberOfResidentsCapacity { get; set; }
    public float ComfortLevel { get; set; }
    public List<Person> PersonList { get; set; }
    
    // Properties and methods specific to houses
    public ResidenceCell(BuildingType buildingType)
    {
        PersonList = new List<Person>();
        if(buildingType == BuildingType.House)
        {
            Cost = 100000;
            MaintenanceCost = 0;
            Beauty = 0.1f;
            EnergyConsumption = 20;
            WasteProduction = 5;
            NumberOfEmployeesCapacity = 0;
            NumberOfResidentsCapacity = 4;
            IncomeGenerated = 100 * NumberOfResidentsCapacity;
            ComfortLevel = 0.1f;
            CarbonMonoxideProduction = 0.1f;
        }

        if (buildingType == BuildingType.HighDensityHouse)
        {
            Cost = 300000;
            MaintenanceCost = 0;
            Beauty = -0.01f;
            EnergyConsumption = 200;
            WasteProduction = 50;
            NumberOfEmployeesCapacity = 0;
            NumberOfResidentsCapacity = 40;
            IncomeGenerated = 100 * NumberOfResidentsCapacity;
            ComfortLevel = -0.5f;
            CarbonMonoxideProduction = 0.5f;
        }

    }
}

public class EnergyProductionCell : JobCell
{
    private float _energyProduced;
    public float EnergyProduced { get; set; }

    public EnergyProductionCell(BuildingType buildingType)
    {
        EmployeeList = new List<Person>();
        if (buildingType == BuildingType.SolarPanel)
        {
            Cost = 500000;
            MaintenanceCost = 100;
            IncomeGenerated = 0;
            Beauty = 1f;
            EnergyConsumption = 0;
            WasteProduction = 1;
            NumberOfEmployeesCapacity = 2;
            EnergyProduced = 1000;
            CarbonMonoxideProduction = 0;
        }

        if (buildingType == BuildingType.WindTurbine)
        {
            Cost = 500000;
            MaintenanceCost = 100;
            IncomeGenerated = 0;
            Beauty = 1f;
            EnergyConsumption = 0;
            WasteProduction = 1;
            NumberOfEmployeesCapacity = 2;
            EnergyProduced = 1000;
            CarbonMonoxideProduction = 0;

        }

        if (buildingType == BuildingType.CarbonPowerPlant)
        {
            Cost = 1000000;
            MaintenanceCost = 500;
            IncomeGenerated = 0;
            Beauty = -5f;
            EnergyConsumption = 0;
            WasteProduction = 20;
            NumberOfEmployeesCapacity = 10;
            EnergyProduced = 10000;
            CarbonMonoxideProduction = 10;

        }

        if (buildingType == BuildingType.NuclearPlant)
        {
            Cost = 10000000;
            MaintenanceCost = 1000;
            IncomeGenerated = 0;
            Beauty = -1f;
            EnergyConsumption = 0;
            WasteProduction = 1;
            NumberOfEmployeesCapacity = 25;
            EnergyProduced = 50000;
            CarbonMonoxideProduction = 0;

        }
    }
}

public class EntertainmentCell : JobCell
{
    private int _costumersCapacity;
    
    public int CostumersCapacity { get; set; }
    
    public EntertainmentCell(BuildingType buildingType)
    {
        EmployeeList = new List<Person>();
        if (buildingType == BuildingType.Shop)
        {
            Cost = 100000;
            MaintenanceCost = 0;
            IncomeGenerated = 1000;
            Beauty = 0.5f;
            EnergyConsumption = 50;
            WasteProduction = 1;
            NumberOfEmployeesCapacity = 5;
            CostumersCapacity = 20;

        }
        if (buildingType == BuildingType.Bar)
        {
            Cost = 50000;
            MaintenanceCost = 0;
            IncomeGenerated = 1200;
            Beauty = -0.5f;
            EnergyConsumption = 30;
            WasteProduction = 5;
            NumberOfEmployeesCapacity = 5;
            CostumersCapacity = 10;

        }
        if (buildingType == BuildingType.Restaurant)
        {
            Cost = 200000;
            MaintenanceCost = 0;
            IncomeGenerated = 2500;
            Beauty = 2f;
            EnergyConsumption = 100;
            WasteProduction = 10;
            NumberOfEmployeesCapacity = 15;
            CostumersCapacity = 30;

        }
        if (buildingType == BuildingType.Cinema)
        {
            StructureWidth = 1;
            StructureHeight = 2;
            Cost = 400000;
            MaintenanceCost = 0;
            IncomeGenerated = 1600;
            Beauty = 1f;
            EnergyConsumption = 60;
            WasteProduction = 3;
            NumberOfEmployeesCapacity = 6;
            CostumersCapacity = 40;

        }
        
    }
    
    
}

public class PublicServiceCell : JobCell
{
    private int _criminalsCovered;
    private int _firesCovered;
    public int CriminalsCovered { get; set; }
    public int FiresCovered { get; set; }
    public PublicServiceCell(BuildingType buildingType)
    {
        EmployeeList = new List<Person>();
        if(buildingType == BuildingType.PoliceStation)
        {
            Cost = 250000;
            MaintenanceCost = 1000;
            IncomeGenerated = 0;
            Beauty = 0;
            EnergyConsumption = 100;
            WasteProduction = 5;
            NumberOfEmployeesCapacity = 20;
            CriminalsCovered = 100;
        }
        if(buildingType == BuildingType.FireStation)
        {
            Cost = 250000;
            MaintenanceCost = 500;
            IncomeGenerated = 0;
            Beauty = 0;
            EnergyConsumption = 100;
            WasteProduction = 5;
            NumberOfEmployeesCapacity = 10;
        }
        if(buildingType == BuildingType.University)
        {
            Cost = 1000000;
            MaintenanceCost = 1500;
            IncomeGenerated = 0;
            Beauty = 2.5f;
            EnergyConsumption = 300;
            WasteProduction = 20;
            NumberOfEmployeesCapacity = 30;
        }
        
    }
}
public class IndustryCell : JobCell
{
    private int _goods;
    private int _vegetablesProduced;
    private int _meatProduced;
    public int GoodsProduced { get; set;}
    public int VegetablesProduced { get; set; }
    public int MeatProduced { get; set; }

    public IndustryCell(BuildingType buildingType)
    {
        EmployeeList = new List<Person>();
        if (buildingType == BuildingType.Factory)
        {
            Cost = 1000000;
            MaintenanceCost = 0;
            IncomeGenerated = 4000;
            Beauty = -1f;
            EnergyConsumption = 300;
            WasteProduction = 30;
            NumberOfEmployeesCapacity = 50;
            GoodsProduced = 50;
            VegetablesProduced = 0;
            MeatProduced = 0;


        }
        if (buildingType == BuildingType.Crop)
        {
            StructureWidth = 2;
            StructureHeight = 2;
            Cost = 300000;
            MaintenanceCost = 0;
            IncomeGenerated = 1500;
            Beauty = 0.1f;
            EnergyConsumption = 50;
            WasteProduction = 10;
            NumberOfEmployeesCapacity = 40;
            GoodsProduced = 0;
            VegetablesProduced = 120;
            MeatProduced = 0;

        }
        if (buildingType == BuildingType.Livestock)
        {
            Cost = 350000;
            MaintenanceCost = 0;
            IncomeGenerated = 1500;
            Beauty = 0.2f;
            EnergyConsumption = 60;
            WasteProduction = 15;
            NumberOfEmployeesCapacity = 30;
            GoodsProduced = 0;
            VegetablesProduced = 0;
            MeatProduced = 100;
        }
    }
}

public class GarbageDisposalCell : JobCell
{
    private int _garbageDisposed;
    public int GarbageDisposed { get; set; }

    public GarbageDisposalCell(BuildingType buildingType)
    {
        EmployeeList = new List<Person>();
        if (buildingType == BuildingType.Landfill)
        {
            Cost = 50000;
            MaintenanceCost = 50;
            IncomeGenerated = 0;
            Beauty = -1f;
            EnergyConsumption = 10;
            WasteProduction = 0;
            NumberOfEmployeesCapacity = 10;
            GarbageDisposed = 500;
        }
        if (buildingType == BuildingType.IncinerationPlant)
        {
            Cost = 400000;
            MaintenanceCost = 500;
            IncomeGenerated = 0;
            Beauty = 0f;
            EnergyConsumption = 150;
            WasteProduction = 0;
            NumberOfEmployeesCapacity = 15;
            GarbageDisposed = 200;
        }
        if (buildingType == BuildingType.WasteToEnergyPlant)
        {
            Cost = 1000000;
            MaintenanceCost = 1000;
            IncomeGenerated = 0;
            Beauty = 0f;
            EnergyConsumption = 0;
            WasteProduction = 0;
            NumberOfEmployeesCapacity = 20;
            GarbageDisposed = 500;
        }
    }
}

public class Grid
{
    private Cell[,] _grid;
    private int _width;
    public int Width { get { return _width; } }
    private int _height;
    public int Height { get { return _height; } }

    private List<Point> _roadList = new List<Point>();
    private List<Point> _hospitalList = new List<Point>();

    public Grid(int width, int height)
    {
        _width = width;
        _height = height;
        _grid = new Cell[width, height];
        
        // Initialize the grid with EmptyCell instances
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                _grid[i, j] = new EmptyCell();
            }
        }
    }
    

    // Adding index operator to our Grid class so that we can use grid[][] to access specific cell from our grid. 
    public Cell this[int i, int j]
    {
        get
        {
            return _grid[i, j];
        }
        set
        {
            if (value is RoadCell)
            {
                _roadList.Add(new Point(i, j));
            }
            else
            {
                _roadList.Remove(new Point(i, j));
            }
            if (value is SanityCell)
            {
                _hospitalList.Add(new Point(i, j));
            }
            else
            {
                _hospitalList.Remove(new Point(i, j));
            }
            _grid[i, j] = value;
        }
    }

    public static bool IsCellWakable(Cell cellType, bool aiAgent = false)
    {
        if (aiAgent)
        {
            return cellType is RoadCell;
        }
        return cellType is EmptyCell || cellType is RoadCell;
    }

    public Point GetRandomRoadPoint()
    {
        Random rand = new Random();
        return _roadList[rand.Next(0, _roadList.Count - 1)];
    }

    public Point GetRandomSpecialStructurePoint()
    {
        Random rand = new Random();
        return _roadList[rand.Next(0, _roadList.Count - 1)];
    }

    public List<Point> GetAdjacentCells(Point cell, bool isAgent)
    {
        return GetWakableAdjacentCells((int)cell.X, (int)cell.Y, isAgent);
    }

    public float GetCostOfEnteringCell(Point cell)
    {
        return 1;
    }

    public List<Point> GetAllAdjacentCells(int x, int y)
    {
        List<Point> adjacentCells = new List<Point>();
        if (x > 0)
        {
            adjacentCells.Add(new Point(x - 1, y));
        }
        if (x < _width - 1)
        {
            adjacentCells.Add(new Point(x + 1, y));
        }
        if (y > 0)
        {
            adjacentCells.Add(new Point(x, y - 1));
        }
        if (y < _height - 1)
        {
            adjacentCells.Add(new Point(x, y + 1));
        }
        return adjacentCells;
    }

    public List<Point> GetWakableAdjacentCells(int x, int y, bool isAgent)
    {
        List<Point> adjacentCells = GetAllAdjacentCells(x, y);
        for (int i = adjacentCells.Count - 1; i >= 0; i--)
        {
            if(IsCellWakable(_grid[adjacentCells[i].X, adjacentCells[i].Y], isAgent)==false)
            {
                adjacentCells.RemoveAt(i);
            }
        }
        return adjacentCells;
    }

    public List<Point> GetAdjacentCellsOfType(int x, int y, Type cellType)
    {
        List<Point> adjacentCells = GetAllAdjacentCells(x, y);
        for (int i = adjacentCells.Count - 1; i >= 0; i--)
        {
            if (_grid[adjacentCells[i].X, adjacentCells[i].Y].GetType() != cellType)
            {
                adjacentCells.RemoveAt(i);
            }
        }
        return adjacentCells;
    }

    /// <summary>
    /// Returns array [Left neighbour, Top neighbour, Right neighbour, Down neighbour]
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public Type[] GetAllAdjacentCellTypes(int x, int y)
    {
        Type[] neighbours = new Type[4];
        if (x > 0)
        {
            neighbours[0] = _grid[x - 1, y].GetType();
        }
        if (x < _width - 1)
        {
            neighbours[2] = _grid[x + 1, y].GetType();
        }
        if (y > 0)
        {
            neighbours[3] = _grid[x, y - 1].GetType();
        }
        if (y < _height - 1)
        {
            neighbours[1] = _grid[x, y + 1].GetType();
        }
        return neighbours;
    }

    public Vector3Int? GetRandomPositionOfTypeCell(Type cellType)
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                if (_grid[i, j].GetType() == cellType)
                {
                    return new Vector3Int(i, j, 0);
                }
            }
        }
        return null;
    }
}
