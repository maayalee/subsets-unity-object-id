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

    public void test_create_id() {
      libunity.object_id.object_id id = new object_id_builder(counter).build();
      assert(id.get_timestamp() == counter.get_last_inc_time());

      string machine_id = libunity.object_id.id.hash(Environment.MachineName).
        Substring(0, 3);
      assert(id.get_machine_id() == machine_id);
      assert(id.get_process_id() ==
        System.Diagnostics.Process.GetCurrentProcess().Id);
    }

    public void test_equals() {
      libunity.object_id.object_id id = new object_id_builder(counter).build();

      libunity.object_id.object_id compare_id = 
        new libunity.object_id.object_id(id.to_string());
      assert(id.equal(compare_id), "Two id is equal");
      assert(id.get_timestamp() == compare_id.get_timestamp());
      assert(id.get_machine_id() == compare_id.get_machine_id());
      assert(id.get_process_id() == compare_id.get_process_id());
    }

    private counter counter;


    override public List<test_case> get_tests() {
      List<test_case> result = new List<test_case>();
      result.Add(create_test_case(typeof(object_id_test), "test_create_id"));
      result.Add(create_test_case(typeof(object_id_test), "test_equals"));
      return result;
    }
  }
}
