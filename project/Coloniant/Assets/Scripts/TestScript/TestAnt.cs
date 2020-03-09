using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

public class TestAnt {

	[Test]
    public void AssignWaypointTest()
    {
        Ant newAnt = new Ant();
        GameObject newWaypoint = new GameObject().AddComponent<WayPoint>();
        AssertTrue(newAnt.AssignTargetWaypoint(newWaypoint));
    }

    [Test]
    public void ChangeView()
    {
        Ant newAnt = new Ant();
        newAnt.antLevel = AntManager.SceneView.UNDER_GROUND;
        AssertTrue(newAnt.ChangeView(AntManager.SceneView.ABOVE_GROUND));
    }
}
