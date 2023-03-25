﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace final_project
{
    /// <summary>
    /// Interaction logic for ExamWindow.xaml
    /// </summary>
    public partial class ExamWindow : Window
    {
        Exam exam;
        DispatcherTimer _timer;
        TimeSpan _time;
        public ExamWindow(Exam exam)
        {
            InitializeComponent();
            this.exam = exam;

            // Set Timer
            _time = TimeSpan.FromSeconds(exam.Duration * 60);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                tbTime.Text = _time.ToString("c");
                if (_time == TimeSpan.Zero)
                {
                    _timer.Stop();
                    // navigate to exam choosing window
                }
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);
            _timer.Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // init the number of answered questions to 0 out of num of questions
            this.QuestionsNumberBox.Text = "0/" +
                this.exam.Questions.Count.ToString();

            // add the questions to the questions listbox
            foreach (Question q in this.exam.Questions)
            {
                this.ListBoxQuestions.Items.Add(q);
            }

        }

        private void ListBoxQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
             * Get the selected question from the ListBox and present it in
             * the quesion appearance part of the screen. If the question already
             * been answered restore the answer selected
             */
            int i = 0;
            this.QuestionContent.Children.Clear();
            this.OptionalAnswers.Children.Clear();

            if (this.ListBoxQuestions.SelectedItem is Question q)
            {
                if (q.imgName == "")
                {
                    TextBlock question = new TextBlock();
                    question.Text = q.content;
                    this.QuestionContent.Children.Add(question);
                }
                else
                {
                    Image img = new Image();
                    string location = q.imgPath + "/" + q.imgName;
                    img.Source = new BitmapImage(new Uri(location));
                    this.QuestionContent.Children.Add(img);
                }

                foreach (Answer ans in q.answers)
                {
                    int j = 0;
                    Answer answer = ans;
                    RadioButton rb = new RadioButton()
                    {
                        Content = answer.Content,
                    };

                    rb.Name = "Name" + (j.ToString());

                    rb.Checked += (sender, args) =>
                    {
                        q.chosenAnswer = rb.Name[rb.Name.Length - 1] - '0';
                        int newCount = this.exam.getSolvedQuestionsCount();
                        this.QuestionsNumberBox.Text = newCount.ToString() +
                        "/" + this.exam.Questions.Count.ToString();

                    };

                    rb.Unchecked += (sender, args) =>
                    {
                        q.chosenAnswer = -1;
                    };

                    rb.Tag = i++;

                    if (q.chosenAnswer == j)
                    {
                        rb.IsChecked = true;
                    }

                    else
                    {
                        rb.IsChecked = false;
                    }

                    OptionalAnswers.Children.Add(rb);
                    j++;

                }
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

        private void exitBtnClick(object sender, RoutedEventArgs e)
        {
            /*
             * Exit button functionality: close the exam
             * (might be changed to Finish button)
             */
            this.Close();
        }
    }
}