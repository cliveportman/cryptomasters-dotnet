namespace Cryptopals;

public class Encrypt
{

    // Encrypt a string using a repeating key
    public static string RepeatingKey(string text, string key)
    {
        var textBytes = System.Text.Encoding.ASCII.GetBytes(text);
        var keyBytes = System.Text.Encoding.ASCII.GetBytes(key);

        var resultBytes = textBytes.Select((b, i) =>
                Comparators.Xor(new byte[] { b }, new byte[] { keyBytes[i % keyBytes.Length] }))
            .SelectMany(x => x)
            .ToArray();

        return Converters.BytesToHex(resultBytes);
    }
}