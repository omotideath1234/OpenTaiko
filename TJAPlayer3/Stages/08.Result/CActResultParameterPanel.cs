﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using SlimDX;
using FDK;

namespace TJAPlayer3
{
	internal class CActResultParameterPanel : CActivity
	{
		// コンストラクタ

		public CActResultParameterPanel()
		{
			ST文字位置[] st文字位置Array = new ST文字位置[11];
			ST文字位置 st文字位置 = new ST文字位置();
			st文字位置.ch = '0';
			st文字位置.pt = new Point(0, 0);
			st文字位置Array[0] = st文字位置;
			ST文字位置 st文字位置2 = new ST文字位置();
			st文字位置2.ch = '1';
			st文字位置2.pt = new Point(32, 0);
			st文字位置Array[1] = st文字位置2;
			ST文字位置 st文字位置3 = new ST文字位置();
			st文字位置3.ch = '2';
			st文字位置3.pt = new Point(64, 0);
			st文字位置Array[2] = st文字位置3;
			ST文字位置 st文字位置4 = new ST文字位置();
			st文字位置4.ch = '3';
			st文字位置4.pt = new Point(96, 0);
			st文字位置Array[3] = st文字位置4;
			ST文字位置 st文字位置5 = new ST文字位置();
			st文字位置5.ch = '4';
			st文字位置5.pt = new Point(128, 0);
			st文字位置Array[4] = st文字位置5;
			ST文字位置 st文字位置6 = new ST文字位置();
			st文字位置6.ch = '5';
			st文字位置6.pt = new Point(160, 0);
			st文字位置Array[5] = st文字位置6;
			ST文字位置 st文字位置7 = new ST文字位置();
			st文字位置7.ch = '6';
			st文字位置7.pt = new Point(192, 0);
			st文字位置Array[6] = st文字位置7;
			ST文字位置 st文字位置8 = new ST文字位置();
			st文字位置8.ch = '7';
			st文字位置8.pt = new Point(224, 0);
			st文字位置Array[7] = st文字位置8;
			ST文字位置 st文字位置9 = new ST文字位置();
			st文字位置9.ch = '8';
			st文字位置9.pt = new Point(256, 0);
			st文字位置Array[8] = st文字位置9;
			ST文字位置 st文字位置10 = new ST文字位置();
			st文字位置10.ch = '9';
			st文字位置10.pt = new Point(288, 0);
			st文字位置Array[9] = st文字位置10;
			ST文字位置 st文字位置11 = new ST文字位置();
			st文字位置11.ch = ' ';
			st文字位置11.pt = new Point(0, 0);
			st文字位置Array[10] = st文字位置11;
			this.st小文字位置 = st文字位置Array;

			ST文字位置[] st文字位置Array2 = new ST文字位置[11];
			ST文字位置 st文字位置12 = new ST文字位置();
			st文字位置12.ch = '0';
			st文字位置12.pt = new Point(0, 0);
			st文字位置Array2[0] = st文字位置12;
			ST文字位置 st文字位置13 = new ST文字位置();
			st文字位置13.ch = '1';
			st文字位置13.pt = new Point(32, 0);
			st文字位置Array2[1] = st文字位置13;
			ST文字位置 st文字位置14 = new ST文字位置();
			st文字位置14.ch = '2';
			st文字位置14.pt = new Point(64, 0);
			st文字位置Array2[2] = st文字位置14;
			ST文字位置 st文字位置15 = new ST文字位置();
			st文字位置15.ch = '3';
			st文字位置15.pt = new Point(96, 0);
			st文字位置Array2[3] = st文字位置15;
			ST文字位置 st文字位置16 = new ST文字位置();
			st文字位置16.ch = '4';
			st文字位置16.pt = new Point(128, 0);
			st文字位置Array2[4] = st文字位置16;
			ST文字位置 st文字位置17 = new ST文字位置();
			st文字位置17.ch = '5';
			st文字位置17.pt = new Point(160, 0);
			st文字位置Array2[5] = st文字位置17;
			ST文字位置 st文字位置18 = new ST文字位置();
			st文字位置18.ch = '6';
			st文字位置18.pt = new Point(192, 0);
			st文字位置Array2[6] = st文字位置18;
			ST文字位置 st文字位置19 = new ST文字位置();
			st文字位置19.ch = '7';
			st文字位置19.pt = new Point(224, 0);
			st文字位置Array2[7] = st文字位置19;
			ST文字位置 st文字位置20 = new ST文字位置();
			st文字位置20.ch = '8';
			st文字位置20.pt = new Point(256, 0);
			st文字位置Array2[8] = st文字位置20;
			ST文字位置 st文字位置21 = new ST文字位置();
			st文字位置21.ch = '9';
			st文字位置21.pt = new Point(288, 0);
			st文字位置Array2[9] = st文字位置21;
			ST文字位置 st文字位置22 = new ST文字位置();
			st文字位置22.ch = '%';
			st文字位置22.pt = new Point(0x37, 0);
			st文字位置Array2[10] = st文字位置22;
			this.st大文字位置 = st文字位置Array2;

			ST文字位置[] stScore文字位置Array = new ST文字位置[10];
			ST文字位置 stScore文字位置 = new ST文字位置();
			stScore文字位置.ch = '0';
			stScore文字位置.pt = new Point(0, 0);
			stScore文字位置Array[0] = stScore文字位置;
			ST文字位置 stScore文字位置2 = new ST文字位置();
			stScore文字位置2.ch = '1';
			stScore文字位置2.pt = new Point(51, 0);
			stScore文字位置Array[1] = stScore文字位置2;
			ST文字位置 stScore文字位置3 = new ST文字位置();
			stScore文字位置3.ch = '2';
			stScore文字位置3.pt = new Point(102, 0);
			stScore文字位置Array[2] = stScore文字位置3;
			ST文字位置 stScore文字位置4 = new ST文字位置();
			stScore文字位置4.ch = '3';
			stScore文字位置4.pt = new Point(153, 0);
			stScore文字位置Array[3] = stScore文字位置4;
			ST文字位置 stScore文字位置5 = new ST文字位置();
			stScore文字位置5.ch = '4';
			stScore文字位置5.pt = new Point(204, 0);
			stScore文字位置Array[4] = stScore文字位置5;
			ST文字位置 stScore文字位置6 = new ST文字位置();
			stScore文字位置6.ch = '5';
			stScore文字位置6.pt = new Point(255, 0);
			stScore文字位置Array[5] = stScore文字位置6;
			ST文字位置 stScore文字位置7 = new ST文字位置();
			stScore文字位置7.ch = '6';
			stScore文字位置7.pt = new Point(306, 0);
			stScore文字位置Array[6] = stScore文字位置7;
			ST文字位置 stScore文字位置8 = new ST文字位置();
			stScore文字位置8.ch = '7';
			stScore文字位置8.pt = new Point(357, 0);
			stScore文字位置Array[7] = stScore文字位置8;
			ST文字位置 stScore文字位置9 = new ST文字位置();
			stScore文字位置9.ch = '8';
			stScore文字位置9.pt = new Point(408, 0);
			stScore文字位置Array[8] = stScore文字位置9;
			ST文字位置 stScore文字位置10 = new ST文字位置();
			stScore文字位置10.ch = '9';
			stScore文字位置10.pt = new Point(459, 0);
			stScore文字位置Array[9] = stScore文字位置10;
			this.stScoreFont = stScore文字位置Array;

			base.list子Activities.Add(this.PuchiChara = new PuchiChara());

			this.ptFullCombo位置 = new Point[] { new Point(0x80, 0xed), new Point(0xdf, 0xed), new Point(0x141, 0xed) };
			base.b活性化してない = true;
		}


