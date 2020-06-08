using Book.Utils;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Book.MVVM.Models
{
    public class Part : Notifier, IComparable
    {
        public Part(int id, string name, int chapterid, int orderNumber, string description = "", string quote = "", string authorOfTheQuote = "", string fileName = "", bool done = false)
        {
            ID = id;
            ChapterID = chapterid;
            _name = name;
            _description = description;
            _orderNumber = orderNumber;
            _quote = quote;
            _authorOfTheQuote = authorOfTheQuote;
            FileName = fileName;
            _done = done;
        }

        #region Fields
        private string _name;
        private string _description;
        private int _orderNumber;
        private string _quote;
        private string _authorOfTheQuote;
        private string _fileName;
        private bool _done;
        #endregion

        #region Properties
        public int ID { get; }
        public int ChapterID { get; }
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
        public int OrderNumber
        {
            get { return _orderNumber; }
            set { SetProperty(ref _orderNumber, value); }
        }
        public string Quote
        {
            get { return _quote; }
            set { SetProperty(ref _quote, value); }
        }
        public string AuthorOfTheQuote
        {
            get { return _authorOfTheQuote; }
            set { SetProperty(ref _authorOfTheQuote, value); }
        }
        public string FileName
        {
            get { return _fileName; }
            set { SetProperty(ref _fileName, value); }
        }
        public bool Done
        {
            get { return _done; }
            set { SetProperty(ref _done, value); }
        }
        #endregion

        #region Public Methods
        public static List<Part> LoadAllParts(string fileName = Constants.PartsFileName)
        {
            try
            {
                if (!File.Exists(fileName))
                    throw new ApplicationException("Файл с частями не найден");

                XDocument partList = XDocument.Load(fileName);
                List<Part> parts = new List<Part>();
                foreach (XElement part in partList.Element("Parts").Elements("Part"))
                {
                    int _id = Int32.Parse(part.Element("ID").Value);
                    string _name = part.Element("Name").Value;
                    int _chapterID = Int32.Parse(part.Element("ChapterID").Value);
                    string _description = part.Element("Description").Value;
                    int _orderNumber = Int32.Parse(part.Element("OrderNumber").Value);
                    string _quote = part.Element("Quote").Value;
                    string _authorOfTheQuote = part.Element("AuthorOfTheQuote").Value;
                    string _fileName = part.Element("FileName").Value;
                    bool _done = Boolean.Parse(part.Element("Done").Value);
                    Part newPart = new Part(id: _id, name: _name, chapterid: _chapterID, orderNumber: _orderNumber, description: _description, quote: _quote, authorOfTheQuote: _authorOfTheQuote, fileName: _fileName, done: _done);
                    parts.Add(newPart);
                }
                parts.Sort();
                return parts;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException($"Ошибка при попытке получить полный список частей: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Непредвиденная ошибка при попытке получить полный список частей: {ex.Message}");
            }
        }

        public static List<Part> LoadPartListForChapter(int ChapterID)
        {
            try
            {
                XDocument partList = XDocument.Load(Constants.PartsFileName);
                List<Part> parts = new List<Part>();
                foreach (XElement part in partList.Element("Parts").Elements("Part"))
                {
                    if (Int32.Parse(part.Element("ChapterID").Value) != ChapterID) continue;
                    int _id = Int32.Parse(part.Element("ID").Value);
                    string _name = part.Element("Name").Value;
                    int _chapterID = Int32.Parse(part.Element("ChapterID").Value);
                    string _description = part.Element("Description").Value;
                    int _orderNumber = Int32.Parse(part.Element("OrderNumber").Value);
                    string _quote = part.Element("Quote").Value;
                    string _authorOfTheQuote = part.Element("AuthorOfTheQuote").Value;
                    string _fileName = part.Element("FileName").Value;
                    bool _done = Boolean.Parse(part.Element("Done").Value);
                    Part newPart = new Part(id: _id, name: _name, chapterid: _chapterID, orderNumber: _orderNumber, description: _description, quote: _quote, authorOfTheQuote: _authorOfTheQuote, fileName: _fileName, done: _done);
                    parts.Add(newPart);
                }
                if (parts.Count > 1)
                    parts.Sort();
                return parts;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException($"Ошибка при попытке загрузить список частей для определенной главы: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Непредвиденная ошибка при попытке загрузить список частей для определенной главы: {ex.Message}");
            }
        }

        public static void CreateNewPart(Chapter chapter, string name, string quote = "", string authorOfTheQuote = "", string description = "", string FileName = "")
        {
            try
            {
                Validator.ValidateName(name);

                if (!String.IsNullOrWhiteSpace(quote) && String.IsNullOrWhiteSpace(authorOfTheQuote))
                    throw new ApplicationException("Не указан автор цитаты");
                if (String.IsNullOrWhiteSpace(quote) && !String.IsNullOrWhiteSpace(authorOfTheQuote))
                    throw new ApplicationException("Указан автор цитаты, но не указана цитата");

                int ID = IDGenerator.GenerateID();
                int orderNumber = chapter.Parts.Count + 1;
                Part newPart = new Part(id: ID, name: name, chapterid: chapter.ID, orderNumber: orderNumber, description: description, quote: quote, authorOfTheQuote: authorOfTheQuote, fileName: FileName);
                if (!String.IsNullOrWhiteSpace(FileName)) SaveFile(ref newPart);
                AddPartInXML(newPart);
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException($"Ошибка при создании новой части: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Непредвиденная ошибка при создании новой части: {ex.Message}");
            }
        }

        public static void OpenFile(Part part)
        {
            try
            {
                if (part == null)
                    throw new ApplicationException("Не удалось открыть часть, не передана часть");
                if (String.IsNullOrWhiteSpace(part.FileName))
                    throw new ApplicationException("У выбранной части нет файла");
                string argument = @"/c " + part.FileName;
                Process.Start(@"cmd.exe ", argument);
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException($"Ошибка при попытке открыть файл: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Непредвиденная ошибка при попытке открыть файл: {ex.Message}");
            }
        }

        public static string GetPath()
        {
            try
            {
                var dialog = new OpenFileDialog();
                dialog.ShowDialog();
                return dialog.FileName;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException($"Ошибка при попытке получить путь к файлу: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Непредвиденная ошибка при попытке получить путь к файлу: {ex.Message}");
            }
        }

        public static void Remove(Part part)
        {
            try
            {
                if (part == null)
                    throw new ApplicationException("Критическая ошибка, невозможно провести удаление, не передана часть для удаления");
                Delete(part);
                XDocument partList = XDocument.Load(Constants.PartsFileName);
                var itemsHigher = from pt in partList.Root.Elements("Part")
                                  where Int32.Parse(pt.Element("OrderNumber").Value) > part.OrderNumber
                                  select pt;
                for (int i = 0; i < itemsHigher.Count(); i++)
                    itemsHigher.ElementAt(i).Element("OrderNumber").Value = (Int32.Parse(itemsHigher.ElementAt(i).Element("OrderNumber").Value) - 1).ToString();

                partList.Save(Constants.PartsFileName);
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException($"Ошибка при попытке удалить часть: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Непредвиденная ошибка при попытке удалить часть: {ex.Message}");
            }
        }

        public static void SaveChangesInXML(Part part)
        {
            try
            {
                if (part == null)
                    throw new ApplicationException("Критическая ошибка, часть для удаления не выбрана");

                Part changedPart = FindPart(part.ID);
                List<string> distinctProperties = Except(part, changedPart);
                if (distinctProperties.Count == 0)
                    return;
                if (!String.IsNullOrWhiteSpace(part.FileName) && distinctProperties.Contains("FileName")) SaveFile(ref part);
                if (distinctProperties.Contains("Name"))
                    Validator.ValidateName(part.Name);
                Delete(changedPart);
                AddPartInXML(part);
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException($"Ошибка при схранении изменений части: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Непредвиденная ошибка при схранении изменений части: {ex.Message}");
            }
        }

        public static Part FindPart(int id)
        {
            try
            {
                XDocument partList = XDocument.Load(Constants.PartsFileName);
                var items = from pt in partList.Element("Parts").Elements("Part")
                            where Int32.Parse(pt.Element("ID").Value) == id
                            select pt;

                if (items.Count() == 0)
                    throw new ApplicationException("Критическая ошибка, не найден изменяемый элемент");
                if (items.Count() > 1)
                    throw new ApplicationException("Критическая ошибка, найдено несколько элементов с одинаковым ID. Это не должно было случиться!");

                return ConvertXElementToPart(items.First());
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException($"Ошибка при попытке найти часть: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Непредвиденная ошибка при попытке найти часть: {ex.Message}");
            }
        }

        public static bool PartsAreIdentical(Part part1, Part part2)
        {
            try
            {
                if (part1 == null || part2 == null)
                    throw new ApplicationException("Критическая ошибка. Не удалось сравнить две части, одна из частей, либо обе, отсутствует");
                if (part1.ID != part2.ID)
                    throw new ApplicationException("Критическая ошибка, ID не может различаться");
                if (part1.Name != part2.Name || part1.ChapterID != part2.ChapterID ||
                    part1.Quote != part2.Quote || part1.AuthorOfTheQuote != part2.AuthorOfTheQuote ||
                    part1.Description != part2.Description || part1.OrderNumber != part2.OrderNumber ||
                    part1.FileName != part2.FileName || part1.Done != part2.Done)
                    return false;
                else
                    return true;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException($"Ошибка при установлении идентичности частей: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Непредвиденная ошибка при установлении идентичности частей: {ex.Message}");
            }
        }

        public static List<string> Except(Part part1, Part part2)
        {
            try
            {
                if (part1 == null || part2 == null)
                    throw new ApplicationException("Критическая ошибка. Не удалось сравнить две части, одна из частей, либо обе, отсутствует");
                if (part1.ID != part2.ID)
                    throw new ApplicationException("Критическая ошибка, ID не может различаться");
                List<string> distinct = new List<string>();
                if (part1.Name != part2.Name) distinct.Add("Name");
                if (part1.ChapterID != part2.ChapterID) distinct.Add("ChapterID");
                if (part1.Quote != part2.Quote) distinct.Add("Quote");
                if (part1.AuthorOfTheQuote != part2.AuthorOfTheQuote) distinct.Add("AuthorOfTheQuote");
                if (part1.Description != part2.Description) distinct.Add("Description");
                if (part1.OrderNumber != part2.OrderNumber) distinct.Add("OrderNumber");
                if (part1.FileName != part2.FileName) distinct.Add("FileName");
                if (part1.Done != part2.Done) distinct.Add("Done");
                return distinct;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException($"Ошибка при сравнении частей: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Непредвиденная ошибка при сравнении частей: {ex.Message}");
            }
        }
        #endregion

        #region Private Methods
        private static Part ConvertXElementToPart(XElement element)
        {
            if (element.Name != "Part")
                throw new ApplicationException("Критическая ошибка, невозможно конвертировать элемент данных в часть, элемент не является частью");
            int _id = Int32.Parse(element.Element("ID").Value);
            string _name = element.Element("Name").Value;
            int _chapterID = Int32.Parse(element.Element("ChapterID").Value);
            string _description = element.Element("Description").Value;
            int _orderNumber = Int32.Parse(element.Element("OrderNumber").Value);
            string _quote = element.Element("Quote").Value;
            string _authorOfTheQuote = element.Element("AuthorOfTheQuote").Value;
            string _fileName = element.Element("FileName").Value;
            bool _done = Boolean.Parse(element.Element("Done").Value);
            return new Part(id: _id, name: _name, chapterid: _chapterID, orderNumber: _orderNumber, description: _description, quote: _quote, authorOfTheQuote: _authorOfTheQuote, fileName: _fileName, done: _done);
        }

        private static void Delete(Part part)
        {
            if (part == null)
                throw new ApplicationException("Часть для удаления не выбрана");

            XDocument partList = XDocument.Load(Constants.PartsFileName);
            var items = from pt in partList.Element("Parts").Elements("Part")
                        where Int32.Parse(pt.Element("ID").Value) == part.ID
                        select pt;

            if (items.Count() == 0)
                throw new ApplicationException("Критическая ошибка, не найден изменяемый элемент");
            if (items.Count() > 1)
                throw new ApplicationException("Критическая ошибка, найдено несколько элементов для удаления. Это не должно было случиться!");

            XElement changedPart = items.First();
            bool successfullRemove = false;
            foreach (var pt in partList.Element("Parts").Elements("Part"))
                if (pt.Element("ID").Value == changedPart.Element("ID").Value)
                {
                    pt.Remove();
                    successfullRemove = true;
                    break;
                }

            if (!successfullRemove)
                throw new ApplicationException("Критическая ошибка, не удалось удалить часть");

            partList.Save(Constants.PartsFileName);
            IDGenerator.DeleteID(Int32.Parse(changedPart.Element("ID").Value));
        }

        private static void AddPartInXML(Part part)
        {
            OptimizeData(ref part);
            XDocument partList = XDocument.Load(Constants.PartsFileName);
            XElement newPart = GetXElementForPart(part);
            partList.Root.Add(newPart);
            partList.Save(Constants.PartsFileName);
        }

        private static void OptimizeData(ref Part part)
        {
            part.Name = part.Name.Trim().Replace("  ", " ");
            if (!String.IsNullOrWhiteSpace(part.Description))
                part.Description = "\t" + part.Description.Trim().Replace("  ", " ");
            if (!String.IsNullOrWhiteSpace(part.Quote))
                part.Quote = part.Quote.Trim(new char[] { '.', '"', ' ' }).Replace("  ", " ");
            if (!String.IsNullOrWhiteSpace(part.AuthorOfTheQuote))
                part.AuthorOfTheQuote = part.AuthorOfTheQuote.Trim().Replace("  ", " ");
            if (part.Description == null) part.Description = "";
            if (part.Quote == null) part.Quote = "";
            if (part.AuthorOfTheQuote == null) part.AuthorOfTheQuote = "";
            if (part.FileName == null) part.FileName = "";
        }

        private static void SaveFile(ref Part part)
        {
            if (String.IsNullOrWhiteSpace(part.FileName))
                throw new ApplicationException("Невозможно сохранить файл, путь к нему не указан");
            string copyPath = @"Data\Chapters\" + part.ID + ".docx";
            File.Copy(part.FileName, copyPath, true);
            if (!File.Exists(copyPath))
                throw new ApplicationException("Критическая ошибка, не удалось сохранить файл части");
            part.FileName = copyPath;
        }

        private static XElement GetXElementForPart(Part part)
        {
            XElement xElementPart = new XElement("Part");
            XElement ID = new XElement("ID", part.ID);
            XElement Name = new XElement("Name", part.Name);
            XElement ChapterID = new XElement("ChapterID", part.ChapterID);
            XElement Description = new XElement("Description", part.Description);
            XElement OrderNumber = new XElement("OrderNumber", part.OrderNumber);
            XElement Quote = new XElement("Quote", part.Quote);
            XElement AuthorOfTheQuote = new XElement("AuthorOfTheQuote", part.AuthorOfTheQuote);
            XElement FileName = new XElement("FileName", part.FileName);
            XElement Done = new XElement("Done", part.Done);
            xElementPart.Add(ID);
            xElementPart.Add(Name);
            xElementPart.Add(ChapterID);
            xElementPart.Add(Description);
            xElementPart.Add(OrderNumber);
            xElementPart.Add(Quote);
            xElementPart.Add(AuthorOfTheQuote);
            xElementPart.Add(FileName);
            xElementPart.Add(Done);
            return xElementPart;
        }
        #endregion

        #region IComporable Implemetation
        public int CompareTo(object obj)
        {
            Part part = obj as Part;
            return OrderNumber.CompareTo(part.OrderNumber);
        }
        #endregion

        #region Object Methods Override
        public override string ToString()
        {
            return _name;
        }
        #endregion
    }
}