using Book.MVVM.Models;
using Book.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Book.MVVM.ViewModels
{
	public class Chapters_ViewModel : Notifier
	{
		public Chapters_ViewModel()
		{
			Chapters = Chapter.LoadAllChapters();
			PropertyChanged += UpdateChapterList;
			PropertyChanged += BuildQuote;
			PropertyChanged += ChangeFileBtn;
			PropertyChanged += QuoteViewChange;
			PropertyChanged += DescriptionVisibilityChange;
			DescriptionVisibility = Visibility.Collapsed;
			QuoteViewVisibility = Visibility.Collapsed;
			FileBtnVisibility = Visibility.Collapsed;
		}

		#region Fields
		private object _selectedItem;
		private string _quoteView;
		private string _authorOfTheQuoteView;
		private Visibility _quoteViewVisibility;
		private Visibility _fileBtnVisibility;
		private Visibility _descriptionVisibility;
		private string _fileBtnContent;
		private RelayCommand _changePosUpCmd;
		private RelayCommand _changePosDownCmd;
		private RelayCommand _addChapterCmd;
		private RelayCommand _addPartCmd;
		private RelayCommand _redactCmd;
		private RelayCommand _fileOpenOrAddCmd;
		private RelayCommand _deleteCmd;
		#endregion

		#region Properties
		public List<Chapter> Chapters { get; set; }
		public object SelectedItem
		{
			get { return _selectedItem; }
			set { SetProperty(ref _selectedItem, value); }
		}
		public string QuoteView
		{
			get { return _quoteView; }
			set { SetProperty(ref _quoteView, value); }
		}
		public string AuthorOfTheQuoteView
		{
			get { return _authorOfTheQuoteView; }
			set { SetProperty(ref _authorOfTheQuoteView, value); }
		}
		public Visibility QuoteViewVisibility
		{
			get { return _quoteViewVisibility; }
			set { SetProperty(ref _quoteViewVisibility, value); }
		}
		public Visibility FileBtnVisibility
		{
			get { return _fileBtnVisibility; }
			set { SetProperty(ref _fileBtnVisibility, value); }
		}
		public Visibility DescriptionVisibility
		{
			get { return _descriptionVisibility; }
			set { SetProperty(ref _descriptionVisibility, value); }
		}
		public string FileBtnContent
		{
			get { return _fileBtnContent; }
			set { SetProperty(ref _fileBtnContent, value); }
		}
		public RelayCommand ChangePosUpCmd
		{
			get { return _changePosUpCmd ?? (_changePosUpCmd = new RelayCommand(ChangePosUp, ChangePosUp_CanExecute)); }
		}
		public RelayCommand ChangePosDownCmd
		{
			get { return _changePosDownCmd ?? (_changePosDownCmd = new RelayCommand(ChangePosDown, ChangePosDown_CanExecute)); }
		}
		public RelayCommand AddChapterCmd
		{
			get { return _addChapterCmd ?? (_addChapterCmd = new RelayCommand(AddChapter, AddChapter_CanExecute)); }
		}
		public RelayCommand AddPartCmd
		{
			get { return _addPartCmd ?? (_addPartCmd = new RelayCommand(AddPart, AddPart_CanExecute)); }
		}
		public RelayCommand RedactCmd
		{
			get { return _redactCmd ?? (_redactCmd = new RelayCommand(Redact, Redact_CanExecute)); }
		}
		public RelayCommand FileOpenOrAddCmd
		{
			get { return _fileOpenOrAddCmd ?? (_fileOpenOrAddCmd = new RelayCommand(FileOpenOrAdd, FileOpenOrAdd_CanExecute)); }
		}
		public RelayCommand DeleteCmd
		{
			get { return _deleteCmd ?? (_deleteCmd = new RelayCommand(Delete, Delete_CanExecute)); }
		}
		#endregion

		#region Command Methods
		#region ChangePosUp
		private void ChangePosUp(object parameter)
		{
			if (_selectedItem is Chapter)
			{
				Chapter selectedChapter = _selectedItem as Chapter;

				int indexOfSwappingItem = Chapters.IndexOf(selectedChapter) - 1;
				Chapter swapingChapter = Chapters[indexOfSwappingItem] as Chapter;

				int temp = selectedChapter.OrderNumber;
				selectedChapter.OrderNumber = swapingChapter.OrderNumber;
				swapingChapter.OrderNumber = temp;

				Chapter.SaveChangesInXML(selectedChapter);
				Chapter.SaveChangesInXML(swapingChapter);
				OnPropertyChanged("Chapters");
			}
			else if (_selectedItem is Part)
			{
				Part selectedPart = _selectedItem as Part;

				List<Part> parts = Part.LoadPartListForChapter(selectedPart.ChapterID);

				Part swapingPart = (from p in parts
									where p.OrderNumber == selectedPart.OrderNumber - 1
									select p).First();

				int temp = selectedPart.OrderNumber;
				selectedPart.OrderNumber = swapingPart.OrderNumber;
				swapingPart.OrderNumber = temp;

				Part.SaveChangesInXML(selectedPart);
				Part.SaveChangesInXML(swapingPart);
				OnPropertyChanged("Chapters");
			}
		}

		public bool ChangePosUp_CanExecute(object parameter)
		{
			try
			{
				if (_selectedItem == null)
				{
					return false;
				}
				if (_selectedItem is Chapter)
				{
					Chapter selectedChapter = _selectedItem as Chapter;
					if (selectedChapter.OrderNumber == 1)
						return false;
					return true;
				}
				if (_selectedItem is Part)
				{
					Part selectedPart = _selectedItem as Part;
					if (selectedPart.OrderNumber == 1)
						return false;
					return true;
				}
				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Критическая ошибка: {ex.Message}");
				return false;
			}
		}
		#endregion

		#region ChangePosDown
		private void ChangePosDown(object parameter)
		{
			if (_selectedItem is Chapter)
			{
				Chapter selectedChapter = _selectedItem as Chapter;

				int indexOfSwappingItem = Chapters.IndexOf(selectedChapter) + 1;
				Chapter swapingChapter = Chapters[indexOfSwappingItem] as Chapter;

				int temp = selectedChapter.OrderNumber;
				selectedChapter.OrderNumber = swapingChapter.OrderNumber;
				swapingChapter.OrderNumber = temp;

				Chapter.SaveChangesInXML(selectedChapter);
				Chapter.SaveChangesInXML(swapingChapter);
				OnPropertyChanged("Chapters");
			}
			else if (_selectedItem is Part)
			{
				Part selectedPart = _selectedItem as Part;

				List<Part> parts = Part.LoadPartListForChapter(selectedPart.ChapterID);

				Part swapingPart = (from p in parts
									where p.OrderNumber == selectedPart.OrderNumber + 1
									select p).First();

				int temp = selectedPart.OrderNumber;
				selectedPart.OrderNumber = swapingPart.OrderNumber;
				swapingPart.OrderNumber = temp;

				Part.SaveChangesInXML(selectedPart);
				Part.SaveChangesInXML(swapingPart);
				OnPropertyChanged("Chapters");
			}
		}

		public bool ChangePosDown_CanExecute(object parameter)
		{
			try
			{
				if (_selectedItem == null)
				{
					return false;
				}
				if (_selectedItem is Chapter)
				{
					Chapter selectedChapter = _selectedItem as Chapter;
					if (Chapters.IndexOf(selectedChapter) == Chapters.Count - 1)
						return false;
					return true;
				}
				if (_selectedItem is Part)
				{
					Part selectedPart = _selectedItem as Part;
					List<Part> parts = Part.LoadPartListForChapter(selectedPart.ChapterID);
					if (selectedPart.OrderNumber == parts.Count)
						return false;
					return true;
				}
				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Критическая ошибка: {ex.Message}");
				return false;
			}
		}
		#endregion

		#region AddChapter
		private void AddChapter(object parameter)
		{
			try
			{
				Navigation.NavigateToPage(Navigation.Pages.AddChapter_View);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Не удалось открыть страницу добавления главы: {ex.Message}");
			}
		}

		private bool AddChapter_CanExecute(object parameter)
		{
			return true;
		}
		#endregion

		#region AddPart
		private void AddPart(object parameter)
		{
			try
			{
				Navigation.NavigateToPage(Navigation.Pages.AddPart_View);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Не удалось открыть страницу добавления части: {ex.Message}");
			}
		}

		private bool AddPart_CanExecute(object parameter)
		{
			if (Chapters.Count == 0) return false;
			return true;
		}
		#endregion

		#region Redact
		private void Redact(object parameter)
		{
			try
			{
				if (_selectedItem is Chapter)
				{
					Navigation.NavigateToPage(Navigation.Pages.ChapterRedact_View, _selectedItem);
				}
				else if (_selectedItem is Part)
				{
					Navigation.NavigateToPage(Navigation.Pages.PartRedact_View, _selectedItem);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Не удалось открыть страницу редактирования: {ex.Message}");
			}
		}

		private bool Redact_CanExecute(object parameter)
		{
			if (_selectedItem == null) return false;
			return true;
		}
		#endregion

		#region FileOpenOrAdd
		private void FileOpenOrAdd(object parameter)
		{
			try
			{
				Part part = _selectedItem as Part;
				if (String.IsNullOrWhiteSpace(part.FileName))
				{
					part.FileName = Part.GetPath();
					Part.SaveChangesInXML(part);
					Navigation.ShowAlarm("Файл прикреплен");
					OnPropertyChanged("Chapters");
				}
				else
					Part.OpenFile(part);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private bool FileOpenOrAdd_CanExecute(object parameter)
		{
			if (_selectedItem is Part) return true;
			return false;
		}
		#endregion

		#region Delete
		public void Delete(object parameter)
		{
			try
			{
				StringBuilder message = new StringBuilder(100);
				message.Append("Вы уверены, что хотите удалить ");
				message.Append(_selectedItem is Chapter ? "главу: " : "часть: ");
				message.Append(_selectedItem is Chapter ? (_selectedItem as Chapter).Name : (_selectedItem as Part).Name);
				message.Append("?");
				MessageBoxResult result = MessageBox.Show(message.ToString(), "Подтвердите действие", MessageBoxButton.YesNo);
				if (result == MessageBoxResult.Yes)
				{
					if (_selectedItem is Chapter)
					{
						Chapter.Remove(_selectedItem as Chapter);
						OnPropertyChanged("Chapters");
						SelectedItem = null;
					}
					else if (_selectedItem is Part)
					{
						Part.Remove(_selectedItem as Part);
						OnPropertyChanged("Chapters");
						SelectedItem = null;
					}
					else
					{
						MessageBox.Show("Элемент не выбран");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Не удалось удалить элемент: {ex.Message}");
			}
		}

		public bool Delete_CanExecute(object parameter)
		{
			if (_selectedItem != null && (_selectedItem is Chapter || _selectedItem is Part))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		#endregion
		#endregion

		#region Methods
		private void DescriptionVisibilityChange(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "SelectedItem")
			{
				if (SelectedItem is Part)
				{
					Part part = SelectedItem as Part;
					if (!String.IsNullOrWhiteSpace(part.Description))
					{
						DescriptionVisibility = Visibility.Visible;
					}
					else
					{
						DescriptionVisibility = Visibility.Collapsed;
					}
				}
				else if (SelectedItem is Chapter)
				{
					Chapter chapter = SelectedItem as Chapter;
					if (!String.IsNullOrWhiteSpace(chapter.Description))
					{
						DescriptionVisibility = Visibility.Visible;
					}
					else
					{
						DescriptionVisibility = Visibility.Collapsed;
					}
				}
			}
		}

		private void QuoteViewChange(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "SelectedItem")
			{
				if (SelectedItem is Part)
				{
					Part part = SelectedItem as Part;
					if (!String.IsNullOrWhiteSpace(part.Quote) && !String.IsNullOrWhiteSpace(part.AuthorOfTheQuote))
					{
						QuoteViewVisibility = Visibility.Visible;
						return;
					}
				}
				QuoteViewVisibility = Visibility.Collapsed;
			}
		}

		private void ChangeFileBtn(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "SelectedItem")
			{
				if (SelectedItem is Part)
				{
					FileBtnVisibility = Visibility.Visible;
					Part part = SelectedItem as Part;
					if (String.IsNullOrWhiteSpace(part.FileName))
					{
						FileBtnContent = "Прикрепить файл";
					}
					else
					{
						FileBtnContent = "Открыть файл";
					}
				}
				else
				{
					FileBtnVisibility = Visibility.Collapsed;
				}
			}
		}

		private void BuildQuote(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "SelectedItem" && SelectedItem is Part)
			{
				Part part = SelectedItem as Part;
				if (!String.IsNullOrWhiteSpace(part.Quote) && !String.IsNullOrWhiteSpace(part.AuthorOfTheQuote))
				{
					StringBuilder quote = new StringBuilder(1000);
					quote.Append("\t");
					quote.Append("\"");
					quote.Append(part.Quote);
					quote.AppendLine("\"");
					QuoteView = quote.ToString();
					StringBuilder author = new StringBuilder(100);
					author.Append(part.AuthorOfTheQuote);
					author.Append(" \u00A9");
					AuthorOfTheQuoteView = author.ToString();
				}
			}
			else if (e.PropertyName == "SelectedItem" && SelectedItem is Chapter)
			{
				QuoteView = "";
			}
		}

		private void UpdateChapterList(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Chapters")
				Chapters = Chapter.LoadAllChapters();
		}
		#endregion
	}
}
