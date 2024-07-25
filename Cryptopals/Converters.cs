namespace Cryptopals;

public class Converters
{
    public static string HexToBase64(string hexString)
    {
        byte[] bytes = HexToBytes(hexString);
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
        return BitConverter.ToString(bytes).Replace("-", "").ToLower();
    }
}