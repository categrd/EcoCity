using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{
    public NavMeshSurface navMeshSurfaceWalkable;
    public NavMeshSurface navMeshSurfaceRoad;

    void Start()
    {
        BakeNavMesh();
    }

    void BakeNavMesh()
    {
        if (navMeshSurfaceWalkable != null)
        {
            navMeshSurfaceWalkable.BuildNavMesh();
        }
        if (navMeshSurfaceRoad != null)
        {
            navMeshSurfaceRoad.BuildNavMesh();
        }
    }
    void Update()
    {
        BakeNavMesh();
    }
}