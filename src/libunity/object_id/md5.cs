using System.Text;
using System.Security.Cryptography;

namespace libunity.object_id {
  public static class md5 {
    public static string hash(string source) {
      MD5 md5 = MD5.Create();
      byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(source));
      StringBuilder hash = new StringBuilder();
      foreach (byte b in bytes) {
        hash.Append(b.ToString());
      }
      return hash.ToString();
    }
  }
}
