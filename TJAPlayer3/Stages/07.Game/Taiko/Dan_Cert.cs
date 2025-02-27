﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using FDK;
using System.IO;
using TJAPlayer3;
using System.Linq;

namespace TJAPlayer3
{
    static internal class CExamInfo
    {
        // Includes the gauge exam, DanCert max number of exams is 6
        public static readonly int cMaxExam = 7;

        // Max number of songs for a Dan chart
        public static readonly int cExamMaxSongs = 3;
    }


    internal class Dan_Cert : CActivity
    {
        /// <summary>
        /// 段位認定
        /// </summary>
        public Dan_Cert()
        {
            base.b活性化してない = true;
        }

        //
        Dan_C[] Challenge = new Dan_C[CExamInfo.cMaxExam];
        //

        public void Start(int number)
        {
            NowShowingNumber = number;
            if (number == 0)
            {
                Counter_Wait = new CCounter(0, 2299, 1, TJAPlayer3.Timer);
            }
            else
            {
                Counter_In = new CCounter(0, 999, 1, TJAPlayer3.Timer);
            }
            bExamChangeCheck = false;

            if (number == 0)
            {
                for (int i = 1; i < CExamInfo.cMaxExam; i++)
                    ExamChange[i] = false;

                for (int j = 1; j < CExamInfo.cMaxExam; j++)  //段位条件のループ(魂ゲージを除く) 縦(y)
                {
                    if (TJAPlayer3.stage選曲.r確定された曲.DanSongs[0].Dan_C[j] != null)
                    {
                        if (TJAPlayer3.stage選曲.r確定された曲.DanSongs[TJAPlayer3.stage選曲.r確定された曲.DanSongs.Count - 1].Dan_C[j] != null) //個別の条件がありますよー
                        {
                            if (TJAPlayer3.stage選曲.r確定された曲.DanSongs[NowShowingNumber].Dan_C[j].GetExamRange() == Exam.Range.Less)
                            {
                                TJAPlayer3.stage選曲.r確定された曲.DanSongs[NowShowingNumber].Dan_C[j].Amount = TJAPlayer3.stage選曲.r確定された曲.DanSongs[NowShowingNumber].Dan_C[j].Value[0];
                            }
                            else
                            {
                                TJAPlayer3.stage選曲.r確定された曲.DanSongs[NowShowingNumber].Dan_C[j].Amount = 0;
                            }

                            Challenge[j] = TJAPlayer3.stage選曲.r確定された曲.DanSongs[NowShowingNumber].Dan_C[j];
                            ExamChange[j] = true;
                        }
                    }
                }
            }

            ScreenPoint = new double[] { TJAPlayer3.Skin.nScrollFieldBGX[0] - TJAPlayer3.Tx.DanC_Screen.szテクスチャサイズ.Width / 2, 1280 };
            TJAPlayer3.stage演奏ドラム画面.ReSetScore(TJAPlayer3.DTX.List_DanSongs[NowShowingNumber].ScoreInit, TJAPlayer3.DTX.List_DanSongs[NowShowingNumber].ScoreDiff);
            IsAnimating = true;
            TJAPlayer3.stage演奏ドラム画面.actPanel.SetPanelString(TJAPlayer3.DTX.List_DanSongs[NowShowingNumber].Title, TJAPlayer3.DTX.List_DanSongs[NowShowingNumber].Genre, 1 + NowShowingNumber + "曲目");
            if(number == 0) Sound_Section_First?.t再生を開始する();
            else Sound_Section?.t再生を開始する();
        }

        public override void On活性化()
        {
            for (int i = 0; i < CExamInfo.cMaxExam; i++)
            {
                if(TJAPlayer3.DTX.Dan_C[i] != null) Challenge[i] = new Dan_C(TJAPlayer3.DTX.Dan_C[i]);

                for (int j = 0; j < TJAPlayer3.stage選曲.r確定された曲.DanSongs.Count; j++)
                {
                    if (TJAPlayer3.stage選曲.r確定された曲.DanSongs[j].Dan_C[i] != null)
                    {
                        TJAPlayer3.stage選曲.r確定された曲.DanSongs[j].Dan_C[i] = new Dan_C(TJAPlayer3.stage選曲.r確定された曲.DanSongs[j].Dan_C[i]);
                    }
                }
            }

            if(TJAPlayer3.stage演奏ドラム画面.ListDan_Number >= 1 && FirstSectionAnime)
                TJAPlayer3.stage演奏ドラム画面.ListDan_Number = 0;

            FirstSectionAnime = false;
            // 始点を決定する。
            // ExamCount = 0;

            songsnotesremain = new int[TJAPlayer3.stage選曲.r確定された曲.DanSongs.Count];
            this.ct虹アニメ = new CCounter(0, TJAPlayer3.Skin.Game_Gauge_Dan_Rainbow_Ptn - 1, 30, TJAPlayer3.Timer);
            this.ct虹透明度 = new CCounter(0, TJAPlayer3.Skin.Game_Gauge_Rainbow_Timer - 1, 1, TJAPlayer3.Timer);


            /*
            for (int i = 0; i < CExamInfo.cMaxExam; i++)
            {
                if (Challenge[i] != null && Challenge[i].GetEnable() == true)
                    this.ExamCount++;
            }
            */

            NowCymbolShowingNumber = 0;
            bExamChangeCheck = false;

            for (int i = 0; i < CExamInfo.cMaxExam; i++)
            {
                Status[i] = new ChallengeStatus();
                Status[i].Timer_Amount = new CCounter();
                Status[i].Timer_Gauge = new CCounter();
                Status[i].Timer_Failed = new CCounter();
            }
            
            IsEnded = new bool[TJAPlayer3.stage選曲.r確定された曲.DanSongs.Count];

            if (TJAPlayer3.stage選曲.n確定された曲の難易度[0] == (int)Difficulty.Dan) IsAnimating = true;
            base.On活性化();
        }

