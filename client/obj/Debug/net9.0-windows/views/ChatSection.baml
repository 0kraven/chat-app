   M S B A M L   `   `   `  ����  chatapp    chat_app.views.ChatWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Chat Window��5   	   $	��600��6   $	��800��6*    � WindowState$ 	Maximized=�5   	    � WindowStyle$
 None=�6!   
���   �.��5      �  � RowDefinitions 5	   
   �� 5
      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $	��200 �6   .+�5   
   �� $��Zero Day Crew��6   $
��50px��5      $��Center=�5      $��Center=�5      $��40(�5      $��Courier New)�5      $
��Bold"�5       ���#00FF00$��0,0,0,0q�5"      �� 5%   
   -   5         ��Name$ ScrollViewer��5%      $��1��6-   $��0��6:   $��Hidden=�6J   ���#1A1A1A$��10,10,10,10q�6�   .��5&      �� -   5         ��Name$ MessagesPanel��5&      $��Bottom=�60   5(      5)      �� 5,   
   -   5        $ ConnectedUsersPanel��5,      $��1��62   $��1��6?   ���#262626$��10,10,10,10q�6d   .+�5-      �� $��Connected Users��6   $��Courier New)�5.      $��18(�5/      $
��Bold"�50      ���#00FF00$��10,10,10,10q�52      �� 53      -   5         ��Name$ 
ClientList��53      ���#1A1A1A���#00FF00���#00FF00$��1q�6j   $��Courier New)�6~   55      56      � 59   
   $��2��6   $��2��6   ���#1A1A1A$��10,10,10,10q�6E    5:      �� 5;      $��* �6#   �� 5=      $
��Auto �6#   .+�5@      �� -   5         ��Name$ MessageInput��5@      $��40��6,   $��0��68   $��Center=�6H   $��16(�6c   $
��10pxq�6q   ���#1A1A1A
����������#00FF00$��1q�5A   G   $��
0,10,10,10q�5B   
   $��Courier New)�6   $��Type your message here��67   5C   +   �� 5E      -   5        $��1��5E      $
��Send��6%   $	��120��64   $��40��6@   $��
0,10,10,10q�6L   $��Center=�5F      ���#00FF00���#0F0F0F$
��Bold"�5G      $��Courier New)�6'   6@   5H      5I      5J      