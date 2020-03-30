using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.TestTools;

public class TestLeaf {

    [Test]
    public void TestResetDecay()
    {
        Leaf l = new Leaf();
        l.ResetDecay();
        Assert.True(l.decay == 0);
    }

    [Test]
    public void TestSetLeafState()
    {
        Leaf l = new Leaf();
        l.SetLeafState(LeafManager.State.TRASH);
        Assert.True(l.state == LeafManager.State.TRASH);
    }

    [Test]
    public void TestSetDecayRate()
    {
        Leaf l = new Leaf();
        l.SetDecayRate(0.5f);
        Assert.True(l.decayRate == 0.5f);
    }

    [Test]
    public void TestSetDecayMultiplyere()
    {
        Leaf l = new Leaf();
        l.SetDecayMultiplyer(0.5f);
        Assert.True(l.SetDecayMultiplyer == 0.5f);
    }

    [Test]
    public void TestStartDecay()
    {
        Leaf l = new Leaf();
        Assert.True(l.StartDecay());
    }

    [Test]
    public void TestSetLeafLife()
    {
        Leaf l = new Leaf();
        l.SetLeafLife(100);
        Assert.True(l.leafLife == 100);
    }

    [Test]
    public void TestStartLeafLife()
    {
        Leaf l = new Leaf();
        Assert.True(l.StartLeafLife());
    }

    [Test]
    public void TestReturnLeafState()
    {
        Leaf l = new Leaf();
        Assert.Null(l.ReturnLeafState());
    }

    [Test]
    public void TestReturnDecayLevel()
    {
        Leaf l = new Leaf();
        Assert.True(l.ReturnDecayLevel() == 0);
    }
}
