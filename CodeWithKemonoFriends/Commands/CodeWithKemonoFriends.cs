using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using Windows.UI.Notifications;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using CodeWithKemonoFriends.Model;
using System.Linq;

namespace CodeWithKemonoFriends.Commands
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class CodeWithKemonoFriends
    {
        #region private member

        /// <summary>
        /// ビルドイベント
        /// </summary>
        private readonly BuildEvents _buildEvents;

        /// <summary>
        /// デバッガイベント
        /// </summary>
        private readonly DebuggerEvents _debuggerEvents;

        /// <summary>
        /// 有効かどうか
        /// </summary>
        private bool _isEnabled;

        /// <summary>
        /// 通知器
        /// </summary>
        private readonly ToastNotifier _notifier;

        /// <summary>
        /// このプロジェクトのディレクトリ
        /// </summary>
        private readonly string _projectDirectory;

        /// <summary>
        /// 最後の通知
        /// </summary>
        private ToastNotification _lastNotification;

        /// <summary>
        /// すべてのプロジェクトのビルド結果
        /// </summary>
        private readonly List<bool> _projectsResults = new List<bool>();

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package _package;

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider => _package;

        #endregion

        #region public member

        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("7c211e49-d5d8-47d1-a42c-f921d70fd94a");

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static CodeWithKemonoFriends Instance { get; private set; }

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeWithKemonoFriends"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private CodeWithKemonoFriends(Package package)
        {
            _package = package ?? throw new ArgumentNullException(nameof(package));

            OleMenuCommandService commandService = ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandId = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(MenuItemCallback, menuCommandId);
                commandService.AddCommand(menuItem);
            }

            // このプロジェクトのディレクトリを取得
            var codebase = typeof(CodeWithKemonoFriends).Assembly.CodeBase;
            var uri = new Uri(codebase, UriKind.Absolute);
            _projectDirectory = Path.GetDirectoryName(uri.LocalPath);

            // イベントを初期化
            var dte = (DTE)ServiceProvider.GetService(typeof(DTE));
            _buildEvents = dte.Events.BuildEvents;
            _debuggerEvents = dte.Events.DebuggerEvents;

            // Notifierを初期化
            var appId = "VisualStudio." + dte.RegistryRoot.Split('_').Last();
            _notifier = ToastNotificationManager.CreateToastNotifier(appId);
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new CodeWithKemonoFriends(package);
        }

        #endregion

        #region event handler

        /// <summary>
        /// コマンド実行時イベントハンドラ
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            var logoPath = Path.Combine(_projectDirectory, @"Resources\Logo.ico");
            if (_isEnabled)
            {
                // イベントハンドラを解除
                _buildEvents.OnBuildBegin -= BuildEvents_OnBuildBegin;
                _buildEvents.OnBuildDone -= BuildEvents_OnBuildDone;
                _buildEvents.OnBuildProjConfigDone -= BuildEvents_OnBuildProjectDone;
                _debuggerEvents.OnEnterBreakMode -= DebuggerEvents_OnEnterBreakMode;

                // 通知
                Notify(logoPath, string.Empty, "フレンズが帰っていきました", false);
            }
            else
            {
                // イベントハンドラを追加
                _buildEvents.OnBuildBegin += BuildEvents_OnBuildBegin;
                _buildEvents.OnBuildDone += BuildEvents_OnBuildDone;
                _buildEvents.OnBuildProjConfigDone += BuildEvents_OnBuildProjectDone;
                _debuggerEvents.OnEnterBreakMode += DebuggerEvents_OnEnterBreakMode;

                // 通知
                Notify(logoPath, string.Empty, "フレンズがやってきました", false);
            }
            _isEnabled = !_isEnabled;
        }

        /// <summary>
        /// ビルド開始時イベントハンドラ
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="action"></param>
        private void BuildEvents_OnBuildBegin(vsBuildScope scope, vsBuildAction action)
        {
            // ビルド結果をクリア
            _projectsResults.Clear();
        }

        /// <summary>
        /// ビルド終了時イベントハンドラ
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="action"></param>
        private void BuildEvents_OnBuildDone(vsBuildScope scope, vsBuildAction action)
        {
            // 一つでもビルドに失敗した場合
            if (_projectsResults.Contains(false))
            {
                var friends = Friends.GetRandomFailedMessage();
                var path = Path.Combine(_projectDirectory, @"Resources\Icons\", friends.IconFileName);
                Notify(path, friends.Speaker, friends.Message);
            }
            // 全てのビルドが成功した場合
            else
            {
                var friends = Friends.GetRandomPassedMessage();
                var path = Path.Combine(_projectDirectory, @"Resources\Icons\", friends.IconFileName);
                Notify(path, friends.Speaker, friends.Message);
            }
        }

        /// <summary>
        /// プロジェクトのビルド終了時イベントハンドラ
        /// </summary>
        /// <param name="project"></param>
        /// <param name="projectconfig"></param>
        /// <param name="platform"></param>
        /// <param name="solutionconfig"></param>
        /// <param name="success"></param>
        private void BuildEvents_OnBuildProjectDone(string project, string projectconfig, string platform, string solutionconfig, bool success)
        {
            // ビルド結果を追加
            _projectsResults.Add(success);
        }

        /// <summary>
        /// デバッガで中断モード突入時イベントハンドラ
        /// </summary>
        /// <param name="reason"></param>
        /// <param name="executionaction"></param>
        private void DebuggerEvents_OnEnterBreakMode(dbgEventReason reason, ref dbgExecutionAction executionaction)
        {

            // 未補足の例外が発生した場合
            if (reason == dbgEventReason.dbgEventReasonExceptionNotHandled)
            {
                var friends = Friends.GetRandomFailedMessage();
                var path = Path.Combine(_projectDirectory, @"Resources\Icons\", friends.IconFileName);
                Notify(path, friends.Speaker, friends.Message);
            }
        }
        #endregion

        #region private member

        /// <summary>
        /// 通知センターに通知を送ります
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="caption"></param>
        /// <param name="text"></param>
        /// <param name="silent"></param>
        private void Notify(string imagePath, string caption, string text, bool silent = true)
        {
            var template = ToastTemplateType.ToastImageAndText02;
            var xml = ToastNotificationManager.GetTemplateContent(template);

            // 画像をセット
            var images = xml.GetElementsByTagName("image");
            var src = images[0].Attributes.GetNamedItem("src");
            if (src != null)
            {
                src.InnerText = imagePath;
            }

            // テキストをセット
            var texts = xml.GetElementsByTagName("text");
            texts[0].AppendChild(xml.CreateTextNode(caption));
            texts[1].AppendChild(xml.CreateTextNode(text));

            // 音を消す
            if (silent)
            {
                var audio = xml.CreateElement("audio");
                audio.SetAttribute("silent", "true");
                xml.GetElementsByTagName("toast")[0].AppendChild(audio);
            }

            // 前の通知があったら隠す
            if (_lastNotification != null)
            {
                _notifier.Hide(_lastNotification);
            }

            // 通知
            _lastNotification = new ToastNotification(xml);
            _notifier.Show(_lastNotification);
        }

        #endregion
    }
}
