/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

public class TestWaypointManager {

    
    [Test]
    public void TestSpawnWaypoint()
    {
        Vector3 spawn = new Vector3(0, 0, 0);
        List<Waypoint> fakeList = new List<Waypoint>();
        Waypoint waypoint = new Waypoint();
        Assert.Null(WaypointManager.main.SpawnWaypoint(WaypointManager.WaypointType.TRANSITION, WaypointManager.Level.UNDER_GROUND, null, spawn));
        Assert.Null(WaypointManager.main.SpawnWaypoint(WaypointManager.WaypointType.TRANSITION, WaypointManager.Level.UNDER_GROUND, fakeList, spawn));
        Assert.Null(WaypointManager.main.SpawnWaypoint(WaypointManager.WaypointType.TRANSITION, WaypointManager.Level.UNDER_GROUND, fakeList, null));
        fakeList.Add(waypoint);
        Assert.IsNotNull(WaypointManager.main.SpawnWaypoint(WaypointManager.WaypointType.TRANSITION, WaypointManager.Level.UNDER_GROUND, fakeList, spawn));
    }

    [Test]
    public void TestRemoveWaypoint()
    {
        Waypoint waypoint = new Waypoint();
        Vector3 spawn = new Vector3(0, 0, 0);
        List<Waypoint> fakeList = new List<Waypoint>();
        Waypoint waypoint2 = new Waypoint();
        fakeList.Add(waypoint2);
        Assert.False(WaypointManager.main.RemoveWaypoint(null));
        WaypointManager.main.SpawnWaypoint(WaypointManager.WaypointType.TRANSITION, WaypointManager.Level.UNDER_GROUND, fakeList, spawn);
        Assert.True(WaypointManager.main.RemoveWaypoint(waypoint));
    }

    [Test]
    public void TestAddToAntCount()
    {
        Waypoint waypoint = new Waypoint();
        Vector3 spawn = new Vector3(0, 0, 0);
        List<Waypoint> fakeList = new List<Waypoint>();
        Waypoint waypoint2 = new Waypoint();
        fakeList.Add(waypoint2);
        WaypointManager.main.SpawnWaypoint(WaypointManager.WaypointType.TRANSITION, WaypointManager.Level.UNDER_GROUND, fakeList, spawn);
        Assert.False(WaypointManager.main.AddToAntCountInBridge(null, waypoint2));
        Assert.False(WaypointManager.main.AddToAntCountInBridge(waypoint, null));
        Assert.True(WaypointManager.main.AddToAntCountInBridge(waypoint, waypoint2));
    }

    [Test]
    public void TestRemoveFromAntCount()
    {
        Waypoint waypoint = new Waypoint();
        Vector3 spawn = new Vector3(0, 0, 0);
        List<Waypoint> fakeList = new List<Waypoint>();
        Waypoint waypoint2 = new Waypoint();
        fakeList.Add(waypoint2);
        WaypointManager.main.SpawnWaypoint(WaypointManager.WaypointType.TRANSITION, WaypointManager.Level.UNDER_GROUND, fakeList, spawn);
        Assert.False(WaypointManager.main.RemoveAntFromBridgeCount(null, waypoint2));
        Assert.False(WaypointManager.main.RemoveAntFromBridgeCount(waypoint, null));
        Assert.True(WaypointManager.main.RemoveAntFromBridgeCount(waypoint, waypoint2));
    }

    [Test]
    public void SwitchLevelTest()
    {
        Waypoint waypoint = new Waypoint();
        Vector3 spawn = new Vector3(0, 0, 0);
        List<Waypoint> fakeList = new List<Waypoint>();
        Waypoint waypoint2 = new Waypoint();
        fakeList.Add(waypoint2);
        WaypointManager.main.SpawnWaypoint(WaypointManager.WaypointType.TRANSITION, WaypointManager.Level.UNDER_GROUND, fakeList, spawn);
        WaypointManager.main.SwitchWaypointLevel(WaypointManager.Level.ABOVE_GROUND);
        AssertTrue(fakeList[0].CurrentLevel() == WaypointManager.Level.ABOVE_GROUND);
    }
}
*/