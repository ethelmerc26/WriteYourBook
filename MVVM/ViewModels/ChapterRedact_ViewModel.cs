using Book.MVVM.Models;
using Book.Utils;
using System;
using System.Windows;

namespace Book.MVVM.ViewModels
{
    public class ChapterRedact_ViewModel : Notifier
    {
        public ChapterRedact_ViewModel(object parameter)
        {
            if (!(parameter is Chapter))
                return;
            Chapter = parameter as Chapter;
        }

        #region Fields
        private RelayCommand _completeCmd;
        private RelayCommand _cancelCmd;
        #endregion

        #region Properties
        public Chapter Chapter { get; }
        public RelayCommand CompleteCmd
        {
            get { return _completeCmd ?? (_completeCmd = new RelayCommand(Complete, Complete_CanExecute)); }
        }
        public RelayCommand CancelCmd
        {
            get { return _cancelCmd ?? (_cancelCmd = new RelayCommand(Cancel, Cancel_CanExecute)); }
        }
        #endregion

        #region Command Methods

        #region Complete
        private void Complete(object parameter)
        {
            try
            {
                Chapter.SaveChangesInXML(Chapter);
                Navigation.NavigateToPage(Navigation.Pages.Chapters_View);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось завершить изменения: {ex.Message}");
            }
        }

        public bool Complete_CanExecute(object parameter)
        {
            if (String.IsNullOrWhiteSpace(Chapter.Name))
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Cancel
        private void Cancel(object parameter)
        {
            try
            {
                if (!Chapter.ChaptersAreIdentical(Chapter, Chapter.FindChapter(Chapter.ID)))
                {
                    MessageBoxResult result = MessageBox.Show("Отменить изменения?", "Подтвердите действие", MessageBoxButton.YesNo);
                    if (result != MessageBoxResult.Yes)
                        return;
                }
                Navigation.NavigateToPage(Navigation.Pages.Chapters_View);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось отменить изменения: {ex.Message}");
            }
        }

        public bool Cancel_CanExecute(object parameter)
        {
            return true;
        }
        #endregion

        #endregion
    }
}