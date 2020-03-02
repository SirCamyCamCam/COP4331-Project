using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

public class TestGameManager {

    [Test]
    public void TestFlowUpdateSeconds()
    {
        GameManager gameManager = new GameManager();
        AssertTrue(gameManager.FlowUpdateSeconds() == 0);
    }

}
