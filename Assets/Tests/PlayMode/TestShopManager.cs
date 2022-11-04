using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class ShopManagerTest
{
    public DummyButton dummyButtonObj;
    public ShopManager shopManager;
    // A Test behaves as an ordinary method
    [Test]
    public void CreditsTest()
    {
        //Need Interfaces for everthing...
        
        //ShopManager shopManager = new ShopManager();
        //DummyButton dummyButtonObj = new DummyButton();
        //Assert.AreEqual(0, shopManager.creditUIText, dummyButtonObj.GetCredits().ToString());
        //dummyButtonObj.MainButtonAction();
        //Assert.AreEqual(1, shopManager.creditUIText, dummyButtonObj.GetCredits().ToString());

        //Test Credits
        //Get Size of shop item and check if 0-n panels are visible & other (unused) are hidden.
        //Are Description, Price, Title matching
    }

}
