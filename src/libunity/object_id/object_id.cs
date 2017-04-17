using System;
using System.Text;
using System.Security.Cryptography;
using UnityEngine;

namespace libunity.object_id {
  public class object_id : id {
    const int TIMESTAMP_BYTE = 4;
    const int MACHINE_ID_BYTE = 3;
    const int PROCESS_ID_BYTE = 2;
    const int INCREMENT_COUNT_BYTE = 2;
    const int MAX_INCREMENT_COUNT_PER_SEC = 65535;

    public object_id(string hex) {
      binary = new byte[hex.Length / 2];
      for (int i = 0; i < hex.Length; i += 2) {
        binary[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
      }
    }

    public object_id(object_id_builder builder) {
      int count = builder.get_counter().inc();
      if (count >= MAX_INCREMENT_COUNT_PER_SEC) {
        throw new Exception("increment is overflow");
      }

      binary = new byte[get_total_size()];
      append_timestamp(builder.get_counter().get_last_inc_time());
      if (0 == builder.get_machine_name().Length) {
        append_machine_id(System.Environment.MachineName);
      }
      else {
        append_machine_id(builder.get_machine_name());
      }
      if (0 == builder.get_process_id()) {
        append_process_id(System.Diagnostics.Process.GetCurrentProcess().Id);
      }
      else {
        append_process_id(builder.get_process_id());
      }
      append_increment_count(Convert.ToUInt16(count));
    }

    private int get_total_size() {
      return TIMESTAMP_BYTE + MACHINE_ID_BYTE + PROCESS_ID_BYTE + 
        INCREMENT_COUNT_BYTE;
    } 

    override public bool equal(id id) {
      return id.to_string() == to_string();
    }

    override public string to_string() {
      if (0 == cache_string.Length) {
        string result = BitConverter.ToString(binary);
        string[] parts = result.Split('-');
        StringBuilder sb = new StringBuilder();
        foreach (string part in parts) {
          sb.Append(part);
        }
        cache_string = sb.ToString();
      }
      return cache_string;
    }

    private void append_timestamp(uint time) {
      byte[] bytes = BitConverter.GetBytes(time);
      Array.Copy(bytes, 0, binary, index, TIMESTAMP_BYTE);
      index += TIMESTAMP_BYTE;
    }

    private void append_machine_id(string machine_name) {
      string machine_name_hash = id.hash(machine_name);
      Array.Copy(Encoding.UTF8.GetBytes(machine_name_hash), 0, binary, index, MACHINE_ID_BYTE);
      index += MACHINE_ID_BYTE;
    }

    private void append_process_id(int process_id) {
      Array.Copy(BitConverter.GetBytes(process_id), 0, binary, index, PROCESS_ID_BYTE);
      index += PROCESS_ID_BYTE;
    }

    private void append_increment_count(ushort count) {
      byte[] bytes = BitConverter.GetBytes(count);
      Array.Copy(bytes, 0, binary, index, INCREMENT_COUNT_BYTE);
      index += INCREMENT_COUNT_BYTE;
    }

    public uint get_timestamp() {
      return BitConverter.ToUInt32(binary, 0);
    }

    public string get_machine_id() {
      byte[] bytes = new byte[MACHINE_ID_BYTE];
      Array.Copy(binary, 4, bytes, 0, MACHINE_ID_BYTE);
      return Encoding.UTF8.GetString(bytes);
    }

    public ushort get_process_id() {
      byte[] bytes = new byte[PROCESS_ID_BYTE];
      Array.Copy(binary, 7, bytes, 0, PROCESS_ID_BYTE);
      return BitConverter.ToUInt16(bytes, 0);
    }

    private int index = 0;
    private string cache_string = "";
    private byte[] binary;
  }
}
