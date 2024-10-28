﻿using System;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls;
using Wino.Calendar.Args;
using Wino.Calendar.Views.Abstract;
using Wino.Core.Domain.Models.Calendar;
using Wino.Messaging.Client.Calendar;

namespace Wino.Calendar.Views
{
    public sealed partial class CalendarPage : CalendarPageAbstract,
        IRecipient<ScrollToDateMessage>,
        IRecipient<GoNextDateRequestedMessage>,
        IRecipient<GoPreviousDateRequestedMessage>
    {
        private DateTime? selectedDateTime;
        public CalendarPage()
        {
            InitializeComponent();
        }

        public void Receive(ScrollToDateMessage message) => CalendarControl.NavigateToDay(message.Date);

        public void Receive(GoNextDateRequestedMessage message) => CalendarControl.GoNextRange();

        public void Receive(GoPreviousDateRequestedMessage message) => CalendarControl.GoPreviousRange();

        private void CellSelected(object sender, TimelineCellSelectedArgs e)
        {
            selectedDateTime = e.ClickedDate;

            TeachingTipPositionerGrid.Width = e.CellSize.Width;
            TeachingTipPositionerGrid.Height = e.CellSize.Height;

            Canvas.SetLeft(TeachingTipPositionerGrid, e.PositionerPoint.X);
            Canvas.SetTop(TeachingTipPositionerGrid, e.PositionerPoint.Y);

            WeakReferenceMessenger.Default.Send(new CalendarEventAdded(new CalendarItem(selectedDateTime.Value, default)));

            //var t = new Flyout()
            //{
            //    Content = new TextBlock() { Text = "Create event" }
            //};

            //t.ShowAt(TeachingTipPositionerGrid, new FlyoutShowOptions()
            //{
            //    ShowMode = FlyoutShowMode.Transient,
            //    Placement = FlyoutPlacementMode.Right
            //});

            NewEventTip.IsOpen = true;
        }

        private void CellUnselected(object sender, TimelineCellUnselectedArgs e)
        {
            NewEventTip.IsOpen = false;
            selectedDateTime = null;
        }

        private void CreateEventTipClosed(TeachingTip sender, TeachingTipClosedEventArgs args)
        {
            // Reset the timeline selection when the tip is closed.
            CalendarControl.ResetTimelineSelection();
        }

        private void AddEventClicked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (selectedDateTime == null) return;

            var eventEndDate = selectedDateTime.Value.Add(EventTimePicker.Time);

            // Create the event.
            WeakReferenceMessenger.Default.Send(new CalendarEventAdded(new CalendarItem(selectedDateTime.Value, eventEndDate)));
        }
    }
}
