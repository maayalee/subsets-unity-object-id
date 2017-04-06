using System;

namespace libunity.object_id {
  abstract public class counter {
    public counter(int max_increment_count) {
      this.max_increment_count = max_increment_count;
    }

    abstract protected uint get_current_time();

    public int inc() {
      uint current_time = get_current_time();
      if (current_time < last_time) {
        throw new Exception("current time is little than last time");
      }
      if (is_same_sec(current_time)) {
        if (increment >= max_increment_count) {
          throw new Exception("increment is overflow");
        }
      }
      else {
        increment = 0;
      }
      increment++;
      last_time = current_time;
      return increment;
    }

    private bool is_same_sec(double time) {
      return time == last_time;
    }

    public uint get_last_inc_time() {
      return last_time;
    }

    public int get_increment() {
      return increment;
    }

    private int increment = 0;
    private uint last_time = 0;
    private int max_increment_count = 0;
  }
}
