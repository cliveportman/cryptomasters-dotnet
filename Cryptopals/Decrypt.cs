namespace Cryptopals;

public class Decrypt
{
    private const string Keys = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";

    public class DecryptionResult
    {
        public char Character { get; init; }
        public int Score { get; init; }
        public string? DecryptedText { get; init; }
    }

    // Given a hex encoded string, XOR it against a single character and return a DecryptionResult object
    public static DecryptionResult SingleCharacterXor(string Encoded)
    {
        var encodedBytes = Converters.HexToBytes(Encoded);
        var keysBytes = System.Text.Encoding.ASCII.GetBytes(Keys);

        var results = keysBytes.Select(key =>
            {
                var possibleKeyResults = encodedBytes // get the encoded bytes
                    .Select(b =>
                        Comparators.Xor(new byte[] { b }, new byte[] { key })) // XOR the encoded bytes with the key
                    .SelectMany(x =>
                        x) // The comparator returns an array of bytes, so we get an array of an array of bytes. SelectMany flattens it to just an array of bytes.
                    .ToArray();

                // Convert the results to a string
                var decryptedText = System.Text.Encoding.ASCII.GetString(possibleKeyResults);
                // Carry out the frequency analysis on the results - higher score indicates closer match to English text
                var score = Comparators.ScoreText(decryptedText);

                // Create a new DecryptionResult object to hold the results, passing in the character, score and decrypted text
                return new DecryptionResult
                {
                    Character = (char)key,
                    Score = score,
                    DecryptedText = decryptedText
                };
            })
            // Sort the results so the highest score is first
            .OrderByDescending(result => result.Score)
            .ToList();

        // Return the first result in the list
        return results[0];
    }

    public static string RepeatingKeyXor(string text, string key)
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