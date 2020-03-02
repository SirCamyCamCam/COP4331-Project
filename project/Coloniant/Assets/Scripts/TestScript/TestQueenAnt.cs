using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

public class TestQueenAnt {

    [Test]
	public void TestSetNursery()
    {
        GameObject newGameObject = new GameObject();
        QueenAnt ant = new QueenAnt();
        ant.SetNursery(newGameObject);
        AssertTrue(ant.nursery == newGameObject);
    }

    [Test]
    public void TestAddAntToSpawn()
    {
        QueenAnt ant = new QueenAnt();
        AssertTrue(ant.AddAntToSpawn(QueenAnt.Ants.EXCAVATOR, 100));
    }
}
