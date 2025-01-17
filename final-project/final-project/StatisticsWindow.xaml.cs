﻿using System.Collections.Generic;
using System.Windows;

namespace final_project
{
    /// <summary>
    /// Interaction logic for StatisticsWindow.xaml
    /// </summary>
    public partial class StatisticsWindow : Window
    {
        Exam exam;
        List<ExamUser> examUsers;
        List<int> grades;
        public StatisticsWindow(Exam exam)
        {
            InitializeComponent();
            this.exam = exam;
            this.grades = new List<int>();
            if (exam.examUser != null)
            {
                this.examUsers = (List<ExamUser>?)exam.examUser;
            }

            // fill the listbox with the students done the exam
            foreach (ExamUser examUser in this.examUsers)
            {
                studentsListBox.Items.Add(examUser);
                this.grades.Add(examUser.Grade);
            }

            // calculate the average, find the highest and lowest grades            

            int highestGrade = 0;
            int lowestGrade = 100;
            float averageGrade = 0;

            if (grades.Count > 0)
            {
                for (int i = 0; i < grades.Count; i++)
                {
                    averageGrade += grades[i];

                    if (grades[i] > highestGrade)
                    {
                        highestGrade = grades[i];
                    }

                    if (grades[i] < lowestGrade)
                    {
                        lowestGrade = grades[i];
                    }
                }

                averageGrade = averageGrade / grades.Count;
            }
            else
            {
                averageGrade = 0;
                lowestGrade = 0;
            }


            // set the TextBlocks to show the information
            highestGradeTB.Text = highestGrade.ToString();
            lowestGradeTB.Text = lowestGrade.ToString();
            averageGradeTB.Text = averageGrade.ToString();
            numberOfStudentsTB.Text = this.studentsListBox.Items.Count.ToString();
        }
        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
