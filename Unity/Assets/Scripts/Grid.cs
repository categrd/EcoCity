using System;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEditor.Search;

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

    
}


public class Cell
{
    private int _cost;
    private int height;
    private int width;
    public int Cost { get; set; }
    public int Height { get; set; }
    public int Width { get; set; }
    public Cell()
    {
        // Set the default value for _dimensions to 1
        Height = 1;
        Width = 1;
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
    private int _numberOfEmployees;

    public BuildingType BuildingType { get; set; }
    public int MaintenanceCost { get; set; }
    public int IncomeGenerated { get; set; }
    public float Beauty { get; set; }
    public float EnergyConsumption { get; set; }
    public float WasteProduction { get; set; }
    public int NumberOfEmployees { get; set; }
}

public class SanityCell : StructureCell
{
    private int _patientCapacity;
    public int PatientCapacity { get; set; }
    // Properties and methods specific to hospitals
    
    public SanityCell(BuildingType buildingType)
    {
        if(buildingType == BuildingType.Clinic)
        {
            Cost = 200000;
            MaintenanceCost = 200;
            IncomeGenerated = 0;
            Beauty = 0;
            EnergyConsumption = 50;
            WasteProduction = 20;
            NumberOfEmployees = 20;
            PatientCapacity = 100;
        }

        if (buildingType == BuildingType.Hospital)
        {
            Cost = 1000000;
            MaintenanceCost = 2000;
            IncomeGenerated = 0;
            Beauty = 0;
            EnergyConsumption = 400;
            WasteProduction = 150;
            NumberOfEmployees = 150;
            PatientCapacity = 1500;
            
        }
    }

}

public class ResidenceCell : StructureCell
{
    private int _numberOfResidents;
    private float _comfortLevel;
    public int NumberOfResidents { get; set; }
    public float ComfortLevel { get; set; }
    // Properties and methods specific to houses
    public ResidenceCell(BuildingType buildingType)
    {
        if(buildingType == BuildingType.House)
        {
            Cost = 100000;
            MaintenanceCost = 0;
            Beauty = 1f;
            EnergyConsumption = 20;
            WasteProduction = 5;
            NumberOfEmployees = 0;
            NumberOfResidents = 4;
            IncomeGenerated = 100 * NumberOfResidents;
            ComfortLevel = 5;
        }

        if (buildingType == BuildingType.HighDensityHouse)
        {
            Cost = 300000;
            MaintenanceCost = 0;
            Beauty = -0.1f;
            EnergyConsumption = 200;
            WasteProduction = 50;
            NumberOfEmployees = 0;
            NumberOfResidents = 40;
            IncomeGenerated = 100 * NumberOfResidents;
            ComfortLevel = 2;
        }

    }
}

public class EnergyProductionCell : StructureCell
{
    private float _energyProduced;
    public float EnergyProduced { get; set; }

    public EnergyProductionCell(BuildingType buildingType)
    {
        if (buildingType == BuildingType.SolarPanel)
        {
            Cost = 500000;
            MaintenanceCost = 100;
            IncomeGenerated = 0;
            Beauty = 1f;
            EnergyConsumption = 0;
            WasteProduction = 1;
            NumberOfEmployees = 2;
            EnergyProduced = 1000;
        }

        if (buildingType == BuildingType.WindTurbine)
        {
            Cost = 500000;
            MaintenanceCost = 100;
            IncomeGenerated = 0;
            Beauty = 1f;
            EnergyConsumption = 0;
            WasteProduction = 1;
            NumberOfEmployees = 2;
            EnergyProduced = 1000;

        }

        if (buildingType == BuildingType.CarbonPowerPlant)
        {
            Cost = 1000000;
            MaintenanceCost = 500;
            IncomeGenerated = 0;
            Beauty = -2f;
            EnergyConsumption = 0;
            WasteProduction = 20;
            NumberOfEmployees = 10;
            EnergyProduced = 10000;

        }

        if (buildingType == BuildingType.NuclearPlant)
        {
            Cost = 10000000;
            MaintenanceCost = 1000;
            IncomeGenerated = 0;
            Beauty = 1f;
            EnergyConsumption = 0;
            WasteProduction = 1;
            NumberOfEmployees = 25;
            EnergyProduced = 50000;

        }
    }
}

public class EntertainmentCell : StructureCell
{
    private float _happiness;
    
