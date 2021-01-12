using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//  RtMidi.Core
//  Copyright (c) 2017 Michael Dahl
//
//  RtMidi: realtime MIDI i/o C++ classes
//  Copyright (c) 2003-2017 Gary P. Scavone
//
using RtMidi.Core;
using RtMidi.Core.Devices;
using RtMidi.Core.Messages;

namespace RSC15toSC88
{
    /// <summary>
    /// MIDI port to Serial port Bridge
    /// </summary>
    /// <remarks>
    /// 参考文献
    /// 　MIDI SOUND GENERATOR SC-88 Pro - Roland 
    /// 　　http://lib.roland.co.jp/support/jp/manuals/res/1810289/SC-88Pro_j9.pdf
    /// 　　0x8n～0xEn, 0xF0...0xF7 (sysEx) のメッセージについてはこちらを参考にしました。
    /// 　
    /// 　MIDI 1.0 規格書 - 音楽電子事業協会
    /// 　　http://amei.or.jp/midistandardcommittee/MIDI1.0.pdf
    /// 　　0xF1, 0xF2, 0xF3, 0xF6 のメッセージについてはこちらを参考にしました。
    /// 　　
    /// </remarks>
    class Midi2RsBridge
    {
        private IMidiInputDevice _midiport;

        /// <summary>
        /// シリアルポート
        /// </summary>
        private ComWrapper _comport;

        /// <summary>
        /// 自身の Part 属性 (PartA = false, PartB = true)
        /// </summary>
        private bool _part;

        /// <summary>
        /// Open 状態
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// New
        /// </summary>
        /// <param name="com">出力先シリアルポート</param>
        /// <param name="port">MIDI ポート (portA=False, portB=True)</param>
        public Midi2RsBridge(ComWrapper com, bool port)
        {
            _comport = com;
            _part = port;
        }

        /// <summary>
        /// ノート・オフ (0x8n)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        private void NoteOffMessageHandler(IMidiInputDevice sender, in NoteOffMessage msg)
        {
            var bytes = new List<Byte>();

            bytes.Add((byte)(0x80 | (int)msg.Channel));
            bytes.Add((byte)msg.Key);
            bytes.Add((byte)msg.Velocity);

            _comport.Send(bytes.ToArray(), _part);

        }

        /// <summary>
        /// ノート・オン (0x9n)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        private void NoteOnMessageHandler(IMidiInputDevice sender, in NoteOnMessage msg)
        {
            var bytes = new List<Byte>();

            bytes.Add((byte)(0x90 | (int)msg.Channel));
            bytes.Add((byte)msg.Key);
            bytes.Add((byte)msg.Velocity);

            _comport.Send(bytes.ToArray(), _part);

        }

        /// <summary>
        /// ポリフォニック・キー・プレッシャー (0xAn)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        private void PolyphonicKeyPressureMessageHandler(IMidiInputDevice sender, in PolyphonicKeyPressureMessage msg)
        {
            var bytes = new List<Byte>();

            bytes.Add((byte)(0xA0 | (int)msg.Channel));
            bytes.Add((byte)msg.Key);
            bytes.Add((byte)msg.Pressure);

            _comport.Send(bytes.ToArray(), _part);

        }

        /// <summary>
        /// コントロール・チェンジ (0xBn)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        private void ControlChangeMessageHandler(IMidiInputDevice sender, in ControlChangeMessage msg)
        {
            var bytes = new List<Byte>();

            bytes.Add((byte)(0xB0 | (int)msg.Channel));
            bytes.Add((byte)msg.Control);
            bytes.Add((byte)msg.Value);

            _comport.Send(bytes.ToArray(), _part);

        }

        /// <summary>
        /// プログラム・チェンジ (0xCn)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        private void ProgramChangeMessageHandler(IMidiInputDevice sender, in ProgramChangeMessage msg)
        {
            var bytes = new List<Byte>();

            bytes.Add((byte)(0xC0 | (int)msg.Channel));
            bytes.Add((byte)msg.Program);

            _comport.Send(bytes.ToArray(), _part);

        }

        /// <summary>
        /// チャンネル・プレッシャー (0xDn)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        private void ChannelPressureMessageHandler(IMidiInputDevice sender, in ChannelPressureMessage msg)
        {
            var bytes = new List<Byte>();

            bytes.Add((byte)(0xD0 | (int)msg.Channel));
            bytes.Add((byte)msg.Pressure);

            _comport.Send(bytes.ToArray(), _part);

        }

        /// <summary>
        /// ピッチ・ベンド・チェンジ (0xEn)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        private void PitchBendMessageHandler(IMidiInputDevice sender, in PitchBendMessage msg)
        {
            var v = msg.Value;
            var vl = v & 0x7F;
            var vh = (v & 0x3F80) >> 7;

            var bytes = new List<Byte>();
            bytes.Add((byte)(0xE0 | (int)msg.Channel));
            bytes.Add((byte)vl);
            bytes.Add((byte)vh);

            _comport.Send(bytes.ToArray(), _part);
        }

