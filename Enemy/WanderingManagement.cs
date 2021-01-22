using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WanderingManagement
{
    private List<Vector3> wanderingPoints;
    private System.Random random;
    // Start is called before the first frame update

    public WanderingManagement()
    {
        wanderingPoints = initializeWanderingPoints();
        random = new System.Random();
    }

    public Vector3 getWanderingPoint(Vector3 excludePoint)
    {
        var tmpWanderingPoints = excludePoint == null? new List<Vector3>(wanderingPoints): getWithoutExcluded(excludePoint);

        int randomIndex = random.Next(0, tmpWanderingPoints.Count);

        return tmpWanderingPoints[randomIndex];
    }

    private List<Vector3> getWithoutExcluded(Vector3 excludePoint)
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

    private List<Vector3> initializeWanderingPoints()
    {
        return Help.returnPositionsOfChildrens("WanderingPoints");
    }
}
