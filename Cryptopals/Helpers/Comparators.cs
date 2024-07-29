namespace Cryptopals;

public class Comparators
{
    private const string EnglishLetterFrequency = " etaoinshrdlcumwfgypbvkjxqz";

    public static byte[] Xor(byte[] bytes1, byte[] bytes2)
    {
        // Throw an error if the byte arrays are not the same length
        if (bytes1.Length != bytes2.Length)
        {
            throw new ArgumentException("Byte arrays must be of equal length");
        }

        // Something to hold the results
        var result = new byte[bytes1.Length];

        // Make the XOR comparision one byte at a time and return the results
        for (var i = 0; i < bytes1.Length; i++)
        {
            result[i] = (byte)(bytes1[i] ^ bytes2[i]);
        }

        return result;
    }

    // Carries out a frequency analysis on some text, using the English letter frequency
    public static int ScoreText(string text)
    {
        var score = 0;

        // Loop through the text string we're analysing using a FOREACH loop (we don't need an index)
        foreach (var c in text)
        {
            // Loop through the English letter frequency using a FOR loop (we need the index)
            for (var j = 0; j < EnglishLetterFrequency.Length; j++)
            {
                // Is the character we're looking at the same as the character in the English letter frequency we're checking for?
                if (c == EnglishLetterFrequency[j])
                {
                    // Yes. So we add a score to the total score, weighted on the popularity of the character.
                    // We weight the score by subtracting the index from the length of the array, so characters at the end of the array score lower.
                    score += EnglishLetterFrequency.Length - j;
                }
            }
        }

        return score;
    }

    public static int HammingDistance(byte[] bytes1, byte[] bytes2)
    {
        // Throw an error if the byte arrays are not the same length
        if (bytes1.Length != bytes2.Length)
        {
            throw new ArgumentException("Byte arrays must be of equal length");
        }

        // Something to hold the results
        var result = 0;

        // Make the XOR comparision one byte at a time and return the results
        for (var i = 0; i < bytes1.Length; i++)
        {
            var xor = bytes1[i] ^ bytes2[i];
            while (xor > 0)
            {
                result += xor & 1; // If the least significant bit is 1, add 1 to the result
                xor >>= 1; // Right shift the bits by 1
            }
        }

        return result;
    }
}