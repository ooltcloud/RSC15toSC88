using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using RtMidi.Core;
using RtMidi.Core.Devices;
using RtMidi.Core.Messages;

using System.IO.Ports;

namespace RSC15toSC88
{
    /// <summary>
    /// メインフォーム
    /// </summary>
    public partial class MainForm:Form
    {
        /// <summary>
        /// MIDI Port A
        /// </summary>
        private Midi2RsBridge _midiportA;

        /// <summary>
        /// MIDI Port B
        /// </summary>
        private Midi2RsBridge _midiportB;

        /// <summary>
        /// シリアルポート
        /// </summary>
        private ComWrapper _com;
        
        /// <summary>
        /// 実行状態
        /// </summary>
        private bool _exec = false;

        /// <summary>
        /// Form
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ポート名リストの取得
        /// </summary>
        private void reload()
        {
            this.MidiPortAComboBox.Items.Clear();
            this.MidiPortAComboBox.Items.Add("");

            this.MidiPortBComboBox.Items.Clear();
            this.MidiPortBComboBox.Items.Add("");

            foreach (var inputDeviceInfo in MidiDeviceManager.Default.InputDevices)
            {
                this.MidiPortAComboBox.Items.Add(inputDeviceInfo.Name);
                this.MidiPortBComboBox.Items.Add(inputDeviceInfo.Name);
            }

            this.ComPortComboBox.Items.Clear();
            this.ComPortComboBox.Items.Add("");

            foreach (var comName in SerialPort.GetPortNames())
            {
                this.ComPortComboBox.Items.Add(comName);
            }
        }

        /// <summary>
        /// ブリッジ開始
        /// </summary>
        private void start()
        {
            try
            {
                // 設定の確認
                if (this.ComPortComboBox.Text == "")
                {
                    throw new ApplicationException(
                        "シリアルポートを設定してください。\n" +
                        "(Select the Serial Out.)"
                    );
                }

                if (this.MidiPortAComboBox.Text == this.MidiPortBComboBox.Text)
                {
                    if (this.MidiPortAComboBox.Text == "")
                    {
                        throw new ApplicationException(
                            "MIDI ポートを設定してください。\n" +
                            "(Select the MIDI In.)"
                        );
                    }
                    else
                    {
                        throw new ApplicationException(
                            "MIDI ポート A と MIDI ポート B は異なる MIDI ポートを設定してください。\n" +
                            "(Select the MIDI In Port B different from Port A.)"
                        );
                    }
                }

                // New & Open
                try
                {
                    // New
                    _com = new ComWrapper(this.ComPortComboBox.Text);
                    _midiportA = new Midi2RsBridge(_com, false);
                    _midiportB = new Midi2RsBridge(_com, true);

                    // COM Port Open
                    try
                    {
                        _com.Open();
                    }
                    catch
                    {
                        throw new ApplicationException(
                            "シリアルポート が開けません。\n" +
                            "(Serial port open is fail.)"
                        );
                    }

                    var devs = MidiDeviceManager.Default.InputDevices;

                    // MIDI Port A Open
                    if (this.MidiPortAComboBox.Text != "")
                    {
                        try
                        {
                            var midi = (from x in devs
                                        where x.Name == this.MidiPortAComboBox.Text
                                        select x).ToArray();

                            _midiportA.Open(midi[0].CreateDevice());
                        }
                        catch
                        {
                            throw new ApplicationException(
                                "MIDI ポート A が開けません。\n" + 
                                "(MIDI In Port A open is fail.)"
                            );
                        }
                    }

                    // MIDI Port B Open
                    if (this.MidiPortBComboBox.Text != "")
                    {
                        try
                        {
                            var midi = (from x in devs
                                        where x.Name == this.MidiPortBComboBox.Text
                                        select x).ToArray();

                            _midiportB.Open(midi[0].CreateDevice());
                        }
                        catch
                        {
                            throw new ApplicationException(
                                "MIDI ポート B が開けません。\n" + 
                                "(MIDI In Port B open is fail.)"
                            );
                        }
                    }

                }
                catch (ApplicationException ex)
                {
                    // 中断に伴う後始末
                    _com.Close();
                    _midiportA.Close();
                    _midiportB.Close();

                    throw;
                }

            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message, "RSC15toSC88", MessageBoxButtons.OK);
                return;
            }

            // ランプ表示
            this.ComPortLampDisplay.BackColor = Color.LawnGreen;

            if (_midiportA.IsOpen == true)
            {
                this.MidiPortALampDisplay.BackColor = Color.LawnGreen;
            }

            if (_midiportB.IsOpen == true)
            {
                this.MidiPortBLampDisplay.BackColor = Color.LawnGreen;
            }

            // 選択禁止
            this.MidiPortAComboBox.Enabled = false;
            this.MidiPortBComboBox.Enabled = false;
            this.ComPortComboBox.Enabled = false;
            this.ReloadPortlistButton.Enabled = false;

            // ボタン名変更
            this.StartButton.Text = "Stop";

            // 実行中
            _exec = true;

        }

        /// <summary>
        /// ブリッジ停止
        /// </summary>
        void stop()
        {
            // Close
            _midiportA.Close();
            _midiportB.Close();
            _com.Close();

            // ランプ表示
            this.MidiPortALampDisplay.BackColor = Color.Gray;
            this.MidiPortBLampDisplay.BackColor = Color.Gray;
            this.ComPortLampDisplay.BackColor = Color.Gray;

            // 選択許可
            this.MidiPortAComboBox.Enabled = true;
            this.MidiPortBComboBox.Enabled = true;
            this.ComPortComboBox.Enabled = true;
            this.ReloadPortlistButton.Enabled = true;

            // ボタン名変更
            this.StartButton.Text = "Start";

            // 停止
            _exec = false;
        }

        /// <summary>
        /// ポート名リストの再取得
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReloadPortlistButton_Click(object sender, EventArgs e)
        {
            reload();
        }

        /// <summary>
        /// 実行／停止操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartButton_Click(object sender, EventArgs e)
        {
            if (_exec == false)
            {
                start();
            }
            else
            {
                stop();
            }
        }

        /// <summary>
        /// 起動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Shown(object sender, EventArgs e)
        {
            // ポート名リストの登録
            reload();

            // 前回設定の呼び戻し
            this.MidiPortAComboBox.Text = Properties.Settings.Default.MIDIportA;
            this.MidiPortBComboBox.Text = Properties.Settings.Default.MIDIportB;
            this.ComPortComboBox.Text = Properties.Settings.Default.COMport;

            if (Properties.Settings.Default.IsExecute == true)
            {
                // 前回実行中で終了していたら自動実行
                start();
            }
        }

        /// <summary>
        /// 終了
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 設定の記録
            Properties.Settings.Default.MIDIportA = this.MidiPortAComboBox.Text;
            Properties.Settings.Default.MIDIportB = this.MidiPortBComboBox.Text;
            Properties.Settings.Default.COMport = this.ComPortComboBox.Text;
            Properties.Settings.Default.IsExecute = _exec;

            Properties.Settings.Default.Save();
        }

    }
}
