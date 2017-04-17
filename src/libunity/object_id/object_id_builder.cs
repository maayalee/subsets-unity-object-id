
namespace libunity.object_id {
  public class object_id_builder {
    public object_id_builder(counter counter) {
      this.counter = counter;
    }

    public object_id_builder machine_name(string name) {
      _machine_name = name;
      return this;
    }

    public object_id_builder process_id(int id) {
      _process_id = id;
      return this;
    }

    public counter get_counter() {
      return counter;
    }

    public string get_machine_name() {
      return _machine_name;
    }

    public int get_process_id() {
      return _process_id;
    }

    public object_id build() {
      return new object_id(this);
    }

    private counter counter;
    private string _machine_name = "";
    private int _process_id = 0;
  }
}
