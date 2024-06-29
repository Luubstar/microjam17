using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus.Components;
public class AIComponent : MonoBehaviour
{
    private static NavMeshSurface Surface2D;
    public ShipComponent player;

    public static void Start(NavMeshSurface m){Surface2D = m;}

	public static void UpdateMesh()
    { 
        Surface2D.UpdateNavMesh(Surface2D.navMeshData);

    }
}