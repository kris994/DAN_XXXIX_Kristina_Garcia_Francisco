using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DAN_XXXIX_Kristina_Garcia_Francisco
{
    class WriteReadFile
    {
        #region Property

        #endregion

        /// <summary>
        /// Write routes to file
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
        /// Read routes from file
        /// </summary>
        public void ReadMusicFile(List<Song> list, string file)
        {
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

                            Song s = new Song(authorName, songName, songDuration);
                            list.Add(s);
                        }
                    }
                }            
            }
        }
    }
}
