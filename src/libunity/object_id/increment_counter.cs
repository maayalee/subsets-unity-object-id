using System;
using libunity.core;

namespace libunity.object_id {
  public class increment_counter : counter {
    public increment_counter(int max_increment_counter) :
      base(max_increment_counter) {
    }

    protected override double get_current_time() {
      return time.get_unixtimestamp();
    }
  }
}