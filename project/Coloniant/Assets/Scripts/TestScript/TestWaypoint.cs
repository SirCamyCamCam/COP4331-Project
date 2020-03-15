/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

public class TestWaypoint {

    [Test]
    public void TestSetUpWaypointTypes()
    {
        AssertTrue(SetUpWaypointTypes(WaypointManager.WaypointType.ENTRANCE, WaypointManager.Level.UNDER_GROUND, null));
    }

    [Test]
    public void TestAddAConnectedWaypoint()
    {
        Waypoint w1 = new Waypoint();
        Waypoint w2 = new Waypoint();
        AssertTrue(w1.AddAConnectedWaypoint(w2));
    }

    [Test]
    public void TestRemoveAConnectedWaypoint()
    {
        Waypoint w1 = new Waypoint();
        Waypoint w2 = new Waypoint();
        w1.AddConnectedWaypoint(w2);
        AssertTrue(w1.RemoveAConnectedWaypoint(w2));
    }

    [Test]
    public void TestCurrentLevel()
    {
        Waypoint w1 = new Waypoint();
        w1.level = WaypointManager.Level.UNDER_GROUND;
        AssertTrue(w1.CurrentLevel() == WaypointManager.Level.UNDER_GROUND);
    }

    [Test]
    public void TestSwitchLevel()
    {
        Waypoint w1 = new Waypoint();
        w1.level = WaypointManager.Level.UNDER_GROUND;
        w1.SwitchLevel(WaypointManager.Level.ABOVE_GROUND);
        AssertTrue(w1.CurrentLevel() == WaypointManager.Level.ABOVE_GROUND);
    }
}
*/