		// メソッド

		public void tアニメを完了させる()
		{
			this.ct表示用.n現在の値 = (int)this.ct表示用.n終了値;
		}


		public void tSkipResultAnimations()
        {
			ct全体進行.n現在の値 = (int)MountainAppearValue;
			
			for (int i = 0; i < 9; i++)
            {
				b音声再生[i] = true;
			}

			if (!ctゲージアニメ.b進行中)
				ctゲージアニメ.t開始(0, GaugeFactor, 59, TJAPlayer3.Timer);
			ctゲージアニメ.n現在の値 = (int)ctゲージアニメ.n終了値;
			TJAPlayer3.Skin.soundGauge.t停止する();
		}

		// CActivity 実装

		public override void On活性化()
		{
			this.sdDTXで指定されたフルコンボ音 = null;

			base.On活性化();
		}
		public override void On非活性化()
		{
			if (this.ct表示用 != null)
			{
				this.ct表示用 = null;
			}

			for(int i = 0; i < this.b音声再生.Length; i++)
            {
				b音声再生[i] = false;
            }

			if (this.sdDTXで指定されたフルコンボ音 != null)
			{
				TJAPlayer3.Sound管理.tサウンドを破棄する(this.sdDTXで指定されたフルコンボ音);
				this.sdDTXで指定されたフルコンボ音 = null;
			}
			base.On非活性化();
		}
		public override void OnManagedリソースの作成()
		{
			if (!base.b活性化してない)
			{
				ct全体進行 = new CCounter(0, 50000, 1, TJAPlayer3.Timer);

				ctゲージアニメ = new CCounter();
				ct虹ゲージアニメ = new CCounter();

				ctSoul = new CCounter();

				ctEndAnime = new CCounter();
				ctBackgroundAnime = new CCounter(0, 1000, 1, TJAPlayer3.Timer);
				ctBackgroundAnime_Clear = new CCounter(0, 1000, 1, TJAPlayer3.Timer);
				ctMountain_ClearIn = new CCounter();

				RandomText = TJAPlayer3.Random.Next(3);

				ctFlash_Icon = new CCounter(0, 3000, 1, TJAPlayer3.Timer);
				ctRotate_Flowers = new CCounter(0, 1500, 1, TJAPlayer3.Timer);
				ctShine_Plate = new CCounter(0, 1000, 1, TJAPlayer3.Timer);

				ctDonchan_Normal = new CCounter(0, TJAPlayer3.Tx.Result_Donchan_Normal.Length - 1, 1000 / 60, TJAPlayer3.Timer);
				ctDonchan_Clear = new CCounter();
				ctDonchan_Failed = new CCounter();
				ctDonchan_Failed_In = new CCounter();

				GaugeFactor = (int)TJAPlayer3.stage結果.st演奏記録.Drums.fゲージ / 2;
				MountainAppearValue = 2000 + (66 * GaugeFactor) + 8360 - 85;

				this.PuchiChara.IdleAnimation();

				base.OnManagedリソースの作成();
			}
		}
		public override void OnManagedリソースの解放()
		{
			if (!base.b活性化してない)
			{
				base.OnManagedリソースの解放();
			}
		}
		public override int On進行描画()
		{
			if (base.b活性化してない)
			{
				return 0;
			}
			if (base.b初めての進行描画)
			{
				this.ct表示用 = new CCounter(0, 0x3e7, 2, TJAPlayer3.Timer);
				base.b初めての進行描画 = false;
			}
			this.ct表示用.t進行();

			ct全体進行.t進行();
			ctゲージアニメ.t進行();

			ctEndAnime.t進行();
			ctBackgroundAnime.t進行Loop();
			ctMountain_ClearIn.t進行();
			ctDonchan_Clear.t進行Loop();
			ctDonchan_Failed.t進行Loop();
			ctDonchan_Failed_In.t進行();
			ctDonchan_Normal.t進行Loop();

			ctFlash_Icon.t進行Loop();
			ctRotate_Flowers.t進行Loop();
			ctShine_Plate.t進行Loop();

			// this.PuchiChara.IdleAnimation();

			if (TJAPlayer3.stage選曲.n確定された曲の難易度[0] != (int)Difficulty.Dan && TJAPlayer3.stage選曲.n確定された曲の難易度[0] != (int)Difficulty.Tower)
			{
				#region [ 通常時リザルト ]

				int AnimeCount = 3000 + GaugeFactor * 59;
				int ScoreApparitionTimeStamp = AnimeCount + 420 * 4 + 840;

				TJAPlayer3.Tx.Result_Panel.t2D描画(TJAPlayer3.app.Device, 0, 0);

				TJAPlayer3.Tx.Result_Diff_Bar.t2D描画(TJAPlayer3.app.Device, 18, 101, new RectangleF(0, TJAPlayer3.stage選曲.n確定された曲の難易度[0] * 54, 185, 54));

				TJAPlayer3.Tx.Result_Gauge_Base.t2D描画(TJAPlayer3.app.Device, 55, 140);

				if (ct全体進行.n現在の値 >= 2000)
                {
                    #region [ ゲージ関連 ]

                    if (!b音声再生[0])
					{
						TJAPlayer3.Skin.soundGauge.t再生する();
						b音声再生[0] = true;
					}

					if (!ctゲージアニメ.b進行中)
					{
						ctゲージアニメ.t開始(0, GaugeFactor, 59, TJAPlayer3.Timer);
						if (ct全体進行.n現在の値 >= MountainAppearValue)
							ctゲージアニメ.n現在の値 = (int)ctゲージアニメ.n終了値;
					}

					TJAPlayer3.Tx.Result_Gauge.t2D描画(TJAPlayer3.app.Device, 57, 140, new RectangleF(0, 0, 9.74f * ctゲージアニメ.n現在の値, 36));

					if (ctゲージアニメ.b終了値に達した)
                    {
						if(ctゲージアニメ.n現在の値 != 50)
							TJAPlayer3.Skin.soundGauge.t停止する();
                        else
						{
							if (!TJAPlayer3.Skin.soundGauge.b再生中)
								TJAPlayer3.Skin.soundGauge.t停止する();

							if (!ct虹ゲージアニメ.b進行中)
								ct虹ゲージアニメ.t開始(0, 40, 1000 / 60, TJAPlayer3.Timer);

							if (!ctSoul.b進行中)
								ctSoul.t開始(0, 8, 33, TJAPlayer3.Timer);

							ct虹ゲージアニメ.t進行Loop();
							ctSoul.t進行Loop();

							TJAPlayer3.Tx.Result_Rainbow[ct虹ゲージアニメ.n現在の値].t2D描画(TJAPlayer3.app.Device, 57, 144);

							TJAPlayer3.Tx.Result_Soul_Fire.t2D中心基準描画(TJAPlayer3.app.Device, 576, 160, new Rectangle(150 * ctSoul.n現在の値, 0, 150, 131));

							TJAPlayer3.Tx.Result_Soul_Text.t2D中心基準描画(TJAPlayer3.app.Device, 575, 159, new Rectangle(37 * 1, 0, 37, 37));

							if (ctSoul.n現在の値 % 2 == 0)
								TJAPlayer3.Tx.Result_Soul_Text.t2D中心基準描画(TJAPlayer3.app.Device, 575, 159, new Rectangle(37 * 2, 0, 37, 37));
						}
					}

					#endregion
				}

				if (ct全体進行.n現在の値 >= 2000)
				{
					#region [ 成績(スコアを除く)関連 ]

					int Interval = 420;

					float AddCount = 135;

					if (ct全体進行.n現在の値 >= AnimeCount)
					{
						TJAPlayer3.Tx.Result_Number.vc拡大縮小倍率.X = ct全体進行.n現在の値 <= AnimeCount + AddCount ? 1.3f - (float)Math.Sin((ct全体進行.n現在の値 - AnimeCount) / (AddCount / 90) * (Math.PI / 180)) * 0.3f : 1.0f;
						TJAPlayer3.Tx.Result_Number.vc拡大縮小倍率.Y = ct全体進行.n現在の値 <= AnimeCount + AddCount ? 1.3f - (float)Math.Sin((ct全体進行.n現在の値 - AnimeCount) / (AddCount / 90) * (Math.PI / 180)) * 0.3f : 1.0f;
						this.t小文字表示(TJAPlayer3.Skin.nResultNumberP1X, TJAPlayer3.Skin.nResultNumberY + TJAPlayer3.Skin.nResultNumberYPadding * 0, string.Format("{0,4:###0}", TJAPlayer3.stage結果.st演奏記録.Drums.nPerfect数.ToString()));
						if (!this.b音声再生[1])
						{
							TJAPlayer3.Skin.soundPon.t再生する();
							this.b音声再生[1] = true;
						}
					}
					if (ct全体進行.n現在の値 >= AnimeCount + Interval)
					{
						TJAPlayer3.Tx.Result_Number.vc拡大縮小倍率.X = ct全体進行.n現在の値 <= AnimeCount + Interval + AddCount ? 1.3f - (float)Math.Sin((ct全体進行.n現在の値 - (AnimeCount + Interval)) / (AddCount / 90) * (Math.PI / 180)) * 0.3f : 1.0f;
						TJAPlayer3.Tx.Result_Number.vc拡大縮小倍率.Y = ct全体進行.n現在の値 <= AnimeCount + Interval + AddCount ? 1.3f - (float)Math.Sin((ct全体進行.n現在の値 - (AnimeCount + Interval)) / (AddCount / 90) * (Math.PI / 180)) * 0.3f : 1.0f;
						this.t小文字表示(TJAPlayer3.Skin.nResultNumberP1X, TJAPlayer3.Skin.nResultNumberY + TJAPlayer3.Skin.nResultNumberYPadding * 1, string.Format("{0,4:###0}", TJAPlayer3.stage結果.st演奏記録.Drums.nGreat数.ToString()));
						if (!this.b音声再生[2])
						{
							TJAPlayer3.Skin.soundPon.t再生する();
							this.b音声再生[2] = true;
						}
					}
					if (ct全体進行.n現在の値 >= AnimeCount + Interval * 2)
					{
						TJAPlayer3.Tx.Result_Number.vc拡大縮小倍率.X = ct全体進行.n現在の値 <= AnimeCount + Interval * 2 + AddCount ? 1.3f - (float)Math.Sin((ct全体進行.n現在の値 - (AnimeCount + Interval * 2)) / (AddCount / 90) * (Math.PI / 180)) * 0.3f : 1.0f;
						TJAPlayer3.Tx.Result_Number.vc拡大縮小倍率.Y = ct全体進行.n現在の値 <= AnimeCount + Interval * 2 + AddCount ? 1.3f - (float)Math.Sin((ct全体進行.n現在の値 - (AnimeCount + Interval * 2)) / (AddCount / 90) * (Math.PI / 180)) * 0.3f : 1.0f;
						this.t小文字表示(TJAPlayer3.Skin.nResultNumberP1X, TJAPlayer3.Skin.nResultNumberY + TJAPlayer3.Skin.nResultNumberYPadding * 2, string.Format("{0,4:###0}", TJAPlayer3.stage結果.st演奏記録.Drums.nMiss数.ToString()));
						if (!this.b音声再生[3])
						{
							TJAPlayer3.Skin.soundPon.t再生する();
							this.b音声再生[3] = true;
						}
					}
					if (ct全体進行.n現在の値 >= AnimeCount + Interval * 3)
					{
						TJAPlayer3.Tx.Result_Number.vc拡大縮小倍率.X = ct全体進行.n現在の値 <= AnimeCount + Interval * 3 + AddCount ? 1.3f - (float)Math.Sin((ct全体進行.n現在の値 - (AnimeCount + Interval * 3)) / (AddCount / 90) * (Math.PI / 180)) * 0.3f : 1.0f;
						TJAPlayer3.Tx.Result_Number.vc拡大縮小倍率.Y = ct全体進行.n現在の値 <= AnimeCount + Interval * 3 + AddCount ? 1.3f - (float)Math.Sin((ct全体進行.n現在の値 - (AnimeCount + Interval * 3)) / (AddCount / 90) * (Math.PI / 180)) * 0.3f : 1.0f;
						this.t小文字表示(TJAPlayer3.Skin.nResultNumberP1X, TJAPlayer3.Skin.nResultNumberY + TJAPlayer3.Skin.nResultNumberYPadding * 3, string.Format("{0,4:###0}", TJAPlayer3.stage結果.st演奏記録.Drums.n連打数.ToString()));
						if (!this.b音声再生[4])
						{
							TJAPlayer3.Skin.soundPon.t再生する();
							this.b音声再生[4] = true;
						}
					}
					if (ct全体進行.n現在の値 >= AnimeCount + Interval * 4)
					{
						TJAPlayer3.Tx.Result_Number.vc拡大縮小倍率.X = ct全体進行.n現在の値 <= AnimeCount + Interval * 4 + AddCount ? 1.3f - (float)Math.Sin((ct全体進行.n現在の値 - (AnimeCount + Interval * 4)) / (AddCount / 90) * (Math.PI / 180)) * 0.3f : 1.0f;
						TJAPlayer3.Tx.Result_Number.vc拡大縮小倍率.Y = ct全体進行.n現在の値 <= AnimeCount + Interval * 4 + AddCount ? 1.3f - (float)Math.Sin((ct全体進行.n現在の値 - (AnimeCount + Interval * 4)) / (AddCount / 90) * (Math.PI / 180)) * 0.3f : 1.0f;
						this.t小文字表示(TJAPlayer3.Skin.nResultNumberP1X, TJAPlayer3.Skin.nResultNumberY + TJAPlayer3.Skin.nResultNumberYPadding * 4, string.Format("{0,4:###0}", TJAPlayer3.stage結果.st演奏記録.Drums.n最大コンボ数.ToString()));
						if (!this.b音声再生[5])
						{
							TJAPlayer3.Skin.soundPon.t再生する();
							this.b音声再生[5] = true;
						}
					}

					#endregion

					#region [ スコア関連 ]

					if (ct全体進行.n現在の値 >= AnimeCount + Interval * 4 + 840)
					{
						int AnimeCount1 = AnimeCount + Interval * 4 + 840;

						TJAPlayer3.Tx.Result_Score_Number.vc拡大縮小倍率.X = ct全体進行.n現在の値 <= AnimeCount1 + 270 ? 1.0f + (float)Math.Sin((ct全体進行.n現在の値 - AnimeCount1) / 1.5f * (Math.PI / 180)) * 0.65f :
																		  ct全体進行.n現在の値 <= AnimeCount1 + 360 ? 1.0f - (float)Math.Sin((ct全体進行.n現在の値 - AnimeCount1 - 270) * (Math.PI / 180)) * 0.1f : 1.0f;
						TJAPlayer3.Tx.Result_Score_Number.vc拡大縮小倍率.Y = ct全体進行.n現在の値 <= AnimeCount1 + 270 ? 1.0f + (float)Math.Sin((ct全体進行.n現在の値 - AnimeCount1) / 1.5f * (Math.PI / 180)) * 0.65f :
																		  ct全体進行.n現在の値 <= AnimeCount1 + 360 ? 1.0f - (float)Math.Sin((ct全体進行.n現在の値 - AnimeCount1 - 270) * (Math.PI / 180)) * 0.1f : 1.0f;

						this.tスコア文字表示(TJAPlayer3.Skin.nResultScoreP1X, TJAPlayer3.Skin.nResultScoreP1Y, string.Format("{0,7:######0}", TJAPlayer3.stage結果.st演奏記録.Drums.nスコア));

						if (!b音声再生[6])
						{
							TJAPlayer3.Skin.soundScoreDon.t再生する();
							b音声再生[6] = true;
						}
					}

					#endregion
				}

				

				if (ct全体進行.n現在の値 >= ScoreApparitionTimeStamp + 1000)
                {
                    #region [Score rank apparition]

                    if (ct全体進行.n現在の値 <= ScoreApparitionTimeStamp + 1180)
					{
						TJAPlayer3.Tx.Result_ScoreRankEffect.Opacity = (int)((ct全体進行.n現在の値 - (ScoreApparitionTimeStamp + 1000)) / 180.0f * 255.0f);
						TJAPlayer3.Tx.Result_ScoreRankEffect.vc拡大縮小倍率.X = 1.0f + (float)Math.Sin((float)(ct全体進行.n現在の値 - (ScoreApparitionTimeStamp + 910)) / 1.5f * (Math.PI / 180)) * 1.4f;
						TJAPlayer3.Tx.Result_ScoreRankEffect.vc拡大縮小倍率.Y = 1.0f + (float)Math.Sin((float)(ct全体進行.n現在の値 - (ScoreApparitionTimeStamp + 910)) / 1.5f * (Math.PI / 180)) * 1.4f;
					}
					else if (ct全体進行.n現在の値 <= ScoreApparitionTimeStamp + 1270)
					{
						TJAPlayer3.Tx.Result_ScoreRankEffect.vc拡大縮小倍率.X = 0.5f + (float)Math.Sin((float)(ct全体進行.n現在の値 - (ScoreApparitionTimeStamp + 1180)) * (Math.PI / 180)) * 0.5f;
						TJAPlayer3.Tx.Result_ScoreRankEffect.vc拡大縮小倍率.Y = 0.5f + (float)Math.Sin((float)(ct全体進行.n現在の値 - (ScoreApparitionTimeStamp + 1180)) * (Math.PI / 180)) * 0.5f;
					}
					else
                    {
						TJAPlayer3.Tx.Result_ScoreRankEffect.Opacity = 255;
						TJAPlayer3.Tx.Result_ScoreRankEffect.vc拡大縮小倍率.X = 1f;
						TJAPlayer3.Tx.Result_ScoreRankEffect.vc拡大縮小倍率.Y = 1f;
					}

					if (TJAPlayer3.stage選曲.n確定された曲の難易度[0] != (int)Difficulty.Dan && TJAPlayer3.stage結果.nスコアランク > 0)
                    {
						int CurrentFlash = 0;
						int[] FlashTimes = { 1500, 1540, 1580, 1620, 1660, 1700, 1740, 1780 };

						if (ctFlash_Icon.n現在の値 >= FlashTimes[0] && ctFlash_Icon.n現在の値 <= FlashTimes[1] || ctFlash_Icon.n現在の値 >= FlashTimes[4] && ctFlash_Icon.n現在の値 <= FlashTimes[5])
							CurrentFlash = 1;
						else if (ctFlash_Icon.n現在の値 >= FlashTimes[1] && ctFlash_Icon.n現在の値 <= FlashTimes[2] || ctFlash_Icon.n現在の値 >= FlashTimes[5] && ctFlash_Icon.n現在の値 <= FlashTimes[6])
							CurrentFlash = 2;
						else if (ctFlash_Icon.n現在の値 >= FlashTimes[2] && ctFlash_Icon.n現在の値 <= FlashTimes[3] || ctFlash_Icon.n現在の値 >= FlashTimes[6] && ctFlash_Icon.n現在の値 <= FlashTimes[7])
							CurrentFlash = 3;


						TJAPlayer3.Tx.Result_ScoreRankEffect.t2D拡大率考慮中央基準描画(TJAPlayer3.app.Device, 135, 339, new Rectangle((TJAPlayer3.stage結果.nスコアランク - 1) * 229, CurrentFlash * 194, 229, 194));

						if (!b音声再生[7] && ct全体進行.n現在の値 >= ScoreApparitionTimeStamp + 1180)
						{
							TJAPlayer3.Skin.soundRankIn.t再生する();
							b音声再生[7] = true;
						}
					}

                    #endregion
                }


                if (ct全体進行.n現在の値 >= ScoreApparitionTimeStamp + 2500)
				{
                    #region [Crown apparition]

                    if (ct全体進行.n現在の値 <= ScoreApparitionTimeStamp + 2680)
					{
						TJAPlayer3.Tx.Result_CrownEffect.Opacity = (int)((ct全体進行.n現在の値 - (ScoreApparitionTimeStamp + 2500)) / 180.0f * 255.0f);
						TJAPlayer3.Tx.Result_CrownEffect.vc拡大縮小倍率.X = 1.0f + (float)Math.Sin((float)(ct全体進行.n現在の値 - (ScoreApparitionTimeStamp + 2410)) / 1.5f * (Math.PI / 180)) * 1.4f;
						TJAPlayer3.Tx.Result_CrownEffect.vc拡大縮小倍率.Y = 1.0f + (float)Math.Sin((float)(ct全体進行.n現在の値 - (ScoreApparitionTimeStamp + 2410)) / 1.5f * (Math.PI / 180)) * 1.4f;
					}
					else if (ct全体進行.n現在の値 <= ScoreApparitionTimeStamp + 2770)
					{
						TJAPlayer3.Tx.Result_CrownEffect.vc拡大縮小倍率.X = 0.5f + (float)Math.Sin((float)(ct全体進行.n現在の値 - (ScoreApparitionTimeStamp + 2680)) * (Math.PI / 180)) * 0.5f;
						TJAPlayer3.Tx.Result_CrownEffect.vc拡大縮小倍率.Y = 0.5f + (float)Math.Sin((float)(ct全体進行.n現在の値 - (ScoreApparitionTimeStamp + 2680)) * (Math.PI / 180)) * 0.5f;
					}
					else
                    {
						TJAPlayer3.Tx.Result_CrownEffect.Opacity = 255;
						TJAPlayer3.Tx.Result_CrownEffect.vc拡大縮小倍率.X = 1f;
						TJAPlayer3.Tx.Result_CrownEffect.vc拡大縮小倍率.Y = 1f;
					}

					int ClearType = TJAPlayer3.stage結果.nクリア - 1;

					if (TJAPlayer3.stage選曲.n確定された曲の難易度[0] != (int)(Difficulty.Dan) && ClearType >= 0)
					{
						int CurrentFlash = 0;
						int[] FlashTimes = { 2000, 2040, 2080, 2120, 2160, 2200, 2240, 2280 };

						if (ctFlash_Icon.n現在の値 >= FlashTimes[0] && ctFlash_Icon.n現在の値 <= FlashTimes[1] || ctFlash_Icon.n現在の値 >= FlashTimes[4] && ctFlash_Icon.n現在の値 <= FlashTimes[5])
							CurrentFlash = 1;
						else if (ctFlash_Icon.n現在の値 >= FlashTimes[1] && ctFlash_Icon.n現在の値 <= FlashTimes[2] || ctFlash_Icon.n現在の値 >= FlashTimes[5] && ctFlash_Icon.n現在の値 <= FlashTimes[6])
							CurrentFlash = 2;
						else if (ctFlash_Icon.n現在の値 >= FlashTimes[2] && ctFlash_Icon.n現在の値 <= FlashTimes[3] || ctFlash_Icon.n現在の値 >= FlashTimes[6] && ctFlash_Icon.n現在の値 <= FlashTimes[7])
							CurrentFlash = 3;

						TJAPlayer3.Tx.Result_CrownEffect.t2D拡大率考慮中央基準描画(TJAPlayer3.app.Device, 262, 336, new Rectangle(ClearType * 113, CurrentFlash * 112, 113, 112));

						if (!b音声再生[8] && ct全体進行.n現在の値 >= ScoreApparitionTimeStamp + 2680)
						{
							TJAPlayer3.Skin.soundCrownIn.t再生する();
							b音声再生[8] = true;
						}
					}

                    #endregion
                }

				// Should be Score + 4000, to synchronize with Stage Kekka

                // MountainAppearValue = 2000 + (ctゲージアニメ.n終了値 * 66) + 8360 - 85;

				if (ct全体進行.n現在の値 >= MountainAppearValue)
				{
                    #region [Mountain animation counter setup]

                    if (!this.ctMountain_ClearIn.b進行中)
						this.ctMountain_ClearIn.t開始(0, 515, 3, TJAPlayer3.Timer);

					if (TJAPlayer3.stage結果.st演奏記録[0].fゲージ >= 80.0f)
                    {
						if (!this.ctDonchan_Clear.b進行中)
							this.ctDonchan_Clear.t開始(0, TJAPlayer3.Tx.Result_Donchan_Clear.Length - 1, 1000 / 60, TJAPlayer3.Timer);
					}
					else
                    {
						if (!this.ctDonchan_Failed_In.b進行中)
							this.ctDonchan_Failed_In.t開始(0, TJAPlayer3.Tx.Result_Donchan_Failed_In.Length - 1, 1000 / 60, TJAPlayer3.Timer);
						else if (this.ctDonchan_Failed_In.b終了値に達した && !this.ctDonchan_Failed.b進行中)
							this.ctDonchan_Failed.t開始(0, TJAPlayer3.Tx.Result_Donchan_Failed.Length - 1, 1000 / 60, TJAPlayer3.Timer);	
					}
						

                    #endregion

					/* TO DO */

					// Alter Mountain appear value/Crown appear value if no Score Rank/no Crown
                }

				#region [Don-chan Animations]

				if (this.ctDonchan_Clear.b進行中)
				{
					TJAPlayer3.Tx.Result_Donchan_Clear[ctDonchan_Clear.n現在の値].vc拡大縮小倍率.X = 0.8f;
					TJAPlayer3.Tx.Result_Donchan_Clear[ctDonchan_Clear.n現在の値].vc拡大縮小倍率.Y = 0.8f;

					TJAPlayer3.Tx.Result_Donchan_Clear[ctDonchan_Clear.n現在の値].t2D中心基準描画(TJAPlayer3.app.Device, 202, 532);
				}
				else if (this.ctDonchan_Failed.b進行中)
				{
					TJAPlayer3.Tx.Result_Donchan_Failed[ctDonchan_Failed.n現在の値].vc拡大縮小倍率.X = 0.8f;
					TJAPlayer3.Tx.Result_Donchan_Failed[ctDonchan_Failed.n現在の値].vc拡大縮小倍率.Y = 0.8f;

					TJAPlayer3.Tx.Result_Donchan_Failed[ctDonchan_Failed.n現在の値].t2D中心基準描画(TJAPlayer3.app.Device, 202, 532);
				}
				else if (this.ctDonchan_Failed_In.b進行中)
                {
					TJAPlayer3.Tx.Result_Donchan_Failed_In[ctDonchan_Failed_In.n現在の値].vc拡大縮小倍率.X = 0.8f;
					TJAPlayer3.Tx.Result_Donchan_Failed_In[ctDonchan_Failed_In.n現在の値].vc拡大縮小倍率.Y = 0.8f;

					TJAPlayer3.Tx.Result_Donchan_Failed_In[ctDonchan_Failed_In.n現在の値].t2D中心基準描画(TJAPlayer3.app.Device, 202, 532);
				}
				else
				{
					TJAPlayer3.Tx.Result_Donchan_Normal[ctDonchan_Normal.n現在の値].vc拡大縮小倍率.X = 0.8f;
					TJAPlayer3.Tx.Result_Donchan_Normal[ctDonchan_Normal.n現在の値].vc拡大縮小倍率.Y = 0.8f;

					TJAPlayer3.Tx.Result_Donchan_Normal[ctDonchan_Normal.n現在の値].t2D中心基準描画(TJAPlayer3.app.Device, 202, 532);
				}

				#endregion

				#region [PuchiChara]

				this.PuchiChara.On進行描画(202 - 70, 532 + 30, false);

				#endregion

				if (ct全体進行.n現在の値 >= MountainAppearValue)
                {
					float AddCount = 135;

					int baseX = 182;
					int baseY = 602;

					#region [Cherry blossom animation]

					if (TJAPlayer3.stage結果.st演奏記録[0].fゲージ >= 80.0f)
                    {
						TJAPlayer3.Tx.Result_Flower.vc拡大縮小倍率.X = 0.6f * (ct全体進行.n現在の値 <= MountainAppearValue + AddCount ? 1.3f - (float)Math.Sin((ct全体進行.n現在の値 - MountainAppearValue) / (AddCount / 90) * (Math.PI / 180)) * 0.3f : 1.0f);
						TJAPlayer3.Tx.Result_Flower.vc拡大縮小倍率.Y = 0.6f * (ct全体進行.n現在の値 <= MountainAppearValue + AddCount ? 1.3f - (float)Math.Sin((ct全体進行.n現在の値 - MountainAppearValue) / (AddCount / 90) * (Math.PI / 180)) * 0.3f : 1.0f);
						TJAPlayer3.Tx.Result_Flower.t2D拡大率考慮中央基準描画(TJAPlayer3.app.Device, baseX, baseY, new Rectangle(0, 0, 520, 195));
					}

					#endregion

					#region [Cherry blossom Rotating flowers]

					if (TJAPlayer3.stage結果.st演奏記録[0].fゲージ >= 80.0f)
					{
						float FlowerTime = ctRotate_Flowers.n現在の値;

						for (int i = 0; i < 5; i++)
                        {
							
							if ((int)FlowerTime < ApparitionTimeStamps[i] || (int)FlowerTime > ApparitionTimeStamps[i] + 2 * ApparitionFade + ApparitionDuration)
								TJAPlayer3.Tx.Result_Flower_Rotate[i].Opacity = 0;
							else if ((int)FlowerTime <= ApparitionTimeStamps[i] + ApparitionDuration + ApparitionFade && (int)FlowerTime >= ApparitionTimeStamps[i] + ApparitionFade)
								TJAPlayer3.Tx.Result_Flower_Rotate[i].Opacity = 255;
							else
                            {
								int CurrentGradiant = 0;
								if ((int)FlowerTime >= ApparitionTimeStamps[i] + ApparitionFade + ApparitionDuration)
									CurrentGradiant = ApparitionFade - ((int)FlowerTime - ApparitionTimeStamps[i] - ApparitionDuration - ApparitionFade);
								else
									CurrentGradiant = (int)FlowerTime - ApparitionTimeStamps[i];


								TJAPlayer3.Tx.Result_Flower_Rotate[i].Opacity = (255 * CurrentGradiant) / ApparitionFade;
							}

							TJAPlayer3.Tx.Result_Flower_Rotate[i].vc拡大縮小倍率.X = 0.6f;
							TJAPlayer3.Tx.Result_Flower_Rotate[i].vc拡大縮小倍率.Y = 0.6f;
							TJAPlayer3.Tx.Result_Flower_Rotate[i].fZ軸中心回転 = (float)(FlowerTime - ApparitionTimeStamps[i]) / (FlowerRotationSpeeds[i] * 360f);

							TJAPlayer3.Tx.Result_Flower_Rotate[i].t2D中心基準描画(TJAPlayer3.app.Device, baseX + FlowerXPos[i] - 20, baseY + FlowerYPos[i] - 20);
						}

					}

					#endregion

					#region [Panel shines]

					if (TJAPlayer3.stage結果.st演奏記録[0].fゲージ >= 80.0f)
					{
						int ShineTime = (int)ctShine_Plate.n現在の値;
						int Quadrant500 = ShineTime % 500;

						for (int i = 0; i < 6; i++)
                        {
							if (i < 3 && ShineTime >= 500 || i >= 3 && ShineTime < 500)
								TJAPlayer3.Tx.Result_Shine.Opacity = 0;
							else if (Quadrant500 >= ShinePFade && Quadrant500 <= 500 - ShinePFade)
								TJAPlayer3.Tx.Result_Shine.Opacity = 255;
							else
								TJAPlayer3.Tx.Result_Shine.Opacity = (255 * Math.Min(Quadrant500, 500 - Quadrant500)) / ShinePFade;

							TJAPlayer3.Tx.Result_Shine.vc拡大縮小倍率.X = 0.15f;
							TJAPlayer3.Tx.Result_Shine.vc拡大縮小倍率.Y = 0.15f;

							TJAPlayer3.Tx.Result_Shine.t2D中心基準描画(TJAPlayer3.app.Device, baseX + ShinePXPos[i] + 62, baseY + ShinePYPos[i] + 52);
						}

					}


					#endregion

					#region [Speech bubble animation]
					// Speech Bubble

					int Mood = 0;

					if (TJAPlayer3.stage結果.st演奏記録[0].fゲージ >= 100.0f)
						Mood = 3;
					else if (TJAPlayer3.stage結果.st演奏記録[0].fゲージ >= 80.0f)
						Mood = 2;
					else if (TJAPlayer3.stage結果.st演奏記録[0].fゲージ >= 40.0f)
						Mood = 1;


					TJAPlayer3.Tx.Result_Speech_Bubble.vc拡大縮小倍率.X = 0.9f * (ct全体進行.n現在の値 <= MountainAppearValue + AddCount ? 1.3f - (float)Math.Sin((ct全体進行.n現在の値 - MountainAppearValue) / (AddCount / 90) * (Math.PI / 180)) * 0.3f : 1.0f);
					TJAPlayer3.Tx.Result_Speech_Bubble.vc拡大縮小倍率.Y = 0.9f * (ct全体進行.n現在の値 <= MountainAppearValue + AddCount ? 1.3f - (float)Math.Sin((ct全体進行.n現在の値 - MountainAppearValue) / (AddCount / 90) * (Math.PI / 180)) * 0.3f : 1.0f);
					TJAPlayer3.Tx.Result_Speech_Bubble.t2D拡大率考慮中央基準描画(TJAPlayer3.app.Device, 430, 526, new Rectangle(Mood * 358, RandomText * 230, 358, 230));

					if (!b音声再生[9])
					{
						if (TJAPlayer3.stage結果.st演奏記録[0].fゲージ >= 80.0f)
							TJAPlayer3.Skin.soundDonClear.t再生する();
						else
							TJAPlayer3.Skin.soundDonFailed.t再生する();
						b音声再生[9] = true;
					}

					#endregion
				}


				if (ctゲージアニメ.n現在の値 != 50)
				{
					TJAPlayer3.Tx.Result_Gauge.t2D描画(TJAPlayer3.app.Device, 441, 142, new RectangleF(ctゲージアニメ.n現在の値 < 40 ? 0 : 42, 35, 42, 20));

					TJAPlayer3.Tx.Result_Soul_Text.t2D中心基準描画(TJAPlayer3.app.Device, 575, 159, new Rectangle(37 * (ctゲージアニメ.n現在の値 <= 30 ? 0 : 1), 0, 37, 37));
				}

				#endregion
			}

			if (!this.ct表示用.b終了値に達した)
			{
				return 0;
			}
			return 1;
		}



