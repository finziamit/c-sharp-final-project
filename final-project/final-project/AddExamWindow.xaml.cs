﻿using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace final_project
{
    /// <summary>
    /// Interaction logic for AddExamWindow.xaml
    /// </summary>
    public partial class AddExamWindow : Window
    {
        private List<Question> _questions;
        private List<Answer> _answers;
        User currentUser;
        HttpClient client = new HttpClient();
        public AddExamWindow(User currentUser)
        {
            InitializeComponent();
            client.BaseAddress = new Uri("https://localhost:7002/api/");
            this._questions = new List<Question>();
            this._answers = new List<Answer>();
            this.currentUser = currentUser;
        }

        private void addExamBtn_Click(object sender, RoutedEventArgs e)
        {
            /*
             * Button to add the exam we just created into the exams list
             * and exit this window
             */

            // delete all empty questions
            List<Question> tmp = new List<Question>();
            foreach (Question question in this._questions)
            {
                if (question.content != string.Empty)
                {
                    tmp.Add(question);
                }
            }
            this._questions = tmp;

            // clear all the elements in the window
            Window addExamWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.Title == "AddExamWindow");

            if (addExamWindow != null)
            {
                addExamWindow.Content = string.Empty;
            }

            // add a Grid for the empty window
            Grid grid = new Grid();

            // add the fields of the exam
            Button submitBtn = new Button();
            StackPanel stackPanel = new StackPanel();

            // set StackPanel location
            stackPanel.Margin = new Thickness(30);
            stackPanel.HorizontalAlignment = HorizontalAlignment.Left;
            stackPanel.VerticalAlignment = VerticalAlignment.Top;

            // Stack Panel Conent
            TextBox nameTB = new TextBox();
            /* TextBox teacherNameTB = new TextBox(); */
            DatePicker dateTB = new DatePicker();
            TextBox durationTB = new TextBox();
            CheckBox isRandomCheck = new CheckBox();

            // add labels
            Label namelbl = new Label();
            /* Label teacherNamelbl = new Label(); */
            Label datelbl = new Label();
            Label durationlbl = new Label();

            // set labels
            namelbl.Content = "Exam's name:";
            /* teacherNamelbl.Content = "Teacher's name:"; */
            datelbl.Content = "Exam's date:";
            durationlbl.Content = "Exam's duration:";

            // set nameTB
            nameTB.Margin = new Thickness(5, 0, 0, 10);
            nameTB.HorizontalAlignment = HorizontalAlignment.Left;
            nameTB.Width = 400;
            nameTB.Background = Brushes.LightGray;
            nameTB.Name = "nameTB";

            stackPanel.Children.Add(namelbl);
            stackPanel.Children.Add(nameTB);

            // set dateTB
            dateTB.Margin = new Thickness(5, 0, 0, 10);
            dateTB.HorizontalAlignment = HorizontalAlignment.Left;
            dateTB.Width = 400;
            dateTB.Background = Brushes.LightGray;
            dateTB.Name = "dateTB";

            stackPanel.Children.Add(datelbl);
            stackPanel.Children.Add(dateTB);

            // set durationTB
            durationTB.Margin = new Thickness(5, 0, 0, 10);
            durationTB.HorizontalAlignment = HorizontalAlignment.Left;
            durationTB.Width = 400;
            durationTB.Background = Brushes.LightGray;
            durationTB.Name = "durationTB";

            stackPanel.Children.Add(durationlbl);
            stackPanel.Children.Add(durationTB);

            // set isRandomCheck
            isRandomCheck.Content = "Show questions randomally";
            isRandomCheck.Margin = new Thickness(0, 0, 0, 10);
            isRandomCheck.HorizontalAlignment = HorizontalAlignment.Left;

            stackPanel.Children.Add(isRandomCheck);

            // set submitBtn
            submitBtn.Content = "Submit";
            submitBtn.HorizontalAlignment = HorizontalAlignment.Right;
            submitBtn.VerticalAlignment = VerticalAlignment.Bottom;
            submitBtn.Margin = new Thickness(10);
            submitBtn.Width = 80;
            submitBtn.Height = 30;
            submitBtn.Click += new RoutedEventHandler(SubmitBtn_Click);

            void SubmitBtn_Click(object sender, RoutedEventArgs e)
            {
                /*
                 * Final submition button click.
                 */

                // get all the content entered
                string name;
                DateTimeOffset dateTime;
                int duration;
                bool isRandom;                

                name = nameTB.Text;

                DateTime selectedDate = dateTB.SelectedDate.Value;
                TimeSpan offset = TimeZoneInfo.Local.GetUtcOffset(selectedDate);
                dateTime = new DateTimeOffset(selectedDate, offset);

                duration = int.Parse(durationTB.Text);

                if (isRandomCheck.IsChecked == true)
                {
                    isRandom = true;
                }
                else
                {
                    isRandom = false;
                }


                // adding the exam and closing the window
                Exam exam = new Exam(name, dateTime, currentUser.Name, duration, isRandom, this._questions);

                Submit(exam);

                this.Close();
            }

            grid.Children.Add(submitBtn);
            // add the StackPanel to the grid and display the grid
            grid.Children.Add(stackPanel);
            addExamWindow.Content = grid;
        }

        private async void Submit(Exam exam)
        {
            // Serialize the object to JSON
            var json = JsonConvert.SerializeObject(exam);

            // Create the request content
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "endpoint");
            request.Content = content;

            // Send the POST request
            var response = await client.PostAsync("Exam", content);

            if (!response.IsSuccessStatusCode)
            {
                // The request failed
                var errorMessage = await response.Content.ReadAsStringAsync();
                MessageBox.Show(errorMessage);
                // Handle the error message
            }
        }


        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            /*
             * Next button functionality: go to the next question.
             * If no question selected do nothing
             */

            int selectedIndex = this.ListBoxQuestions.SelectedIndex;
            if (selectedIndex == -1) { return; }
            if (selectedIndex != this.ListBoxQuestions.Items.Count)
            {
                this.ListBoxQuestions.SelectedIndex = selectedIndex + 1;
            }
        }

        private void PrevBtn_Click(object sender, RoutedEventArgs e)
        {
            /*
             * Previous button functionality: go to the prev question.
             * If no question selected do nothing
             */

            int selectedIndex = this.ListBoxQuestions.SelectedIndex;
            if (selectedIndex == -1) { return; }
            if (selectedIndex != 0)
            {
                this.ListBoxQuestions.SelectedIndex = selectedIndex - 1;
            }
        }

        private void AddQuestionBtn_Click(object sender, RoutedEventArgs e)
        {
            /*
             * Button to add another question to the exam and show its empty fields.
             */

            // Add an empty question
            int i = ListBoxQuestions.Items.Count;

            Question question = new Question("", this._answers);         
            this._questions.Add(question);

            ListBoxQuestions.Items.Add((Question)question);

            // update the questions count
            QuestionsNumberBox.Text = (i + 1).ToString();

            // select the new question
            ListBoxQuestions.SelectedIndex = i;
        }

        private void AddAnswerBtn_Click(object sender, RoutedEventArgs e)
        {
            /*
             * Button to add another Answer to the exam and show its empty field
             */

            if (ListBoxQuestions.SelectedIndex == -1)
            {
                return;
            }

            TextBox tb = new TextBox();
            CheckBox cb = new CheckBox();
            int numOfQuestions = (OptionalAnswers.Children.Count / 2) + 1;
            string name = "Answer" + numOfQuestions.ToString();

            tb.Margin = new Thickness(10);
            tb.HorizontalAlignment = HorizontalAlignment.Left;
            tb.Width = 400;
            tb.Background = Brushes.LightGray;
            tb.Name = name;

            cb.Content = "Mark as correct answer";
            cb.Margin = new Thickness(0, 0, 0, 10);
            cb.Name = name + "CheckBox";

            OptionalAnswers.Children.Add(tb);
            OptionalAnswers.Children.Add(cb);
        }

        private void AddQuestionImage_Click(object sender, RoutedEventArgs e)
        {
            /*
             * Add the question content as image instead of text
             */
            if (this.ListBoxQuestions.Items.Count == 0) { return; }

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = "C:\\";

            int index = this.ListBoxQuestions.SelectedIndex;
            Question question = this._questions[index];

            if (dialog.ShowDialog() == true)
            {
                string name = dialog.FileName;
                string fileName = System.IO.Path.GetFileName(name);
                string location = Environment.CurrentDirectory + fileName;
                ContentTxt.Text = name;
                File.Copy(name, location, true);

                question.imgName = fileName;
                question.imgPath = location;
                question.content = string.Empty;
                Image img = new Image();
                img.Source = new BitmapImage(new Uri(question.imgPath));

                QuestionContent.Children.Add(img);
            }
        }

        private void ListBoxQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
             * Set the fields of the selected question to match
             * the data inserted             
             */

            if (ListBoxQuestions.SelectedIndex == -1) { return; }

            // clear the question and answers fields
            OptionalAnswers.Children.Clear();
            ContentTxt.Clear();
            for (int i = 0; i < QuestionContent.Children.Count; i++)
            {
                if (QuestionContent.Children[i] is Image)
                {
                    QuestionContent.Children.RemoveAt(i);
                }
            }

            // get the currently selected index
            int index = ListBoxQuestions.SelectedIndex;

            if (this._questions[index].imgName != string.Empty)
            {
                if (this._questions[index].answers.Count() >= 2)
                {
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri(this._questions[index].imgPath));

                    ContentTxt.Text = this._questions[index].imgPath;
                    QuestionContent.Children.Add(img);
                }
            }
            else
            {
                ContentTxt.Text = this._questions[index].content;
            }


            CheckBox cb;
            TextBox tb;

            foreach (Answer answer in this._questions[index].answers)
            {
                tb = new TextBox();
                cb = new CheckBox();

                tb.Margin = new Thickness(10);
                tb.HorizontalAlignment = HorizontalAlignment.Left;
                tb.Width = 400;
                tb.Background = Brushes.LightGray;

                cb.Content = "Mark as correct answer";
                cb.Margin = new Thickness(0, 0, 0, 10);

                tb.Text = answer.Content;
                if (answer.CorrectAnswer)
                {
                    cb.IsChecked = true;
                }
                else
                {
                    cb.IsChecked = false;
                }

                OptionalAnswers.Children.Add(tb);
                OptionalAnswers.Children.Add(cb);
            }
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            /*
             * Button to confirm the question's fields modifications
             */
            int index = ListBoxQuestions.SelectedIndex;
            Answer answer;

            for (int j = 0; j < this.OptionalAnswers.Children.Count; j++)
            {
                answer = new Answer(string.Empty, false);

                if (OptionalAnswers.Children[j] is TextBox)
                {
                    TextBox tb = (TextBox)OptionalAnswers.Children[j];
                    if (tb != null)
                    {
                        answer.Content = tb.Text;
                    }
                }

                else if (OptionalAnswers.Children[j] is CheckBox)
                {
                    CheckBox cb = (CheckBox)OptionalAnswers.Children[j];
                    int cbIndex = j / 2;

                    if (cb.IsChecked == true)
                    {
                        this._answers[cbIndex].CorrectAnswer = true;
                    }

                    else
                    {
                        this._answers[cbIndex].CorrectAnswer = false;
                    }

                }

                if (answer.Content != string.Empty)
                {
                    this._answers.Add(answer);
                }
            }

            // if there is no question content or at least 2 answers dont update
            if (ContentTxt.Text == string.Empty || this._answers.Count < 2)
            {
                this._answers.Clear();
                return;
            }

            // check if there are no correct answers
            bool correct_answer_found = false;

            foreach (Answer ans in this._answers)
            {
                if (ans.CorrectAnswer == true)
                {
                    correct_answer_found = true;
                    break;
                }
            }
            if (correct_answer_found == false)
            {
                this._answers.Clear();
                return;
            }

            // update the data
            if (this._questions[index].imgName == string.Empty)
            {
                this._questions[index].content = ContentTxt.Text;
            }
            else
            {
                this._questions[index].content = this._questions[index].imgName;
            }

            this._questions[index].answers = this._answers.ToArray();
            this._answers.Clear();
        }
    }
}