        public void Update()
        {
            for (int i = 0; i < CExamInfo.cMaxExam; i++)
            {
                if (Challenge[i] == null || !Challenge[i].GetEnable()) continue;
                var oldReached = Challenge[i].GetReached();
                var isChangedAmount = false;

                int totalGoods = (int)TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含む.Drums.Perfect + TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Perfect;
                int totalOks = (int)TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含む.Drums.Great + TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Great;
                int totalBads = (int)TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Miss;

                int individualGoods = TJAPlayer3.stage演奏ドラム画面.n良[NowShowingNumber];
                int individualOks = TJAPlayer3.stage演奏ドラム画面.n可[NowShowingNumber];
                int individualBads = TJAPlayer3.stage演奏ドラム画面.n不可[NowShowingNumber];

                double accuracy = (totalGoods * 100 + totalOks * 50) / (double)(totalGoods + totalOks + totalBads);
                double individualAccuracy = (individualGoods * 100 + individualOks * 50) / (double)(individualGoods + individualOks + individualBads);

                switch (Challenge[i].GetExamType())
                {
                    case Exam.Type.Gauge:
                        isChangedAmount = Challenge[i].Update((int)TJAPlayer3.stage演奏ドラム画面.actGauge.db現在のゲージ値[0]);
                        break;
                    case Exam.Type.JudgePerfect:
                        isChangedAmount = Challenge[i].Update(ExamChange[i] ? individualGoods : totalGoods);
                        break;
                    case Exam.Type.JudgeGood:
                        isChangedAmount = Challenge[i].Update(ExamChange[i] ? individualOks : totalOks);
                        break;
                    case Exam.Type.JudgeBad:
                        isChangedAmount = Challenge[i].Update(ExamChange[i] ? individualBads : totalBads);
                        break;
                    case Exam.Type.Score:
                        isChangedAmount = Challenge[i].Update((int)TJAPlayer3.stage演奏ドラム画面.actScore.GetScore(0));
                        break;
                    case Exam.Type.Roll:
                        isChangedAmount = Challenge[i].Update(ExamChange[i] ? TJAPlayer3.stage演奏ドラム画面.n連打[NowShowingNumber] : (int)(TJAPlayer3.stage演奏ドラム画面.GetRoll(0)));
                        break;
                    case Exam.Type.Hit:
                        isChangedAmount = Challenge[i].Update(ExamChange[i] ? TJAPlayer3.stage演奏ドラム画面.n良[NowShowingNumber] + TJAPlayer3.stage演奏ドラム画面.n可[NowShowingNumber] + TJAPlayer3.stage演奏ドラム画面.n連打[NowShowingNumber] : (int)(TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含む.Drums.Perfect + TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Perfect + TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含む.Drums.Great + TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Great + TJAPlayer3.stage演奏ドラム画面.GetRoll(0)));
                        break;
                    case Exam.Type.Combo:
                        isChangedAmount = Challenge[i].Update((int)TJAPlayer3.stage演奏ドラム画面.actCombo.n現在のコンボ数.最高値[0]);
                        break;
                    case Exam.Type.Accuracy:
                        isChangedAmount = Challenge[i].Update(ExamChange[i] ? (int)individualAccuracy : (int)accuracy);
                        break;
                    default:
                        break;
                }

                // 値が変更されていたらアニメーションを行う。
                if (isChangedAmount)
                {
                    if(Status[i].Timer_Amount != null && Status[i].Timer_Amount.b終了値に達してない)
                    {
                        Status[i].Timer_Amount = new CCounter(0, 11, 12, TJAPlayer3.Timer);
                        Status[i].Timer_Amount.n現在の値 = 1;
                    }
                    else
                    {
                        Status[i].Timer_Amount = new CCounter(0, 11, 12, TJAPlayer3.Timer);
                    }
                }

                // 条件の達成見込みがあるかどうか判断する。
                if (Challenge[i].GetExamRange() == Exam.Range.Less)
                {
                    Challenge[i].SetReached(!Challenge[i].IsCleared[0]);
                }
                else
                {
                    songsnotesremain[NowShowingNumber] = TJAPlayer3.DTX.nDan_NotesCount[NowShowingNumber] - (TJAPlayer3.stage演奏ドラム画面.n良[NowShowingNumber] + TJAPlayer3.stage演奏ドラム画面.n可[NowShowingNumber] + TJAPlayer3.stage演奏ドラム画面.n不可[NowShowingNumber]);
                    notesremain = TJAPlayer3.DTX.nノーツ数[3] - (TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含む.Drums.Perfect + TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Perfect) - (TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含む.Drums.Great + TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Great) - (TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含む.Drums.Miss + TJAPlayer3.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Miss);
                    // 残り音符数が0になったときに判断されるやつ
                    if (ExamChange[i] ? songsnotesremain[NowShowingNumber] <= 0 : notesremain <= 0)
                    {
                        // 残り音符数ゼロ
                        switch (Challenge[i].GetExamType())
                        {
                            case Exam.Type.Gauge:
                                if (Challenge[i].Amount < Challenge[i].Value[0]) Challenge[i].SetReached(true);
                                break;
                            case Exam.Type.Score:
                                if (Challenge[i].Amount < Challenge[i].Value[0]) Challenge[i].SetReached(true);
                                break;
                            case Exam.Type.Accuracy:
                                if (Challenge[i].Amount < Challenge[i].Value[0]) Challenge[i].SetReached(true);
                                break;
                            default:
                                // 何もしない
                                break;
                        }
                    }
                    
                    // 常に監視されるやつ。
                    switch (Challenge[i].GetExamType())
                    {
                        case Exam.Type.JudgePerfect:
                        case Exam.Type.JudgeGood:
                        case Exam.Type.JudgeBad:
                            if (ExamChange[i] ? songsnotesremain[NowShowingNumber] < (Challenge[i].Value[0] - Challenge[i].Amount) : notesremain < (Challenge[i].Value[0] - Challenge[i].Amount)) Challenge[i].SetReached(true);
                            break;
                        case Exam.Type.Combo:
                            if (notesremain + TJAPlayer3.stage演奏ドラム画面.actCombo.n現在のコンボ数.P1 < ((Challenge[i].Value[0])) && TJAPlayer3.stage演奏ドラム画面.actCombo.n現在のコンボ数.最高値[0] < (Challenge[i].Value[0])) Challenge[i].SetReached(true);
                            break;
                        default:
                            break;
                    }

                    // 音源が終了したやつの分岐。
                    // ( CDTXMania.DTX.listChip.Count > 0 ) ? CDTXMania.DTX.listChip[ CDTXMania.DTX.listChip.Count - 1 ].n発声時刻ms : 0;
                    if(!IsEnded[NowShowingNumber])
                    {
                        if (TJAPlayer3.DTX.listChip.Count <= 0) continue;
                        if (ExamChange[i] ? TJAPlayer3.DTX.pDan_LastChip[NowShowingNumber].n発声時刻ms <= TJAPlayer3.Timer.n現在時刻 : TJAPlayer3.DTX.listChip[TJAPlayer3.DTX.listChip.Count - 1].n発声時刻ms <= TJAPlayer3.Timer.n現在時刻)
                        {
                            switch (Challenge[i].GetExamType())
                            {
                                case Exam.Type.Score:
                                case Exam.Type.Hit:
                                    if (Challenge[i].Amount < Challenge[i].Value[0]) Challenge[i].SetReached(true);
                                    break;
                                case Exam.Type.Roll:
                                    if (Challenge[i].Amount < Challenge[i].Value[0]) Challenge[i].SetReached(true);
                                    break;
                                default:
                                    break;
                            }
                            IsEnded[NowShowingNumber] = true;
                        }
                    }
                }
                if(oldReached == false && Challenge[i].GetReached() == true)
                {
                    Sound_Failed?.t再生を開始する();
                }
            }
        }

        public override void On非活性化()
        {
            for (int i = 0; i < CExamInfo.cMaxExam; i++)
            {
                Challenge[i] = null;
            }

            for (int i = 0; i < CExamInfo.cMaxExam; i++)
            {
                Status[i].Timer_Amount = null;
                Status[i].Timer_Gauge = null;
                Status[i].Timer_Failed = null;
            }
            for(int i = 0; i < IsEnded.Length; i++)
                IsEnded[i] = false;

            base.On非活性化();
        }

        public override void OnManagedリソースの作成()
        {
            Dan_Plate = TJAPlayer3.tテクスチャの生成(Path.GetDirectoryName(TJAPlayer3.DTX.strファイル名の絶対パス) + @"\Dan_Plate.png");
            Sound_Section = TJAPlayer3.Sound管理.tサウンドを生成する(CSkin.Path(@"Sounds\Dan\Section.ogg"), ESoundGroup.SoundEffect);
            Sound_Section_First = TJAPlayer3.Sound管理.tサウンドを生成する(CSkin.Path(@"Sounds\Dan\Section_First.wav"), ESoundGroup.SoundEffect);
            Sound_Failed = TJAPlayer3.Sound管理.tサウンドを生成する(CSkin.Path(@"Sounds\Dan\Failed.ogg"), ESoundGroup.SoundEffect);
            base.OnManagedリソースの作成();
        }

