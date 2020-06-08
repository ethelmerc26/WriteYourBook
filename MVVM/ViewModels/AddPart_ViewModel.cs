using Book.MVVM.Models;
using Book.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Book.MVVM.ViewModels
{
    public class AddPart_ViewModel : Notifier
    {
        public AddPart_ViewModel()
        {
            Chapters = Chapter.LoadAllChapters();
            PropertyChanged += ChangeFileStatus;
            FileSelected = false;
            FileStatus = "Файл не прикреплен";
            Btn_AddFile_Content = "Прикрепить файл";
        }

        #region Fields
        private string _name;
        private string _quote;
        private string _authorOfTheQuote;
        private string _description;
        private Chapter _selectedChapter;
        private string _fileName;
        private bool _fileSelected;
        private string _fileStatus;
        private string _btn_AddFile_Content;
        private RelayCommand _completeCmd;
        private RelayCommand _cancelCmd;
        private RelayCommand _choseFilePathCmd;
        #endregion

        #region Properties
        public List<Chapter> Chapters { get; }
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
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
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
        public Chapter SelectedChapter
        {
            get { return _selectedChapter; }
            set { SetProperty(ref _selectedChapter, value); }
        }
        public string FileName
        {
            get { return _fileName; }
            set { SetProperty(ref _fileName, value); }
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
        #endregion

        #region Command Methods

        #region Complete
        private void Complete(object parameter)
        {
            try
            {
                Part.CreateNewPart(chapter: _selectedChapter, name: _name, quote: _quote, authorOfTheQuote: _authorOfTheQuote, description: _description, FileName: _fileName);
                Navigation.NavigateToPage(Navigation.Pages.Chapters_View);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось добавить часть: {ex.Message}");
            }
        }

        private bool Complete_CanExecute(object parameter)
        {
            if (!String.IsNullOrWhiteSpace(_name) && _selectedChapter != null)
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
                MessageBox.Show($"Не удалось отменить добавление части: {ex.Message}");
            }
        }

        public bool Cancel_CanExecute(object parameter)
        {
            return true;
        }
        #endregion

        #region ChoseFilePath
        private void ChoseFilePath(object parameter)
        {
            try
            {
                FileName = Part.GetPath();
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

        #endregion

        #region Methods
        private void ChangeFileStatus(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "FileName")
            {
                if (String.IsNullOrWhiteSpace(FileName))
                {
                    FileSelected = false;
                    FileStatus = "Файл не прикреплен";
                    Btn_AddFile_Content = "Прикрепить файл";
                }
                else
                {
                    FileSelected = true;
                    FileStatus = "Файл прикреплен";
                    Btn_AddFile_Content = "Прикрепить другой файл";
                    Navigation.ShowAlarm(FileStatus);
                }
            }
        }
        #endregion
    }
}