        /// <summary>
        /// NRPN 値のデコード値の通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        /// <remarks>
        /// ----
        /// NOP（ここにはこない）
        /// NRPN mode は off（=NRPN はデコードせず素の Control Change として通知) としているため）
        /// ----
        /// このイベントは、NRPM モード（.SetNrpnMode) を On または OnSendControlChange としたときに、
        /// Control Change のうちの NRPN メッセージを受信すると、その値をデコードして呼び出される。
        /// 
        /// 例えば、以下の 4 つの Control Change を受け取った場合。
        /// 　0xB1 0x63 0x01
        /// 　0xB1 0x62 0x20
        /// 　0xB1 0x06 0x32
        /// 　0xB1 0x26 0x00
        /// 
        /// NRPN mode が On のときは、
        ///   msg.Channel   = 1
        ///   msg.Parameter = 0x00A0 (000 0001 010 0000) 
        ///   msg.Value     = 0x1900 (011 0010 000 0000)
        /// として伝えられる。
        /// また、このとき、元の Control Change のメッセージは ControlChangeMessageHandler には伝えられない。
        /// 
        /// NRPN mode が Off のときは、NrpnMessageHandler は呼び出されず、元の Control Change がそのまま
        /// ControlChangeMessageHandler に伝えられる。
        /// 
        /// NRPN mode を OnSendControlChange にすると、NrpnMessageHandler にも ControlChangeMessageHandler
        /// 両方に伝えられる。
        /// 
        /// 今回は NRPN の値が知りたいわけではなく Control Change の値がそのまま中継できればいいので
        /// NRPN mode は off としている。
        /// 
        /// </remarks>
        private void NrpnMessageHandler(IMidiInputDevice sender, in NrpnMessage msg)
        {
            // NOP
        }

        /// <summary>
        /// システム・エクスクルーシブ・メッセージ (0xF0 .... 0xF7)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        private void SysExMessageHandler(IMidiInputDevice sender, in SysExMessage msg)
        {
            var bytes = new List<byte>();
            bytes.Add(0xF0);
            bytes.AddRange(msg.Data);
            bytes.Add(0xF7);

            _comport.Send(bytes.ToArray(), _part);

        }

        /// <summary>
        /// MIDI タイムコード・クォーター・フレーム (0xF1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        /// <remarks>SC-88では使用していない? (マニュアルに未記載?)</remarks>
        private void MidiTimeCodeQuarterFrameHandler(IMidiInputDevice sender, in MidiTimeCodeQuarterFrameMessage msg)
        {
            var vh = msg.MessageType;
            var vl = msg.Values;
            var v = vh << 4 + vl;

            var bytes = new List<Byte>();
            bytes.Add((byte)0xF1);
            bytes.Add((byte)v);

            _comport.Send(bytes.ToArray(), _part);
 
        }

        /// <summary>
        /// ソング・ポジション・ポインター (0xF2)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        /// <remarks>SC-88では使用していない? (マニュアルに未記載?)</remarks>
        private void SongPositionPointerHandler(IMidiInputDevice sender, in SongPositionPointerMessage msg)
        {
            var v = msg.MidiBeats;
            var vl = v & 0x7F;
            var vh = (v & 0x3F80) >> 7;

            var bytes = new List<Byte>();
            bytes.Add((byte)0xF2);
            bytes.Add((byte)vl);
            bytes.Add((byte)vh);

            _comport.Send(bytes.ToArray(), _part);

        }

        /// <summary>
        /// ソング・セレクト (0xF3)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        /// <remarks>SC-88では使用していない? (マニュアルに未記載?)</remarks>
        private void SongSelectHandler(IMidiInputDevice sender, in SongSelectMessage msg)
        {
            var bytes = new List<Byte>();
            bytes.Add((byte)0xF3);
            bytes.Add((byte)msg.Song);

            _comport.Send(bytes.ToArray(), _part);
 
        }

        /// <summary>
        /// チューン・リクエスト (0xF6)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        /// <remarks>SC-88では使用していない? (マニュアルに未記載?)</remarks>
        private void TuneRequestHandler(IMidiInputDevice sender, in TuneRequestMessage msg)
        {
            var bytes = new List<Byte>();
            bytes.Add((byte)0xF6);

            _comport.Send(bytes.ToArray(), _part);
 
        }

        /// <summary>
        /// Port Open
        /// </summary>
        /// <param name="dev"></param>
        public void Open(IMidiInputDevice midiport)
        {
            _midiport = midiport;

            _midiport.NoteOff += NoteOffMessageHandler;
            _midiport.NoteOn += NoteOnMessageHandler;
            _midiport.PolyphonicKeyPressure += PolyphonicKeyPressureMessageHandler;
            _midiport.ControlChange += ControlChangeMessageHandler;
            _midiport.ProgramChange += ProgramChangeMessageHandler;
            _midiport.ChannelPressure += ChannelPressureMessageHandler;
            _midiport.PitchBend += PitchBendMessageHandler;
            _midiport.Nrpn += NrpnMessageHandler;
            _midiport.SysEx += SysExMessageHandler;
            _midiport.MidiTimeCodeQuarterFrame += MidiTimeCodeQuarterFrameHandler;
            _midiport.SongPositionPointer += SongPositionPointerHandler;
            _midiport.SongSelect += SongSelectHandler;
            _midiport.TuneRequest += TuneRequestHandler;

            // NRPN をデコードしない
            _midiport.SetNrpnMode(RtMidi.Core.Devices.Nrpn.NrpnMode.Off);

            _midiport.Open();
            this.IsOpen = true;

        }

        /// <summary>
        /// Port Close
        /// </summary>
        public void Close()
        {
            if (this.IsOpen == true)
            {
                _midiport.Close();
                this.IsOpen = false;
            }
        }
    }
}