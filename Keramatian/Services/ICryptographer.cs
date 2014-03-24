namespace Keramatian.Services
{
    public interface ICryptographer
    {
        string CreateSalt();
        string ComputeHash(string valueToHash);
        string GetPasswordHash(string password, string salt);
        string Encrypt(string data);
        string Decrypt(string data);
        string GetHashFromKeyAndSalt(string key, string salt);
    }
}
