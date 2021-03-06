﻿using JBToolkit.Windows;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using YTMusicUploader.Providers;

namespace YTMusicUploader
{
    public partial class MainForm
    {
        delegate void SetStatusMessageDelegate(string statusMessage, string systemTrayIconText = null);
        public void SetStatusMessage(string statusMessage, string systemTrayIconText = null)
        {
            if (lblStatus.InvokeRequired)
            {
                var d = new SetStatusMessageDelegate(SetStatusMessage);
                Invoke(d, new object[] { statusMessage, systemTrayIconText });
            }
            else
            {
                lblStatus.Text = statusMessage;

                if (!string.IsNullOrEmpty(systemTrayIconText))
                    SetSystemTrayIconText(systemTrayIconText);
            }
        }

        delegate void SetPausedDelegate(bool paused);
        public void SetPaused(bool paused)
        {
            if (pbArtwork.InvokeRequired ||
                lblStatus.InvokeRequired)
            {
                var d = new SetPausedDelegate(SetPaused);
                Invoke(d, new object[] { paused });
            }
            else
            {
                if (paused)
                {
                    lblStatus.Text = "Paused";
                    lblUploadingMessage.Text = "Paused";
                    SetSystemTrayIconText("Paused");
                    pbPaused.Visible = true;
                }
                else
                {
                    lblStatus.Text = "Idle";
                    lblUploadingMessage.Text = "Idle";
                    SetSystemTrayIconText("Idle");
                    pbPaused.Visible = false;
                }
            }
        }


        public void SetSystemTrayIconText(string text)
        {
            TrayIcon.Text = "YT Music Uploader\r\n" + text;
        }

        delegate void SetConnectedToYouTubeMusicDelegate(bool connectedToYouTubeMusic);
        public void SetConnectedToYouTubeMusic(bool connectedToYouTubeMusic)
        {
            try
            {
                ConnectedToYTMusic = connectedToYouTubeMusic;

                if (pbConnectedToYoutube.InvokeRequired ||
                    pbNotConnectedToYoutube.InvokeRequired)
                {
                    var d = new SetConnectedToYouTubeMusicDelegate(SetConnectedToYouTubeMusic);
                    Invoke(d, new object[] { connectedToYouTubeMusic });
                }
                else
                {
                    if (connectedToYouTubeMusic)
                    {
                        pbConnectedToYoutube.Visible = true;
                        pbNotConnectedToYoutube.Visible = false;
                    }
                    else
                    {
                        pbConnectedToYoutube.Visible = false;
                        pbNotConnectedToYoutube.Visible = true;
                    }
                }
            }
            catch { }
        }

        delegate void SetStartWithWindowsDelegate(bool startWithWindows);
        public void SetStartWithWindows(bool startWithWindows)
        {
            if (cbStartWithWindows.InvokeRequired)
            {
                var d = new SetStartWithWindowsDelegate(SetStartWithWindows);
                Invoke(d, new object[] { startWithWindows });
            }
            else
            {
                cbStartWithWindows.Checked = startWithWindows;
            }
        }

        delegate void SetSendLogsToSourceDelegate(bool sendLogsToSource);
        public void SetSendLogsToSource(bool sendLogsToSource)
        {
            if (cbStartWithWindows.InvokeRequired)
            {
                var d = new SetSendLogsToSourceDelegate(SetSendLogsToSource);
                Invoke(d, new object[] { sendLogsToSource });
            }
            else
            {
                cbSendErrorLogsToSource.Checked = sendLogsToSource;
            }
        }

        delegate void SetManageTYMusicButtonEnabledDelegate(bool enabled);
        public void SetManageTYMusicButtonEnabled(bool enabled)
        {
            if (pbYtMusicManage.InvokeRequired)
            {
                var d = new SetManageTYMusicButtonEnabledDelegate(SetManageTYMusicButtonEnabled);
                Invoke(d, new object[] { enabled });
            }
            else
            {
                pbYtMusicManage.Enabled = enabled;
                if (enabled)
                {
                    EnableTrayPauseResume(true);
                    pbYtMusicManage.Image = Properties.Resources.ytmusic_manage;
                }
                else
                {
                    pbYtMusicManage.Image = Properties.Resources.ytmusic_manage_disabled;
                    EnableTrayPauseResume(false);
                }
            }
        }

        delegate void SetManageTYMusicButtonImageDelegate(Image image);
        public void SetManageTYMusicButtonImage(Image image)
        {
            if (pbYtMusicManage.InvokeRequired)
            {
                var d = new SetManageTYMusicButtonImageDelegate(SetManageTYMusicButtonImage);
                Invoke(d, new object[] { image });
            }
            else
                pbYtMusicManage.Image = image;
        }

        delegate void SetAboutButtonImageDelegate(Image image);
        public void SetAboutButtonImage(Image image)
        {
            if (pbAbout.InvokeRequired)
            {
                var d = new SetAboutButtonImageDelegate(SetAboutButtonImage);
                Invoke(d, new object[] { image });
            }
            else
                pbAbout.Image = image;
        }

