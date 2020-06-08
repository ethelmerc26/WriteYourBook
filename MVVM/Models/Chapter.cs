using Book.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Book.MVVM.Models
{
    public class Chapter : Notifier, IComparable
    {
        public Chapter(int id, string name, int orderNumber, string description = "", List<Part> parts = null, bool done = false)
        {
            ID = id;
            _name = name;
            Parts = parts;
            _description = description;
            _ordernumber = orderNumber;
            _done = done;
        }

        #region Fields
        private string _name;
        private string _description;
        private int _ordernumber;
        private bool _done;
        #endregion

        #region Properties
        public int ID { get; }
        public List<Part> Parts { get; }
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
            get { return _ordernumber; }
            set { SetProperty(ref _ordernumber, value); }
        }
        public bool Done
        {
            get { return _done; }
            set { SetProperty(ref _done, value); }
        }
        #endregion

        #region Public Methods
        public static List<Chapter> LoadAllChapters(string fileName = Constants.ChaptersFileName)
        {
            try
            {
                if (!File.Exists(fileName))
                    throw new ApplicationException("Критическая ошибка, файл не найден");
                XDocument chapterList = XDocument.Load(fileName);
                List<Chapter> chapters = new List<Chapter>();
                foreach (XElement chapter in chapterList.Root.Elements("Chapter"))
                {
                    int id = Int32.Parse(chapter.Element("ID").Value);
                    string name = chapter.Element("Name").Value;
                    string description = chapter.Element("Description").Value;
                    int orderNumber = Int32.Parse(chapter.Element("OrderNumber").Value);
                    List<Part> parts = Part.LoadPartListForChapter(id);
                    bool done = Boolean.Parse(chapter.Element("Done").Value);
                    Chapter newChapter = new Chapter(id: id, name: name, orderNumber: orderNumber, description: description, parts: parts, done: done);
                    chapters.Add(newChapter);
                }
                chapters.Sort();
                return chapters;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException($"Ошибка при попытке получить полный список глав: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Непредвиденная ошибка при попытке получить полный список глав: {ex.Message}");
            }
        }

        public static void CreateNewChapter(int orderNumber, string name, string description)
        {
            try
            {
                Validator.ValidateName(name);
                Chapter newChapter = new Chapter(id: IDGenerator.GenerateID(), name: name, orderNumber: orderNumber, description: description);
                AddChapterInXML(newChapter);
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException($"Ошибка при создании новой главы: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Непредвиденная ошибка при создании новой главы: {ex.Message}");
            }
        }

        public static void Remove(Chapter chapter)
        {
            try
            {
                if (chapter == null)
                    throw new ApplicationException("Критическая ошибка, не передана глава для удаления");
                Delete(chapter);
                XDocument chapterList = XDocument.Load(Constants.ChaptersFileName);
                var itemsHigher = from chapt in chapterList.Element("Chapters").Elements("Chapter")
                                  where Int32.Parse(chapt.Element("OrderNumber").Value) > chapter.OrderNumber
                                  select chapt;
                for (int i = 0; i < itemsHigher.Count(); i++)
                    itemsHigher.ElementAt(i).Element("OrderNumber").Value = (Int32.Parse(itemsHigher.ElementAt(i).Element("OrderNumber").Value) - 1).ToString();

                chapterList.Save(Constants.ChaptersFileName);
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException($"Ошибка при попытке удалить главу: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Непредвиденная ошибка при попытке удалить главу: {ex.Message}");
            }
        }

        public static void SaveChangesInXML(Chapter chapter)
        {
            try
            {
                if (chapter == null)
                    throw new ApplicationException("Критическая ошибка, невозможно сохранить изменения, не передана глава для сохранения");
                Delete(chapter);
                AddChapterInXML(chapter);
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException($"Ошибка при схранении изменений главы: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Непредвиденная ошибка при схранении изменений главы: {ex.Message}");
            }
        }

        public static Chapter FindChapter(int ID)
        {
            try
            {
                List<Chapter> allChapters = LoadAllChapters();
                if (allChapters.Count == 0)
                    throw new ApplicationException("Список глав пустой");
                var items = from chapt in allChapters
                            where chapt.ID == ID
                            select chapt;
                if (items.Count() == 0)
                    throw new ApplicationException("Критическая ошибка, не удалось найти главу");
                if (items.Count() > 1)
                    throw new ApplicationException("Критическая ошибка, найдено несколько глав с одним ID. Это не должно было случиться!");
                return items.First();
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException($"Ошибка при попытке найти главу: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Непредвиденная ошибка при попытке найти главу: {ex.Message}");
            }
        }

        public static bool ChaptersAreIdentical(Chapter chapter1, Chapter chapter2)
        {
            try
            {
                if (chapter1 == null || chapter2 == null)
                    throw new ApplicationException("Критическая ошибка. Не удалось сравнить две главы, одна из глав, либо обе, отсутствует");
                if (chapter1.ID != chapter2.ID)
                    throw new ApplicationException("Критическая ошибка, ID не может различаться");
                if (chapter1.Name != chapter2.Name || chapter1.Description != chapter2.Description ||
                    chapter1.OrderNumber != chapter2.OrderNumber || chapter1.Done != chapter2.Done)
                    return false;
                else
                    return true;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException($"Ошибка при установлении идентичности глав: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Непредвиденная ошибка при установлении идентичности глав: {ex.Message}");
            }
        }

        public static List<string> Except(Chapter chapter1, Chapter chapter2)
        {
            try
            {
                if (chapter1 == null || chapter2 == null)
                    throw new ApplicationException("Критическая ошибка. Не удалось сравнить две главы, одна из глав, либо обе, отсутствует");
                if (chapter1.ID != chapter2.ID)
                    throw new ApplicationException("Критическая ошибка, ID глав не должен различаться!");
                List<string> distinct = new List<string>();
                if (chapter1.Name != chapter2.Name) distinct.Add("Name");
                if (chapter1.Description != chapter2.Description) distinct.Add("Description");
                if (chapter1.OrderNumber != chapter2.OrderNumber) distinct.Add("OrderNumber");
                if (chapter1.Done != chapter2.Done) distinct.Add("Done");
                return distinct;
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException($"Ошибка при сравнении глав: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Непредвиденная ошибка при сравнении глав: {ex.Message}");
            }
        }
        #endregion

        #region Private Methods
        private static void Delete(Chapter chapter)
        {
            try
            {
                XDocument chapterList = XDocument.Load(Constants.ChaptersFileName);
                var items = from chapt in chapterList.Root.Elements("Chapter")
                            where Int32.Parse(chapt.Element("ID").Value) == chapter.ID
                            select chapt;
                if (items.Count() == 0)
                    throw new ApplicationException("Критическая ошибка, не найден изменяемый элемент");
                if (items.Count() > 1)
                    throw new ApplicationException("Критическая ошибка, найдено несколько элементов для удаления. Это не должно было случиться!");
                XElement changedChapter = items.First();
                foreach (var chapt in chapterList.Root.Elements("Chapter"))
                {
                    if (chapt.Element("ID").Value == changedChapter.Element("ID").Value)
                    {
                        chapt.Remove();
                        break;
                    }
                }
                chapterList.Save(Constants.ChaptersFileName);
                IDGenerator.DeleteID(Int32.Parse(changedChapter.Element("ID").Value));
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static void AddChapterInXML(Chapter chapter)
        {
            try
            {
                if (chapter == null)
                    throw new ApplicationException("Критическая ошибка, не удалось сохранить главу в файл, глава для сохранения не передана");
                OptimizeData(ref chapter);
                XDocument chapterList = XDocument.Load(Constants.ChaptersFileName);
                XElement ChapterNode = new XElement("Chapter");
                XElement ID = new XElement("ID", chapter.ID);
                XElement Name = new XElement("Name", chapter.Name);
                XElement Description = new XElement("Description", chapter.Description);
                XElement OrderNumber = new XElement("OrderNumber", chapter.OrderNumber);
                XElement Done = new XElement("Done", chapter.Done);
                ChapterNode.Add(ID);
                ChapterNode.Add(Name);
                ChapterNode.Add(Description);
                ChapterNode.Add(OrderNumber);
                ChapterNode.Add(Done);
                chapterList.Root.Add(ChapterNode);
                chapterList.Save(Constants.ChaptersFileName);
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static void OptimizeData(ref Chapter chapter)
        {
            try
            {
                chapter.Name = chapter.Name.Trim().Replace("  ", " ");
                if (!String.IsNullOrWhiteSpace(chapter.Description))
                    chapter.Description = chapter.Description.Trim().Replace("  ", " ");
                if (chapter.Description == null) chapter.Description = "";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region IComporable Implementation
        public int CompareTo(object obj)
        {
            Chapter chapter = obj as Chapter;
            return OrderNumber.CompareTo(chapter.OrderNumber);
        }
        #endregion

        #region Object Methods Override
        public override string ToString() => _name;
        #endregion
    }
}