        public override void OnManagedリソースの解放()
        {
            Dan_Plate?.Dispose();
            Sound_Section_First?.Dispose();
            Sound_Section?.t解放する();
            Sound_Failed?.t解放する();
            base.OnManagedリソースの解放();
        }

        public override int On進行描画()
        {
            if (TJAPlayer3.stage選曲.n確定された曲の難易度[0] != (int)Difficulty.Dan) return base.On進行描画();
            Counter_In?.t進行();
            Counter_Wait?.t進行();
            Counter_Out?.t進行();
            Counter_Text?.t進行();

            if (Counter_Text != null)
            {
                if (Counter_Text.n現在の値 >= 2000)
                {
                    for (int i = Counter_Text_Old; i < Counter_Text.n現在の値; i++)
                    {
                        if (i % 2 == 0)
                        {
                            if (TJAPlayer3.DTX.List_DanSongs[NowShowingNumber].TitleTex != null)
                            {
                                TJAPlayer3.DTX.List_DanSongs[NowShowingNumber].TitleTex.Opacity--;
                            }
                            if (TJAPlayer3.DTX.List_DanSongs[NowShowingNumber].SubTitleTex != null)
                            {
                                TJAPlayer3.DTX.List_DanSongs[NowShowingNumber].SubTitleTex.Opacity--;
                            }
                        }
                    }
                }
                else
                {
                    if (TJAPlayer3.DTX.List_DanSongs[NowShowingNumber].TitleTex != null)
                    {
                        TJAPlayer3.DTX.List_DanSongs[NowShowingNumber].TitleTex.Opacity = 255;
                    }
                    if (TJAPlayer3.DTX.List_DanSongs[NowShowingNumber].SubTitleTex != null)
                    {
                        TJAPlayer3.DTX.List_DanSongs[NowShowingNumber].SubTitleTex.Opacity = 255;
                    }
                }
                Counter_Text_Old = Counter_Text.n現在の値;
            }

            for (int i = 0; i < CExamInfo.cMaxExam; i++)
            {
                Status[i].Timer_Amount?.t進行();
            }

            //for (int i = 0; i < 3; i++)
            //{
            //    if (Challenge[i] != null && Challenge[i].GetEnable())
            //        CDTXMania.act文字コンソール.tPrint(0, 20 * i, C文字コンソール.Eフォント種別.白, Challenge[i].ToString());
            //    else
            //        CDTXMania.act文字コンソール.tPrint(0, 20 * i, C文字コンソール.Eフォント種別.白, "None");
            //}
            //CDTXMania.act文字コンソール.tPrint(0, 80, C文字コンソール.Eフォント種別.白, String.Format("Notes Remain: {0}", CDTXMania.DTX.nノーツ数[3] - (CDTXMania.stage演奏ドラム画面.nヒット数_Auto含む.Drums.Perfect + CDTXMania.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Perfect) - (CDTXMania.stage演奏ドラム画面.nヒット数_Auto含む.Drums.Great + CDTXMania.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Great) - (CDTXMania.stage演奏ドラム画面.nヒット数_Auto含む.Drums.Miss + CDTXMania.stage演奏ドラム画面.nヒット数_Auto含まない.Drums.Miss)));

            // 背景を描画する。

            TJAPlayer3.Tx.DanC_Background?.t2D描画(TJAPlayer3.app.Device, 0, 0);

            DrawExam(Challenge);

            // 幕のアニメーション
            if (Counter_In != null)
            {
                if (Counter_In.b終了値に達してない)
                {
                    for (int i = Counter_In_Old; i < Counter_In.n現在の値; i++)
                    {
                        ScreenPoint[0] += (TJAPlayer3.Skin.nScrollFieldBGX[0] - ScreenPoint[0]) / 180.0;
                        ScreenPoint[1] += ((1280 / 2 + TJAPlayer3.Skin.nScrollFieldBGX[0] / 2) - ScreenPoint[1]) / 180.0;
                    }
                    Counter_In_Old = Counter_In.n現在の値;
                    TJAPlayer3.Tx.DanC_Screen?.t2D描画(TJAPlayer3.app.Device, (int)ScreenPoint[0], TJAPlayer3.Skin.nScrollFieldY[0], new Rectangle(0, 0, TJAPlayer3.Tx.DanC_Screen.szテクスチャサイズ.Width / 2, TJAPlayer3.Tx.DanC_Screen.szテクスチャサイズ.Height));
                    TJAPlayer3.Tx.DanC_Screen?.t2D描画(TJAPlayer3.app.Device, (int)ScreenPoint[1], TJAPlayer3.Skin.nScrollFieldY[0], new Rectangle(TJAPlayer3.Tx.DanC_Screen.szテクスチャサイズ.Width / 2, 0, TJAPlayer3.Tx.DanC_Screen.szテクスチャサイズ.Width / 2, TJAPlayer3.Tx.DanC_Screen.szテクスチャサイズ.Height));
                    //CDTXMania.act文字コンソール.tPrint(0, 420, C文字コンソール.Eフォント種別.白, String.Format("{0} : {1}", ScreenPoint[0], ScreenPoint[1]));
                }
                if (Counter_In.b終了値に達した)
                {
                    Counter_In = null;
                    Counter_Wait = new CCounter(0, 2299, 1, TJAPlayer3.Timer);
                }
            }

            if (Counter_Wait != null)
            {
                if (Counter_Wait.b終了値に達してない)
                {
                    TJAPlayer3.Tx.DanC_Screen?.t2D描画(TJAPlayer3.app.Device, TJAPlayer3.Skin.nScrollFieldBGX[0], TJAPlayer3.Skin.nScrollFieldY[0]);

                    if (NowShowingNumber != 0)
                    {
                        if (Counter_Wait.n現在の値 >= 800)
                        {
                            if (!bExamChangeCheck)
                            {
                                for (int i = 0; i < CExamInfo.cMaxExam; i++)
                                    ExamChange[i] = false;

                                for (int j = 0; j < CExamInfo.cMaxExam; j++)  // EXAM1 check for individual conditions added back, so gauge can be EXAM2, 5, 7 whatever
                                {
                                    if (TJAPlayer3.stage選曲.r確定された曲.DanSongs[0].Dan_C[j] != null)
                                    {
                                        if (TJAPlayer3.stage選曲.r確定された曲.DanSongs[TJAPlayer3.stage選曲.r確定された曲.DanSongs.Count - 1].Dan_C[j] != null) //個別の条件がありますよー
                                        {
                                            Challenge[j] = TJAPlayer3.stage選曲.r確定された曲.DanSongs[NowShowingNumber].Dan_C[j];
                                            ExamChange[j] = true;
                                        }
                                    }
                                }
                                NowCymbolShowingNumber = NowShowingNumber;
                                bExamChangeCheck = true;
                            }
                        }
                    }
                }
                if (Counter_Wait.b終了値に達した)
                {
                    Counter_Wait = null;
                    Counter_Out = new CCounter(0, 90, 3, TJAPlayer3.Timer);
                    Counter_Text = new CCounter(0, 2899, 1, TJAPlayer3.Timer);
                }
            }
            if (Counter_Text != null)
            {
                if (Counter_Text.b終了値に達してない)
                {
                    var title = TJAPlayer3.DTX.List_DanSongs[NowShowingNumber].TitleTex;
                    var subTitle = TJAPlayer3.DTX.List_DanSongs[NowShowingNumber].SubTitleTex;
                    if (subTitle == null)
                        title?.t2D拡大率考慮中央基準描画(TJAPlayer3.app.Device, 1280 / 2 + TJAPlayer3.Skin.nScrollFieldBGX[0] / 2, TJAPlayer3.Skin.nScrollFieldY[0] + 65);
                    else
                    {
                        title?.t2D拡大率考慮中央基準描画(TJAPlayer3.app.Device, 1280 / 2 + TJAPlayer3.Skin.nScrollFieldBGX[0] / 2, TJAPlayer3.Skin.nScrollFieldY[0] + 45);
                        subTitle?.t2D拡大率考慮中央基準描画(TJAPlayer3.app.Device, 1280 / 2 + TJAPlayer3.Skin.nScrollFieldBGX[0] / 2, TJAPlayer3.Skin.nScrollFieldY[0] + 85);
                    }
                }
                if (Counter_Text.b終了値に達した)
                {
                    Counter_Text = null;
                    IsAnimating = false;
                }
            }
            if (Counter_Out != null)
            {
                if (Counter_Out.b終了値に達してない)
                {
                    ScreenPoint[0] = TJAPlayer3.Skin.nScrollFieldBGX[0] - Math.Sin(Counter_Out.n現在の値 * (Math.PI / 180)) * 500;
                    ScreenPoint[1] = TJAPlayer3.Skin.nScrollFieldBGX[0] + TJAPlayer3.Tx.DanC_Screen.szテクスチャサイズ.Width / 2 + Math.Sin(Counter_Out.n現在の値 * (Math.PI / 180)) * 500;
                    TJAPlayer3.Tx.DanC_Screen?.t2D描画(TJAPlayer3.app.Device, (int)ScreenPoint[0], TJAPlayer3.Skin.nScrollFieldY[0], new Rectangle(0, 0, TJAPlayer3.Tx.DanC_Screen.szテクスチャサイズ.Width / 2, TJAPlayer3.Tx.DanC_Screen.szテクスチャサイズ.Height));
                    TJAPlayer3.Tx.DanC_Screen?.t2D描画(TJAPlayer3.app.Device, (int)ScreenPoint[1], TJAPlayer3.Skin.nScrollFieldY[0], new Rectangle(TJAPlayer3.Tx.DanC_Screen.szテクスチャサイズ.Width / 2, 0, TJAPlayer3.Tx.DanC_Screen.szテクスチャサイズ.Width / 2, TJAPlayer3.Tx.DanC_Screen.szテクスチャサイズ.Height));
                    //CDTXMania.act文字コンソール.tPrint(0, 420, C文字コンソール.Eフォント種別.白, String.Format("{0} : {1}", ScreenPoint[0], ScreenPoint[1]));
                }
                if (Counter_Out.b終了値に達した)
                {
                    Counter_Out = null;
                }
            } 
            // 段プレートを描画する。
            Dan_Plate?.t2D中心基準描画(TJAPlayer3.app.Device, TJAPlayer3.Skin.Game_DanC_Dan_Plate[0], TJAPlayer3.Skin.Game_DanC_Dan_Plate[1]);

            TJAPlayer3.act文字コンソール.tPrint(0, 0, C文字コンソール.Eフォント種別.白, TJAPlayer3.DTX.pDan_LastChip[NowShowingNumber].n発声時刻ms + " / " + CSound管理.rc演奏用タイマ.n現在時刻);


            return base.On進行描画();
        }

