
namespace libunity.object_id {
  public class object_id_builder {
    public object_id_builder(counter counter) {
      this.counter = counter;
    }

    public object_id build() {
      return new object_id(counter);
    }

    private counter counter;
    private string machine_name;
    private int process_id;
  }
}
