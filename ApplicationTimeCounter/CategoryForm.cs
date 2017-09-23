﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace ApplicationTimeCounter
{
    class CategoryForm
    {
        public Canvas MainCanvasCategory { get; set; }
        private Canvas contentPage;
        private MyLabel notAssignedApplications;
        private Label buttonAssignActivity;
        private MyLabel numberActivity;
        private Label buttonShowActivity;
        private MyLabel empty1;
        private Label empty2;
        private DispatcherTimer timerAnimation;
        private int repeatIntervals;
        LoadingWindow loadingWindow;

        private ViewContent viewContent;

        public CategoryForm(Canvas contentPage, ref ViewContent viewContent)
        {
            this.contentPage = contentPage;
            this.viewContent = viewContent;

            timerAnimation = new DispatcherTimer();
            timerAnimation.Interval = new TimeSpan(0, 0, 0, 0, 10);
            repeatIntervals = 0;

            CreateCategoryForm();
        }

        private void CreateCategoryForm()
        {
            MainCanvasCategory = CanvasCreator.CreateCanvas(contentPage, 620, 410, Color.FromArgb(255, 30, 39, 93), 0, 0);
            MainCanvasCategory.Opacity = 0;
            Canvas.SetZIndex(MainCanvasCategory, 0);

            Canvas canvasMembership = CanvasCreator.CreateCanvas(MainCanvasCategory, 290, 380, Color.FromArgb(155, 4, 0, 35), 15, 15);
            ImageCreator.CreateImage(canvasMembership, 60, 60, 40, 40, "Pictures/MembershipImages.png");
            MyLabel label = new MyLabel(canvasMembership, "Przynależność", 250, 40, 20, 60, 60, Color.FromArgb(255, 255, 255, 255),
                Color.FromArgb(0, 255, 255, 255));

            CreateCanvasActivityWithElements();
            
            
        }

        public void ShowCategoryForm()
        {
            viewContent.ChangeContent(MainCanvasCategory);
            UpdateView();
        }

        public void UpdateView()
        {
            GetNotAssignedApplicationsAndSetLabel();
            GetAllActivityAndSetLabel();
        }

        private void CreateCanvasActivityWithElements()
        {
            Canvas canvasActivity = CanvasCreator.CreateCanvas(MainCanvasCategory, 290, 380, Color.FromArgb(155, 4, 0, 35), 320, 15);
            ImageCreator.CreateImage(canvasActivity, 60, 60, 40, 40, "Pictures/ActivityImages.png");
            MyLabel label2 = new MyLabel(canvasActivity, "Aktywność", 250, 40, 20, 60, 60, Color.FromArgb(255, 255, 255, 255),
                Color.FromArgb(0, 255, 255, 255));
            MyRectangle r0 = new MyRectangle(canvasActivity, 250, 1, Color.FromArgb(40, 255, 255, 255), 20, 140);


            // przypisz aktywności----------------------------------------------------------------------------------------------
            notAssignedApplications = new MyLabel(canvasActivity, "0", 50, 38, 18, 30, 200, Color.FromArgb(255, 125, 255, 0), Color.FromArgb(255, 255, 255, 255), 0);
            buttonAssignActivity = ButtonCreator.CreateButton(canvasActivity, "     Przypisz Aktywności", 225, 38, 16, 30, 200,
                Color.FromArgb(255, 255, 255, 255), Color.FromArgb(255, 255, 255, 255), 0);

            buttonAssignActivity.MouseMove += buttonAssignActivity_MouseMove;
            buttonAssignActivity.MouseLeave += buttonAssignActivity_MouseLeave;
            buttonAssignActivity.MouseLeftButtonDown += buttonAssignActivity_MouseLeftButtonDown;
            MyRectangle r = new MyRectangle(canvasActivity, 200, 1, Color.FromArgb(100, 255, 255, 255), 40, 237);
            //-----------------------------------------------------------------------------------------------------------------

            // przegląd aktywności
            numberActivity = new MyLabel(canvasActivity, "0", 50, 38, 18, 30, 237, Color.FromArgb(255, 255, 255, 255), Color.FromArgb(255, 255, 255, 255), 0);
            buttonShowActivity = ButtonCreator.CreateButton(canvasActivity, "     Przegląd Aktywności", 225, 38, 16, 30, 237,
                Color.FromArgb(255, 255, 255, 255), Color.FromArgb(255, 255, 255, 255), 0);

            buttonShowActivity.MouseMove += buttonShowActivity_MouseMove;
            buttonShowActivity.MouseLeave += buttonShowActivity_MouseLeave;
            buttonShowActivity.MouseLeftButtonDown += buttonShowActivity_MouseLeftButtonDown;
            MyRectangle r2 = new MyRectangle(canvasActivity, 200, 1, Color.FromArgb(100, 255, 255, 255), 40, 275);
            //------------------------------------------------------------------------------------------------------------------

            // puste pole
            empty1 = new MyLabel(canvasActivity, "0", 50, 38, 18, 30, 275, Color.FromArgb(255, 255, 255, 255), Color.FromArgb(255, 255, 255, 255), 0);
            empty2 = ButtonCreator.CreateButton(canvasActivity, "     Puste Pole    ", 225, 38, 16, 30, 275,
                Color.FromArgb(255, 255, 255, 255), Color.FromArgb(255, 255, 255, 255), 0);

            empty2.MouseMove += empty2_MouseMove;
            empty2.MouseLeave += empty2_MouseLeave;
            empty2.MouseLeftButtonDown += empty2_MouseLeftButtonDown;
            MyRectangle r3 = new MyRectangle(canvasActivity, 200, 1, Color.FromArgb(100, 255, 255, 255), 40, 312);
            //------------------------------------------------------------------------------------------------------------------
        }

        /// <summary>
        /// Pobiera ilość nie przypisanych aplikacji i ustawia kontrolkę 'notAssignedApplications'.
        /// </summary>
        private void GetNotAssignedApplicationsAndSetLabel()
        {

            string allNotAssignedApplicationString = ActiveApplication_db.GetAllNotAssignedApplication();
            int allNotAssignedApplicationInt;
            if (int.TryParse(allNotAssignedApplicationString, out allNotAssignedApplicationInt))
            {
                    notAssignedApplications.SetContent(allNotAssignedApplicationInt.ToString());
                    SetColorNotAssignedApplications(allNotAssignedApplicationInt);
            }
            else notAssignedApplications.SetContent("-1");
        }

        private void GetAllActivityAndSetLabel()
        {
            numberActivity.SetContent(NameActivity_db.GetAllNameActivity());
        }

        private void SetColorNotAssignedApplications(int value)
        {
            int red = 125 + value * 2;
            int green = 255 - value * 2;
            red = (red > 255) ? 255 : red;
            green = (green < 0) ? 0 : green;
            notAssignedApplications.SetFontColor(Color.FromArgb(255, (byte)red, (byte)green, 0));
        }

        private void buttonAssignActivity_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            buttonAssignActivity.Background = new SolidColorBrush(Color.FromArgb(140, 0, 123, 255));
        }

        private void buttonAssignActivity_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            buttonAssignActivity.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }

        private void buttonAssignActivity_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var tempRef = MainCanvasCategory;
            AssignedActivity assignedActivity = new AssignedActivity(ref tempRef);

            //timerAnimation.Tick += new EventHandler(LoadingWindowWait);
            
           // loadingWindow = new LoadingWindow(ref tempRef);
           // Thread thread = new Thread(new ThreadStart(loadingWindow.test));
           // thread.Start();

            

        }

       

        private void LoadingWindowWait(object sender, EventArgs e)
        {
            repeatIntervals++;
            if (repeatIntervals == 1)
            {
              //  loadingWindow = new LoadingWindow(MainCanvasCategory);
            }
            if(repeatIntervals == 100)
            {
               // MainCanvasCategory.Children.Remove(loadingWindow.LoadCanvas);
                repeatIntervals = 0;
                timerAnimation.Stop();
            }
        }

        private void buttonShowActivity_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            buttonShowActivity.Background = new SolidColorBrush(Color.FromArgb(140, 0, 123, 255));
        }

        private void buttonShowActivity_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            buttonShowActivity.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }

        private void buttonShowActivity_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           // var tempRef = MainCanvasCategory;
            //AssignedActivity assignedActivity = new AssignedActivity(ref tempRef);
            //LoadingWindow loadingWindows = new LoadingWindow(MainCanvasCategory);
        }

        private void empty2_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            empty2.Background = new SolidColorBrush(Color.FromArgb(140, 0, 123, 255));
        }

        private void empty2_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            empty2.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }

        private void empty2_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           // okno
        }

        
    }
}