        public void DrawExam(Dan_C[] dan_C)
        {
            int count = 0;
            int countNoGauge = 0;

            // Count exams, both with and without gauge
            for (int i = 0; i < CExamInfo.cMaxExam; i++)
            {
                if (dan_C[i] != null && dan_C[i].GetEnable() == true)
                {
                    count++;
                    if (dan_C[i].GetExamType() != Exam.Type.Gauge)
                        countNoGauge++;
                }
                    
            }

            // Bar position on the cert
            int currentPosition = -1;

            for (int i = 0; i < CExamInfo.cMaxExam; i++)
            {
                if (dan_C[i] == null || dan_C[i].GetEnable() != true)
                    continue ;

                if (dan_C[i].GetExamType() != Exam.Type.Gauge)
                {
                    currentPosition++;

                    // Determines if a small bar will be used to optimise the display layout
                    bool isSmallGauge = currentPosition >= 3 || (countNoGauge > 3 && countNoGauge % 3 > currentPosition);

                    // Y index of the gauge
                    int yIndex = (currentPosition % 3) + 1;

                    // Origin position which will be used as a reference for bar elements
                    int barXOffset = TJAPlayer3.Skin.Game_DanC_X[1] + (currentPosition >= 3 ? 503 : 0);
                    int barYOffset = TJAPlayer3.Skin.Game_DanC_Y[1] + TJAPlayer3.Skin.Game_DanC_Size[1] * yIndex + (yIndex * TJAPlayer3.Skin.Game_DanC_Padding);
                    int lowerBarYOffset = barYOffset + TJAPlayer3.Skin.Game_DanC_Size[1] + TJAPlayer3.Skin.Game_DanC_Padding;

                    // Skin X : 70
                    // Skin Y : 292

                    #region [Gauge base]

                    if (!isSmallGauge)
                        TJAPlayer3.Tx.DanC_Base?.t2D描画(TJAPlayer3.app.Device, barXOffset, barYOffset, new RectangleF(0, ExamChange[i] ? 92 : 0, 1006, 92));
                    else
                        TJAPlayer3.Tx.DanC_Base_Small?.t2D描画(TJAPlayer3.app.Device, barXOffset, barYOffset, new RectangleF(0, ExamChange[i] ? 92 : 0, 503, 92));

                    #endregion

                    #region [Counter wait variables]

                    int counter800 = (Counter_Wait != null ? Counter_Wait.n現在の値 - 800 : 0);
                    int counter255M255 = (Counter_Wait != null ? 255 - (Counter_Wait.n現在の値 - (800 - 255)) : 0);

                    #endregion

                    #region [Small bars]

                    if (ExamChange[i] == true)
                    {
                        for (int j = 1; j < CExamInfo.cExamMaxSongs; j++)
                        {

                            if (!(TJAPlayer3.stage選曲.r確定された曲.DanSongs[j - 1].Dan_C[i] != null && TJAPlayer3.stage選曲.r確定された曲.DanSongs[NowShowingNumber].Dan_C[i] != null))
                                continue;

                            // rainbowBetterSuccess (bool) : is current minibar better success ? | drawGaugeTypetwo (int) : Gauge style [0,2]
                            #region [Success type variables]
        
                            bool rainbowBetterSuccess = GetExamStatus(TJAPlayer3.stage選曲.r確定された曲.DanSongs[j - 1].Dan_C[i]) == Exam.Status.Better_Success 
                                && GetExamConfirmStatus(TJAPlayer3.stage選曲.r確定された曲.DanSongs[j - 1].Dan_C[i]);

                            int amountToPercent;
                            int drawGaugeTypetwo = 0;

                            if (!rainbowBetterSuccess)
                            {
                                amountToPercent = TJAPlayer3.stage選曲.r確定された曲.DanSongs[j - 1].Dan_C[i].GetAmountToPercent();

                                if (amountToPercent >= 100)
                                    drawGaugeTypetwo = 2;
                                else if (TJAPlayer3.stage選曲.r確定された曲.DanSongs[j - 1].Dan_C[i].GetExamRange() == Exam.Range.More && amountToPercent >= 70 || amountToPercent > 70)
                                    drawGaugeTypetwo = 1;
                            }

                            #endregion

                            // Small bar elements base opacity
                            #region [Default opacity]

                            TJAPlayer3.Tx.DanC_SmallBase.Opacity = 255;
                            TJAPlayer3.Tx.DanC_Small_ExamCymbol.Opacity = 255;

                            TJAPlayer3.Tx.Gauge_Dan_Rainbow[0].Opacity = 255;
                            TJAPlayer3.Tx.DanC_MiniNumber.Opacity = 255;

                            TJAPlayer3.Tx.DanC_Gauge[drawGaugeTypetwo].Opacity = 255;

                            #endregion

                            // Currently showing song parameters
                            if (NowShowingNumber == j)
                            {
                                if (Counter_Wait != null && Counter_Wait.n現在の値 >= 800)
                                {
                                    #region [counter800 opacity]

                                    TJAPlayer3.Tx.DanC_SmallBase.Opacity = counter800;
                                    TJAPlayer3.Tx.DanC_Small_ExamCymbol.Opacity = counter800;

                                    TJAPlayer3.Tx.Gauge_Dan_Rainbow[0].Opacity = counter800;
                                    TJAPlayer3.Tx.DanC_MiniNumber.Opacity = counter800;

                                    TJAPlayer3.Tx.DanC_Gauge[drawGaugeTypetwo].Opacity = counter800;

                                    #endregion
                                }
                                else if (Counter_In != null || (Counter_Wait != null && Counter_Wait.n現在の値 < 800))
                                {
                                    #region [0 opacity]

                                    TJAPlayer3.Tx.DanC_SmallBase.Opacity = 0;
                                    TJAPlayer3.Tx.DanC_Small_ExamCymbol.Opacity = 0;

                                    TJAPlayer3.Tx.Gauge_Dan_Rainbow[0].Opacity = 0;
                                    TJAPlayer3.Tx.DanC_MiniNumber.Opacity = 0;

                                    TJAPlayer3.Tx.DanC_Gauge[drawGaugeTypetwo].Opacity = 0;

                                    #endregion
                                }
                            }

                            // Bars starting from the song N
                            if (NowShowingNumber >= j)
                            {
                                // Determine bars width
                                TJAPlayer3.Tx.DanC_SmallBase.vc拡大縮小倍率.X = isSmallGauge ? 0.34f : 1f;

                                // 815 : Small base (70 + 745)
                                int miniBarPositionX = barXOffset + (isSmallGauge ? 410 : 745);

                                // 613 + (j - 1) * 33 : Small base (barYoffset for 3rd exam : 494 + 119 + Local song offset (j - 1) * 33)
                                int miniBarPositionY = (barYOffset + 119) + (j - 1) * 33 - (TJAPlayer3.Skin.Game_DanC_Size[1] + (TJAPlayer3.Skin.Game_DanC_Padding));

                                // Display bars
                                #region [Displayables]

                                // Display mini-bar base and small symbol
                                TJAPlayer3.Tx.DanC_SmallBase?.t2D描画(TJAPlayer3.app.Device, miniBarPositionX, miniBarPositionY);
                                TJAPlayer3.Tx.DanC_Small_ExamCymbol?.t2D描画(TJAPlayer3.app.Device, miniBarPositionX - 30, miniBarPositionY - 3, new RectangleF(0, (j - 1) * 28, 30, 28));

                                // Display bar content
                                if (rainbowBetterSuccess)
                                {
                                    TJAPlayer3.Tx.Gauge_Dan_Rainbow[0].vc拡大縮小倍率.X = 0.23875f * TJAPlayer3.Tx.DanC_SmallBase.vc拡大縮小倍率.X * (isSmallGauge ? 0.94f : 1f);
                                    TJAPlayer3.Tx.Gauge_Dan_Rainbow[0].vc拡大縮小倍率.Y = 0.35185f;

                                    TJAPlayer3.Tx.Gauge_Dan_Rainbow[0]?.t2D描画(TJAPlayer3.app.Device, miniBarPositionX + 3, miniBarPositionY + 2,
                                        new Rectangle(0, 0, (int)(TJAPlayer3.stage選曲.r確定された曲.DanSongs[j - 1].Dan_C[i].GetAmountToPercent() * (TJAPlayer3.Tx.Gauge_Dan_Rainbow[0].szテクスチャサイズ.Width / 100.0)), TJAPlayer3.Tx.Gauge_Dan_Rainbow[0].szテクスチャサイズ.Height));
                                }
                                else
                                {
                                    TJAPlayer3.Tx.DanC_Gauge[drawGaugeTypetwo].vc拡大縮小倍率.X = 0.23875f * TJAPlayer3.Tx.DanC_SmallBase.vc拡大縮小倍率.X * (isSmallGauge ? 0.94f : 1f);
                                    TJAPlayer3.Tx.DanC_Gauge[drawGaugeTypetwo].vc拡大縮小倍率.Y = 0.35185f;

                                    TJAPlayer3.Tx.DanC_Gauge[drawGaugeTypetwo]?.t2D描画(TJAPlayer3.app.Device, miniBarPositionX + 3, miniBarPositionY + 2,
                                        new Rectangle(0, 0, (int)(TJAPlayer3.stage選曲.r確定された曲.DanSongs[j - 1].Dan_C[i].GetAmountToPercent() * (TJAPlayer3.Tx.DanC_Gauge[drawGaugeTypetwo].szテクスチャサイズ.Width / 100.0)), TJAPlayer3.Tx.DanC_Gauge[drawGaugeTypetwo].szテクスチャサイズ.Height));
                                }

                                // Usually +23 for gold and +17 for white, to test
                                DrawMiniNumber(TJAPlayer3.stage選曲.r確定された曲.DanSongs[j - 1].Dan_C[i].GetAmount(), miniBarPositionX + 11, miniBarPositionY + 20, 14, TJAPlayer3.stage選曲.r確定された曲.DanSongs[j - 1].Dan_C[i]);

                                #endregion
                            }
                        }
                    }

                    #endregion

                    #region [Currently playing song icons]

                    TJAPlayer3.Tx.DanC_ExamCymbol.Opacity = 255;

                    if (ExamChange[i] && NowShowingNumber != 0)
                    {
                        if (Counter_Wait != null)
                        {
                            if (Counter_Wait.n現在の値 >= 800)
                                TJAPlayer3.Tx.DanC_ExamCymbol.Opacity = counter800;
                            else if (Counter_Wait.n現在の値 >= 800 - 255)
                                TJAPlayer3.Tx.DanC_ExamCymbol.Opacity = counter255M255;
                        }
                    }

                    //75, 418
                    // 292 - 228 = 64
                    if (ExamChange[i])
                    {
                        TJAPlayer3.Tx.DanC_ExamCymbol.t2D描画(TJAPlayer3.app.Device, barXOffset + 5, lowerBarYOffset - 64, new RectangleF(0, 41 * NowCymbolShowingNumber, 197, 41));
                    }

                    #endregion

                    #region [Large bars]

                    // LrainbowBetterSuccess (bool) : is current minibar better success ? | LdrawGaugeTypetwo (int) : Gauge style [0,2]
                    #region [Success type variables]

                    bool LrainbowBetterSuccess = GetExamStatus(dan_C[i]) == Exam.Status.Better_Success && GetExamConfirmStatus(dan_C[i]);

                    int LamountToPercent;
                    int LdrawGaugeTypetwo = 0;

                    if (!LrainbowBetterSuccess)
                    {
                        LamountToPercent = dan_C[i].GetAmountToPercent();

                        if (LamountToPercent >= 100)
                            LdrawGaugeTypetwo = 2;
                        else if (dan_C[i].GetExamRange() == Exam.Range.More && LamountToPercent >= 70 || LamountToPercent > 70)
                            LdrawGaugeTypetwo = 1;
                    }



                    #endregion

                    // rainbowIndex : Rainbow bar texture to display (int), rainbowBase : same as rainbowIndex, but 0 if the counter is maxed
                    #region [Rainbow gauge counter]

                    int rainbowIndex = 0;
                    int rainbowBase = 0;
                    if (LrainbowBetterSuccess)
                    {
                        this.ct虹アニメ.t進行Loop();
                        this.ct虹透明度.t進行Loop();

                        rainbowIndex = this.ct虹アニメ.n現在の値;

                        rainbowBase = rainbowIndex;
                        if (rainbowBase == ct虹アニメ.n終了値) rainbowBase = 0;
                    }

                    #endregion

                    #region [Default opacity]

                    TJAPlayer3.Tx.DanC_Gauge[LdrawGaugeTypetwo].Opacity = 255;

                    TJAPlayer3.Tx.Gauge_Dan_Rainbow[rainbowIndex].Opacity = 255;

                    TJAPlayer3.Tx.DanC_Number.Opacity = 255;
                    TJAPlayer3.Tx.DanC_ExamRange.Opacity = 255;
                    TJAPlayer3.Tx.DanC_Small_Number.Opacity = 255;

                    #endregion

                    if (ExamChange[i] && NowShowingNumber != 0 && Counter_Wait != null)
                    {
                        if (Counter_Wait.n現在の値 >= 800)
                        {
                            #region [counter800 opacity]

                            TJAPlayer3.Tx.DanC_Gauge[LdrawGaugeTypetwo].Opacity = counter800;

                            TJAPlayer3.Tx.Gauge_Dan_Rainbow[rainbowIndex].Opacity = counter800;

                            TJAPlayer3.Tx.DanC_Number.Opacity = counter800;
                            TJAPlayer3.Tx.DanC_ExamRange.Opacity = counter800;
                            TJAPlayer3.Tx.DanC_Small_Number.Opacity = counter800;

                            #endregion
                        }
                        else if (Counter_Wait.n現在の値 >= 800 - 255)
                        {
                            #region [counter255M255 opacity]

                            TJAPlayer3.Tx.DanC_Gauge[LdrawGaugeTypetwo].Opacity = counter255M255;

                            TJAPlayer3.Tx.Gauge_Dan_Rainbow[rainbowIndex].Opacity = counter255M255;

                            TJAPlayer3.Tx.DanC_Number.Opacity = counter255M255;
                            TJAPlayer3.Tx.DanC_ExamRange.Opacity = counter255M255;
                            TJAPlayer3.Tx.DanC_Small_Number.Opacity = counter255M255;

                            #endregion
                        }
                    }

                    #region [Displayables]

                    // Non individual : 209 / 650 : 0.32154f
                    // Individual : 97 / 432 : 0.22454f

                    float xExtend = ExamChange[i] ? (isSmallGauge ? 0.215f * 0.663333333f : 0.663333333f) : (isSmallGauge ? 0.32154f : 1.0f);

                    if (LrainbowBetterSuccess)
                    {
                        #region [Rainbow gauge display]

                        TJAPlayer3.Tx.Gauge_Dan_Rainbow[rainbowIndex].vc拡大縮小倍率.X = xExtend;

                        // Reset base since it was used for minibars
                        TJAPlayer3.Tx.Gauge_Dan_Rainbow[0].vc拡大縮小倍率.X = xExtend;
                        TJAPlayer3.Tx.Gauge_Dan_Rainbow[0].vc拡大縮小倍率.Y = 1.0f;

                        if (Counter_Wait != null && !(Counter_Wait.n現在の値 <= 1055 && Counter_Wait.n現在の値 >= 800 - 255))
                        {
                            TJAPlayer3.Tx.Gauge_Dan_Rainbow[rainbowIndex].Opacity = 255;
                        }

                        TJAPlayer3.Tx.Gauge_Dan_Rainbow[rainbowIndex]?.t2D拡大率考慮下基準描画(TJAPlayer3.app.Device,
                            barXOffset + TJAPlayer3.Skin.Game_DanC_Offset[0], lowerBarYOffset - TJAPlayer3.Skin.Game_DanC_Offset[1],
                            new Rectangle(0, 0, (int)(dan_C[i].GetAmountToPercent() * (TJAPlayer3.Tx.Gauge_Dan_Rainbow[rainbowIndex].szテクスチャサイズ.Width / 100.0)), TJAPlayer3.Tx.Gauge_Dan_Rainbow[rainbowIndex].szテクスチャサイズ.Height));

                        if (Counter_Wait != null && !(Counter_Wait.n現在の値 <= 1055 && Counter_Wait.n現在の値 >= 800 - 255))
                        {
                            TJAPlayer3.Tx.Gauge_Dan_Rainbow[rainbowBase].Opacity = (ct虹透明度.n現在の値 * 255 / (int)ct虹透明度.n終了値) / 1;
                        }

                        TJAPlayer3.Tx.Gauge_Dan_Rainbow[rainbowBase]?.t2D拡大率考慮下基準描画(TJAPlayer3.app.Device,
                            barXOffset + TJAPlayer3.Skin.Game_DanC_Offset[0], lowerBarYOffset - TJAPlayer3.Skin.Game_DanC_Offset[1],
                            new Rectangle(0, 0, (int)(dan_C[i].GetAmountToPercent() * (TJAPlayer3.Tx.Gauge_Dan_Rainbow[rainbowBase].szテクスチャサイズ.Width / 100.0)), TJAPlayer3.Tx.Gauge_Dan_Rainbow[rainbowIndex].szテクスチャサイズ.Height));

                        #endregion
                    }
                    else
                    {
                        #region [Regular gauge display]

                        TJAPlayer3.Tx.DanC_Gauge[LdrawGaugeTypetwo].vc拡大縮小倍率.X = xExtend;
                        TJAPlayer3.Tx.DanC_Gauge[LdrawGaugeTypetwo].vc拡大縮小倍率.Y = 1.0f;
                        TJAPlayer3.Tx.DanC_Gauge[LdrawGaugeTypetwo]?.t2D拡大率考慮下基準描画(TJAPlayer3.app.Device,
                            barXOffset + TJAPlayer3.Skin.Game_DanC_Offset[0], lowerBarYOffset - TJAPlayer3.Skin.Game_DanC_Offset[1],
                            new Rectangle(0, 0, (int)(dan_C[i].GetAmountToPercent() * (TJAPlayer3.Tx.DanC_Gauge[LdrawGaugeTypetwo].szテクスチャサイズ.Width / 100.0)), TJAPlayer3.Tx.DanC_Gauge[LdrawGaugeTypetwo].szテクスチャサイズ.Height));

                        #endregion
                    }

                    #endregion


                    #endregion

                    #region [Print the current value number]

                    int nowAmount = dan_C[i].Amount;

                    if (dan_C[i].GetExamRange() == Exam.Range.Less)
                        nowAmount = dan_C[i].Value[0] - dan_C[i].Amount;

                    if (nowAmount < 0) nowAmount = 0;

                    float numberXScale = isSmallGauge ? TJAPlayer3.Skin.Game_DanC_Number_Small_Scale * 0.6f : TJAPlayer3.Skin.Game_DanC_Number_Small_Scale;
                    float numberYScale = isSmallGauge ? TJAPlayer3.Skin.Game_DanC_Number_Small_Scale * 0.8f : TJAPlayer3.Skin.Game_DanC_Number_Small_Scale;
                    int numberPadding = (int)(TJAPlayer3.Skin.Game_DanC_Number_Padding * (isSmallGauge ? 0.6f : 1f));

                    DrawNumber(nowAmount,
                        barXOffset + TJAPlayer3.Skin.Game_DanC_Number_Small_Number_Offset[0],
                        lowerBarYOffset - TJAPlayer3.Skin.Game_DanC_Number_Small_Number_Offset[1],
                        numberPadding, 
                        true, 
                        Challenge[i], 
                        numberXScale, 
                        numberYScale, 
                        (Status[i].Timer_Amount != null ? ScoreScale[Status[i].Timer_Amount.n現在の値] : 0f));

                    #endregion

                    #region [Dan conditions display]

                    int offset = TJAPlayer3.Skin.Game_DanC_Exam_Offset[0];

                    TJAPlayer3.Tx.DanC_ExamType.vc拡大縮小倍率.X = 1.0f;
                    TJAPlayer3.Tx.DanC_ExamType.vc拡大縮小倍率.Y = 1.0f;

                    // Exam range (Less than/More)
                    TJAPlayer3.Tx.DanC_ExamRange?.t2D拡大率考慮下基準描画(TJAPlayer3.app.Device,
                        barXOffset + offset - TJAPlayer3.Tx.DanC_ExamRange.szテクスチャサイズ.Width,
                        lowerBarYOffset - TJAPlayer3.Skin.Game_DanC_Exam_Offset[1], 
                        new Rectangle(0, TJAPlayer3.Skin.Game_DanC_ExamRange_Size[1] * (int)dan_C[i].GetExamRange(), TJAPlayer3.Skin.Game_DanC_ExamRange_Size[0], TJAPlayer3.Skin.Game_DanC_ExamRange_Size[1]));
                    
                    offset -= TJAPlayer3.Skin.Game_DanC_ExamRange_Padding;

                    // Condition number
                    DrawNumber(
                        dan_C[i].Value[0],
                        barXOffset + offset - dan_C[i].Value[0].ToString().Length * (int)(TJAPlayer3.Skin.Game_DanC_Number_Small_Padding * TJAPlayer3.Skin.Game_DanC_Exam_Number_Scale),
                        lowerBarYOffset - TJAPlayer3.Skin.Game_DanC_Exam_Offset[1] - 1, 
                        (int)(TJAPlayer3.Skin.Game_DanC_Number_Small_Padding * TJAPlayer3.Skin.Game_DanC_Exam_Number_Scale), 
                        false, 
                        Challenge[i]);


                    // Exam type flag
                    TJAPlayer3.Tx.DanC_ExamType?.t2D拡大率考慮下基準描画(TJAPlayer3.app.Device,
                        barXOffset + TJAPlayer3.Skin.Game_DanC_Exam_Offset[0] - TJAPlayer3.Tx.DanC_ExamType.szテクスチャサイズ.Width + 22,
                        lowerBarYOffset - TJAPlayer3.Skin.Game_DanC_Exam_Offset[1] - 48, 
                        new Rectangle(0, TJAPlayer3.Skin.Game_DanC_ExamType_Size[1] * (int)dan_C[i].GetExamType(), TJAPlayer3.Skin.Game_DanC_ExamType_Size[0], TJAPlayer3.Skin.Game_DanC_ExamType_Size[1]));

                    #endregion

                    #region [Failed condition box]

                    TJAPlayer3.Tx.DanC_Failed.vc拡大縮小倍率.X = isSmallGauge ? 0.33f : 1f;

                    if (dan_C[i].GetReached())
                    {
                        TJAPlayer3.Tx.DanC_Failed.t2D拡大率考慮下基準描画(TJAPlayer3.app.Device,
                            barXOffset + TJAPlayer3.Skin.Game_DanC_Offset[0],
                            lowerBarYOffset - TJAPlayer3.Skin.Game_DanC_Offset[1]);
                    }

                    #endregion
                
                }
                else
                {
                    #region [Gauge dan condition]

                    TJAPlayer3.Tx.DanC_Gauge_Base?.t2D描画(TJAPlayer3.app.Device, 
                        TJAPlayer3.Skin.Game_DanC_X[0] - ((50 - dan_C[i].GetValue(false) / 2) * 14) + 4, 
                        TJAPlayer3.Skin.Game_DanC_Y[0]);

                    // Display percentage here
                    DrawNumber(
                        dan_C[i].Value[0],
                        TJAPlayer3.Skin.Game_DanC_X[0] - ((50 - dan_C[i].GetValue(false) / 2) * 14) + 292 - dan_C[i].Value[0].ToString().Length * (int)(TJAPlayer3.Skin.Game_DanC_Number_Small_Padding * TJAPlayer3.Skin.Game_DanC_Exam_Number_Scale),
                        TJAPlayer3.Skin.Game_DanC_Y[0] - TJAPlayer3.Skin.Game_DanC_Exam_Offset[1] + 64,
                        (int)(TJAPlayer3.Skin.Game_DanC_Number_Small_Padding * TJAPlayer3.Skin.Game_DanC_Exam_Number_Scale),
                        false,
                        Challenge[i]);

                    #endregion
                }
            }
        }

