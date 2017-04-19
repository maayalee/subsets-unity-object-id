using System.Text;
using System.Security.Cryptography;

namespace LibUnity.ObjectID {
  public interface ID {
    string ToString();
    bool Equals(ID id);
  }
}
