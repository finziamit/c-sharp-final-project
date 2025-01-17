﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace final_project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        User currentUser;
        List<Exam> examsList;
        Exam? selectedExam;
        HttpClient client = new HttpClient();
        public MainWindow(User currentUser)
        {
            InitializeComponent();
            client.BaseAddress = new Uri("https://localhost:7002/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            this.currentUser = currentUser;
            hello_msg.Text = $"Hello, {currentUser.Name}";
            if (!currentUser.IsTeacher)
            {
                Add_Exam_Button.Visibility = Visibility.Hidden;
                View_Exam_Button.Visibility = Visibility.Hidden;
            }
            else
            {
                Start_Exam_Button.Visibility = Visibility.Hidden;
            }
            examsList = new List<Exam>();
            GetExams("");
        }

        private void Add_Exam_Button_Click(object sender, RoutedEventArgs e)
        {
            /*
             * Add Exam Button functionality: open the AddExam window. send as argument the teacher user
             */
            AddExamWindow addExamWindow = new AddExamWindow(currentUser);
            addExamWindow.Show();
        }

        /* Getting all Exams from database and adding them to the examsList: */
        private async void GetExams(string keyWord)
        {

            // Send the GET request
            var response = await client.GetStringAsync("Exam");

            // Deserialize from JSON to Exams List
            examsList = JsonConvert.DeserializeObject<List<Exam>>(response);

            // Filtering the examsList with the search-bar key word
            examsList = examsList.Where(exam => exam.Name.ToLower().Contains(keyWord.ToLower())).ToList<Exam>();

            // For each exam in the list, add the exam to the ExamsList (The listbox in the xaml)
            foreach (Exam item in examsList)
            {
                this.ExamsList.Items.Add(item);
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*
             * Search bar functionality: show the matching exams according to the search
             */
            string keyWord = this.Search_bar.Text;
            this.ExamsList.Items.Clear();
            GetExams(keyWord);
        }

        private void Start_Exam_Button_Click(object sender, RoutedEventArgs e)
        {
            // check if the exam is marked
            if(ExamsList.SelectedIndex == -1)
            {
                return;
            }

            // check if the current date is the examination date
            if ((DateTimeOffset)this.selectedExam.DateTime != DateTimeOffset.Now.Date)
            {
                MessageBox.Show("Exam can't be taken in the current date", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // check if the user already solved the exam
            if (this.selectedExam.examUser.Any(examUser => (string)examUser.userName == this.currentUser.Name))
            {
                MessageBox.Show("Exam can't be taken more than once", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            ExamWindow examWindow = new ExamWindow(this.selectedExam, this.currentUser);
            
            examWindow.Show();
        }

        private void View_Exam_Button_Click(object sender, RoutedEventArgs e)
        {
            // check if the exam is marked
            if (ExamsList.SelectedIndex == -1)
            {
                return;
            }
            StatisticsWindow statisticsWindow = new StatisticsWindow(this.selectedExam);
            statisticsWindow.Show();
        }

        private void ExamsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (currentUser.IsTeacher)
            {
                View_Exam_Button.IsEnabled = true;
                Edit_Exam_Button.Visibility = Visibility.Visible;
                Edit_Exam_Button.IsEnabled = true;
            }
            else
            {
                Start_Exam_Button.IsEnabled = true;
            }
            this.selectedExam = (Exam)ExamsList.SelectedItem;
            if (selectedExam != null)
            {
                this.examDateTB.Text = this.selectedExam.DateTime.ToString();
                this.examTeacherTB.Text = this.selectedExam.TeacherName;
                this.examDurationTB.Text = this.selectedExam.Duration.ToString();
            }
            
        }

        private void Edit_Exam_Button_Click(object sender, RoutedEventArgs e)
        {
            // check if the exam is marked
            if (ExamsList.SelectedIndex == -1)
            {
                return;
            }
            EditExamWindow editExamWindow = new EditExamWindow((Exam)ExamsList.SelectedItem);
            editExamWindow.Show();
        }
    }
}
