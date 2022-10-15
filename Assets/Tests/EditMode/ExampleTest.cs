using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class NewTestScript
{
    // This ist just a example test that should pass always.
    [Test]
    public void ExampleTest()
    {
        Assert.AreEqual(true, true);
    }

}
