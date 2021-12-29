using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Hosting;

namespace AnReshWebApp.Models
{
    public class Description
    {
        public string descriptionText;
        readonly string filePath = AppConfiguration.DescriptionFilePath;
        
        private string DescriptionRead()
        {
            if (File.Exists(filePath))
            {
                using (TextReader descriptionReader = new StreamReader(filePath))
                {
                    return descriptionReader.ReadToEnd();
                }
            }
            else return "Файл не существует!";
        }

        private void DescriptionWrite(string newDescription) 
        {
            using (TextWriter descriptionWriter = new StreamWriter(filePath))
            {
                descriptionWriter.WriteLine(newDescription);
            }
        }
        public Description()
        {
            this.descriptionText = DescriptionRead();
        }

        public string DescriptionTextGet()
        {
            this.descriptionText = DescriptionRead();
            return descriptionText;
        }

        public string DescriptionTextSet(string newDescription)
        {
            this.descriptionText = newDescription;
            DescriptionWrite(newDescription);
            return descriptionText;
        }

    }
}