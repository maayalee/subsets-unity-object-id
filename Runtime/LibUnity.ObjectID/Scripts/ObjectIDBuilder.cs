
namespace LibUnity.ObjectID {
  public class ObjectIDBuilder {
    public ObjectIDBuilder(Counter counter) {
      this.counter = counter;
    }

    public ObjectIDBuilder MachineName(string name) {
      machineName = name;
      return this;
    }

    public ObjectIDBuilder ProcessID(int id) {
      processID = id;
      return this;
    }

    public Counter GetCounter() {
      return counter;
    }

    public string GetMachineName() {
      return machineName;
    }

    public int GetProcessID() {
      return processID;
    }

    public ObjectID Build() {
      return new ObjectID(this);
    }

    private Counter counter;
    private string machineName = "";
    private int processID = 0;
  }
}