        /// <summary>
        /// 段位チャレンジの数字フォントで数字を描画します。
        /// </summary>
        /// <param name="value">値。</param>
        /// <param name="x">一桁目のX座標。</param>
        /// <param name="y">一桁目のY座標</param>
        /// <param name="padding">桁数間の字間</param>
        /// <param name="scaleX">拡大率X</param>
        /// <param name="scaleY">拡大率Y</param>
        /// <param name="scaleJump">アニメーション用拡大率(Yに加算される)。</param>
        private void DrawNumber(int value, int x, int y, int padding,　bool bBig, Dan_C dan_c, float scaleX = 1.0f, float scaleY = 1.0f, float scaleJump = 0.0f)
        {

            if (TJAPlayer3.Tx.DanC_Number == null || TJAPlayer3.Tx.DanC_Small_Number == null || value < 0)
                return;

            if (value == 0)
            {
                TJAPlayer3.Tx.DanC_Number.color4 = Color.Gray;
                TJAPlayer3.Tx.DanC_Small_Number.color4 = Color.Gray;
            }
            else
            {
                TJAPlayer3.Tx.DanC_Number.color4 = Color.White;
                TJAPlayer3.Tx.DanC_Small_Number.color4 = Color.White;
            }
                
            if (bBig)
            {
                var notesRemainDigit = 0;
                for (int i = 0; i < value.ToString().Length; i++)
                {
                    var number = Convert.ToInt32(value.ToString()[i].ToString());
                    Rectangle rectangle = new Rectangle(TJAPlayer3.Skin.Game_DanC_Number_Size[0] * number - 1, GetExamConfirmStatus(dan_c) ? TJAPlayer3.Skin.Game_DanC_Number_Size[1] : 0, TJAPlayer3.Skin.Game_DanC_Number_Size[0], TJAPlayer3.Skin.Game_DanC_Number_Size[1]);
                    if (TJAPlayer3.Tx.DanC_Number != null)
                    {
                        TJAPlayer3.Tx.DanC_Number.vc拡大縮小倍率.X = scaleX;
                        TJAPlayer3.Tx.DanC_Number.vc拡大縮小倍率.Y = scaleY + scaleJump;
                    }
                    TJAPlayer3.Tx.DanC_Number?.t2D拡大率考慮下中心基準描画(TJAPlayer3.app.Device, x - (notesRemainDigit * padding), y, rectangle);
                    notesRemainDigit--;
                }
            }
            else
            {
                var notesRemainDigit = 0;
                for (int i = 0; i < value.ToString().Length; i++)
                {
                    var number = Convert.ToInt32(value.ToString()[i].ToString());
                    Rectangle rectangle = new Rectangle(TJAPlayer3.Skin.Game_DanC_Small_Number_Size[0] * number - 1, 0, TJAPlayer3.Skin.Game_DanC_Small_Number_Size[0], TJAPlayer3.Skin.Game_DanC_Small_Number_Size[1]);
                    if (TJAPlayer3.Tx.DanC_Small_Number != null)
                    {
                        TJAPlayer3.Tx.DanC_Small_Number.vc拡大縮小倍率.X = scaleX;
                        TJAPlayer3.Tx.DanC_Small_Number.vc拡大縮小倍率.Y = scaleY + scaleJump;
                    }
                    TJAPlayer3.Tx.DanC_Small_Number?.t2D拡大率考慮下中心基準描画(TJAPlayer3.app.Device, x - (notesRemainDigit * padding), y, rectangle);
                    notesRemainDigit--;
                }
            }
        }

