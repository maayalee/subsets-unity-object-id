using System.Text;
using System.Security.Cryptography;

namespace LibUnity.ObjectID {
  public static class MD5 {
    public static string Hash(string source) {
      System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
      byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(source));
      StringBuilder hash = new StringBuilder();
      foreach (byte b in bytes) {
        hash.Append(b.ToString());
      }
      return hash.ToString();
    }
  }
}
