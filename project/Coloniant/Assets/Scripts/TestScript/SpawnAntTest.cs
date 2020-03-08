/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

public class SpawnAntTest {

    // Tests large number of ant spawn
    [Test]
	public void TestAntSpawning()
    {
        Assert.True(QueenAnt.main.AddAntToSpawn(QueenAnt.Ants.EXCAVATOR, 500));
        Assert.True(QueenAnt.main.AddAntToSpawn(QueenAnt.Ants.FORAGER, 500));
        Assert.True(QueenAnt.main.AddAntToSpawn(QueenAnt.Ants.GARDENER, 500));
        Assert.True(QueenAnt.main.AddAntToSpawn(QueenAnt.Ants.QUEEN, 500));
        Assert.True(QueenAnt.main.AddAntToSpawn(QueenAnt.Ants.SOLIDER, 500));
        Assert.True(QueenAnt.main.AddAntToSpawn(QueenAnt.Ants.TRASH_HANDLER, 500));

        Assert.False(QueenAnt.main.AddAntToSpawn(QueenAnt.Ants.EXCAVATOR, -1));
        Assert.False(QueenAnt.main.AddAntToSpawn(QueenAnt.Ants.FORAGER, -20));
        Assert.False(QueenAnt.main.AddAntToSpawn(QueenAnt.Ants.GARDENER, -30));
        Assert.False(QueenAnt.main.AddAntToSpawn(QueenAnt.Ants.QUEEN, -100));
        Assert.False(QueenAnt.main.AddAntToSpawn(QueenAnt.Ants.SOLIDER, -300000));
        Assert.False(QueenAnt.main.AddAntToSpawn(QueenAnt.Ants.TRASH_HANDLER, 0));
    }

}*/
