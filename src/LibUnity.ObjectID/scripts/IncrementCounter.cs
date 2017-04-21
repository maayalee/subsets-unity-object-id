using System;
using LibUnity.Core;

namespace LibUnity.ObjectID {
  public class IncrementCounter : Counter {
    public IncrementCounter() {
    }

    protected override uint GetCurrentTime() {
      return Convert.ToUInt32(Time.GetUnixtimestamp());
    }
  }
}