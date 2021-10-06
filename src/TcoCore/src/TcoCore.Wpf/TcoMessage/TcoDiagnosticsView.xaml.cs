﻿using System.Windows;
using System.Windows.Controls;
using TcOpen.Inxton.TcoCore.Wpf;

namespace TcoCore
{
    /// <summary>
    /// Interaction logic for stMessageQueueView.xaml
    /// </summary>
    public partial class TcoDiagnosticsView : UserControl
    {
        public TcoDiagnosticsView()
        {
            InitializeComponent();
            this.DiagnosticsUpdateTimer();
        }

        private System.Timers.Timer messageUpdateTimer;
        private int diagnosticsDepth;

        private void DiagnosticsUpdateTimer()
        {
            if (messageUpdateTimer == null)
            {
                messageUpdateTimer = new System.Timers.Timer(2500);
                messageUpdateTimer.Elapsed += MessageUpdateTimer_Elapsed;
                messageUpdateTimer.AutoReset = true;
                messageUpdateTimer.Enabled = true;
            }
        }
        private void MessageUpdateTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            UpdateMessages();
        }

        private TcoDiagnosticsViewModel _context { get { return this.DataContext as TcoDiagnosticsViewModel; } }

        private void UpdateMessages()
        {
            var inSight = false;
            TcoDiagnosticsViewModel MessageHandler = null;
            TcOpen.Inxton.TcoAppDomain.Current.Dispatcher.Invoke(() =>
            {
                if ((MessageHandler != null) && !MessageHandler.AutoUpdate)
                {
                    return;
                }

                inSight = UIElementAccessibilityHelper.IsInSight<Grid>(this.Element, this);
                if (inSight)
                {
                    MessageHandler = this.DataContext as TcoDiagnosticsViewModel;
                }
            });
            bool isAutoUpdate = MessageHandler == null ? false : MessageHandler.AutoUpdate;

            if (inSight && isAutoUpdate)
            {
                MessageHandler?.UpdateMessages();
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
                _context?.UpdateMessages();
            }
        }

        /// <summary>
        /// Gets or sets the diagnostics depth for the observed object.
        /// </summary>
        public int DiagnosticsDepth
        {
            get { return this._context.DiagnosticsDepth; }
            set { diagnosticsDepth = value; this._context.DiagnosticsDepth = value; }
        }
    }
}
