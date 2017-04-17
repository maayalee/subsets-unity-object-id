using System;
using System.Collections.Generic;
using libunity.test;
using libunity.object_id;
using UnityEngine;

namespace libunity.tests.object_id {
  public class object_id_test : test_case {
    override protected void set_up() {
      counter = new increment_counter();
    }

    override protected void tear_down() {
    }

    [test_method]
    public void test_create_id() {
      libunity.object_id.object_id id = new object_id_builder(counter).build();
      assert(id.get_timestamp() == counter.get_last_inc_time());
      assert(id.get_machine_id() == libunity.object_id.id.hash(Environment.MachineName).Substring(
        0, 3));
      assert(id.get_process_id() == (ushort)System.Diagnostics.Process.GetCurrentProcess().Id);

      id = new object_id_builder(counter).machine_name("test").build();
      assert(id.get_timestamp() == counter.get_last_inc_time());
      assert(id.get_machine_id() == libunity.object_id.id.hash("test").Substring(0, 3));

      id = new object_id_builder(counter).machine_name("test").process_id(10).build();
      assert(id.get_timestamp() == counter.get_last_inc_time());
      assert(id.get_machine_id() == libunity.object_id.id.hash("test").Substring(0, 3));
      assert(id.get_process_id() == (ushort)10);
    }

    [test_method]
    public void test_equals() {
      libunity.object_id.object_id id = new object_id_builder(counter).build();

      libunity.object_id.object_id compare_id = new libunity.object_id.object_id(id.to_string());
      assert(id.equal(compare_id), "Two id is equal");
      assert(id.get_timestamp() == compare_id.get_timestamp());
      assert(id.get_machine_id() == compare_id.get_machine_id());
      assert(id.get_process_id() == compare_id.get_process_id());
    }

    private counter counter;
  }
}
