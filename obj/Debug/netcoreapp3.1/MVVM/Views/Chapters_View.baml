   M S B A M L   `   `   `  ����?  ;Book, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Book.MVVM.ViewModelsBook.MVVM.ViewModels  5      +clr-namespace:Book.HelpersBook.Helpers  5      #    Book.MVVM.Views.Chapters_View   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1vm"clr-namespace:Book.MVVM.ViewModels     5      .helpersclr-namespace:Book.Helpers     5        N� Title$  
MainWindow��5	      $
��25cm��6   $
��40cm��6(    � DataContext 5      -   'Book.MVVM.ViewModels.Chapters_ViewModel  5   
    � 	Resources 5       �� 
TargetType   TextBlockStyle �� Property �� Value  ButtonStyle��%  &	        &	 �     �� 5   
   " ��6   .�5      ��  � ��$ 10q�6'   �� 5       � ��$ Comic Sans MS)�6+   �� 5       � ��$ 14(�6)   5      �� 5   
   " ��6   .�5      ��  � ��$ 5q�6'   �� 5       � ��$ 0.7cm��6'   �� 5       � ��$
 Auto��6&   �� 5       � ��$ Center=�64   �� 5       � �� �	#FFF8F8F96+   �� 5       � ��$ Comic Sans MS)�6+   �� 5       � ��$ 14(�6)   �� 5       � y�$ Visible=�6+   5      5       .��5"      � $��20q�6   $��24 cm��6   $��Center=�6%   $	��Top=�6B    � ColumnDefinitions 5#   
   �� 5$      $��24 cm �6    � RowDefinitions 5&   
   �� 5'      $
��2 cm �6   �� 5(      .+�5+   
   � $��0��6   $��24 cm��6    5-      �� 5.      $
��4 cm �6#   �� 5/      .+�52      �� $��1��6   $
��Left=�6)    �� Orientation$ 
Horizontal=�6D   .+�53      �� $!��Добавить главу��6   #��[ 6T   ��62   �� *AddChapterCmd+6T   �� 54      $!��Добавить часть��6   #��[ 6Q   ��62   �� *
AddPartCmd+6Q   �� 55      $ ��Редактировать��6   #��[ 6O   ��61   �� *	RedactCmd+6O   �� 56      $��Удалить��6   #��[ 6I   ��6+   �� *	DeleteCmd+6I   57      58      � 5:   
   $��1��6   $��Stretch=�6   $
��Auto��68   $��24 cm��6F    5;      �� 5<      $
��1 cm �6#   �� 5=      $��13 cm �6#   �� 5>      $��82.572 �6#   �� 5?      $��333.176 �6#   .+�5B      �� $ Vertical=�6   $��2��60   $��	0,0,491,0q�6D   .+�5C      �� $
��1 cm��6   $
��1 cm��6'   ��65   �� *ChangePosUpCmd+�� 5D      $
��1 cm��6   $
��1 cm��6&   ��64   �� *ChangePosDownCmd+5E      #   Book.Helpers.ExtendedTreeView  5G      -   5        	  Name$	 ChapterList��5G   '   $��0, 0, 0, 1cmq�6<   $��1��6R   
   SelectedItem_
 6b   �� *SelectedItem+ �� Mode$ TwoWay=���6�   �� *
Chapters+ �� UpdateSourceTrigger$ PropertyChanged=���5H      �� 5I       �� ItemsSource 6/   ��  �� Path$ Parts�.R�5J      ]� .+�5K      �� $��Candara)�6j   $��Normal"�6   $��0,0,5,0q�6�   $��14(�6�   $y�Visible=�6�   ��6(   �� *OrderNumber+$ PropertyChanged=�6�   �� 5L      $��Comic Sans MS)�6c   $y�Visible=�6~   $��14(�6�   ��6(   �� *Name+$ PropertyChanged=�6�   5M       �� ItemTemplate 5N      �� 5O      .R�5P   "   ]� .+�5Q   &   �� $��Candara)�6r   $��Normal"�6�   $��0,0,5,0q�6�   $��14(�6�   $y�Visible=�6�   ��60   �� *OrderNumber+$ PropertyChanged=�6�   �� 5R   &   $��Comic Sans MS)�6k   $y�Visible=�6�   $��14(�6�   ��60   �� *Name+$ PropertyChanged=�6�   5S   #   5T      5V      5X      �� 5Z      $��1��6   $��3��6)   $��	491,0,0,0q�6=   .+�5[      ��  �� TextAlignment$ Center=�6   $
��Wrap=�63   $
��9 cm��6G   #��[  6z   $��Center=�6�   ��6W   �� *SelectedItem.Name+6�   �� 5\      $��0q�6   $
 Left=�6R   $
��9 cm��6g   $
��Wrap=�6t   #��[  6�   $��Center=�6�   y�6'   �� *QuoteViewVisibility+��6�   �� *	QuoteView+$ PropertyChanged=�6�   �� 5]      $��0q�6   $ Right=�6R   $
��9 cm��6h   #��[  6�   $��Center=�6�   y�6'   �� *QuoteViewVisibility+��6u   �� *AuthorOfTheQuoteView+$ PropertyChanged=�6�   �� 5^      $��10q�6   $
��9 cm��6%   ._�5_      �� #��[  6w   $
��Wrap=�6�   $��Center=�6�   y�6    �� *DescriptionVisibility+��6M   �� *SelectedItem.Description+6�   5`      �� 5a      #��[ 6a   ��6   �� *FileOpenOrAddCmd+��6>   �� *FileBtnContent+y�6�   �� *FileBtnVisibility+5b      5c      5d      5e      