        public void DrawMiniNumber(int value, int x, int y, int padding, Dan_C dan_c)
        {
            if (TJAPlayer3.Tx.DanC_MiniNumber == null || value < 0)
                return;

            var notesRemainDigit = 0;
            if (value < 0)
                return;
            for (int i = 0; i < value.ToString().Length; i++)
            {
                var number = Convert.ToInt32(value.ToString()[i].ToString());
                Rectangle rectangle = new Rectangle(TJAPlayer3.Skin.Game_DanC_MiniNumber_Size[0] * number - 1, GetExamConfirmStatus(dan_c) ? TJAPlayer3.Skin.Game_DanC_MiniNumber_Size[1] : 0, TJAPlayer3.Skin.Game_DanC_MiniNumber_Size[0], TJAPlayer3.Skin.Game_DanC_MiniNumber_Size[1]);
                TJAPlayer3.Tx.DanC_MiniNumber.t2D拡大率考慮下中心基準描画(TJAPlayer3.app.Device, x - (notesRemainDigit * padding), y, rectangle);
                notesRemainDigit--;
            }
        }

        /// <summary>
        /// n個の条件がひとつ以上達成失敗しているかどうかを返します。
        /// </summary>
        /// <returns>n個の条件がひとつ以上達成失敗しているか。</returns>
        public bool GetFailedAllChallenges()
        {
            var isFailed = false;
            for (int i = 0; i < CExamInfo.cMaxExam; i++)
            {
                if (Challenge[i] == null) continue;
                
                for(int j = 0; j < TJAPlayer3.stage選曲.r確定された曲.DanSongs.Count; j++ )
                {
                    if(TJAPlayer3.stage選曲.r確定された曲.DanSongs[j].Dan_C[i] != null)
                    {
                        if (TJAPlayer3.stage選曲.r確定された曲.DanSongs[j].Dan_C[i].GetReached()) isFailed = true;
                    }
                }
                if (Challenge[i].GetReached()) isFailed = true;
            }
            return isFailed;
        }

