   M S B A M L   `   `   `  ����   chatapp    chat_app.views.ChatWindow   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Chat Window��$	��600��$	��800�� � WindowState$ 	Maximized=� � WindowStyle$
 None=�
���   �.���  � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto � � ColumnDefinitions �� $��* ��� $	��200 �.+��� $��Zero Day Crew��$
��50px��$��Center=�$��Center=�$��40(�$��Courier New)�$
��Bold"����#00FF00$��0,0,0,0q��� -    ��Name$ ScrollViewer��$��1��$��0��$��Hidden=����#1A1A1A$��10,10,10,10q�.���� -    ��Name$ MessagesPanel��$��Bottom=��� -   $ ConnectedUsersPanel��$��1��$��1�����#262626$��10,10,10,10q�.+��� $��Connected Users��$��Courier New)�$��18(�$
��Bold"����#00FF00$��10,10,10,10q��� -    ��Name$ 
ClientList�����#1A1A1A���#00FF00���#00FF00$��1q�$��Courier New)�� $��2��$��2�����#1A1A1A$��10,10,10,10q� �� $��* ��� $
��Auto �.+��� -    ��Name$ MessageInput��$��40��$��0��$��Center=�$��16(�$
��10pxq����#1A1A1A
����������#00FF00$��1q�$��
0,10,10,10q�$��Courier New)�$��Type your message here���� -   $��1��$
��Send��$	��120��$��40��$��
0,10,10,10q�$��Center=����#00FF00���#0F0F0F$
��Bold"�$��Courier New)