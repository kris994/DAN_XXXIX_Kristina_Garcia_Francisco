using System;
using System.Collections.Generic;
using System.Threading;

namespace DAN_XXXIX_Kristina_Garcia_Francisco
{
    class Program
    {
        public static string musicFile = "music.txt";
        public static string adsFile = "advertisement.txt";
        public static List<Song> allSong = new List<Song>();
        public static List<Advertisement> allAds = new List<Advertisement>();

        public static void Main(string[] args)
        {
            Song s = new Song();
            AudioPlayer ap = new AudioPlayer();
            WriteReadFile wrf = new WriteReadFile();
            Advertisement adv = new Advertisement();

            Thread readMusicFile = new Thread(() => wrf.ReadMusicFile(allSong, musicFile));
            Thread writeAdvertisementFile = new Thread(adv.AddAdvertisement);

            readMusicFile.Start();
            writeAdvertisementFile.Start();
            readMusicFile.Join();
            writeAdvertisementFile.Join();

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
