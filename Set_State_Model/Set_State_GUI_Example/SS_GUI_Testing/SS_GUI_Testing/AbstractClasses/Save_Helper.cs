
using DashBoardDemo.Save_Classes;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace DashBoardDemo.GUI_Handler_Classes
{
    public static class Save_Helper
    {
        /// <summary>
        /// saves the diagram created and squares
        /// </summary>
        public static void saveDiagram(List<SquareDetails> list)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Documents (.xml)|*.xml";
            saveFileDialog.DefaultExt = ".xml";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = Path.GetFullPath(saveFileDialog.FileName);
                System.IO.FileStream fileStream;
                fileStream = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                XmlSerializer xml = new XmlSerializer(typeof(List<SquareDetails>), new XmlRootAttribute("SquareDetails"));
                StreamWriter writer = new StreamWriter(fileStream);
                xml.Serialize(fileStream, list);
                fileStream.Close();
            }
        }
        /// <summary>
        /// loads in a diagram thats in a xml file
        /// </summary>
        public static List<SquareDetails> loadDiagram()
        {
            List<SquareDetails> list = new List<SquareDetails>();
            //Clear current screen
            list.Clear();
            //get the file to read
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Documents (.xml)|*.xml";
            openFileDialog.DefaultExt = ".xml";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = Path.GetFullPath(openFileDialog.FileName);
                //create the xml object 
                XmlSerializer deserializer = new XmlSerializer(typeof(List<SquareDetails>), new XmlRootAttribute("SquareDetails"));
                FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
                list = (List<SquareDetails>)deserializer.Deserialize(stream);
                stream.Close();
                
            }
            return list;
        }
    }
}
