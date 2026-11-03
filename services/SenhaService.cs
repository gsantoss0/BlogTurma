using System.Security.Cryptography;
using Konscious.Security.Cryptography;
using System.Text;

namespace Blog.Services;
public class SenhaService
{
    static byte[] Argon2idHash(string password, byte[] salt)
    {
        using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            DegreeOfParallelism = 1,
            MemorySize = 19456, 
            Iterations = 2
        };
        return argon2.GetBytes(32);
    }

    public string GerarHash(string senha) 
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        byte[] hash = Argon2idHash(senha, salt);
        string b64(byte[] b) => Convert.ToBase64String(b).TrimEnd('=');

        return $"$argon2id$v=19$m=19456,t=2,p=1${b64(salt)}${b64(hash)}";
    }

    public bool Verificar(string senha, string hashArmazenado)
    {
        try
        {
            string[] partes = hashArmazenado.Split('$');
            if (partes.Length < 6) return false;
            byte[] salt = Convert.FromBase64String(PadBase64(partes[4]));
            byte[] hashEsperado = Convert.FromBase64String(PadBase64(partes[5]));
            byte[] hashAtual = Argon2idHash(senha, salt); 
            
            return CryptographicOperations.FixedTimeEquals(hashAtual, hashEsperado);
        }
        catch
        {
            return false;
        }
    }

    private string PadBase64(string b64)
    {
        return b64.PadRight(b64.Length + (4 - b64.Length % 4) % 4, '=');
    }
}