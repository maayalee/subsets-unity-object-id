using System;
using libunity.core;

namespace libunity.object_id {
  public class increment_counter : counter {
    public increment_counter() {
    }

    protected override uint get_current_time() {
      return Convert.ToUInt32(time.get_unixtimestamp());
    }
  }
}