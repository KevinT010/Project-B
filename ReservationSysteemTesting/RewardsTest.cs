

[TestClass]
public class RewardsTest
{
    [TestMethod]
    public void Test_AddVouchers_returnTrue()
    {
        AccountModel testUser = new AccountModel("john", "doe", "johndoe@example.com", "1234567890", "password123", 1, 200, 0);
        Session.CurrentUser = testUser;
        RewardLogic rewardLogic = new RewardLogic();
        bool result = rewardLogic.Add_Vouchers(testUser, 1);

        Assert.IsTrue(testUser.DesertVouchers == 1);
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Test_AddVouchers_returnFalse()
    {
        AccountModel testUser = new AccountModel("john", "doe", "johndoe@example.com", "1234567890", "password123", 1, 0, 0);
        Session.CurrentUser = testUser;
        RewardLogic rewardLogic = new RewardLogic();
        bool result = rewardLogic.Add_Vouchers(testUser, 1);

        Assert.IsFalse(testUser.DesertVouchers == 1);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Test_RemoveVouchers_returnTrue()
    {
        AccountModel testUser = new AccountModel("john", "doe", "johndoe@example.com", "1234567890", "password123", 1, 200, 1);
        Session.CurrentUser = testUser;
        RewardLogic rewardLogic = new RewardLogic();
        bool result = rewardLogic.Remove_Vouchers(testUser, 1);

        Assert.IsTrue(testUser.DesertVouchers == 0);
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Test_RemoveVouchers_returnFalse()
    {
        AccountModel testUser = new AccountModel("john", "doe", "johndoe@example.com", "1234567890", "password123", 1, 200, 0);
        Session.CurrentUser = testUser;
        RewardLogic rewardLogic = new RewardLogic();
        bool result = rewardLogic.Remove_Vouchers(testUser, 1);

        Assert.AreEqual(0, testUser.DesertVouchers);
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void GiveReservationPoints_ReturnsTrue()
    {
        AccountModel testUser = new AccountModel("john", "doe", "johndoe@example.com", "1234567890", "password123", 1, 200, 0);
        Session.CurrentUser = testUser;
        RewardLogic rewardLogic = new RewardLogic();
        rewardLogic.GiveReservationPoints(testUser);

        Assert.IsTrue(testUser.Points == 220);
    
    }
}