   M S B A M L   `   `   `  ���� ?  ;Book, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Book.MVVM.ViewModelsBook.MVVM.ViewModels  "    Book.MVVM.Views.AddPart_View   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1vm"clr-namespace:Book.MVVM.ViewModels       N� Title$  AddPart_View�� � DataContext ,   &Book.MVVM.ViewModels.AddPart_ViewModel   � 	Resources  �� 
TargetType   ButtonStyle �� Property �� Value  TextBoxStyle  	MainStyle��%�  &	        &	 �     &	 #    �� " ��.���  � ��$ 5q���  � ��$ 0.7cm����  �@�Cursor$
 Hand����  � �� �	#FFF8F8F9��  � ��$ Comic Sans MS)���  � ��$ 14(���  � y�$ Visible=��� " ��.���  � ��$ 5q���  � ��$ Comic Sans MS)���  � ��$ 14(��� " ��.���  � ��$ 5q���  � ��$ Comic Sans MS)���  � ��$ 14(���  � ��$ Center=�.���� $��20q�$��14 cm��$��Center=�$��Center=�._�� $��Center=�$	��Top=� � ColumnDefinitions �� $��14 cm � � RowDefinitions �� $��1.7 cm ��� �� �� $��1.7 cm ��� $
��1 cm ��� $
��2 cm �.+��� $��2q�$��0�� �� Orientation$ Vertical=�$��Center=�.+��� $��Center=�#��[ Название�� $
��4 cm������ *Name+	 �� UpdateSourceTrigger$	 PropertyChanged=��� $��2q�$��1��$ Vertical=�$��Center=�.+��� $��2q�$ Vertical=�$��Center=�.+��� $��Center=�#��[ Цитата�� 
 �� TextWrapping$

 Wrap=�$
��2 cm��$��10 cm��#��[ ���� *Quote+$	 PropertyChanged=��� $��2q�$ 
Horizontal=�$��Center=�.+��� $��Center=�#��[ Автор цитаты�� $��0.7 cm��$
��6 cm��#��[ ���� *AuthorOfTheQuote+$	 PropertyChanged=��� $��2q�$��2��$ Vertical=�$��Center=�.+��� $��Center=�#��[ Описание�� $

 Wrap=�$
��5 cm��$��13 cm��#��[ ���� *Description+$	 PropertyChanged=��� $��2q�$��3��$ Vertical=�$��Center=�.+��� $��Center=�#��[ 
Глава�� $	��4cm��$��0.7cm�� �� SelectedItem �� *SelectedChapter+���� *
Chapters+���� .R�]� .+��� $��Candara)�$��Normal"�$��0,0,5,0q�$��12(�$y�Visible=�����  �� Path$ OrderNumber��� $��14(�$��Comic Sans MS)�$y�Visible=����� $
 Name��� $��2q�$��4��$ 
Horizontal=�$��Center=�.+��� #��[ ���� *
FileStatus+$	 PropertyChanged=��� #��[  ���� *Btn_AddFile_Content+$	 PropertyChanged=����� *ChoseFilePathCmd+�� $��2q�$��5��$ 
Horizontal=�$��Center=�.+��� #��[  ���� *CompleteCmd+.��Добавить�� #��[  ���� *	CancelCmd+.��Отмена