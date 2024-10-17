using System.Security.Cryptography;

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
    
    public static byte[] DecryptAes128Ecb(byte[] encodedBytes, byte[] keyBytes)
    {
        /*
         * The using keyword ensure the resources are disposed of correctly when the block is exited.
         * The alternative would be to use a try/finally block to ensure the resources are disposed of correctly
         * with the finally block containing the Dispose() method.
         */
        using var aes = Aes.Create();
        // Required cipher mode is ECB
        aes.Mode = CipherMode.ECB;
        /*
         * PKCS7 pads the plain text with a series of bytes all with the same value as the number of padding bytes, 
         * e.g. if 3 bytes are needed, the padding would be 03 03 03:
         * Plaintext: "HELLO"
         * Block size: 8 bytes
         * Padded plaintext: "HELLO\x03\x03\x03"
        */
        aes.Padding = PaddingMode.PKCS7;     
        aes.KeySize = 128;
        aes.Key = keyBytes;
        // ECB mode doesn't use an (Initialisation Vector) IV, but we need to set it to null explicitly
        aes.IV = new byte[16];              

        // Create the decryptor and use it to decrypt the encoded bytes
        var decryptor = aes.CreateDecryptor();
        using var msDecrypt = new MemoryStream();
        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
        {
            csDecrypt.Write(encodedBytes, 0, encodedBytes.Length);
            csDecrypt.FlushFinalBlock();
        }
        return msDecrypt.ToArray();
    }
}