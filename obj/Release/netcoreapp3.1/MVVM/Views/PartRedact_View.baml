   M S B A M L   `   `   `  ����   Book%    Book.MVVM.Views.PartRedact_View   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       N� Title$  PartRedact_View�� � 	Resources  �� 
TargetType   ButtonStyle �� Property �� Value  TextBoxStyle  	MainStyle��%�  &	        &	 �     &	     �� " ��.���  � ��$ 5q���  � ��$ 0.7cm����  � �� �	#FFF8F8F9��  � ��$ Comic Sans MS)���  � ��$ 14(���  � y�$ Visible=��� " ��.���  � ��$ 5q���  � ��$ Comic Sans MS)���  � ��$ 14(��� " ��.���  � ��$ 5q���  � ��$ Comic Sans MS)���  � ��$ 14(���  � ��$ Center=�.���� $��20q�$��16 cm��$��Center=�$��Center=�._�� $��Center=�$	��Top=� � ColumnDefinitions �� $��16 cm � � RowDefinitions �� $��1.7 cm ��� �� �� $��1.7 cm ��� $
��1 cm ��� $
��2 cm �.+��� $��2q�$��0�� �� Orientation$ Vertical=�$��Center=�.+��� $��Center=�#��[ Название�� $
��4 cm������ *	Part.Name+ �� UpdateSourceTrigger$ PropertyChanged=��� $��2q�$��1��$ Vertical=�$��Center=�.+��� $��2q�$ Vertical=�$��Center=�.+��� $��Center=�#��[ Цитата�� 	 �� TextWrapping$
	 Wrap=�$
��2 cm��$��10 cm������ *
Part.Quote+$ PropertyChanged=��� $��2q�$ 
Horizontal=�$��Center=�.+��� $��Center=�#��[ Автор цитаты�� $��0.7 cm��$
��6 cm������ *Part.AuthorOfTheQuote+$ PropertyChanged=��� $��2q�$��2��$ Vertical=�$��Center=�.+��� $��Center=�#��[ Описание�� $
	 Wrap=�$
��4 cm��$��13 cm������ *Part.Description+$ PropertyChanged=��� $��2q�$��3��$ Vertical=�$��Center=�.+��� $ 
Horizontal=�$��Center=�.+��� $��Center=�#��[ Глава:�� #��[ ���� *CurrentChapter.OrderNumber+�� #��[ ���� *CurrentChapter.Name+�� $ 
Horizontal=�$��Center=�.+��� $��Center=�#��[ (&Выбрать другую главу�� $	��4cm��$��0.7cm��
 �� SelectedItem
 �� *SelectedChapter+���� *
Chapters+���� .R�]� .+��� $��Candara)�$��Normal"�$��0,0,5,0q�$��12(�$y�Visible=�����  �� Path$ OrderNumber��� $��14(�$��Comic Sans MS)�$y�Visible=����� $
 Name��� $��2q�$��4��$ 
Horizontal=�$��Center=�.+��� #��[ ���� *
FileStatus+$ PropertyChanged=��� #��[  ���� *Btn_AddFile_Content+$ PropertyChanged=����� *ChoseFilePathCmd+�� $!��Открепить файл��#��[  ���� *DeleteFileCmd+y��� *DeleteFileButtonVisibility+�� $��2q�$��5��$ 
Horizontal=�$��Center=�.+��� #��[  ���� *CompleteCmd+.��Готово�� #��[  ���� *	CancelCmd+.��Отмена