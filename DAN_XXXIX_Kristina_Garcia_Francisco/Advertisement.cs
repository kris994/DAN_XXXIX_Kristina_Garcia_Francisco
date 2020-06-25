using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XXXIX_Kristina_Garcia_Francisco
{
    class Advertisement
    {
        public string Name { get; set; }

        public Advertisement()
        {

        }

        public Advertisement(string name)
        {
            Name = name;
        }

        public void AddAdvertisement()
        {
            WriteReadFile wrf = new WriteReadFile();

            if (!Program.allAds.Any())
            {
                Advertisement ad1 = new Advertisement("FitPass popusti");
                Program.allAds.Add(ad1);
                wrf.WriteSingleLineToFile(Program.adsFile, ad1.Name);

                Advertisement ad2 = new Advertisement("Letovanje u Srbiji");
                Program.allAds.Add(ad2);
                wrf.WriteSingleLineToFile(Program.adsFile, ad2.Name);

                Advertisement ad3 = new Advertisement("AliExpress free shipping");
                Program.allAds.Add(ad3);
                wrf.WriteSingleLineToFile(Program.adsFile, ad3.Name);

                Advertisement ad4 = new Advertisement("Pizza metar by Popaj");
                Program.allAds.Add(ad4);
                wrf.WriteSingleLineToFile(Program.adsFile, ad4.Name);

                Advertisement ad5 = new Advertisement("Online kurs programiranja");
                Program.allAds.Add(ad5);
                wrf.WriteSingleLineToFile(Program.adsFile, ad5.Name);
            }
        }
    }
}
