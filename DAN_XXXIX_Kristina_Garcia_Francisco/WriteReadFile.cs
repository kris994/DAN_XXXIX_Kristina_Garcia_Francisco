using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DAN_XXXIX_Kristina_Garcia_Francisco
{
    /// <summary>
    /// Reads or Writes from the file
    /// </summary>
    class WriteReadFile
    {
        /// <summary>
        /// Write a single line to the file
        /// </summary>
        public void WriteSingleLineToFile(string file, string line)
        {
            // Save all the routes to file
            using (StreamWriter streamWriter = new StreamWriter(file, append: true))
            {
                streamWriter.WriteLine(line);
            }
        }

        /// <summary>
        /// Reads the song file
        /// </summary>
        /// <param name="list">List where we will save all the songs</param>
        /// <param name="file">File that we are reading from</param>
        public void ReadMusicFile(List<Song> list, string file)
        {
            int id = 0;
            if (File.Exists(file))
            {
                // Only read from file if empty
                if (!Program.allSong.Any())
                {
                    string[] readFile = File.ReadAllLines(file);

                    for (int i = 0; i < readFile.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(readFile[i]))
                        {
                            string[] trim = readFile[i].Split(new char[] { '[', ']'});
                            string authorName = trim[1];
                            string songName = trim[3];
                            string songDuration = trim[5];
                            id++;                           
                            Song s = new Song(id, authorName, songName, songDuration);
                            list.Add(s);
                        }
                    }
                }            
            }
        }

        /// <summary>
        /// Read all advertisements from the file
        /// </summary>
        /// <param name="list">List where we will save all the advertisements</param>
        /// <param name="file">File that we are reading from</param>
        public void ReadAdvertisementFile(List<Advertisement> list, string file)
        {
            // Only read from file if empty
            if (!Program.allAds.Any())
            {
                using (StreamReader streamReader = File.OpenText(file))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        list.Add(new Advertisement(line));
                    }
                }
            }
        }
    }
}
