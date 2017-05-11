using System;
using LibUnity.Core;

namespace LibUnity.ObjectID {
  public class IncrementCounter : Counter {
    public IncrementCounter() {
    }

    protected override uint GetCurrentTime() {
      return Convert.ToUInt32(GetUnixtimestamp());
    }

    private double GetUnixtimestamp() {
      TimeSpan time = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
      return time.TotalSeconds;
    }
  }
}