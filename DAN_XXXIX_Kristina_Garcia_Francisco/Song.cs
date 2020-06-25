using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAN_XXXIX_Kristina_Garcia_Francisco
{
    class Song
    {
        public int SongNumber { get; set; }
        public string AuthorName { get; set; }
        public string SongName { get; set; }
        public string Duration { get; set; }

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

        public void SongStopped()
        {          
            Console.WriteLine("\nAudio player was closed.");
        }

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

        public Song ReadSong()
        {
            Validation val = new Validation();

            if (Program.allSong.Any())
            {
                Console.WriteLine("Press \"ESC\" button to stop the player from running.");
                Console.WriteLine("-------------------------------");
                ListAllSongs();
                Console.WriteLine("-------------------------------");
                Console.Write("\nChoose a song number: ");
                int songNo = val.ValidPositiveNumber(1000);
                bool found = false;
                int count = -1;

                do
                {
                    for (int i = 0; i < Program.allSong.Count; i++)
                    {
                        count++;
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

        public void CreateSong()
        {
            Validation val = new Validation();
            WriteReadFile wrf = new WriteReadFile();

            Console.Write("Please enter the name of the author: ");
            string authorName = val.CheckIfNullOrEmpty();

            Console.Write("Please enter the name of the song: ");
            string songName = val.CheckIfNullOrEmpty();

            Console.WriteLine("Please enter the song duration: ");
            Console.Write("Hour: ");
            int hours = val.ValidPositiveNumber(23);
            Console.Write("Minutes: ");
            int minutes = val.ValidPositiveNumber(59);
            Console.Write("Seconds: ");
            int seconds = val.ValidPositiveNumber(59);
            string songDuration = String.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);

            string song = "[" + authorName + "]:" + "[" + songName + "]" + "[" + songDuration + "]";

            int id = 0;
            // Max number of song numbers
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
