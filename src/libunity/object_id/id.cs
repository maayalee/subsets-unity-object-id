using System.Text;
using System.Security.Cryptography;

namespace libunity.object_id {
  public interface id {
    string to_string();
    bool equals(id id);
  }
}
