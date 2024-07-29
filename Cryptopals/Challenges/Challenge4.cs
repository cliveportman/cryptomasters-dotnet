namespace Cryptopals.Challenges
{
    // Challenge 4: Provided with a text file, find out which line has been encrypted with a single character XOR
    public class Challenge4
    {
        public static string Run()
        {
            // Trying out ReadAllLines to avoid the need to manually split the string on new lines
            try
            {
                var lines = File.ReadAllLines("./Assets/challenge-4.txt");
                var results = lines.Select(Decrypt.SingleCharacterXor)
                    .OrderByDescending(result => result.Score).ToList();
                return results[0].Character + ": " + results[0].Score + " " + results[0].DecryptedText?.TrimEnd(); // Removing the trailing newline
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
        }
    }
}