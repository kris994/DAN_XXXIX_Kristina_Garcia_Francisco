using System;
using System.Linq;

namespace DAN_XXXIX_Kristina_Garcia_Francisco
{
    /// <summary>
    /// The song class that contains all data that a song should have
    /// </summary>
    class Song
    {
        #region Properties
        public int SongNumber { get; set; }
        public string AuthorName { get; set; }
        public string SongName { get; set; }
        public string Duration { get; set; }
        #endregion

        #region Constructor
        public Song()
        {

        }

        public Song(int songNumber, string authorName, string songName, string duration)
        {
            SongNumber = songNumber;
            AuthorName = authorName;
            SongName = songName;
            Duration = duration;
        }
        #endregion

        /// <summary>
        /// Notify the application when the song stops playing
        /// </summary>
        public void SongStopped()
        {          
            Console.WriteLine("\nAudio player was closed.");
        }

        /// <summary>
        /// Lists all songs in the application from the list
        /// </summary>
        public void ListAllSongs()
        {
            if (Program.allSong.Any())
            {
                foreach (var item in Program.allSong)
                {
                    Console.WriteLine("{0}. [{1}]: [{2}] [{3}]", item.SongNumber, item.AuthorName, item.SongName, item.Duration);
                }
            }
            else
            {
                Console.WriteLine("No songs in the list.");
            }
        }

        /// <summary>
        /// Get a song to play by the song number
        /// </summary>
        /// <returns>The song the user is currently playing</returns>
        public Song ReadSong()
        {
            Validation val = new Validation();

            if (Program.allSong.Any())
            {
                Console.WriteLine("Press \"ESC\", or any other button to stop the player from running.");
                Console.WriteLine("-------------------------------");
                ListAllSongs();
                Console.WriteLine("-------------------------------");
                Console.Write("\nChoose a song number: ");
                int songNo = val.ValidPositiveNumber(1000);
                bool found = false;
                // Counts how long it takes to find a valid song
                int count = -1;

                do
                {
                    for (int i = 0; i < Program.allSong.Count; i++)
                    {
                        count++;
                        // Song was found
                        if (Program.allSong[i].SongNumber == songNo)
                        {
                            Console.WriteLine("\nCurrently playing {0}: Duration {1}\n",
                                Program.allSong[i].SongName, Program.allSong[i].Duration);
                            found = true;
                            break;
                        }
                    }

                    if (found == false)
                    {
                        {
                            count = -1;
                            Console.Write("No song with that name. \nPlease try again: ");
                            songNo = val.ValidPositiveNumber(1000);
                        }
                    }
                } while (found == false);

                return Program.allSong[count];
            }
            else
            {
                Console.WriteLine("No songs in the list.");
            }
            return null;
        }

        /// <summary>
        /// Create a song with given user inputs
        /// </summary>
        public void CreateSong()
        {
            Validation val = new Validation();
            WriteReadFile wrf = new WriteReadFile();

            // Author name
            Console.Write("Please enter the name of the author: ");
            string authorName = val.CheckIfNullOrEmpty();

            // Song name
            Console.Write("Please enter the name of the song: ");
            string songName = val.CheckIfNullOrEmpty();

            // Song duration
            Console.WriteLine("Please enter the song duration: ");
            Console.Write("Hour: ");
            int hours = val.ValidPositiveNumber(23);
            Console.Write("Minutes: ");
            int minutes = val.ValidPositiveNumber(59);
            Console.Write("Seconds: ");
            int seconds = val.ValidPositiveNumber(59);
            string songDuration = String.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);

            // String for the file
            string song = "[" + authorName + "]:" + "[" + songName + "]" + "[" + songDuration + "]";

            // Get the new song number value
            int id = 0;
            if (Program.allSong.Any())
            {
               id = Program.allSong.Max(r => r.SongNumber) + 1;
            }
            else
            {
                id = 1;
            }
            
            Song s = new Song(id, authorName, songName, songDuration);
            Program.allSong.Add(s);

            wrf.WriteSingleLineToFile(Program.musicFile, song);

            Console.Write("\nAdded a song with name: {0}\n", song);
        }
    }
}
