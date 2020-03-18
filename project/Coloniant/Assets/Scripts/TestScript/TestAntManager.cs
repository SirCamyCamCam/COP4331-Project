/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

public class TestAntManager {

    [Test]
    public void TestDefaultAntSpeed()
    {
        AntManager antManager = new AntManager();
        AssertTrue(antManager.DefaultAntSpeed != 0);
    }

    [Test]
    public void TestDefaultAntIdleNoise()
    {
        AntManager antManager = new AntManager();
        AssertTrue(antManager.DefaultAntIdleNoise != 0);
    }

    [Test]
    public void TestDefaultRotationSpeed()
    {
        AntManager antManager = new AntManager();
        AssertTrue(antManager.DefaultRotationSpeed != 0);
    }

    [Test]
    public void TestDefaultIdleDistance()
    {
        AntManager antManager = new AntManager();
        AssertTrue(antManager.DefaultIdleDistance != 0);
    }

    [Test]
    public void TestSwitchLevelView()
    {
        AntManager antManager = new AntManager();
        Ant ant = new Ant();
        ant.ChangeView(AntManager.SceneView.ABOVE_GROUND);
        antManager.SwitchLevelView(AntManager.SceneView.UNDER_GROUND);
        AssertTrue(ant.CurrentView() == AntManager.SceneView.UNDER_GROUND);
    }

    [Test]
    public void TestAddToSoliderCount()
    {
        AntManager antManager = new AntManager();
        Ant ant = new Ant();
        antManager.AddToSoldierCount(ant);
        AssertTrue(antManager.GetSoliderCount() == 1);
    }

    [Test]
    public void TestAddToQueenCount()
    {
        AntManager antManager = new AntManager();
        Ant ant = new Ant();
        antManager.AddToQueenCount(ant);
        AssertTrue(antManager.GetQueenCount() == 1);
    }

    [Test]
    public void TestAddToTrashHandlerCount()
    {
        AntManager antManager = new AntManager();
        Ant ant = new Ant();
        antManager.AddToTrashHandlerCount(ant);
        AssertTrue(antManager.GetTrashHandlerCount() == 1);
    }

    [Test]
    public void TestAddToExcavatorCount()
    {
        AntManager antManager = new AntManager();
        Ant ant = new Ant();
        antManager.AddToExcavatorCount(ant);
        AssertTrue(antManager.GetExcavatorCount() == 1);
    }

    [Test]
    public void TestAddToForagerCount()
    {
        AntManager antManager = new AntManager();
        Ant ant = new Ant();
        antManager.AddToForagerCount(ant);
        AssertTrue(antManager.GetForagerCount() == 1);
    }

    [Test]
    public void TestAddToGardenerCount()
    {
        AntManager antManager = new AntManager();
        Ant ant = new Ant();
        antManager.AddToGardenerCount(ant);
        AssertTrue(antManager.GetGardenerCount() == 1);
    }

    [Test]
    public void RemoveFromSoldierCount()
    {
        AntManager antManager = new AntManager();
        Ant ant = new Ant();
        antManager.AddToSoldierCount(ant);
        antManager.RemoveFromSoldierCount(ant);
        AssertTrue(antManager.GetSoliderCount() == 0);
    }

    [Test]
    public void RemoveFromQueenCount()
    {
        AntManager antManager = new AntManager();
        Ant ant = new Ant();
        antManager.AddToQueenCount(ant);
        antManager.RemoveFromQueenCount(ant);
        AssertTrue(antManager.GetQueenCount() == 0);
    }

    [Test]
    public void RemoveFromTrashHandlerCount()
    {
        AntManager antManager = new AntManager();
        Ant ant = new Ant();
        antManager.AddToTrashHandlerCount(ant);
        antManager.RemoveFromTrashHandlerCount(ant);
        AssertTrue(antManager.GetTrashHandlerCount() == 0);
    }

    [Test]
    public void RemoveFromExcavatorCount()
    {
        AntManager antManager = new AntManager();
        Ant ant = new Ant();
        antManager.AddToExcavatorCount(ant);
        antManager.RemoveFromExcavatorCount(ant);
        AssertTrue(antManager.GetExcavatorCount() == 0);
    }

    [Test]
    public void RemoveFromForagerCount()
    {
        AntManager antManager = new AntManager();
        Ant ant = new Ant();
        antManager.AddToForagerCount(ant);
        antManager.RemoveFromForagerCount(ant);
        AssertTrue(antManager.GetForagerCount() == 0);
    }

    [Test]
    public void RemoveFromGardenerCount()
    {
        AntManager antManager = new AntManager();
        Ant ant = new Ant();
        antManager.AddToGardenerCount(ant);
        antManager.RemoveFromGardenerCount(ant);
        AssertTrue(antManager.GetGardenerCount() == 0);
    }

    [Test]
    public void GetSoliderCount()
    {
        AntManager antManager = new AntManager();
        AssertTrue(antManager.GetSoliderCount() == 0);
    }

    [Test]
    public void GetQueenCount()
    {
        AntManager antManager = new AntManager();
        AssertTrue(antManager.GetQueenCount() == 0);
    }

    [Test]
    public void GetTrashHandlerCount()
    {
        AntManager antManager = new AntManager();
        AssertTrue(antManager.GetTrashHandlerCount() == 0);
    }

    [Test]
    public void GetGardenerCount()
    {
        AntManager antManager = new AntManager();
        AssertTrue(antManager.GetGardenerCount() == 0);
    }

    [Test]
    public void GetForagerCount()
    {
        AntManager antManager = new AntManager();
        AssertTrue(antManager.GetForagerCount() == 0);
    }

    [Test]
    public void GetExcavatorCount()
    {
        AntManager antManager = new AntManager();
        AssertTrue(antManager.GetExcavatorCount() == 0);
    }

    [Test]
    public void GetTotalAntCount()
    {
        AntManager antManager = new AntManager();
        AssertTrue(antManager.GetTotalAntCount() == 0);
    }
}
*/