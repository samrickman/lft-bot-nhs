   M S B A M L   `   `   `  ����E  ANHSTestBot, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:NHSTestBot
NHSTestBot  5   	       NHSTestBot.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:NHSTestBot     5   	   -   5          � Title$  NHS LFT Ordering Bot��5   	   $	��450��6&   $	��800��63   .��5
      � $��0,1,111.6,-1q�6    � ColumnDefinitions 5   
   �� 5      $	��44* �6   �� 5      $	��14* �6   �� 5      $
��623* �6   .+�5   
   �� $#��NHS-registered email address:��6   $
��Left=�68   $��8,9,0,0q�6S   $	��Top=�6d   $��3��6|   �� 5   
   $��NHS-registered password:��6   $
��Left=�63   $��8,29,0,0q�6N   $	��Top=�6`   $��3��6x   �� 5   
   -   5         ��Name$ email��5      $
��Left=�6!   $��23��6<   $��128.6,13,0,0q�6H    �� TextWrapping$
 Wrap=�6^   $��name@domain.com��6r   $	��Top=�6�   $	��197��6�   $��2��6�   G� 5   
   -   5         G�Name$ passwordTextBox��5      $
��Left=�60   $��128.6,37,0,0q�6K   $	��Top=�6a   $	��197��6y   $��2��6�   �� 5   
   -   5         ��Name$ orderButton��5      $��Order tests!��6&   $
��Left=�6=   $��44��6X   $��343.6,11,0,0q�6d   $	��Top=�6z   $	��106��6�   $��2��6�   �� 5   
   -   5         ��Name$ 
statusText��5      $
��Left=�6(   $	��259��6C   $��	15,70,0,0q�6P   $
��Wrap=�6c   $	��Top=�6�   $	��608��6�   $��3��6�   ��6w   ��  �� Path$ 
StatusText�6�   5      5      