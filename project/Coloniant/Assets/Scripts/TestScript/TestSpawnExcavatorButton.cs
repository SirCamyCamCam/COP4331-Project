using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

public class TestSpawnExcavatorButton {

    [Test]
    public void TestSpawnButton()
    {
        QueenAnt queenAnt = new QueenAnt();
        SpawnExcavatorButton spawnExcavatorButton = new SpawnExcavatorButton();
        spawnExcavatorButton.SpawnButton();
        AssertTrue(queenAnt.antsToSpawn[0] == 1);
    }

}
