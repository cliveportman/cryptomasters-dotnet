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

    // @todo Check this
    // Transpose an array of bytes, i.e. e.g. put the first bytes of each block into an array, then the second byte of each block into another array, etc.
    public static IEnumerable<byte[][]> TransposeArraysOfBytes(IEnumerable<byte[]> blocks, int chunkSize)
    {
        return Enumerable.Range(0, chunkSize)
            .Select(i => blocks.Where((_, j) => j % chunkSize == i).ToArray());
    }
}