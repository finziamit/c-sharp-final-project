   M S B A M L   `   `   `  ����H  Dfinal-project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:final_projectfinal_project  5   	   $    final_project.StatisticsWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:final_project     5   	     � Title$  StatisticsWindow��5   	   $	��450��6"   $	��800��6/   
����2��.��5	      �  � ColumnDefinitions 5
   
   �� 5      $��4* �6   �� 5      $
��2.5* �6   �� 5      $
��5.5* �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� $��Exam's Statistics:��6   $��0��6.   $��0��6;   $��28(�6K   $
��Bold"�6Y   $��10q�6k    �� Padding$ 10q�6w   $	��Top=�6�   $
��Left=�6�   
������� J� Opacity$	 0.5Q�6�   �� 5   
   -   5        $��studentsListBox��5      $��5��6)   $��1��6:   $��0��6G   ${�false��6W   �� 5   
   -   5        $��highestGradelbl��5      $��Highest grade:��6'   
�������$	 0.6Q�6S   $��1��6a   $��1��6q   $��18(�6~   $��10q�6�   $��Right=�6�   �� 5   
   -   5        $��highestGradeTB��5      
�������$��2��6A   $��1��6Q   $��10q�6^   $ 10q�6j   �� 5   
   -   5        $��averageGradelbl��5      $��Average grade:��6'   
�������$	 0.6Q�6S   $��1��6a   $��2��6q   $��18(�6~   $��10q�6�   $��Right=�6�   �� 5   
   -   5        $��averageGradeTB��5      
�������$��2��6A   $��2��6Q   $��10q�6^   $ 10q�6j   �� 5    
   -   5        $��lowestGradelbl��5       $��Lowest grade:��6&   
�������$	 0.6Q�6Q   $��1��6_   $��3��6o   $��18(�6|   $��10q�6�   $��Right=�6�   �� 5!   
   -   5        $��lowestGradeTB��5!      
�������$��2��6@   $��3��6P   $��10q�6]   $ 10q�6i   �� 5"   
   -   5        $��numberOfStudentslbl��5"      $��Students count:��6+   
�������$	 0.6Q�6X   $��1��6f   $��4��6v   $��18(�6�   $��10q�6�   $��Right=�6�   �� 5#   
   -	   5        $��numberOfStudentsTB��5#      
�������$��2��6E   $��4��6U   $��10q�6b   $ 10q�6n   �� 5'   
   -
   5        $��exitBtn��5'      $
��Exit��6    $��Right=�6/   $��Bottom=�6K   $��2��6f   $��6��6v   $	��100��6�   $��30��6�   $��10q�6�   6�   5(      5)      