using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

public class TestSpawnSoliderButton {

    [Test]
    public void TestSpawnButton()
    {
        QueenAnt queenAnt = new QueenAnt();
        SpawnSoliderButton spawnSoliderButton = new SpawnSoliderButton();
        spawnSoliderButton.SpawnButton();
        AssertTrue(queenAnt.antsToSpawn[0] == 1);
    }

}
