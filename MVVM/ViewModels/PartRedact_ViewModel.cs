using Book.MVVM.Models;
using Book.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Book.MVVM.ViewModels
{
    public class PartRedact_ViewModel : Notifier
    {
        public PartRedact_ViewModel(object parameter)
        {
            if (!(parameter is Part)) return;
            Part selectedPart = parameter as Part;
            selectedPart.PropertyChanged += ChangeFileHandler;
            Chapters = Chapter.LoadAllChapters();
            CurrentChapter = Chapter.FindChapter(selectedPart.ChapterID);
            Part = selectedPart;
            ChangeStatus(Part.FileName);
        }

        #region Fields
        private bool _fileSelected;
        private string _fileStatus;
        private string _btn_AddFile_Content;
        private Visibility _deleteFileButtonVisibility;
        private Chapter _selectedChapter;
        private Chapter _currentChapter;
        private RelayCommand _completeCmd;
        private RelayCommand _cancelCmd;
        private RelayCommand _choseFilePathCmd;
        private RelayCommand _deleteFileCmd;
        #endregion       

        #region Properties
        public List<Chapter> Chapters { get; }
        public Part Part { get; }
        public Chapter CurrentChapter
        {
            get { return _currentChapter; }
            set { SetProperty(ref _currentChapter, value); }
        }
        public bool FileSelected
        {
            get { return _fileSelected; }
            set { SetProperty(ref _fileSelected, value); }
        }
        public string FileStatus
        {
            get { return _fileStatus; }
            set { SetProperty(ref _fileStatus, value); }
        }
        public string Btn_AddFile_Content
        {
            get { return _btn_AddFile_Content; }
            set { SetProperty(ref _btn_AddFile_Content, value); }
        }
        public Visibility DeleteFileButtonVisibility
        {
            get { return _deleteFileButtonVisibility; }
            set { SetProperty(ref _deleteFileButtonVisibility, value); }
        }
        public Chapter SelectedChapter
        {
            get { return _selectedChapter; }
            set
            {
                SetProperty(ref _selectedChapter, value);
                CurrentChapter = _selectedChapter;
            }
        }
        public RelayCommand CompleteCmd
        {
            get { return _completeCmd ?? (_completeCmd = new RelayCommand(Complete, Complete_CanExecute)); }
        }
        public RelayCommand CancelCmd
        {
            get { return _cancelCmd ?? (_cancelCmd = new RelayCommand(Cancel, Cancel_CanExecute)); }
        }
        public RelayCommand ChoseFilePathCmd
        {
            get { return _choseFilePathCmd ?? (_choseFilePathCmd = new RelayCommand(ChoseFilePath, ChoseFilePath_CanExecute)); }
        }
        public RelayCommand DeleteFileCmd
        {
            get { return _deleteFileCmd ?? (_deleteFileCmd = new RelayCommand(DeleteFile, DeleteFile_CanExecute)); }
        }
        #endregion

        #region Command Methods

        #region Complete
        public void Complete(object parameter)
        {
            try
            {
                Part.SaveChangesInXML(Part);
                Navigation.NavigateToPage(Navigation.Pages.Chapters_View);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось завершить изменения: {ex.Message}");
            }
        }

        public bool Complete_CanExecute(object parameter)
        {
            if (String.IsNullOrWhiteSpace(Part.Name) || _currentChapter == null)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Cancel
        public void Cancel(object parameter)
        {
            try
            {
                if (!Part.PartsAreIdentical(Part, Part.FindPart(Part.ID)))
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

        #region ChoseFilePath
        public void ChoseFilePath(object parameter)
        {
            try
            {
                string fileName = Part.GetPath();
                if (String.IsNullOrWhiteSpace(fileName) || fileName.Equals(Part.FileName)) return;
                Part.FileName = fileName;
                //_ = new AlarmWindow(_partRedaction_ViewModel.PartRedaction_Page, "Файл прикреплен");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось выбрать файл: {ex.Message}");
            }
        }

        public bool ChoseFilePath_CanExecute(object parameter)
        {
            return true;
        }
        #endregion

        #region DeleteFile
        public void DeleteFile(object parameter)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Открепить файл?", "Подтвердите действие", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    Part.FileName = "";
                    //_ = new AlarmWindow(_partRedaction_ViewModel.PartRedaction_Page, "Файл откреплен");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось выбрать файл: {ex.Message}");
            }
        }

        public bool DeleteFile_CanExecute(object parameter)
        {
            if (String.IsNullOrWhiteSpace(Part.FileName))
                return false;
            return true;
        }
        #endregion

        #endregion

        #region Methods
        private void ChangeStatus(string fileName)
        {
            if (String.IsNullOrWhiteSpace(fileName))
            {
                FileSelected = false;
                FileStatus = "Файл не прикреплен";
                Btn_AddFile_Content = "Прикрепить файл";
                DeleteFileButtonVisibility = Visibility.Collapsed;
            }
            else
            {
                FileSelected = true;
                FileStatus = "Файл прикреплен";
                Btn_AddFile_Content = "Прикрепить другой файл";
                DeleteFileButtonVisibility = Visibility.Visible;
            }
        }

        private void ChangeFileHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "FileName" && sender is Part)
            {
                Part part = sender as Part;
                ChangeStatus(part.FileName);
            }
        }
        #endregion
    }
}