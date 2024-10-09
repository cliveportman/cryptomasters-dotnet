namespace Cryptopals;

public class Decrypt
{
    private const string Keys = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+:,.-_()!$ @;";

    public class DecryptionResult
    {
        public char Character { get; init; }
        public int Score { get; init; }
        public string? DecryptedText { get; init; }
    }

    // Given a hex encoded string, XOR it against a single character and return a DecryptionResult object
    public static DecryptionResult SingleCharacterXor(string encoded)
    {
        var encodedBytes = Converters.HexToBytes(encoded);
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

    // Given a hex encoded string and a hex-encoded key, decrypt and return a string
    public static string RepeatingKeyXor(string encoded, string key)
    {
        var encodedBytes = Converters.HexToBytes(encoded);
        var keyBytes = Converters.HexToBytes(key);
        // split the encoded bytes into blocks of the same length as the key
        var blocks = Converters.ChunkBytes(encodedBytes, keyBytes.Length);
        // Decrypt each block using the key
        var decryptedBlocks = blocks.Select(block =>
            {
                var decryptedBlock = Encrypt.RepeatingKey(Converters.BytesToHex(block), Converters.BytesToHex(keyBytes));
                return Converters.HexToBytes(decryptedBlock);
            })
            .SelectMany(x => x)
            .ToArray();
        return System.Text.Encoding.ASCII.GetString(decryptedBlocks);

    }
    
    // Given a hex encoded string and a hex-encoded key, decrypt and return a string
    public static string DecryptRepeatingKey(string hexText, string key)
    {
        var textBytes = Converters.HexToBytes(hexText);
        //var keyBytes = System.Text.Encoding.ASCII.GetBytes(key);
        var keyBytes = Converters.HexToBytes(key);

        var resultBytes = textBytes.Select((b, i) =>
                Comparators.Xor(new byte[] { b }, new byte[] { keyBytes[i % keyBytes.Length] }))
            .SelectMany(x => x)
            .ToArray();

        return System.Text.Encoding.ASCII.GetString(resultBytes);
    }
}