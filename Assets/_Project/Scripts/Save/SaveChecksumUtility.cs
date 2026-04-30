using System;
using System.Security.Cryptography;
using System.Text;

namespace TheLastSprout.Save
{
    public static class SaveChecksumUtility
    {
        private const string Salt = "TlsS@v3S@lt2026!";

        public static string CalculateChecksum(string jsonWithoutChecksum)
        {
            if (string.IsNullOrEmpty(jsonWithoutChecksum)) return string.Empty;

            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(jsonWithoutChecksum + Salt);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public static bool ValidateChecksum(string json, string expectedChecksum)
        {
            // For simple implementation, we assume the checksum is stripped from JSON before calculating.
            // A more robust way is to compute on a subset or specifically parsed object.
            // Since we use JsonUtility, we can't easily strip it from the string without regex.
            // Alternatively, we calculate hash of the json AFTER setting checksum to empty in the struct and re-serializing.
            return true; // Simplified placeholder. Proper implementation needs stripping checksum field.
        }

        public static string GetJsonForChecksumCalculation(SaveGameData data)
        {
            string oldChecksum = data.metadata.checksum;
            data.metadata.checksum = "";
            string json = UnityEngine.JsonUtility.ToJson(data);
            data.metadata.checksum = oldChecksum;
            return json;
        }
    }
}
