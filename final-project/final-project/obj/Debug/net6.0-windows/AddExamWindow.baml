   M S B A M L   `   `   `  ����H  Dfinal-project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:final_projectfinal_project  5   	   !    final_project.AddExamWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:final_project     5   	     � Title$  AddExamWindow��5   	   $	��450��6   $	��800��6,   
����p��.��5	      �  � ColumnDefinitions 5
   
   �� 5      $��1* �6   �� 5      $��3* �6   .+�5   
   � -   5        $��	InnerGrid��5       � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $
��auto �6    �� 5      $	��20* �6    �� 5      $��* �6    .+�5      �� -   5        $��QuestionsNumberBox��5      $��Center=�62   $	��Top=�6O   $��50��6g   $��0��6s   �� 5      -   5        $��ListBoxQuestions��5      
�������$��3��6F   $��0q�6T   $��Stretch=�6_   6{   �� 5      -   5        $��AddQuestionBtn��5      $��Add Question��6+   $��0��6B   $��4��6R   $	��Top=�6_   
������6�   5      � 5   
   $��1��6   $��0��6    5      �� 5      $��* �6#    5      �� 5       $��50 �6    �� 5!      $
��auto �6!   �� 5"      $
��auto �6    �� 5#      $
��auto �6    �� 5$      $
��auto �6    �� 5%      $
��auto �6    �� 5&      $
��auto �6    .+�5)      �� -   5        $��
addExamBtn��5)      $��10(�6'   $
��Left=�65   $��Add Exam��6P   $��0��6c   $��510,10,0,10q�6p   $��80��6�   6�   �� 5*      $��1��6   $��Hidden=�6(   $��Hidden=�6M   $	��150��6t   .��5+      �� -   5        $��QuestionContent��5+      $��10,10,10,10q�64   .+�5,      �� -   5        $��
Contentlbl��5,      $��
Question: ��6.   $
��Left=�6C   �� 5-      -   5        $��
ContentTxt��5-   B   $��10q�6   
�������6B   �� 5.      -	   5        $��AddQuestionImage��5.      $��Add Question as Image��65   $	��150��6U   $��30��6a   $��10q�6m   $��Center=�6y   $��Center=�6�   6�   5/      50      �� 51      $��2��6   $��Visible=�6(   $��Disabled=�6N   $	��150��6w   .��52      �� -
   5        $��OptionalAnswers��52      $��10q�65   53      54      �� 56   
   -   5        $��AddAnswerBtn��56      $��
Add Answer��6%   $��1��6:   $
��Left=�6K   $��Bottom=�6f   $��10q�6�   $��30��6�   $	��100��6�   
����� �6�   �� 57   
   -   5        $��
ConfirmBtn��57      $��Confirm Question��6#   $��1��6>   $
��Left=�6O   $��Bottom=�6j   $��
131,0,0,10q�6�   $��30��6�   $	��100��6�   
��� �|�6�   �� 59   
    �� Orientation$ 
Horizontal=�6   $��1��6/   $��Bottom=�6?   $��Right=�6Z   .+�5:      �� -   5        $��PrevBtn��5:   [   $��15(�6   $��10q�6$   $��30��60   $��Previous��6<   $	��100��6O   6j   �� 5;      -   5        $��NextBtn��5;   W   $��15(�6   $��10q�6$   $��30��60   $
��Next��6<   $	��100��6K   6f   5<      5=      5>      