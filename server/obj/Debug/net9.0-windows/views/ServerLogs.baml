   M S B A M L   `   `   `  ����  	ServerApp    server.views.ServerLogs   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Server Window��5   	   $	��600��6   $	��800��6,    � WindowState$ 	Maximized=�5   	    � WindowStyle$
 None=�6!   
���   �.��5      �  � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $	��200 �6   .+�5   
   �� $��Zero Day Crew Server��6   $
��50px��5      $��Center=�5      $��Center=�5      $��40(�5      $��Courier New)�5      $
��Bold"�5      ���#00FF00$��0,0,0,0q�5      �� 5    
   -   5         ��Name$ ScrollViewer��5       $��1��6-   $��0��6:   ���#1A1A1A$��10,10,10,10q�6_   .��5!      �� -   5         ��Name$ ServerLogsPanel��5!      $��Bottom=�62   5#      5$      �� 5'   
   $��1��6   $��1��6"   ���#262626$��10,10,10,10q�6G   .+�5(      �� $��Connected Users��6   $��Courier New)�5)      $��18(�5*      $
��Bold"�5+      ���#00FF00$��10,10,10,10q�5-      �� 5.      -   5         ��Name$ 
ClientList��5.      ���#1A1A1A���#00FF00���#00FF00$��1q�6j   $��Courier New)�6~   50      51      � 54   
   $��2��6   $��2��6   ���#1A1A1A$��10,10,10,10q�6E    55      �� 56      $��* �6#   �� 57      $
��Auto �6#   .+�5:      �� -   5         ��Name$ CommandInput��5:      $��40��6,   $��0��68   $��Center=�6H   $��16(�5;      $
��10pxq�6$   ���#1A1A1A
����������#00FF00$��1q�6p   $��
0,10,10,10q�5<      $��Courier New)�6*   $��Enter server command��6C   5=   7   �� 5?      -   5        $��1��5?      $��Send Command��6%   $	��150��6<   $��40��6H   $��
0,10,10,10q�6T   $��Center=�5@      ���#00FF00���#0F0F0F$
��Bold"�5A      $��Courier New)�6'   6@   5B      5C      5D      