		// その他

		#region [ private ]
		//-----------------
		[StructLayout(LayoutKind.Sequential)]
		private struct ST文字位置
		{
			public char ch;
			public Point pt;
		}

		public CCounter ct全体進行;
		public CCounter ctゲージアニメ;
		private CCounter ct虹ゲージアニメ;
		private CCounter ctSoul;

		public CCounter ctEndAnime;
		public CCounter ctMountain_ClearIn;
		public CCounter ctBackgroundAnime;
		public CCounter ctBackgroundAnime_Clear;
		private CCounter ctDonchan_Normal;
		private CCounter ctDonchan_Clear;
		private CCounter ctDonchan_Failed;
		private CCounter ctDonchan_Failed_In;

		private int RandomText;

		private CCounter ctFlash_Icon;
		private CCounter ctRotate_Flowers;
		private CCounter ctShine_Plate;

		public PuchiChara PuchiChara;

		public float MountainAppearValue;
		private int GaugeFactor;

		public bool[] b音声再生 = { false, false, false, false, false, false, false, false, false, false, false, false, false };

		// Cherry blossom flowers variables
		public int[] FlowerXPos = { -114, -37, 114, 78, -75 };
		public int[] FlowerYPos = { -33, 3, -36, -81, -73 };
		public float[] FlowerRotationSpeeds = { 5f, 3f, -6f, 4f, -2f };
		public int[] ApparitionTimeStamps = { 10, 30, 50, 100, 190 };
		public int ApparitionFade = 100;
		public int ApparitionDuration = 300;

