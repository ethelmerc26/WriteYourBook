using Book.MVVM;
using Book.MVVM.Views;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Book.Utils
{
    public static class Navigation
    {
        public static MainWindow MainWindow;

        public enum Pages
        {
            Chapters_View,
            AddChapter_View,
            AddPart_View,
            ChapterRedact_View,
            PartRedact_View
        }

        private static readonly Dictionary<Pages, Type> pages = new Dictionary<Pages, Type>()
        {
            { Pages.Chapters_View, typeof(Chapters_View) },
            { Pages.AddChapter_View, typeof(AddChapter_View) },
            { Pages.AddPart_View, typeof(AddPart_View) },
            { Pages.ChapterRedact_View, typeof(ChapterRedact_View) },
            { Pages.PartRedact_View, typeof(PartRedact_View) }
        };

        public static void NavigateToPage(Pages page)
        {
            MainWindow.Content = Activator.CreateInstance(pages[page]) as Page;
        }

        public static void NavigateToPage(Pages page, object parameter)
        {
            MainWindow.Content = Activator.CreateInstance(pages[page], parameter) as Page;
        }

        public static void ShowAlarm(string message)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate {
                _ = new Alarm_View(MainWindow, message);
            });
        }
    }
}
