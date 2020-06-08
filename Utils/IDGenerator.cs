using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Book.Utils
{
    public static class IDGenerator
    {
        public static int GenerateID()
        {
            try
            {
                int newID = 1;
                List<int> usedIDs = LoadIDList();

                if (usedIDs.Count != 0)
                    while (usedIDs.Contains(newID))
                        ++newID;

                CreateIDinXML(newID);
                return newID;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Не удалось сгенерировать ID, непредвиденная ошибка: {ex.Message}");
            }
        }

        private static List<int> LoadIDList()
        {
            try
            {
                XDocument usedID = XDocument.Load(Constants.UsedIDFileName);
                List<int> IDs = new List<int>();
                foreach (XElement element in usedID.Root.Elements())
                {
                    int ID = Int32.Parse(element.Value);
                    IDs.Add(ID);
                }
                return IDs;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        private static void CreateIDinXML(int ID)
        {
            try
            {
                XDocument IDList = XDocument.Load(Constants.UsedIDFileName);
                XElement IDNode = new XElement("ID", ID.ToString());
                IDList.Root.Add(IDNode);
                IDList.Save(Constants.UsedIDFileName);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public static void DeleteID(int ID)
        {
            try
            {
                XDocument IDList = XDocument.Load(Constants.UsedIDFileName);
                foreach (var id in IDList.Root.Elements())
                    if (Int32.Parse(id.Value) == ID)
                    {
                        id.Remove();
                        break;
                    }

                IDList.Save(Constants.UsedIDFileName);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}