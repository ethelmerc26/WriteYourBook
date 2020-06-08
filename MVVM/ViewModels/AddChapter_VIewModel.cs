using Book.MVVM.Models;
using Book.Utils;
using System;
using System.Windows;

namespace Book.MVVM.ViewModels
{
    public class AddChapter_ViewModel : Notifier
    {
        private readonly int chaptersCount;
        public AddChapter_ViewModel()
        {
            chaptersCount = Chapter.LoadAllChapters().Count;
        }

        #region Fields
        private string _name;
        private string _description;
        private RelayCommand _completeCmd;
        private RelayCommand _cancelCmd;
        #endregion

        #region Properties
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
                Chapter.CreateNewChapter(chaptersCount + 1, _name, _description);
                Navigation.NavigateToPage(Navigation.Pages.Chapters_View);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось добавить главу: {ex.Message}");
            }
        }

        public bool Complete_CanExecute(object parameter)
        {
            if (!String.IsNullOrWhiteSpace(_name))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Cancel
        private void Cancel(object parameter)
        {
            try
            {
                Navigation.NavigateToPage(Navigation.Pages.Chapters_View);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось отменить добавление главы: {ex.Message}");
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
