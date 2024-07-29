namespace Cryptopals.Challenges
{
    // Challenge 6: Provided with a text file, find out which line has been encrypted with a single character XOR
    public class Challenge6
    {
        private const int MinKeySize = 2;
        private const int MaxKeySize = 40;
        private const int NumberOfHammingBlocks = 20;

        public static string Run()
        {
            // Trying out ReadAllLines to avoid the need to manually split the string on new lines
            try
            {
                var lines = File.ReadAllLines("./Assets/challenge-6.txt");
                // the lines are base64 encoded - convert them to bytes
                var bytes = lines.Select(Convert.FromBase64String).SelectMany(x => x).ToArray();

                // Loop through the key sizes 
                var keySizes = Enumerable.Range(MinKeySize, MaxKeySize - MinKeySize + 1);
                // For each key size, make some comparisons with consecutive blocks and output the average hamming distance for each key size
                var keySizeDistances = keySizes.Select(keySize =>
                {
                    var hammingDistances = new List<int>();
                    for (var i = 0; i < NumberOfHammingBlocks; i++)
                    {
                        var block1 = bytes.Skip(keySize * i).Take(keySize).ToArray();
                        var block2 = bytes.Skip(keySize * (i + 1)).Take(keySize).ToArray();
                        hammingDistances.Add(Comparators.HammingDistance(block1, block2));
                    }

                    var average = hammingDistances.Average();
                    return new
                    {
                        KeySize = keySize,
                        Distance = (float)average / keySize
                    };
                }).OrderBy(x => x.Distance).ToList();
                // Print out the results, the more blocks we use the more accurate the results should be. Seems to be 29 is the best key size.
                // foreach (var keySizeDistance in keySizeDistances)
                // {
                //     Console.WriteLine(keySizeDistance.KeySize + ": " + keySizeDistance.Distance);
                // }
                // get the smallest key size
                var keySize = keySizeDistances.First().KeySize;
                
            }
            // Catching exceptions and providing a more helpful message (more a learning exercise than anything else)
            catch (Exception ex)
            {
                return ex switch
                {
                    FileNotFoundException => "The file was not found: " + ex.Message,
                    IOException => "An I/O error occurred: " + ex.Message,
                    _ => "Error reading file: " + ex.Message
                };
            }

            return "";
        }
    }
}