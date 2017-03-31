using System;
using System.Collections.Generic;
using libunity.test;
using libunity.object_id;

namespace libunity.tests.object_id {
  public class object_id_test : test_case {
    override protected void set_up() {
      counter = new increment_counter(10);
    }

    override protected void tear_down() {
    }

    public void test_create_id() {
      libunity.object_id.object_id id = libunity.object_id.object_id.create(counter);
      assert(id.get_timestamp() == 
        Convert.ToUInt32(counter.get_last_inc_time()));
    }

    private counter counter;


    override public List<test_case> get_tests() {
      List<test_case> result = new List<test_case>();
      result.Add(create_test_case(typeof(object_id_test), "test_create_id"));
      return result;
    }
  }
}
