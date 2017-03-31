namespace libunity.object_id {
  abstract public class id {
    abstract public void init_by_string(string data);
    abstract public string to_string();

    protected byte[] binary;
  }
}
