# RSC15toSC88 

SC-88 用 RS-MIDI ブリッジ

Copyright (c) 2021 ooltcloud


# 概要

&emsp;
RSC15AT を使用して SC-88 を 32ch 鳴らすための RS-MIDI ブリッジです。

&emsp;
かつては Roland 純正のシリアルドライバー (*1) がありましたが、しかしこれは 32bit ドライバーであり 64bit Windows では使用できないため、その代替として機能するソフトウェアです。


# 主な機能
- MIDI 端子を使用せず Conputer 端子と PC の COM ポートを接続することで演奏します。
- 1 本のケーブルで 32ch の演奏ができます。


# 動作に必要な環境

- ソフトウェア
	- Microsoft .net Framework 4.7.2
	- loopMIDI (*2)
	- その他 MIDI Player（TMIDI Player (*3) など）
　
- ハードウェア
	- Roland SC-88 等
	- RSC-15AT 等


# 使用方法

- インストール<br>
	&emsp;
	適当なディレクトリに解凍したファイルをコピーしてください。

- アンインストール<br>
	&emsp;
	インストールしたディレクトリ毎削除してください。

- 起動前準備<br>

	&emsp;
	本ソフトウェアの起動前に以下を行ってください。

	- loopMIDI のインストールおよび起動します。
	- loopMIDI の仮想 MIDI ポートを 2port 作成します。
	- 作成した仮想 MIDI ポートを MIDI プレイヤー（TMIDI Player 等）に割り当てます。

- 操作
	- MIDI In に MIDI プレイヤーに割り当てたloopMIDI の仮想ポートを設定します。
	- SC-88 等 MIDI モジュールが接続されている COM ポートを設定します。（USB-Serial 変換ケーブル可）
	- "Start" ボタンを押すとブリッジ処理を開始します。（ボタンの名称が "Stop" に変わります。）
	- MIDI プレイヤーを使用し MIDI データの演奏を行います。

- その他
	- RSC15toSC88 を起動してから USB-Serial 変換ケーブルを接続した場合は "Reload Portlist" ボタンを押します。そうするとプルダウンのリストが更新されます。
	- "Stop" ボタンを押すことで、ブリッジ処理を終了します。
	- RSC15toSC88 を終了したときの設定を記憶します。このため、ブリッジ処理中にウインドウを閉じた場合、次回起動時はブリッジ処理を自動で開始します。
	- 音源モジュールからの MIDI In には対応していません。


# 使用と配布、および制約

&emsp;
使用および配布は自由に行ってもらって構いません。

&emsp;
ただし、本ソフトウェアの使用によって、いかなる結果になっても責任はもてませんし、動作保証もしません。

&emsp;
また本ソフトウェアは MIT ライセンスに基づき公開しています。


# その他の選択肢

&emsp;
16ch の再生でよいなら、おそらく Hairless MIDI<->Serial Bridge (*4) のほうが再生品質がよいかもしれないので、そちらの使用も検討してみてください。（音源モジュールからの MIDI In にも対応しています。）



---
(*1) Roland シリアルドライバー<br>
https://www.roland.com/jp/support/by_product/fp-2/updates_drivers/d8ea50b0-a429-42fd-b13d-b4a657eb569d)

(*2) loopMIDI<br>
https://www.tobias-erichsen.de/software/loopmidi.html

(*3) TMIDI Player<br>
https://hp.vector.co.jp/authors/VA010012/

(*4) Hairless MIDI<->Serial Bridge<br>
https://projectgus.github.io/hairless-midiserial/