        /// <summary>
        /// n個の条件で段位認定モードのステータスを返します。
        /// </summary>
        /// <param name="dan_C">条件。</param>
        /// <returns>ExamStatus。</returns>
        public Exam.Status GetExamStatus(Dan_C[] dan_C)
        {
            var status = Exam.Status.Better_Success;
            
            for (int i = 0; i < CExamInfo.cMaxExam; i++)
            {
                if (dan_C[i] == null || dan_C[i].GetEnable() != true)
                    continue;

                if (!dan_C[i].GetCleared()[1]) status = Exam.Status.Success;
                if (!dan_C[i].GetCleared()[0]) return (Exam.Status.Failure);
            }

            return status;
        }

        public Exam.Status GetExamStatus(Dan_C dan_C)
        {
            var status = Exam.Status.Better_Success;
            if (!dan_C.GetCleared()[1]) status = Exam.Status.Success;
            if (!dan_C.GetCleared()[0]) status = Exam.Status.Failure;
            return status;
        }

        public bool GetExamConfirmStatus(Dan_C dan_C)
        {
            switch (dan_C.GetExamRange())
            {
                case Exam.Range.Less:
                    {
                        if (GetExamStatus(dan_C) == Exam.Status.Better_Success && notesremain == 0)
                            return true;
                        else
                            return false;
                    }

                case Exam.Range.More:
                    {
                        if (dan_C.GetExamType() == Exam.Type.Accuracy && notesremain != 0)
                            return false;
                        else if (GetExamStatus(dan_C) == Exam.Status.Better_Success)
                            return true;
                        else
                            return false;
                    }
            }
            return false;
        }

