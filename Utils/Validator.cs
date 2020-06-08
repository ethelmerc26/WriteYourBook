using Book.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Book.Utils
{
    public class Validator
    {
        public static void ValidateName(string name)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(name))
                    throw new ApplicationException("Название части не может быть пустой строкой");

                List<string> usedChapterNames = (from c in Chapter.LoadAllChapters()
                                                 select c.Name).ToList();

                List<string> usedPartNames = (from c in Part.LoadAllParts()
                                              select c.Name).ToList();

                if (usedChapterNames.Contains(name))
                    throw new ApplicationException($"Название \"{name}\" уже используется одной из глав");

                if (usedPartNames.Contains(name))
                    throw new ApplicationException($"Название \"{name}\" уже используется другой частью");

                if (Regex.IsMatch(name, @"\d"))
                    throw new ApplicationException("Название части не должно содержать цифр");
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Непредвиденная ошибка: {ex.Message}");
            }
        }
    }
}
