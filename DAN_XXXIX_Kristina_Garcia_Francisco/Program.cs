using System;
using System.Collections.Generic;
using System.Threading;

namespace DAN_XXXIX_Kristina_Garcia_Francisco
{
    /// <summary>
    /// The main program class that contains the main menu
    /// </summary>
    class Program
    {
        /// <summary>
        /// The song/music list file
        /// </summary>
        public static string musicFile = "music.txt";
        /// <summary>
        /// The Advertisement file
        /// </summary>
        public static string adsFile = @"~\..\..\..\advertisement.txt";
        /// <summary>
        /// List of all songs in the application
        /// </summary>
        public static List<Song> allSong = new List<Song>();
        /// <summary>
        /// List of all Advertisements in the application
        /// </summary>
        public static List<Advertisement> allAds = new List<Advertisement>();
        /// <summary>
        /// Signals the song or advertisement when to play or stop playing
        /// </summary>
        public static EventWaitHandle waitAdv = new AutoResetEvent(false);

        /// <summary>
        /// The main method that contains the main menu
        /// </summary>
        /// <param name="args">main method arguments</param>
        public static void Main(string[] args)
        {
            Song s = new Song();
            AudioPlayer ap = new AudioPlayer();
            WriteReadFile wrf = new WriteReadFile();
            Advertisement adv = new Advertisement();

            // First load all files
            Thread readMusicFile = new Thread(() => wrf.ReadMusicFile(allSong, musicFile));
            Thread readAdvertisementFile = new Thread(() => wrf.ReadAdvertisementFile(allAds, adsFile));

            // Start both threads at the same time but let them finish before continuing. 
            // To make sure all data has been loaded.
            readMusicFile.Start();
            readAdvertisementFile.Start();
            readMusicFile.Join();
            readAdvertisementFile.Join();

            bool restart = true;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Select \"b\" to return at any point.");
                Console.WriteLine("=========================================");
                Console.WriteLine("1 - All Songs");
                Console.WriteLine("2 - Add a Song");
                Console.WriteLine("3 - Audio Player");
                Console.WriteLine("0 - Exit");
                Console.WriteLine("=========================================");
                Console.Write(">>> ");

                string option = Console.ReadLine();
                Console.Clear();

                switch (option)
                {
                    case "1":
                        Thread allSongs = new Thread(s.ListAllSongs);
                        allSongs.Start();
                        allSongs.Join();
                        break;
                    case "2":
                        Thread createSong = new Thread(s.CreateSong);
                        createSong.Start();
                        createSong.Join();
                        break;
                    case "3":
                        Thread chooseSong = new Thread(ap.AutioPlayerActivity);
                        chooseSong.Start();
                        chooseSong.Join();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Not a valid option");
                        break;
                }
                ap.Notify();
            } while (restart != false);

            Console.ReadKey();
        }
    }
}
