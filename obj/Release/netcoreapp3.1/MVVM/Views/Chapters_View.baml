   M S B A M L   `   `   `  ���� ?  ;Book, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Book.MVVM.ViewModelsBook.MVVM.ViewModels  +clr-namespace:Book.HelpersBook.Helpers  #    Book.MVVM.Views.Chapters_View   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1vm"clr-namespace:Book.MVVM.ViewModels     .helpersclr-namespace:Book.Helpers       N� Title$  
MainWindow��$
��25cm��$
��40cm�� � DataContext -   'Book.MVVM.ViewModels.Chapters_ViewModel   � 	Resources  �� 
TargetType   TextBlockStyle �� Property �� Value  ButtonStyle��%K  &	        &	 Z     �� " ��.���  � ��$ 10q���  � ��$ Comic Sans MS)���  � ��$ 14(��� " ��.���  � ��$ 5q���  � ��$ 0.7cm����  � ��$
 Auto����  � ��$ Center=���  � �� �	#FFF8F8F9��  � ��$ Comic Sans MS)���  � ��$ 14(���  � y�$ Visible=�.��� $��20q�$��24 cm��$��Center=�$	��Top=� � ColumnDefinitions �� $��24 cm � � RowDefinitions �� $
��2 cm ��� .+�� $��0��$��24 cm�� �� $
��4 cm ��� .+��� $��1��$
��Left=� �� Orientation$ 
Horizontal=�.+��� $!��Добавить главу��#��[ ���� *AddChapterCmd+�� $!��Добавить часть��#��[ ���� *
AddPartCmd+�� $ ��Редактировать��#��[ ���� *	RedactCmd+�� $��Удалить��#��[ ���� *	DeleteCmd+� $��1��$��Stretch=�$
��Auto��$��24 cm�� �� $
��1 cm ��� $��13 cm ��� $��82.572 ��� $��333.176 �.+��� $ Vertical=�$��2��$��	0,0,491,0q�.+��� $
��1 cm��$
��1 cm������ *ChangePosUpCmd+�� $
��1 cm��$
��1 cm������ *ChangePosDownCmd+#   Book.Helpers.ExtendedTreeView  -   	  Name$	 ChapterList��$��0, 0, 0, 1cmq�$��1��
   SelectedItem_
 �� *SelectedItem+ �� Mode$ TwoWay=����� *
Chapters+ �� UpdateSourceTrigger$ PropertyChanged=�����  �� ItemsSource ��  �� Path$ Parts�.R�]� .+��� $��Candara)�$��Normal"�$��0,0,5,0q�$��14(�$y�Visible=����� *OrderNumber+$ PropertyChanged=��� $��Comic Sans MS)�$y�Visible=�$��14(����� *Name+$ PropertyChanged=� �� ItemTemplate �� .R�]� .+��� $��Candara)�$��Normal"�$��0,0,5,0q�$��14(�$y�Visible=����� *OrderNumber+$ PropertyChanged=��� $��Comic Sans MS)�$y�Visible=�$��14(����� *Name+$ PropertyChanged=��� $��1��$��3��$��	491,0,0,0q�.+���  �� TextAlignment$ Center=�$
��Wrap=�$
��9 cm��#��[  $��Center=����� *SelectedItem.Name+�� $��0q�$
 Left=�$
��9 cm��$
��Wrap=�#��[  $��Center=�y��� *QuoteViewVisibility+���� *	QuoteView+$ PropertyChanged=��� $��0q�$ Right=�$
��9 cm��#��[  $��Center=�y��� *QuoteViewVisibility+���� *AuthorOfTheQuoteView+$ PropertyChanged=��� $��10q�$
��9 cm��._��� #��[  $
��Wrap=�$��Center=�y��� *DescriptionVisibility+���� *SelectedItem.Description+�� #��[ ���� *FileOpenOrAddCmd+���� *FileBtnContent+y��� *FileBtnVisibility+