        delegate void SetLogButtonImageDelegate(Image image);
        public void SetLogButtonImage(Image image)
        {
            if (pbLog.InvokeRequired)
            {
                var d = new SetLogButtonImageDelegate(SetLogButtonImage);
                Invoke(d, new object[] { image });
            }
            else
                pbLog.Image = image;
        }

        delegate void SetUpdateButtonImageDelegate(Image image);
        public void SetUpdateButtonImage(Image image)
        {
            if (pbUpdate.InvokeRequired)
            {
                var d = new SetUpdateButtonImageDelegate(SetUpdateButtonImage);
                Invoke(d, new object[] { image });
            }
            else
                pbUpdate.Image = image;
        }

        delegate void SetThrottleSpeedDelegate(string mbps);
        public void SetThrottleSpeed(string mbps)
        {
            if (tbThrottleSpeed.InvokeRequired)
            {
                var d = new SetThrottleSpeedDelegate(SetThrottleSpeed);
                Invoke(d, new object[] { mbps });
            }
            else
            {
                if (mbps == "0" || mbps == "-1" || mbps == "∞")
                    tbThrottleSpeed.Text = "∞";
                else
                {
                    tbThrottleSpeed.Text = mbps.ToString();
                }
            }
        }

        delegate void SetVersionDelegate(string version);
        public void SetVersion(string version)
        {
            if (lblVersion.InvokeRequired)
            {
                var d = new SetVersionDelegate(SetVersion);
                Invoke(d, new object[] { version });
            }
            else
            {
                lblVersion.Text = version;
            }
        }


        delegate void SetVersionWarningVisibleDelegate(bool visible);
        public void SetVersionWarningVisible(bool visible)
        {
            if (pbUpdate.InvokeRequired)
            {
                var d = new SetVersionWarningVisibleDelegate(SetVersionWarningVisible);
                Invoke(d, new object[] { visible });
            }
            else
            {
                pbUpdate.Visible = visible;
            }
        }

        delegate void SetUploadingMessageDelegate(
                        string text,
                        string tooltipText = null,
                        Image artworkImage = null,
                        bool changingArtworkImage = false);
        public void SetUploadingMessage(
                        string text,
                        string tooltipText = null,
                        Image artworkImage = null,
                        bool changingArtworkImage = false)
        {
            if (lblUploadingMessage.InvokeRequired ||
                pbArtwork.InvokeRequired ||
                pbArtworkIdle.InvokeRequired)
            {
                var d = new SetUploadingMessageDelegate(SetUploadingMessage);
                Invoke(d, new object[] { text, tooltipText, artworkImage, changingArtworkImage });
            }
            else
            {
                lblUploadingMessage.Text = text;

                try
                {
                    if (artworkImage == null && changingArtworkImage)
                    {
                        ArtworkImage = null;
                        pbArtwork.Visible = false;
                        pbArtworkIdle.Visible = true;
                    }
                    else if (artworkImage != null && changingArtworkImage)
                    {
                        pbArtworkIdle.Visible = false;
                        pbArtwork.Visible = true;
                        ArtworkImage = artworkImage;
                        pbArtwork.Image = artworkImage;

                        if (tooltipText != null)
                            ArtWorkTooltip.SetToolTip(pbArtwork, tooltipText);
                    }
                    else
                    {
                        if (tooltipText != null)
                            ArtWorkTooltip.SetToolTip(pbArtwork, tooltipText);
                    }
                }
                catch { }
            }
        }

        delegate void SetDiscoveredFilesLabelDelegate(string text);
        public void SetDiscoveredFilesLabel(string text)
        {
            if (lblDiscoveredFiles.InvokeRequired)
            {
                var d = new SetDiscoveredFilesLabelDelegate(SetDiscoveredFilesLabel);
                Invoke(d, new object[] { text });
            }
            else
            {
                lblDiscoveredFiles.Text = text;
            }
        }

        delegate void SetIssuesLabelDelegate(string text);
        public void SetIssuesLabel(string text)
        {
            if (lblIssues.InvokeRequired)
            {
                var d = new SetIssuesLabelDelegate(SetIssuesLabel);
                Invoke(d, new object[] { text });
            }
            else
            {
                lblIssues.Text = text;
            }
        }

        delegate string GetIssuesLabelDelegate();
        public string GetIssuesLabel()
        {
            if (lblIssues.InvokeRequired)
            {
                var d = new GetIssuesLabelDelegate(GetIssuesLabel);
                return (string)Invoke(d, new object[] { });
            }
            else
            {
                return lblIssues.Text;
            }
        }

        delegate void SetUploadedLabelDelegate(string text);
        public void SetUploadedLabel(string text)
        {
            if (lblUploaded.InvokeRequired)
            {
                var d = new SetUploadedLabelDelegate(SetUploadedLabel);
                Invoke(d, new object[] { text });
            }
            else
            {
                lblUploaded.Text = text;
            }
        }