		// Plate shine variables 
		public int[] ShinePXPos = { 114 - 25, 114 - 16, -37 - 23, -37 - 9, -75 + 20, 78 - 13};
		public int[] ShinePYPos = { -36 + 52, -36 + 2, 3 - 7, 3 + 30, -73 - 23, -81 - 31};
		public int ShinePFade = 100;

		private CCounter ct表示用;
		private readonly Point[] ptFullCombo位置;
		private CSound sdDTXで指定されたフルコンボ音;
		private readonly ST文字位置[] st小文字位置;
		private readonly ST文字位置[] st大文字位置;
		private ST文字位置[] stScoreFont;

		private void t小文字表示(int x, int y, string str)
		{
			foreach (char ch in str)
			{
				for (int i = 0; i < this.st小文字位置.Length; i++)
				{
					if (ch == ' ')
					{
						break;
					}

					if (this.st小文字位置[i].ch == ch)
					{
						Rectangle rectangle = new Rectangle(this.st小文字位置[i].pt.X, this.st小文字位置[i].pt.Y, 32, 38);
						if (TJAPlayer3.Tx.Result_Number != null)
						{
							TJAPlayer3.Tx.Result_Number.t2D拡大率考慮中央基準描画(TJAPlayer3.app.Device, x + 16, y + 19, rectangle);
						}
						break;
					}
				}
				x += 22;
			}
		}
		private void t大文字表示(int x, int y, string str)
		{
			this.t大文字表示(x, y, str, false);
		}
		private void t大文字表示(int x, int y, string str, bool b強調)
		{
			foreach (char ch in str)
			{
				for (int i = 0; i < this.st大文字位置.Length; i++)
				{
					if (this.st大文字位置[i].ch == ch)
					{
						Rectangle rectangle = new Rectangle(this.st大文字位置[i].pt.X, this.st大文字位置[i].pt.Y, 11, 0x10);
						if (ch == '.')
						{
							rectangle.Width -= 2;
							rectangle.Height -= 2;
						}
						if (TJAPlayer3.Tx.Result_Number != null)
						{
							TJAPlayer3.Tx.Result_Number.t2D描画(TJAPlayer3.app.Device, x, y, rectangle);
						}
						break;
					}
				}
				x += 8;
			}
		}

		protected void tスコア文字表示(int x, int y, string str)
		{
			foreach (char ch in str)
			{
				for (int i = 0; i < this.stScoreFont.Length; i++)
				{
					if (this.stScoreFont[i].ch == ch)
					{
						Rectangle rectangle = new Rectangle(this.stScoreFont[i].pt.X, 0, 51, 60);
						if (TJAPlayer3.Tx.Result_Score_Number != null)
						{
							TJAPlayer3.Tx.Result_Score_Number.t2D拡大率考慮中央基準描画(TJAPlayer3.app.Device, x - (str.Length * 33) + 25, y + 30, rectangle);
						}
						break;
					}
				}
				x += 33;
			}
		}
		//-----------------
		#endregion
	}
}
