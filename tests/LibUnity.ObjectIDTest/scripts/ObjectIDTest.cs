using System;
using LibUnity.UnitTest;
using LibUnity.ObjectID;

namespace LibUnity.UnitTests.ObjectID {
  public class ObjectIDTest : TestCase {
    override protected void SetUp() {
      counter = new IncrementCounter();
    }

    override protected void TearDown() {
    }

    [TestMethod]
    public void TestCreateID() {
      LibUnity.ObjectID.ObjectID id = new ObjectIDBuilder(counter).Build();
      Assert(id.GetTimestamp() == counter.GetLastIncTime());
      Assert(id.GetMachineID() == MD5.Hash(Environment.MachineName).Substring(
        0, 3));
      Assert(id.GetProcessID() == (ushort)System.Diagnostics.Process.GetCurrentProcess().Id);

      id = new ObjectIDBuilder(counter).MachineName("test").Build();
      Assert(id.GetTimestamp() == counter.GetLastIncTime());
      Assert(id.GetMachineID() == MD5.Hash("test").Substring(0, 3));

      id = new ObjectIDBuilder(counter).MachineName("test").ProcessID(10).Build();
      Assert(id.GetTimestamp() == counter.GetLastIncTime());
      Assert(id.GetMachineID() == MD5.Hash("test").Substring(0, 3));
      Assert(id.GetProcessID() == (ushort)10);
    }

    [TestMethod]
    public void TestEquals() {
      LibUnity.ObjectID.ObjectID id = new ObjectIDBuilder(counter).Build();

      LibUnity.ObjectID.ObjectID compare_id = new LibUnity.ObjectID.ObjectID(id.ToString());
      Assert(id.Equals(compare_id), "Two id is Equals");
      Assert(id.GetTimestamp() == compare_id.GetTimestamp());
      Assert(id.GetMachineID() == compare_id.GetMachineID());
      Assert(id.GetProcessID() == compare_id.GetProcessID());
    }

    private Counter counter;
  }
}
