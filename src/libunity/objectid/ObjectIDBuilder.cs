
namespace LibUnity.ObjectID {
  public class ObjectIDBuilder {
    public ObjectIDBuilder(Counter counter) {
      this.counter = counter;
    }

    public ObjectIDBuilder MachineName(string name) {
      _machine_name = name;
      return this;
    }

    public ObjectIDBuilder ProcessID(int id) {
      _process_id = id;
      return this;
    }

    public Counter GetCounter() {
      return counter;
    }

    public string GetMachineName() {
      return _machine_name;
    }

    public int GetProcessID() {
      return _process_id;
    }

    public ObjectID Build() {
      return new ObjectID(this);
    }

    private Counter counter;
    private string _machine_name = "";
    private int _process_id = 0;
  }
}
