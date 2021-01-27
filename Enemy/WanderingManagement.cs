using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WanderingManagement
{
    private List<Vector3> wanderingPoints;
    private System.Random random;

    public WanderingManagement()
    {
        wanderingPoints = InitializeWanderingPoints();
        random = new System.Random();
    }

    public Vector3 GetWanderingPoint(Vector3 excludePoint) // called by enemy everytime when he arives at destination point
    {
        var tmpWanderingPoints = excludePoint == null? new List<Vector3>(wanderingPoints): GetWithoutExcluded(excludePoint);

        int randomIndex = random.Next(0, tmpWanderingPoints.Count);

        return tmpWanderingPoints[randomIndex];
    }

    private List<Vector3> GetWithoutExcluded(Vector3 excludePoint) // get List of wandering points, where given element is excluded. This way point at which enemy arived is not set again
    {
        var tmpWanderingPoints = new List<Vector3>(wanderingPoints);
        if (excludePoint != null)
        {
            if (tmpWanderingPoints.Contains(excludePoint))
            {
                tmpWanderingPoints.Remove(excludePoint);
                return wanderingPoints;
            } 
        }

        return tmpWanderingPoints;
    }

    private List<Vector3> InitializeWanderingPoints() // wandering points are objects manualy placed on the map
    {
        return Help.ReturnPositionsOfChildrens("WanderingPoints");
    }
}
