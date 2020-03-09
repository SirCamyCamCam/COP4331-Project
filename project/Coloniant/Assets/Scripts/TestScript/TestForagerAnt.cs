using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

public class TestForagerAnt {

	[Test]
    public void TestDecideNextMove()
    {
        ForagerAnt ant = new ForagerAnt();
        AssertTrue(ant.DecideNextMove());
    }
}
