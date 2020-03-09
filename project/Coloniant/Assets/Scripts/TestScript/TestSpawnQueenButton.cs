using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

public class TestSpawnQueenButton  {

    [Test]
    public void TestSpawnButton()
    {
        QueenAnt queenAnt = new QueenAnt();
        SpawnQueenButton spawnQueenButton = new SpawnQueenButton();
        spawnQueenButton.SpawnButton();
        AssertTrue(queenAnt.antsToSpawn[0] == 1);
    }
    
}
