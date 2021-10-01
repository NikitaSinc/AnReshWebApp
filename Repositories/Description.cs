using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AnReshWebApp.Models
{
    public class Description
    {
        public string descriptionText;

        private const string filePath = @"C:\Users\nikit\source\repos\AnReshWebApp\App_Data\Description.txt";
        private string DescriptionRead() // Reading from description file
        {
            using (System.IO.TextReader descriptionReader = new System.IO.StreamReader(filePath)) return descriptionReader.ReadToEnd();
        }

        private void DescriptionWrite(string newDescription) // Writing in description file
        {
            using (System.IO.TextWriter descriptionWriter = new System.IO.StreamWriter(filePath))
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