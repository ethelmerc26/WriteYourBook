   M S B A M L   `   `   `  ����?  ;Book, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Book.MVVM.ViewModelsBook.MVVM.ViewModels  5      "    Book.MVVM.Views.AddPart_View   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1vm"clr-namespace:Book.MVVM.ViewModels     5        N� Title$  AddPart_View��5	       � DataContext 5      ,   &Book.MVVM.ViewModels.AddPart_ViewModel  5   
    � 	Resources 5       �� 
TargetType   ButtonStyle �� Property �� Value  TextBoxStyle  	MainStyle��%�  &	        &	 C    &	 �    �� 5   
   " ��6   .�5      ��  � ��$ 5q�6'   �� 5       � ��$ 0.7cm��6'   �� 5       �@�Cursor$
 Hand��6'   �� 5       � �� �	#FFF8F8F96+   �� 5       � ��$ Comic Sans MS)�6+   �� 5       � ��$ 14(�6)   �� 5       � y�$ Visible=�6+   5      �� 5   
   " ��6   .�5      ��  � ��$ 5q�6'   �� 5       � ��$ Comic Sans MS)�6+   �� 5       � ��$ 14(�6)   5      �� 5   
   " ��6   .�5      ��  � ��$ 5q�6'   �� 5        � ��$ Comic Sans MS)�6+   �� 5!       � ��$ 14(�6)   �� 5"       � ��$ Center=�64   5#      5$      .��5&      �� $��20q�6   $��14 cm��6   $��Center=�6'   $��Center=�6D   ._�5'   
   � $��Center=�6   $	��Top=�6-    � ColumnDefinitions 5(      �� 5)      $��14 cm �6#    � RowDefinitions 5+      �� 5,      $��1.7 cm �6    �� 5-      �� 5.      �� 5/      $��1.7 cm �6    �� 50      $
��1 cm �6    �� 51      $
��2 cm �6    .+�54      �� $��2q�6   $��0��6$    �� Orientation$ Vertical=�61   $��Center=�6H   .+�55      �� $��Center=�6   #��[ 69   Название6f   �� 56      $
��4 cm��6   ��6*   �� *Name+	 �� UpdateSourceTrigger$	 PropertyChanged=�57      �� 59      $��2q�6   $��1��6$   $ Vertical=�61   $��Center=�6H   .+�5:      �� $��2q�6   $ Vertical=�6)   $��Center=�6@   .+�5;      �� $��Center=�6    #��[ 6=   Цитата6h   �� 5<      
 �� TextWrapping$

 Wrap=�6   $
��2 cm��62   $��10 cm��6C   #��[ 6�   ��6Q   �� *Quote+$	 PropertyChanged=�6�   5=      �� 5>      $��2q�6   $ 
Horizontal=�6)   $��Center=�6B   .+�5?      �� $��Center=�6    #��[ 6=   Автор цитаты6n   �� 5@      $��0.7 cm��6   $
��6 cm��6.   #��[ 6�   ��6>   �� *AuthorOfTheQuote+$	 PropertyChanged=�6�   5A      5B      �� 5D      $��2q�6   $��2��6$   $ Vertical=�61   $��Center=�6H   .+�5E      �� $��Center=�6   #��[ 69   Описание6f   �� 5F      $

 Wrap=�6   $
��5 cm��6.   $��13 cm��6?   #��[ 6�   ��6M   �� *Description+$	 PropertyChanged=�6�   5G      �� 5I      $��2q�6   $��3��6$   $ Vertical=�61   $��Center=�6H   .+�5J      �� $��Center=�6   #��[ 69   
Глава6c   �� 5K      $	��4cm��6   $��0.7cm��6'    �� SelectedItem 66   �� *SelectedChapter+��6_   �� *
Chapters+��5L      �� 5M      .R�5N      ]� .+�5O   "   �� $��Candara)�6N   $��Normal"�6c   $��0,0,5,0q�6w   $��12(�6�   $y�Visible=�6�   ��6,   ��  �� Path$ OrderNumber�6�   �� 5P   "   $��14(�6G   $��Comic Sans MS)�6U   $y�Visible=�6p   ��6,   �� $
 Name�6p   5Q      5R      5T      5U      �� 5W      $��2q�6   $��4��6$   $ 
Horizontal=�61   $��Center=�6J   .+�5X      �� #��[ 6]   ��6   �� *
FileStatus+$	 PropertyChanged=�6]   �� 5Y      #��[  6�   ��6   �� *Btn_AddFile_Content+$	 PropertyChanged=���6f   �� *ChoseFilePathCmd+6�   5Z      �� 5\      $��2q�6   $��5��6$   $ 
Horizontal=�61   $��Center=�6J   .+�5]      �� #��[  69   ��6   �� *CompleteCmd+.��6^   Добавить6h   �� 5^      #��[  67   ��6   �� *	CancelCmd+.��6\   Отмена6d   5_      5`      5a      5b      