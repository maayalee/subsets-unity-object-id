using System;
using System.Text;
using System.Security.Cryptography;
using UnityEngine;

namespace LibUnity.ObjectID {
  public class ObjectID : ID {
    const int TIMESTAMP_BYTE = 4;
    const int MACHINE_ID_BYTE = 3;
    const int PROCESS_ID_BYTE = 2;
    const int INCREMENT_COUNT_BYTE = 2;
    const int MAX_INCREMENT_COUNT_PER_SEC = 65535;

    public ObjectID(string hex) {
      binary = new byte[hex.Length / 2];
      for (int i = 0; i < hex.Length; i += 2) {
        binary[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
      }
    }

    public ObjectID(ObjectIDBuilder builder) {
      int count = builder.GetCounter().Inc();
      if (count >= MAX_INCREMENT_COUNT_PER_SEC) {
        throw new Exception("increment is overflow");
      }

      binary = new byte[GetTotalSize()];
      AppendTimestamp(builder.GetCounter().GetLastIncTime());
      if (0 == builder.GetMachineName().Length) {
        AppendMachineID(System.Environment.MachineName);
      }
      else {
        AppendMachineID(builder.GetMachineName());
      }
      if (0 == builder.GetProcessID()) {
        AppendProcessID(System.Diagnostics.Process.GetCurrentProcess().Id);
      }
      else {
        AppendProcessID(builder.GetProcessID());
      }
      AppendIncrementCount(Convert.ToUInt16(count));
    }

    private int GetTotalSize() {
      return TIMESTAMP_BYTE + MACHINE_ID_BYTE + PROCESS_ID_BYTE + 
        INCREMENT_COUNT_BYTE;
    } 

    public bool Equals(ID other) {
      return other.ToString() == ToString();
    }

    override public string ToString() {
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

    private void AppendTimestamp(uint time) {
      byte[] bytes = BitConverter.GetBytes(time);
      Array.Copy(bytes, 0, binary, index, TIMESTAMP_BYTE);
      index += TIMESTAMP_BYTE;
    }

    private void AppendMachineID(string machine_name) {
      string machine_name_hash = MD5.Hash(machine_name);
      Array.Copy(Encoding.UTF8.GetBytes(machine_name_hash), 0, binary, index, MACHINE_ID_BYTE);
      index += MACHINE_ID_BYTE;
    }

    private void AppendProcessID(int process_id) {
      Array.Copy(BitConverter.GetBytes(process_id), 0, binary, index, PROCESS_ID_BYTE);
      index += PROCESS_ID_BYTE;
    }

    private void AppendIncrementCount(ushort count) {
      byte[] bytes = BitConverter.GetBytes(count);
      Array.Copy(bytes, 0, binary, index, INCREMENT_COUNT_BYTE);
      index += INCREMENT_COUNT_BYTE;
    }

    public uint GetTimestamp() {
      return BitConverter.ToUInt32(binary, 0);
    }

    public string GetMachineID() {
      byte[] bytes = new byte[MACHINE_ID_BYTE];
      Array.Copy(binary, 4, bytes, 0, MACHINE_ID_BYTE);
      return Encoding.UTF8.GetString(bytes);
    }

    public ushort GetProcessID() {
      byte[] bytes = new byte[PROCESS_ID_BYTE];
      Array.Copy(binary, 7, bytes, 0, PROCESS_ID_BYTE);
      return BitConverter.ToUInt16(bytes, 0);
    }

    private int index = 0;
    private string cache_string = "";
    private byte[] binary;
  }
}
