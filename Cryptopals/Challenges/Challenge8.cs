using System.Text;
using Cryptopals.Helpers;

namespace Cryptopals.Challenges
{
    /* Challenge 8: Provided with a Hex-encoded text file containing multiple cipher texts,
     * identify which one has been encrypted with ECB.
     * The issue with ECB is it's stateless and deterministic,
     * so the same 16-byte plaintext will always provide the same 16-byte cipher text.
     */
    /* The plan
     * AES-128 breaks text into 16-byte blocks before encrypting. And because it's deterministic, if any
     * two blocks are the same the resulting cipher text will be identical.
     * So we're looking for 16-byte duplicates. Just loop through every cipher text, break it into 16-byte chunks
     * and search those chunks for duplicates. The more duplicates there are, the more likely that cipher text
     * is encrypted with AES-128 in ECB mode.
     */
    public abstract class Challenge8
    {

        public static string Run()
        {
            // Grab the texts from the source file and convert from hex to bytes
            var lines = File.ReadAllLines(Path.Combine(Utility.Assets, "challenge-8.txt"));
            Console.WriteLine($"Number of lines: {lines.Length}");
            var linesAsBytes = lines.Select(Converters.HexToBytes).ToArray();
            // For each lineAsBytes, break it into 16-byte blocks
            foreach (var line in linesAsBytes)
            {
                Console.WriteLine($"Number of bytes in line: {line.Length}");
                var chunks = Converters.ChunkBytes(line, 16).ToArray();
                Console.WriteLine($"Number of chunks: {chunks.Length}");
var groups = chunks.GroupBy(chunk => chunk).Where(g => g.Count() > 1).ToArray();
Console.WriteLine($"Duplicate Count: {groups}");

}



            //var keySizeDistances = keySizes.Select(keySize =>
            //var blocks = linesAsBytes.Select(Converters.ChunkBytes).ToArray();
            // For each block, check if there are any duplicates
            //var duplicates = blocks.Select(block => block.GroupBy(x => x).Any(g => g.Count() > 1)).ToArray();
            // If there are any duplicates, the text is probably encrypted with AES-128 in ECB mode
            // if (duplicates.Any(x => x))
            // {
            //     return "ECB";
            // }
            // else
            
            
            
            
            return "Not implemented";
            // try
            // {
            //     
            //     var keyBytes = Encoding.ASCII.GetBytes(Key);
            //     var decryptedBytes = Decrypt.DecryptAes128Ecb(encodedBytes, keyBytes);
            //     return Encoding.ASCII.GetString(decryptedBytes);
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine($"Decryption failed: {ex.Message}");
            //     return $"Decryption failed: {ex.Message}";
            // }
        }
    }
}
                
                
                
                
                // // Utility created for handling file path
                // var lines = File.ReadAllLines(Path.Combine(Utility.Assets, "challenge-6.txt"));
                // // The lines are base64 encoded - convert them to bytes
                // var bytes = lines.Select(Convert.FromBase64String).SelectMany(x => x).ToArray();
                //
                // // Loop through the key sizes 
                // var keySizes = Enumerable.Range(MinKeySize, MaxKeySize - MinKeySize + 1);
                // // For each key size, make some comparisons with consecutive blocks and output the average hamming distance for each key size
                // var keySizeDistances = keySizes.Select(keySize =>
                // {
                //     var hammingDistances = new List<int>();
                //     // Loop through the hamming blocks and get a hamming distance
                //     for (var i = 0; i < NumberOfHammingBlocks; i++)
                //     {
                //         var block1 = bytes.Skip(keySize * i).Take(keySize).ToArray();
                //         var block2 = bytes.Skip(keySize * (i + 1)).Take(keySize).ToArray();
                //         hammingDistances.Add(Comparators.HammingDistance(block1, block2));
                //     }
                //     // Then get the average for the key size
                //     var average = hammingDistances.Average();
                //     return new
                //     {
                //         KeySize = keySize,
                //         Distance = (float)average / keySize // Hamming distance normalized by key size
                //     };
                // }).OrderBy(x => x.Distance).ToList();
                
                // Logging the results, the more blocks we use the more accurate the results should be. Seems to be 29 is the best key size.
                // foreach (var keySizeDistance in keySizeDistances)
                // {
                //     Console.WriteLine(keySizeDistance.KeySize + ": " + keySizeDistance.Distance);
                // }
                
                // Get the key size - it'll be the first one in the list
                // var keySize = keySizeDistances.First().KeySize;
                // Console.WriteLine("The keysize producing the lowest average Hamming distance is " + keySize);
                //
                // // We now have the key size and can proceed with the rest of the challenge...
                // // Which means decrypting the text using the key size
                // // Start by taking the text and chunking it up into blocks of length keySize
                // var blocksOfKeySize = Converters.ChunkBytes(bytes, keySize);
                // // Transpose the blocks, e.g. put the first bytes of each block into an array, then the second byte of each block into another array, etc.
                // var transposedBlocks = Converters.TransposeArraysOfBytes(blocksOfKeySize);
                //
                // // Just some logging - the number of transposed blocks should match the key size...
                // // Console.WriteLine("The number of blocks is " + blocksOfKeySize.Count());
                // // Console.WriteLine("The number of transposed blocks is " + transposedBlocks.Count());
                //
                // // Convert to hex-encoded strings and XOR to get the key characters
                // var keyCharacters = transposedBlocks
                //     .Select(block => 
                //     {
                //         var hexBlock = Converters.BytesToHex(block);
                //         // I had to update the XOR character list to DEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+:,.-_()!$ @;"
                //         var decryptionResult = Decrypt.SingleCharacterXor(hexBlock);
                //         //Console.WriteLine($"Block: {hexBlock}, Character: {decryptionResult.Character}, Score: {decryptionResult.Score}");
                //         return decryptionResult.Character;
                //     })
                //     .ToArray();
                // // Then put them together and we have the key.
                // var key = new string(keyCharacters);
                // Console.WriteLine("The key is: " + key);
                //
                // // Decrypt the encrypted text with the key.
                // var hexKey = Converters.BytesToHex(Encoding.ASCII.GetBytes(key));
                // var decrypted = Decrypt.DecryptRepeatingKey(Converters.BytesToHex(bytes), hexKey);
                // return decrypted;

            // }
            // // Catching exceptions and providing a more helpful message (more a learning exercise than anything else)
            // catch (Exception ex)
            // {
            //     return ex switch
            //     {
            //         FileNotFoundException => "The file was not found: " + ex.Message,
            //         IOException => "An I/O error occurred: " + ex.Message,
            //         _ => "Error reading file: " + ex.Message
            //     };
            // }
            