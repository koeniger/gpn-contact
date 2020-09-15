using DataBaseUpdater.DataContext;
using Microsoft.Extensions.Configuration;
using Models.gpn;
using System;
using System.Collections.Generic;
using System.IO;
using WebApp.Models;

namespace DataBaseUpdater.ConsoleCommander
{
    class Images
    {
        ImageProfile imageProfile;
        public void AddTestImages(UpdateContext context)
        {
            string read = GetDirectory();

            var configuration = Helpers.ReadConfigFromAppconfig();

            imageProfile = GetSectionContent(configuration);

        }

        private ImageProfile GetSectionContent(IConfiguration configSection)
        {
            ImageProfile content = new ImageProfile();

            foreach (var section in configSection.GetSection("imageProfile").GetChildren())
            {
                switch (section.Key)
                { 
                    case "AllowedExtensions":
                    {
                            content.AllowedExtensions = new List<string>();
                            int index = 0;
                            while(true)
                            {
                                var item = section.GetSection($"{index++}").Value;

                                if (item != null)
                                {
                                    content.AllowedExtensions.Add(item);
                                }
                                else
                                {
                                    break;
                                }
                            }
                            break;
                    }
                    case "MaxSizeBytes":
                        {
                            content.MaxSizeBytes = Convert.ToInt32(section.Value);
                            break;
                        }
                    default:
                    case "Path":
                        {
                            break;
                        }
                }
            }
            return content;
        }

        private string GetDirectory()
        {
            Console.Write("The path to the files to read:");
            return Console.ReadLine();
        }

        public IEnumerable<image> Indexation(string readDirectory)
        {
            var images = new List<image>();

            DirectoryInfo dir = new DirectoryInfo(readDirectory);

            foreach (var item in dir.GetDirectories())
            {
                FileInfo[] files = item.GetFiles();
                foreach (var file in files)
                {
                    try
                    {
                        imageProfile.Validate(file);

                        imageProfile.Validate(file);
                    }
                    catch
                    { }
                    ///Далее прописать заполнение папки "wwwroot\\images" и fdc_image
                }
            }

            return images;
        }
    }
}
