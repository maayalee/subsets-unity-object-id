using System;

namespace LibUnity.ObjectID {
  abstract public class Counter {
    public Counter() {
    }

    abstract protected uint GetCurrentTime();

    public int Inc() {
      uint current_time = GetCurrentTime();
      if (current_time < last_time) {
        throw new Exception("current time is little than last time");
      }
      if (current_time != last_time) {
        increment = 0;
      }
      increment++;
      last_time = current_time;
      return increment;
    }


    public uint GetLastIncTime() {
      return last_time;
    }

    public int GetIncrement() {
      return increment;
    }

    private int increment = 0;
    private uint last_time = 0;
    private int max_increment_count = 0;
  }
}
