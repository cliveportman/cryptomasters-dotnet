namespace Cryptopals;

public class Converters
{
    public static string HexToBase64(string hexString)
    {
        var bytes = HexToBytes(hexString);
        return Convert.ToBase64String(bytes); // Convert is a system class
    } 
    
    public static byte[] HexToBytes(string hexString)
    {
        // Q: Learn more about enumerables. I think here it's just referring to a class of methods for data that can be enumerated over.
        // Enumerable.Range returns an IEnumerable<Int> interface. IEnumerables can be iterated over using foreach or LINQ queries.
        // See also collection types, e.g. arrays, lists, sets. IEnumerable works with all of these.
        return Enumerable.Range(0, hexString.Length) // Creates a sequence of integers from 0 to the length of the hex string
            .Where(x => x % 2 == 0) // Filters out the odd numbers (two hex characters make up a byte)
            .Select(x => Convert.ToByte(hexString.Substring(x, 2), 16)) // Extracts a substring of 2 characters and converts it to a byte
            .ToArray();// Converts the sequence of bytes to an array
    }
    
    public static string BytesToHex(byte[] bytes)
    {
        return BitConverter.ToString(bytes).Replace("-", "");
    }
    
    // Given an array of bytes (generally converted from text), chunk it up into lengths of a given size
    public static IEnumerable<byte[]> ChunkBytes(byte[] bytes, int size)
    {
        return bytes.Select((b, i) => new { b, i })
            .GroupBy(x => x.i / size)
            .Select(g => g.Select(x => x.b).ToArray());
    }
    
    // Given an IEnumerable of byte arrays, transpose them
    public static IEnumerable<byte[]> TransposeArraysOfBytes(IEnumerable<byte[]> blocks)
    {
        var blockList = blocks.ToList();
        if (blockList.Count == 0)
        {
            throw new ArgumentException("The blocks collection is empty.");
        }

        // Pad shorter arrays with default value (0)
        int maxLength = blockList.Max(block => block.Length);
        for (int i = 0; i < blockList.Count; i++)
        {
            if (blockList[i].Length < maxLength)
            {
                var tempArray = blockList[i];
                Array.Resize(ref tempArray, maxLength);
                // Using a ref, which is a bit like a pointer - read more on this
                blockList[i] = tempArray;
            }
        }

        
        var transposed = new byte[blockList[0].Length][];
        for (var i = 0; i < blockList[0].Length; i++)
        {
            transposed[i] = new byte[blockList.Count];
            for (var j = 0; j < blockList.Count; j++)
            {
                transposed[i][j] = blockList[j][i];
            }
        }
        return transposed;
    }
}