        public Dan_C[] GetExam()
        {
            return Challenge;
        }


        private readonly float[] ScoreScale = new float[]
        {
            0.000f,
            0.111f, // リピート
            0.222f,
            0.185f,
            0.148f,
            0.129f,
            0.111f,
            0.074f,
            0.065f,
            0.033f,
            0.015f,
            0.000f
        };

        [StructLayout(LayoutKind.Sequential)]
        struct ChallengeStatus
        {
            public SlimDX.Color4 Color;
            public CCounter Timer_Gauge;
            public CCounter Timer_Amount;
            public CCounter Timer_Failed;
        }

        #region[ private ]
        //-----------------

        private bool bExamChangeCheck;
        private int notesremain;
        private int[] songsnotesremain;
        private bool[] ExamChange = new bool[CExamInfo.cMaxExam];
        private int ExamCount;
        private ChallengeStatus[] Status = new ChallengeStatus[CExamInfo.cMaxExam];
        private CTexture Dan_Plate;
        private bool[] IsEnded;
        public bool FirstSectionAnime;

        // アニメ関連
        public int NowShowingNumber;
        public int NowCymbolShowingNumber;
        private CCounter Counter_In, Counter_Wait, Counter_Out, Counter_Text;
        private double[] ScreenPoint;
        private int Counter_In_Old, Counter_Out_Old, Counter_Text_Old;
        public bool IsAnimating;

        //音声関連
        private CSound Sound_Section;
        private CSound Sound_Section_First;
        private CSound Sound_Failed;

        private CCounter ct虹アニメ;
        private CCounter ct虹透明度;

        //-----------------
        #endregion
    }
}
