using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Ports;

namespace RSC15toSC88
{
    /// <summary>
    /// RS232C 通信ラッパー
    /// </summary>
    class ComWrapper
    {
        /// <summary>
        /// 同期オブジェクト
        /// </summary>
        private static object _sendLcok;

        /// <summary>
        /// 前回要求パート (PartA = false, PartB = true)
        /// </summary>
        private static bool _prevPart = false;

        /// <summary>
        /// COM
        /// </summary>
        private SerialPort _comport;

        /// <summary>
        /// Open 状態
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// New
        /// </summary>
        /// <param name="portname"></param>
        public ComWrapper(string portname)
        {
            _sendLcok = new object();
            _comport = new SerialPort(portname);

        }

        /// <summary>
        /// 受信 (読み捨て)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SerialDataReceivedEventHandler(object sender, SerialDataReceivedEventArgs e)
        {
            // 読み捨てる
            var length = _comport.BytesToRead;
            var buff = new byte[length];
            _comport.Read(buff, 0, length);

        }

        /// <summary>
        /// 送信
        /// </summary>
        /// <param name="sendData"></param>
        /// <param name="part"></param>
        public void Send(byte[] sendData, bool part)
        {
            lock (ComWrapper._sendLcok)
            {
                if (_prevPart != part)
                {
                    if (part == false)
                    {
                        // Part A に切り替え
                        byte[] dat = { 0xF5, 0x01 };
                        _comport.Write(dat, 0, dat.Length);
                    }
                    else
                    {
                        // Part B に切り替え
                        byte[] dat = { 0xF5, 0x02 };
                        _comport.Write(dat, 0, dat.Length);

                    }
                    _prevPart = part;
                }

                _comport.Write(sendData, 0, sendData.Length);

            }
        }

        /// <summary>
        /// Open
        /// </summary>
        public void Open()
        {
            _comport.BaudRate = 38400;
            _comport.Parity = Parity.None;
            _comport.StopBits = StopBits.One;
            _comport.DataBits = 8;

            _comport.DataReceived += SerialDataReceivedEventHandler;

            _comport.Open();
            this.IsOpen = true;
        }

        /// <summary>
        /// Close
        /// </summary>
        public void Close()
        {
            _comport.Close();
            this.IsOpen = false;
        }
    }
}
