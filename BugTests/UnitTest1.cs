namespace BugTests;

[TestClass]
public class BugStateTests
{

    [TestMethod]
    public void TestOpenToAssignedAndBackToOpenMultipleTime()
    {
        var bug = new Bug(Bug.State.Open);
        bug.Assign();
        bug.Close();
        bug.Assign();
        bug.Close();
        bug.Assign();
        bug.Close();
        bug.Assign();                
        Assert.AreEqual(Bug.State.Assigned, bug.getState());
    }

    [TestMethod]
    public void TestTransitionFromClosedToAssigned()
    {
        var bug = new Bug(Bug.State.Closed);
        bug.Assign();
        Assert.AreEqual(Bug.State.Assigned, bug.getState());
    }

    [TestMethod]
    public void TestTransitionFromDeferedToAssigned()
    {
        var bug = new Bug(Bug.State.Defered);
        bug.Assign();
        Assert.AreEqual(Bug.State.Assigned, bug.getState());
    }

    [TestMethod]
    public void TestTransitionFromOpenToAssigned()
    {
        var bug = new Bug(Bug.State.Open);
        bug.Assign();
        Assert.AreEqual(Bug.State.Assigned, bug.getState());
    }

    [TestMethod]
    public void TestTransitionFromAssignedToClosed()
    {
        var bug = new Bug(Bug.State.Assigned);
        bug.Close();
        Assert.AreEqual(Bug.State.Closed, bug.getState());
    }

    [TestMethod]
    public void TestCreateFixAndDeclineFix()
    {
        var bug = new Bug(Bug.State.CreatedFixes);
        bug.DeclineFix();
        Assert.AreEqual(Bug.State.DeclinedFixes, bug.getState());
    }

    [TestMethod]
    public void TestCreateFixAndAcceptFix()
    {
        var bug = new Bug(Bug.State.CreatedFixes);
        bug.AcceptFix();
        Assert.AreEqual(Bug.State.AcceptedFixes, bug.getState());
    }

    [TestMethod]
    public void TestTransitionFromAssignedToDeferred()
    {
        var bug = new Bug(Bug.State.Assigned);
        bug.Defer();
        Assert.AreEqual(Bug.State.Defered, bug.getState());
    }

    [TestMethod]
    public void TestAcceptFixThenClose()
    {
        var bug = new Bug(Bug.State.AcceptedFixes);
        bug.Close();
        Assert.AreEqual(Bug.State.Closed, bug.getState());
    }

    [TestMethod]
    public void TestDeclineFixThenCreateFix()
    {
        var bug = new Bug(Bug.State.DeclinedFixes);
        bug.CreateFix();
        Assert.AreEqual(Bug.State.CreatedFixes, bug.getState());
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestInvalidTransitionFromClosedToAcceptFix()
    {
        var bug = new Bug(Bug.State.Closed);
        bug.AcceptFix();
    }

    [TestMethod]
    public void TestOpenToAssignedAndBackToOpen()
    {
        var bug = new Bug(Bug.State.Open);
        bug.Assign();
        bug.Close();
        bug.Assign();
        Assert.AreEqual(Bug.State.Assigned, bug.getState());
    }

    [TestMethod]
    public void TestCycleThroughFixStates()
    {
        var bug = new Bug(Bug.State.CreatedFixes);
        bug.DeclineFix();
        bug.CreateFix();
        bug.AcceptFix();
        bug.Close();
        Assert.AreEqual(Bug.State.Closed, bug.getState());
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestInvalidTransitionFromAssignedToAcceptFix()
    {
        var bug = new Bug(Bug.State.Assigned);
        bug.AcceptFix();
    }

    [TestMethod]
    public void TestReassignAfterDefer()
    {
        var bug = new Bug(Bug.State.Defered);
        bug.Assign();
        bug.Defer();
        bug.Assign();
        Assert.AreEqual(Bug.State.Assigned, bug.getState());
    }

    [TestMethod]
    public void TestAssignAndDeferMultipleTimes()
    {
        var bug = new Bug(Bug.State.Open);
        bug.Assign();
        bug.Defer();
        bug.Assign();
        bug.Defer();
        Assert.AreEqual(Bug.State.Defered, bug.getState());
    }

    [TestMethod]
    public void TestCloseAssignedBug()
    {
        var bug = new Bug(Bug.State.Assigned);
        bug.Close();
        Assert.AreEqual(Bug.State.Closed, bug.getState());
    }

    [TestMethod]
    public void TestCloseDeferedBug()
    {
        var bug = new Bug(Bug.State.Defered);
        bug.Close();
        Assert.AreEqual(Bug.State.Closed, bug.getState());
    }

    [TestMethod]
    public void TestCloseCreatedFixesBug()
    {
        var bug = new Bug(Bug.State.CreatedFixes);
        bug.Close();
        Assert.AreEqual(Bug.State.Closed, bug.getState());
    }

    [TestMethod]
    public void TestAssignAfterAcceptFix()
    {
        var bug = new Bug(Bug.State.AcceptedFixes);
        bug.AcceptFix();
        bug.Assign();
        Assert.AreEqual(Bug.State.Assigned, bug.getState());
    }

    [TestMethod]
    public void TestAssignAfterDeclineFix()
    {
        var bug = new Bug(Bug.State.DeclinedFixes);
        bug.DeclineFix();
        bug.Assign();
        Assert.AreEqual(Bug.State.Assigned, bug.getState());
    }    
}
