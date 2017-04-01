using System;
using System.Text;
using System.Security.Cryptography;
using UnityEngine;

namespace libunity.object_id {
  public class object_id : id {
    const int TIMESTAMP_BYTE = 4;
    const int MACHINE_ID_BYTE = 4;
    const int PROCESS_ID_BYTE = 2;
    const int INCREMENT_COUNT_BYTE = 2;
    const int TOTAL_BYTE = 12;

    public void init(counter counter) {
      int count = counter.inc();

      binary = new byte[TOTAL_BYTE];
      int index = 0;
      Array.Copy(create_timestamp(Convert.ToUInt32(counter.get_last_inc_time())),
        0, binary, index, TIMESTAMP_BYTE);
      index += TIMESTAMP_BYTE;
      Array.Copy(create_machine_id(), 0, binary, index, MACHINE_ID_BYTE);
      index += MACHINE_ID_BYTE;
      Array.Copy(create_process_id(), 0, binary, index, PROCESS_ID_BYTE);
      index += PROCESS_ID_BYTE;
      Array.Copy(create_increment_count(Convert.ToUInt16(count)), 
        0, binary, index, INCREMENT_COUNT_BYTE);

      Debug.Log(to_string());
      Debug.Log(get_timestamp()); 
    }

    public void init_by_string(string hex) {
      binary = new byte[hex.Length / 2];
      for (int i = 0; i < hex.Length; i += 2) {
        binary[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
      }
    }

    override public string to_string() {
      return BitConverter.ToString(binary);
    }

    private byte[] create_timestamp(uint time) {
      byte[] bytes = BitConverter.GetBytes(time);
      if (!BitConverter.IsLittleEndian) {
        Array.Reverse(bytes);
      }
      return bytes;
    }

    private byte[] create_machine_id() {
      string name = System.Environment.MachineName;
      MD5 md5Hash = MD5.Create();
      return md5Hash.ComputeHash(Encoding.UTF8.GetBytes(name));
    }

    private byte[] create_process_id() {
      uint pid = Convert.ToUInt16(
        System.Diagnostics.Process.GetCurrentProcess().Id);
      byte[] bytes = BitConverter.GetBytes(pid);
      if (!BitConverter.IsLittleEndian) {
        Array.Reverse(bytes);
      }
      return bytes;
    }

    private byte[] create_increment_count(ushort count) {
      byte[] bytes = BitConverter.GetBytes(count);
      if (!BitConverter.IsLittleEndian) {
        Array.Reverse(bytes);
      }
      return bytes;
    }

    public uint get_timestamp() {
      return BitConverter.ToUInt32(binary, 0);
    }

    private byte[] binary;

    public static object_id create(counter counter) {
      object_id result = new object_id();
      result.init(counter);
      return result;
    }

    public static object_id create_with_string(string hex) {
      object_id result = new object_id();
      result.init_by_string(hex);
      return result;
    }
  }
}
