/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

public class TestLeafManager {

	[Test]
    public void TestCollectedALeaf()
    {
        Waypoint w = new Waypoint();
        Assert.IsNotNull(CollectedALeaf(w));
    }

    [Test]
    public void TestAddLeafToFarm()
    {
        Waypoint w = new Waypoint();
        Leaf l = new Leaf();
        Assert.True(AddLeafToFarm(w, l));
    }

    [Test]
    public void TestNewFarmWaypoint()
    {
        Waypoint w = new Waypoint();
        Assert.True(NewFarmWaypoint(w));
    }

    [Test]
    public void TestNewLeafSite()
    {
        Waypoint w = new Waypoint();
        Assert.True(NewLeafSite(w));
    }

    [Test]
    public void TestReturnLeavesAtFarm()
    {
        Waypoint w = new Waypoint();
        Assert.False(ReturnLeavesAtFarm(w));
    }

    [Test]
    public void TestReturnLeavesAtLeafSite()
    {
        Waypoint w = new Waypoint();
        Assert.False(ReturnLeavesAtLeafSite(w));
    }

    [Test]
    public void TestAssignSelectedLeaf()
    {
        Leaf l = new Leaf();
        Assert.True(AssignSelectedLeaf(l));
    }

    [Test]
    public void TestRemoveSelectedLeaf()
    {
        Leaf l = new Leaf();
        Assert.True(RemoveSelectedLeaf(l));
    }

    [Test]
    public void TestIsLeafSelected()
    {
        Leaf l = new Leaf();
        Assert.False(IsLeafSelected(l));
    }
}
*/