    public float Happiness { get; set; }
    
    public EntertainmentCell(BuildingType buildingType)
    {
        if (buildingType == BuildingType.Shop)
        {
            Cost = 100000;
            MaintenanceCost = 0;
            IncomeGenerated = 1000;
            Beauty = 0.5f;
            EnergyConsumption = 50;
            WasteProduction = 1;
            NumberOfEmployees = 5;
            Happiness = 10;

        }
        if (buildingType == BuildingType.Bar)
        {
            Cost = 50000;
            MaintenanceCost = 0;
            IncomeGenerated = 1200;
            Beauty = -0.5f;
            EnergyConsumption = 30;
            WasteProduction = 5;
            NumberOfEmployees = 5;
            Happiness = 12;

        }
        if (buildingType == BuildingType.Restaurant)
        {
            Cost = 200000;
            MaintenanceCost = 0;
            IncomeGenerated = 2500;
            Beauty = 2f;
            EnergyConsumption = 100;
            WasteProduction = 10;
            NumberOfEmployees = 15;
            Happiness = 20;

        }
        if (buildingType == BuildingType.Cinema)
        {
            Cost = 400000;
            MaintenanceCost = 0;
            IncomeGenerated = 1600;
            Beauty = 1f;
            EnergyConsumption = 60;
            WasteProduction = 3;
            NumberOfEmployees = 6;
            Happiness = 15;

        }
        
    }
    
    
}

public class PublicServiceCell : StructureCell
{
    private int _criminalsCovered;
    private int _firesCovered;
    public int CriminalsCovered { get; set; }
    public int FiresCovered { get; set; }
    public PublicServiceCell(BuildingType buildingType)
    {
        if(buildingType == BuildingType.PoliceStation)
        {
            Cost = 250000;
            MaintenanceCost = 1000;
            IncomeGenerated = 0;
            Beauty = 0;
            EnergyConsumption = 100;
            WasteProduction = 5;
            NumberOfEmployees = 20;
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
            NumberOfEmployees = 10;
        }
        if(buildingType == BuildingType.University)
        {
            Cost = 1000000;
            MaintenanceCost = 1500;
            IncomeGenerated = 0;
            Beauty = 0.5f;
            EnergyConsumption = 300;
            WasteProduction = 20;
            NumberOfEmployees = 30;
        }
        
    }
}
public class IndustryCell : StructureCell
{
    private int _goods;
    private int _vegetablesProduced;
    private int _meatProduced;
    public int GoodsProduced { get; set;}
    public int VegetablesProduced { get; set; }
    public int MeatProduced { get; set; }

    public IndustryCell(BuildingType buildingType)
    {
        if (buildingType == BuildingType.Factory)
        {
            Cost = 1000000;
            MaintenanceCost = 0;
            IncomeGenerated = 4000;
            Beauty = -1f;
            EnergyConsumption = 300;
            WasteProduction = 30;
            NumberOfEmployees = 50;
            GoodsProduced = 50;
            VegetablesProduced = 0;
            MeatProduced = 0;


        }
        if (buildingType == BuildingType.Crop)
        {
            Cost = 300000;
            MaintenanceCost = 0;
            IncomeGenerated = 1500;
            Beauty = 0.1f;
            EnergyConsumption = 50;
            WasteProduction = 10;
            NumberOfEmployees = 40;
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
            NumberOfEmployees = 30;
            GoodsProduced = 0;
            VegetablesProduced = 0;
            MeatProduced = 100;
        }
    }
}

public class GarbageDisposalCell : StructureCell
{
    private int _garbageDisposed;
    public int GarbageDisposed { get; set; }

    public GarbageDisposalCell(BuildingType buildingType)
    {
        if (buildingType == BuildingType.Landfill)
        {
            Cost = 50000;
            MaintenanceCost = 50;
            IncomeGenerated = 0;
            Beauty = -5f;
            EnergyConsumption = 10;
            WasteProduction = 0;
            NumberOfEmployees = 10;
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
            NumberOfEmployees = 15;
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
            NumberOfEmployees = 20;
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
}
