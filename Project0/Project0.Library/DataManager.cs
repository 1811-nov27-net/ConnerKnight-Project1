using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Project0.Library
{
    /// <summary>
    /// manages the serialization and deserialization of data for the whole solution
    /// </summary>
    public class DataManager
    {
        private XmlSerializer UserSer;
        private XmlSerializer LocSer;
        private readonly string UserFile;
        private readonly string LocFile;

        public DataManager(string fileLocation)
        {
            UserSer = new XmlSerializer(typeof(List<User>));
            LocSer = new XmlSerializer(typeof(List<Location>));
            UserFile = fileLocation + @"\userdata.xml";
            LocFile = fileLocation + @"\locdata.xml";
        }

        public void SerializeAll(List<User> ul, List<Location> ol)
        {

            //two ways of writing to a file
            //long explicit way
            FileStream userfs = null;
            try
            {
                userfs = new FileStream(UserFile, FileMode.Create);
                UserSer.Serialize(userfs, ul);
            }
            catch (IOException e)
            {

                throw e;
            }
            finally
            {
                userfs?.Dispose();
            }
            //short way
            using (var locfs = new FileStream(LocFile, FileMode.Create))
            {
                LocSer.Serialize(locfs, ol);
            }


        }

        public List<User> DeserializeUsers()
        {
            List<User> result;
            using (var userfs = new FileStream(UserFile, FileMode.Open))
            {
                result = (List<User>)UserSer.Deserialize(userfs);
            }

            return result;
        }

        public List<Location> DeserializeLocation()
        {
            List<Location> result = new List<Location>();
            using (var locfs = new FileStream(LocFile,FileMode.Open))
            {
                result = (List<Location>)LocSer.Deserialize(locfs);
            }

            return result;
        }

    }
}