        delegate string GetUploadLabelDelegate();
        public string GetUploadLabel()
        {
            if (lblUploaded.InvokeRequired)
            {
                var d = new GetUploadLabelDelegate(GetUploadLabel);
                return (string)Invoke(d, new object[] { });
            }
            else
            {
                return lblUploaded.Text;
            }
        }

        delegate Task BindWatchFoldersListDelegate();
        public async Task BindWatchFoldersList()
        {
            if (lbWatchFolders.InvokeRequired)
            {
                var d = new BindWatchFoldersListDelegate(BindWatchFoldersList);
                Invoke(d, new object[] { });
            }
            else
            {
                WatchFolders = await WatchFolderRepo.Load();
                foreach (var watchFolder in WatchFolders)
                {
                    if (watchFolder.Path == @"%USERPROFILE%\Music")
                        watchFolder.Path = DirectoryHelper.EllipsisPath(DirectoryHelper.GetPath(KnownFolder.Music), 100);
                    else
                        DirectoryHelper.EllipsisPath(watchFolder.Path, 100);
                }

                lbWatchFolders.DataSource = new BindingSource(WatchFolders, null);
                lbWatchFolders.DisplayMember = "Path";
                lbWatchFolders.ValueMember = "Id";

                lbWatchFolders.SelectedIndex = -1;
            }
        }

        delegate void SetConnectToYouTubeButtonEnabledDelegate(bool enabled);
        public void SetConnectToYouTubeButtonEnabled(bool enabled)
        {
            if (btnConnectToYoutube.InvokeRequired)
            {
                var d =
                    new SetConnectToYouTubeButtonEnabledDelegate(SetConnectToYouTubeButtonEnabled);
                Invoke(d, new object[] { enabled });
            }
            else
            {
                btnConnectToYoutube.Enabled = enabled;
            }
        }

        delegate void EnableTrayPauseResumeDelegate(bool enabled);
        public void EnableTrayPauseResume(bool enabled)
        {
            if (TrayContextMenuStrip.InvokeRequired || btnConnectToYoutube.InvokeRequired)
            {
                var d =
                    new EnableTrayPauseResumeDelegate(EnableTrayPauseResume);
                Invoke(d, new object[] { enabled });
            }
            else
            {
                if (enabled)
                {
                    PauseResumeEnabled = true;
                    tsmPauseResume.ForeColor = Color.Black;

                    if (IsPausedFromTray)
                    {
                        tsmPauseResume.Text = "Resume";
                        tsmPauseResume.Image = Properties.Resources.resume;
                    }
                    else
                    {
                        tsmPauseResume.Text = "Pause";
                        tsmPauseResume.Image = Properties.Resources.pause;
                    }
                }
                else
                {
                    PauseResumeEnabled = false;
                    tsmPauseResume.Text = "Pause";
                    tsmPauseResume.ForeColor = SystemColors.ControlDark;
                    tsmPauseResume.Image = Properties.Resources.pause_disabled;
                }
            }
        }

        delegate void TrayMenuPauseDelegate(bool pause);
        private void TrayMenuPause(bool pause)
        {
            if (TrayContextMenuStrip.InvokeRequired || btnConnectToYoutube.InvokeRequired)
            {
                var d =
                    new TrayMenuPauseDelegate(TrayMenuPause);
                Invoke(d, new object[] { pause });
            }
            else
            {
                if (pause)
                {
                    tsmPauseResume.Text = "Resume";
                    tsmPauseResume.Image = Properties.Resources.resume;
                    IsPausedFromTray = true;
                    Requests.UploadCheckCache.Pause = true;
                    ManagingYTMusicStatus = MainForm.ManagingYTMusicStatusEnum.Showing;
                    SetPaused(true);
                }
                else
                {
                    tsmPauseResume.Text = "Pause";
                    tsmPauseResume.Image = Properties.Resources.pause;
                    IsPausedFromTray = false;
                    Requests.UploadCheckCache.Pause = false;
                    ManagingYTMusicStatus = ManagingYTMusicStatusEnum.CloseChanges;
                    SetPaused(false);
                    Restart();
                    ManagingYTMusicStatus = ManagingYTMusicStatusEnum.CloseChangesComplete;
                }
            }
        }

        delegate void ShowFormDelegate();
        public void ShowForm()
        {
            if (InvokeRequired)
            {
                var d = new ShowFormDelegate(ShowForm);
                Invoke(d, new object[] { });
            }
            else
            {
                try
                {
                    Show();

                    if (Opacity == 0)
                        CenterForm();

                    if (WindowState == FormWindowState.Minimized)
                    {
                        WindowState = FormWindowState.Normal;
                        if (StartHidden)
                        {
                            StartHidden = false;
                            CenterForm();
                        }
                    }

                    ShowInTaskbar = true;
                    Opacity = 1;
                }
                catch { }

                try
                {
                    Activate();
                }
                catch { }
            }
        }
    }
}
