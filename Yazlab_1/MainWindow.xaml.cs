using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Yazlab_1
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        //Border size data
        int BORDER_SİZE_X = 20;
        int BORDER_SİZE_Y = 20;
        //player a
        Boolean A_WAIT = false;
        int A_SELFGOLD = 200;
        int A_MOVE_GOLD = 5;
        int A_GOLD_SELECT_COST = 5;
        int A_POS_X = 0, A_POS_Y = 0;
        int A_TARGET_X = 0, A_TARGET_Y = 0;
        int A_MOVE_COUNT = 3;
        //player b
        Boolean B_WAIT = true;
        int B_SELFGOLD = 200;
        int B_MOVE_GOLD = 5;
        int B_GOLD_SELECT_COST = 10;
        int B_POS_X = 19, B_POS_Y = 0;
        int B_TARGET_X = 19, B_TARGET_Y = 0;
        int B_MOVE_COUNT = 3;
        //player c
        Boolean C_WAIT = true;
        int C_SELFGOLD = 200;
        int C_MOVE_GOLD = 5;
        int C_GOLD_SELECT_COST = 15;
        int C_POS_X = 0, C_POS_Y = 19;
        int C_TARGET_X = 0, C_TARGET_Y = 19;
        int C_MOVE_COUNT = 3;
        //player d
        Boolean D_WAIT = true;
        int D_SELFGOLD = 200;
        int D_MOVE_GOLD = 5;
        int D_GOLD_SELECT_COST = 20;
        int D_POS_X = 19, D_POS_Y = 19;
        int D_TARGET_X = 19, D_TARGET_Y = 19;
        int D_MOVE_COUNT = 0;

        //Dinamic Gold System
        int Gold_piece, Hidden_Gold_rate;
        //matris system def
        int GOLD = 0;
        int HIDDEN_GOLD = 1;
        int PLAYER = 2;

        //map matris
        int[,,] matris;
        int distance = 9999999;


        //BURAYİ DÜZENLEMEK BEN
        public MainWindow(int _border_x, int _border_y, int _gold_piece, int _hidden_gold_rate, int _a_self_gold, int _a_move_gold, int _a_gold_select_cost, int _b_self_gold, int _b_move_gold, int _b_gold_select_cost, int _c_self_gold, int _c_move_gold, int _c_gold_select_cost, int _d_self_gold, int _d_move_gold, int _d_gold_select_cost)
        {
            InitializeComponent();
            BORDER_SİZE_X = _border_x;
            BORDER_SİZE_Y = _border_y;
            Gold_piece = BORDER_SİZE_X * BORDER_SİZE_Y / (100 / _gold_piece);
            Hidden_Gold_rate = Gold_piece / (100 / _hidden_gold_rate);
            A_SELFGOLD = _a_self_gold;
            A_MOVE_GOLD = _a_move_gold;
            A_GOLD_SELECT_COST = _a_gold_select_cost;
            B_SELFGOLD = _b_self_gold;
            B_MOVE_GOLD = _b_move_gold;
            B_GOLD_SELECT_COST = _b_gold_select_cost;
            C_SELFGOLD = _c_self_gold;
            C_MOVE_GOLD = _c_move_gold;
            C_GOLD_SELECT_COST = _c_gold_select_cost;
            D_SELFGOLD = _d_self_gold;
            D_MOVE_GOLD = _d_move_gold;
            D_GOLD_SELECT_COST = _d_gold_select_cost;
            A_POS_X = 0;
            A_POS_Y = 0;
            A_TARGET_X = A_POS_X;
            A_TARGET_Y = A_POS_Y;
            B_POS_X = BORDER_SİZE_X-1;
            B_POS_Y = 0;
            B_TARGET_X = B_POS_X;
            B_TARGET_Y = B_POS_Y;
            C_POS_X = 0;
            C_POS_Y = BORDER_SİZE_Y-1;
            C_TARGET_X = C_POS_X;
            C_TARGET_Y = C_POS_Y;
            D_POS_X = BORDER_SİZE_X - 1;
            D_POS_Y = BORDER_SİZE_Y - 1;
            D_TARGET_X = D_POS_X;
            D_TARGET_Y = D_POS_Y;

            matris = new int[BORDER_SİZE_X, BORDER_SİZE_Y, 3];





            setPlayers();
            randomizegolds(BORDER_SİZE_X, BORDER_SİZE_Y, matris, Gold_piece - Hidden_Gold_rate, Hidden_Gold_rate);
            splitGrid(BORDER_SİZE_X, BORDER_SİZE_Y);
            brushcells(BORDER_SİZE_X, BORDER_SİZE_Y, matris);




            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(0.5);
            dt.Tick += Dt_Tick;
            dt.Start();








        }
        private void Dt_Tick(object sender, EventArgs e)
        {
            aPlayermove();
            bPlayermove();
            cPlayermove();
            dPlayermove();
            
        }
        Boolean yakin_mi(int targetx, int targety, int rivalx, int rivaly, int myx, int myy)
        {
            if(Math.Abs(targetx-rivalx)+Math.Abs(targety-rivaly)-3> Math.Abs(targetx - myx) + Math.Abs(targety - myy))
            {
                return true;
            }
            else
            {
                return false;
            }

            
        }
        void dPlayermove()
        {
            if (D_WAIT == false)
            {
                System.IO.File.AppendAllText("Dplayer.txt", "D nın goldu: " + D_SELFGOLD+ "\n");
                if (D_MOVE_COUNT != 0 && D_SELFGOLD != 0 && D_SELFGOLD - D_MOVE_GOLD >= 0)
                {
                    if (D_TARGET_X > D_POS_X)
                    {
                        System.IO.File.AppendAllText("Dplayer.txt", "D nın  kordinatları " + D_POS_X.ToString() + "," + D_POS_Y.ToString() + "\n");
                        D_MOVE_COUNT--;
                        D_SELFGOLD -= D_MOVE_GOLD;
                        matris[D_POS_X, D_POS_Y, PLAYER] = 0;
                        D_POS_X++;
                        matris[D_POS_X, D_POS_Y, PLAYER] = 4;
                        brushcells(BORDER_SİZE_X, BORDER_SİZE_Y, matris);
                        if (matris[D_POS_X, D_POS_Y, GOLD] != 0)
                        {
                            D_SELFGOLD += matris[D_POS_X, D_POS_Y, GOLD];
                            matris[D_POS_X, D_POS_Y, GOLD] = 0;

                        }
                        if (matris[D_POS_X, D_POS_Y, HIDDEN_GOLD] != 0)
                        {
                            matris[D_POS_X, D_POS_Y, GOLD] = matris[D_POS_X, D_POS_Y, HIDDEN_GOLD];
                            matris[D_POS_X, D_POS_Y, HIDDEN_GOLD] = 0;
                        }
                    }
                    else if (D_TARGET_X < D_POS_X)
                    {
                        System.IO.File.AppendAllText("Dplayer.txt", "D nın  kordinatları " + D_POS_X.ToString() + "," + D_POS_Y.ToString() + "\n");
                        D_MOVE_COUNT--;
                        D_SELFGOLD -= D_MOVE_GOLD;
                        matris[D_POS_X, D_POS_Y, PLAYER] = 0;
                        D_POS_X--;
                        matris[D_POS_X, D_POS_Y, PLAYER] = 4;
                        brushcells(BORDER_SİZE_X, BORDER_SİZE_Y, matris);
                        if (matris[D_POS_X, D_POS_Y, GOLD] != 0)
                        {
                            D_SELFGOLD += matris[D_POS_X, D_POS_Y, GOLD];
                            matris[D_POS_X, D_POS_Y, GOLD] = 0;

                        }
                        if (matris[D_POS_X, D_POS_Y, HIDDEN_GOLD] != 0)
                        {
                            matris[D_POS_X, D_POS_Y, GOLD] = matris[D_POS_X, D_POS_Y, HIDDEN_GOLD];
                            matris[D_POS_X, D_POS_Y, HIDDEN_GOLD] = 0;
                        }

                    }
                    else if (D_TARGET_Y > D_POS_Y)
                    {
                        System.IO.File.AppendAllText("Dplayer.txt", "D nın  kordinatları " + D_POS_X.ToString() + "," + D_POS_Y.ToString() + "\n");
                        D_MOVE_COUNT--;
                        D_SELFGOLD -= D_MOVE_GOLD;
                        matris[D_POS_X, D_POS_Y, PLAYER] = 0;
                        D_POS_Y++;
                        matris[D_POS_X, D_POS_Y, PLAYER] = 4;
                        brushcells(BORDER_SİZE_X, BORDER_SİZE_Y, matris);
                        if (matris[D_POS_X, D_POS_Y, GOLD] != 0)
                        {
                            D_SELFGOLD += matris[D_POS_X, D_POS_Y, GOLD];
                            matris[D_POS_X, D_POS_Y, GOLD] = 0;

                        }
                        if (matris[D_POS_X, D_POS_Y, HIDDEN_GOLD] != 0)
                        {
                            matris[D_POS_X, D_POS_Y, GOLD] = matris[D_POS_X, D_POS_Y, HIDDEN_GOLD];
                            matris[D_POS_X, D_POS_Y, HIDDEN_GOLD] = 0;
                        }


                    }
                    else if (D_TARGET_Y < D_POS_Y)
                    {
                        System.IO.File.AppendAllText("Dplayer.txt", "D nın  kordinatları " + D_POS_X.ToString() + "," + D_POS_Y.ToString() + "\n");
                        D_MOVE_COUNT--;
                        D_SELFGOLD -= D_MOVE_GOLD;
                        matris[D_POS_X, D_POS_Y, PLAYER] = 0;
                        D_POS_Y--;
                        matris[D_POS_X, D_POS_Y, PLAYER] = 4;
                        brushcells(BORDER_SİZE_X, BORDER_SİZE_Y, matris);
                        if (matris[D_POS_X, D_POS_Y, GOLD] != 0)
                        {
                            D_SELFGOLD += matris[D_POS_X, D_POS_Y, GOLD];
                            matris[D_POS_X, D_POS_Y, GOLD] = 0;

                        }
                        if (matris[D_POS_X, D_POS_Y, HIDDEN_GOLD] != 0)
                        {
                            matris[D_POS_X, D_POS_Y, GOLD] = matris[D_POS_X, D_POS_Y, HIDDEN_GOLD];
                            matris[D_POS_X, D_POS_Y, HIDDEN_GOLD] = 0;
                        }

                    }

                }
                else
                {
                    A_WAIT = false;
                    A_MOVE_COUNT = 3;
                    D_WAIT = true;
                }
            }
            if ((D_TARGET_Y == D_POS_Y && D_POS_X == D_TARGET_X) || matris[D_TARGET_X, D_TARGET_Y, GOLD] == 0)
            {
                Console.WriteLine("D hedef seçti buralarada girdim");
                D_SELFGOLD -= D_GOLD_SELECT_COST;
                int profitable = 1;
                int gl = 0;
                distance = 9999999;
                for (int i = 0; i < BORDER_SİZE_X; i++)
                {
                    for (int j = 0; j < BORDER_SİZE_Y; j++)
                    {
                        if (matris[i, j, GOLD] != 0)
                        {
                            gl = matris[i, j, GOLD];
                            if (Math.Abs((i - D_POS_X)) + Math.Abs((j - D_POS_Y)) != 0 && gl / (Math.Abs((i - D_POS_X)) + Math.Abs((j - D_POS_Y))) > profitable / distance)
                            {
                                distance = Math.Abs((i - D_POS_X)) + Math.Abs((j - D_POS_Y));
                                profitable = gl;
                                D_TARGET_X = i;
                                D_TARGET_Y = j;


                            }

                        }

                    }

                }
                if (yakin_mi(A_TARGET_X, A_TARGET_Y, A_POS_X, A_TARGET_Y, D_POS_X, D_POS_Y))
                {
                    D_TARGET_X = A_TARGET_X;
                    D_TARGET_Y = A_TARGET_Y;
                    Console.WriteLine("girdim 1");
                }
                else if (yakin_mi(B_TARGET_X, B_TARGET_Y, B_POS_X, B_TARGET_Y, D_POS_X, D_POS_Y))
                {
                    D_TARGET_X = B_TARGET_X;
                    D_TARGET_Y = B_TARGET_Y;
                    Console.WriteLine("girdim 2");
                }
                else if (yakin_mi(C_TARGET_X, C_TARGET_Y, C_POS_X, C_TARGET_Y, C_POS_X, C_POS_Y))
                {
                    D_TARGET_X = C_TARGET_X;
                    D_TARGET_Y = C_TARGET_Y;
                    Console.WriteLine("girdim 3");
                }
                System.IO.File.AppendAllText("Dplayer.txt", "D nın hedefi kordinatları " + D_TARGET_X.ToString() + "," + D_TARGET_Y.ToString() + "\n");
            }

        }
        void cPlayermove()
        {
            if (C_WAIT == false)
            {
                System.IO.File.AppendAllText("Cplayer.txt", "C nın goldu: " + C_SELFGOLD+ "\n");
                if (C_MOVE_COUNT != 0 && C_SELFGOLD != 0 && C_SELFGOLD - C_MOVE_GOLD >= 0)
                {
                    if (C_TARGET_X > C_POS_X)
                    {
                        System.IO.File.AppendAllText("Cplayer.txt", "C nın  kordinatları " + C_POS_X.ToString() + "," + C_POS_Y.ToString() + "\n");
                        C_MOVE_COUNT--;
                        C_SELFGOLD -= C_MOVE_GOLD;
                        matris[C_POS_X, C_POS_Y, PLAYER] = 0;
                        C_POS_X++;
                        matris[C_POS_X, C_POS_Y, PLAYER] = 3;
                        brushcells(BORDER_SİZE_X, BORDER_SİZE_Y, matris);
                        if (matris[C_POS_X, C_POS_Y, GOLD] != 0)
                        {
                            C_SELFGOLD += matris[C_POS_X, C_POS_Y, GOLD];
                            matris[C_POS_X, C_POS_Y, GOLD] = 0;

                        }
                        if (matris[C_POS_X, C_POS_Y, HIDDEN_GOLD] != 0)
                        {
                            matris[C_POS_X, C_POS_Y, GOLD] = matris[C_POS_X, C_POS_Y, HIDDEN_GOLD];
                            matris[C_POS_X, C_POS_Y, HIDDEN_GOLD] = 0;
                        }
                    }
                    else if (C_TARGET_X < C_POS_X)
                    {
                        System.IO.File.AppendAllText("Cplayer.txt", "C nın  kordinatları " + C_POS_X.ToString() + "," + C_POS_Y.ToString() + "\n");
                        C_MOVE_COUNT--;
                        C_SELFGOLD -= C_MOVE_GOLD;
                        matris[C_POS_X, C_POS_Y, PLAYER] = 0;
                        C_POS_X--;
                        matris[C_POS_X, C_POS_Y, PLAYER] = 3;
                        brushcells(BORDER_SİZE_X, BORDER_SİZE_Y, matris);
                        if (matris[C_POS_X, C_POS_Y, GOLD] != 0)
                        {
                            C_SELFGOLD += matris[C_POS_X, C_POS_Y, GOLD];
                            matris[C_POS_X, C_POS_Y, GOLD] = 0;

                        }
                        if (matris[C_POS_X, C_POS_Y, HIDDEN_GOLD] != 0)
                        {
                            matris[C_POS_X, C_POS_Y, GOLD] = matris[C_POS_X, C_POS_Y, HIDDEN_GOLD];
                            matris[C_POS_X, C_POS_Y, HIDDEN_GOLD] = 0;
                        }

                    }
                    else if (C_TARGET_Y > C_POS_Y)
                    {
                        System.IO.File.AppendAllText("Cplayer.txt", "C nın  kordinatları " + C_POS_X.ToString() + "," + C_POS_Y.ToString() + "\n");
                        C_MOVE_COUNT--;
                        C_SELFGOLD -= C_MOVE_GOLD;
                        matris[C_POS_X, C_POS_Y, PLAYER] = 0;
                        C_POS_Y++;
                        matris[C_POS_X, C_POS_Y, PLAYER] = 3;
                        brushcells(BORDER_SİZE_X, BORDER_SİZE_Y, matris);
                        if (matris[C_POS_X, C_POS_Y, GOLD] != 0)
                        {
                            C_SELFGOLD += matris[C_POS_X, C_POS_Y, GOLD];
                            matris[C_POS_X, C_POS_Y, GOLD] = 0;

                        }
                        if (matris[C_POS_X, C_POS_Y, HIDDEN_GOLD] != 0)
                        {
                            matris[C_POS_X, C_POS_Y, GOLD] = matris[C_POS_X, C_POS_Y, HIDDEN_GOLD];
                            matris[C_POS_X, C_POS_Y, HIDDEN_GOLD] = 0;
                        }


                    }
                    else if (C_TARGET_Y < C_POS_Y)
                    {
                        System.IO.File.AppendAllText("Cplayer.txt", "C nın  kordinatları " + C_POS_X.ToString() + "," + C_POS_Y.ToString() + "\n");
                        C_MOVE_COUNT--;
                        C_SELFGOLD -= C_MOVE_GOLD;
                        matris[C_POS_X, C_POS_Y, PLAYER] = 0;
                        C_POS_Y--;
                        matris[C_POS_X, C_POS_Y, PLAYER] = 3;
                        brushcells(BORDER_SİZE_X, BORDER_SİZE_Y, matris);
                        if (matris[C_POS_X, C_POS_Y, GOLD] != 0)
                        {
                            C_SELFGOLD += matris[C_POS_X, C_POS_Y, GOLD];
                            matris[C_POS_X, C_POS_Y, GOLD] = 0;

                        }
                        if (matris[C_POS_X, C_POS_Y, HIDDEN_GOLD] != 0)
                        {
                            matris[C_POS_X, C_POS_Y, GOLD] = matris[C_POS_X, C_POS_Y, HIDDEN_GOLD];
                            matris[C_POS_X, C_POS_Y, HIDDEN_GOLD] = 0;
                        }

                    }

                }
                else
                {
                    D_WAIT = false;
                    D_MOVE_COUNT = 3;
                    C_WAIT = true;
                }
            }
            if ((C_TARGET_Y == C_POS_Y && C_POS_X == C_TARGET_X) || matris[C_TARGET_X, C_TARGET_Y, GOLD] == 0)
            {
                Console.WriteLine("C hedef seçti: c_target_x: "+C_TARGET_X+" c_target_y "+C_TARGET_Y+" C_pos_x: "+ C_POS_X+" c_pos_y: "+C_POS_Y);
                C_SELFGOLD -= C_GOLD_SELECT_COST;
                int profitable = 1;
                int gl = 0;
                distance = 9999999;
                for (int i = 0; i < BORDER_SİZE_X; i++)
                {
                    for (int j = 0; j < BORDER_SİZE_Y; j++)
                    {
                        if (matris[i, j, GOLD] != 0 || matris[i, j, HIDDEN_GOLD] != 0)
                        {
                            if (matris[i, j, GOLD] != 0)
                            {
                                gl = matris[i, j, GOLD];

                            }
                            else if (matris[i, j, HIDDEN_GOLD] != 0)
                            {
                                gl = matris[i, j, HIDDEN_GOLD];

                            }

                            if (Math.Abs((i - C_POS_X)) + Math.Abs((j - C_POS_Y)) != 0 && gl / (Math.Abs((i - C_POS_X)) + Math.Abs((j - C_POS_Y))) > profitable / distance)
                            {
                                distance = Math.Abs((i - C_POS_X)) + Math.Abs((j - C_POS_Y));
                                profitable = gl;
                                C_TARGET_X = i;
                                C_TARGET_Y = j;


                            }

                        }

                    }

                }
                if (matris[C_TARGET_X, C_TARGET_Y, HIDDEN_GOLD] != 0)
                {
                    matris[C_TARGET_X, C_TARGET_Y, GOLD] = matris[C_TARGET_X, C_TARGET_Y, HIDDEN_GOLD];
                    matris[C_TARGET_X, C_TARGET_Y, HIDDEN_GOLD] = 0;

                }
                System.IO.File.AppendAllText("Cplayer.txt", "C nın hedefi kordinatları " + C_TARGET_X.ToString() + "," + C_TARGET_Y.ToString() + "\n");
            }
         

        }
        void bPlayermove()
        {
            
            if (B_WAIT == false)
            {
                System.IO.File.AppendAllText("Bplayer.txt", "B nın goldu: " + B_SELFGOLD+ "\n");
                if (B_MOVE_COUNT != 0 && B_SELFGOLD != 0 && B_SELFGOLD - B_MOVE_GOLD >= 0)
                {
                    if (B_TARGET_X > B_POS_X)
                    {
                        System.IO.File.AppendAllText("Bplayer.txt", "B nın  kordinatları " + B_POS_X.ToString() + "," + B_POS_Y.ToString() + "\n");
                        B_MOVE_COUNT--;
                        B_SELFGOLD -= B_MOVE_GOLD;
                        matris[B_POS_X, B_POS_Y, PLAYER] = 0;
                        B_POS_X++;
                        matris[B_POS_X, B_POS_Y, PLAYER] = 2;
                        brushcells(BORDER_SİZE_X, BORDER_SİZE_Y, matris);
                        if (matris[B_POS_X, B_POS_Y, GOLD] != 0)
                        {
                            B_SELFGOLD += matris[B_POS_X, B_POS_Y, GOLD];
                            matris[B_POS_X, B_POS_Y, GOLD] = 0;

                        }
                        if (matris[B_POS_X, B_POS_Y, HIDDEN_GOLD] != 0)
                        {
                            matris[B_POS_X, B_POS_Y, GOLD] = matris[B_POS_X, B_POS_Y, HIDDEN_GOLD];
                            matris[B_POS_X, B_POS_Y, HIDDEN_GOLD] = 0;
                        }
                    }
                    else if (B_TARGET_X < B_POS_X)
                    {
                        System.IO.File.AppendAllText("Bplayer.txt", "B nın  kordinatları " + B_POS_X.ToString() + "," + B_POS_Y.ToString() + "\n");
                        B_MOVE_COUNT--;
                        B_SELFGOLD -= B_MOVE_GOLD;
                        matris[B_POS_X, B_POS_Y, PLAYER] = 0;
                        B_POS_X--;
                        matris[B_POS_X, B_POS_Y, PLAYER] = 2;
                        brushcells(BORDER_SİZE_X, BORDER_SİZE_Y, matris);
                        if (matris[B_POS_X, B_POS_Y, GOLD] != 0)
                        {
                            B_SELFGOLD += matris[B_POS_X, B_POS_Y, GOLD];
                            matris[B_POS_X, B_POS_Y, GOLD] = 0;

                        }
                        if (matris[B_POS_X, B_POS_Y, HIDDEN_GOLD] != 0)
                        {
                            matris[B_POS_X, B_POS_Y, GOLD] = matris[B_POS_X, B_POS_Y, HIDDEN_GOLD];
                            matris[B_POS_X, B_POS_Y, HIDDEN_GOLD] = 0;
                        }

                    }
                    else if (B_TARGET_Y > B_POS_Y)
                    {
                        System.IO.File.AppendAllText("Bplayer.txt", "B nın  kordinatları " + B_POS_X.ToString() + "," + B_POS_Y.ToString() + "\n");
                        B_MOVE_COUNT--;
                        B_SELFGOLD -= B_MOVE_GOLD;
                        matris[B_POS_X, B_POS_Y, PLAYER] = 0;
                        B_POS_Y++;
                        matris[B_POS_X, B_POS_Y, PLAYER] = 2;
                        brushcells(BORDER_SİZE_X, BORDER_SİZE_Y, matris);
                        if (matris[B_POS_X, B_POS_Y, GOLD] != 0)
                        {
                            B_SELFGOLD += matris[B_POS_X, B_POS_Y, GOLD];
                            matris[B_POS_X, B_POS_Y, GOLD] = 0;

                        }
                        if (matris[B_POS_X, B_POS_Y, HIDDEN_GOLD] != 0)
                        {
                            matris[B_POS_X, B_POS_Y, GOLD] = matris[B_POS_X, B_POS_Y, HIDDEN_GOLD];
                            matris[B_POS_X, B_POS_Y, HIDDEN_GOLD] = 0;
                        }


                    }
                    else if (B_TARGET_Y < B_POS_Y)
                    {
                        System.IO.File.AppendAllText("Bplayer.txt", "B nın  kordinatları " + B_POS_X.ToString() + "," + B_POS_Y.ToString() + "\n");
                        B_MOVE_COUNT--;
                        B_SELFGOLD -= B_MOVE_GOLD;
                        matris[B_POS_X, B_POS_Y, PLAYER] = 0;
                        B_POS_Y--;
                        matris[B_POS_X, B_POS_Y, PLAYER] = 2;
                        brushcells(BORDER_SİZE_X, BORDER_SİZE_Y, matris);
                        if (matris[B_POS_X, B_POS_Y, GOLD] != 0)
                        {
                            B_SELFGOLD += matris[B_POS_X, B_POS_Y, GOLD];
                            matris[B_POS_X, B_POS_Y, GOLD] = 0;

                        }
                        if (matris[B_POS_X, B_POS_Y, HIDDEN_GOLD] != 0)
                        {
                            matris[B_POS_X, B_POS_Y, GOLD] = matris[B_POS_X, B_POS_Y, HIDDEN_GOLD];
                            matris[B_POS_X, B_POS_Y, HIDDEN_GOLD] = 0;
                        }

                    }

                }
                else
                {
                    C_WAIT = false;
                    C_MOVE_COUNT = 3;
                    B_WAIT = true;
                }
            }
            if ((B_TARGET_Y == B_POS_Y && B_POS_X == B_TARGET_X) || matris[B_TARGET_X, B_TARGET_Y, GOLD] == 0)
            {
                B_SELFGOLD -= B_GOLD_SELECT_COST;
                int profitable = 1;
                int gl = 0;
                distance = 9999999;
                for (int i = 0; i < BORDER_SİZE_X; i++)
                {
                    for (int j = 0; j < BORDER_SİZE_Y; j++)
                    {
                        if (matris[i, j, GOLD] != 0)
                        {
                            gl = matris[i, j, GOLD];
                            if (Math.Abs((i - B_POS_X)) + Math.Abs((j - B_POS_Y)) != 0 && gl / (Math.Abs((i - B_POS_X)) + Math.Abs((j - B_POS_Y))) > profitable / distance)
                            {
                                distance = Math.Abs((i - B_POS_X)) + Math.Abs((j - B_POS_Y));
                                profitable = gl;
                                B_TARGET_X = i;
                                B_TARGET_Y = j;


                            }

                        }

                    }

                }
                System.IO.File.AppendAllText("Bplayer.txt", "B nın hedefi kordinatları " + B_TARGET_X.ToString() + "," + B_TARGET_Y.ToString() + "\n");
            }


        }
        void aPlayermove()
        {
            
            
            if (A_WAIT == false)
            {
                System.IO.File.AppendAllText("Aplayer.txt", "A nın goldu: " + A_SELFGOLD+ "\n");
                if (A_MOVE_COUNT != 0 && A_SELFGOLD!=0 && A_SELFGOLD-A_MOVE_GOLD>=0)
                {
                    if (A_TARGET_X > A_POS_X)
                    {
                        System.IO.File.AppendAllText("Aplayer.txt", "A nın  kordinatları " + A_POS_X.ToString() + "," + A_POS_Y.ToString()+"\n");
                        A_MOVE_COUNT--;
                        A_SELFGOLD -= A_MOVE_GOLD;
                        matris[A_POS_X, A_POS_Y, PLAYER] = 0;
                        A_POS_X++;
                        matris[A_POS_X, A_POS_Y, PLAYER] = 1;
                        brushcells(BORDER_SİZE_X, BORDER_SİZE_Y, matris);
                        if (matris[A_POS_X, A_POS_Y, GOLD] != 0)
                        {
                            A_SELFGOLD += matris[A_POS_X, A_POS_Y, GOLD];
                            matris[A_POS_X, A_POS_Y, GOLD] = 0;

                        }
                        if (matris[A_POS_X, A_POS_Y, HIDDEN_GOLD] != 0)
                        {
                            matris[A_POS_X, A_POS_Y, GOLD] = matris[A_POS_X, A_POS_Y, HIDDEN_GOLD];
                            matris[A_POS_X, A_POS_Y, HIDDEN_GOLD] = 0;
                        }
                    }
                    else if (A_TARGET_X < A_POS_X)
                    {
                        System.IO.File.AppendAllText("Aplayer.txt", "A nın  kordinatları " + A_POS_X.ToString() + "," + A_POS_Y.ToString() + "\n");
                        A_MOVE_COUNT--;
                        A_SELFGOLD -= A_MOVE_GOLD;
                        matris[A_POS_X, A_POS_Y, PLAYER] = 0;
                        A_POS_X--;
                        matris[A_POS_X, A_POS_Y, PLAYER] = 1;
                        brushcells(BORDER_SİZE_X, BORDER_SİZE_Y, matris);
                        if (matris[A_POS_X, A_POS_Y, GOLD] != 0)
                        {
                            A_SELFGOLD += matris[A_POS_X, A_POS_Y, GOLD];
                            matris[A_POS_X, A_POS_Y, GOLD] = 0;

                        }
                        if (matris[A_POS_X, A_POS_Y, HIDDEN_GOLD] != 0)
                        {
                            matris[A_POS_X, A_POS_Y, GOLD] = matris[A_POS_X, A_POS_Y, HIDDEN_GOLD];
                            matris[A_POS_X, A_POS_Y, HIDDEN_GOLD] = 0;
                        }

                    }
                    else if (A_TARGET_Y > A_POS_Y)
                    {
                        System.IO.File.AppendAllText("Aplayer.txt", "A nın  kordinatları " + A_POS_X.ToString() + "," + A_POS_Y.ToString() + "\n");
                        A_MOVE_COUNT--;
                        A_SELFGOLD -= A_MOVE_GOLD;
                        matris[A_POS_X, A_POS_Y, PLAYER] = 0;
                        A_POS_Y++;
                        matris[A_POS_X, A_POS_Y, PLAYER] = 1;
                        brushcells(BORDER_SİZE_X, BORDER_SİZE_Y, matris);
                        if (matris[A_POS_X, A_POS_Y, GOLD] != 0)
                        {
                            A_SELFGOLD += matris[A_POS_X, A_POS_Y, GOLD];
                            matris[A_POS_X, A_POS_Y, GOLD] = 0;

                        }
                        if (matris[A_POS_X, A_POS_Y, HIDDEN_GOLD] != 0)
                        {
                            matris[A_POS_X, A_POS_Y, GOLD] = matris[A_POS_X, A_POS_Y, HIDDEN_GOLD];
                            matris[A_POS_X, A_POS_Y, HIDDEN_GOLD] = 0;
                        }


                    }
                    else if (A_TARGET_Y < A_POS_Y)
                    {
                        System.IO.File.AppendAllText("Aplayer.txt", "A nın  kordinatları " + A_POS_X.ToString() + "," + A_POS_Y.ToString() + "\n");
                        A_MOVE_COUNT--;
                        A_SELFGOLD -= A_MOVE_GOLD;
                        matris[A_POS_X, A_POS_Y, PLAYER] = 0;
                        A_POS_Y--;
                        matris[A_POS_X, A_POS_Y, PLAYER] = 1;
                        if (matris[A_POS_X, A_POS_Y, GOLD] != 0)
                        {
                            A_SELFGOLD += matris[A_POS_X, A_POS_Y, GOLD];
                            matris[A_POS_X, A_POS_Y, GOLD] = 0;

                        }
                        brushcells(BORDER_SİZE_X, BORDER_SİZE_Y, matris);
                        if (matris[A_POS_X, A_POS_Y, HIDDEN_GOLD] != 0)
                        {
                            matris[A_POS_X, A_POS_Y, GOLD] = matris[A_POS_X, A_POS_Y, HIDDEN_GOLD];
                            matris[A_POS_X, A_POS_Y, HIDDEN_GOLD] = 0;
                        }


                    }
                }
                else
                {
                    B_MOVE_COUNT = 3;
                    B_WAIT = false;
                    A_WAIT = true;
                }
            }
            if ((A_TARGET_Y == A_POS_Y && A_POS_X == A_TARGET_X) ||matris[A_TARGET_X,A_TARGET_Y,GOLD]==0)
            {
                A_SELFGOLD -= A_GOLD_SELECT_COST;
                distance = 9999999;
                for (int i = 0; i < BORDER_SİZE_X; i++)
                {
                    for (int j = 0; j < BORDER_SİZE_Y; j++)
                    {
                        if (matris[i, j, GOLD] != 0)
                        {
                            if (Math.Abs((i - A_POS_X)) + Math.Abs((j - A_POS_Y)) < distance)
                            {
                                distance = Math.Abs((i - A_POS_X)) + Math.Abs((j - A_POS_Y));

                                A_TARGET_X = i;
                                A_TARGET_Y = j;


                            }

                        }

                    }

                }
                System.IO.File.AppendAllText("Aplayer.txt", "A nın hedefi kordinatları " + A_TARGET_X.ToString() + "," + A_TARGET_Y.ToString()+"\n");
            }



        }
        //creating board
        void splitGrid(int x, int y)
        {

            //Splitting screen to based on Border_size
            for (int i = 0; i < x; i++)
            {
                RowDefinition row = new RowDefinition();
                ak47.RowDefinitions.Add(row);
            }
            for (int i = 0; i < y; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                ak47.ColumnDefinitions.Add(column);
            }


        }
        // brushing board by matris
        void brushcells(int x, int y, int[,,] m)
        {
            //brishing cells
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Label border = new Label();
                    border.Margin = new Thickness(1, 1, 1, 1);
                    border.HorizontalContentAlignment = HorizontalAlignment.Center;
                    border.VerticalContentAlignment = VerticalAlignment.Center;
                    border.FontSize = 26;
                    border.FontStyle = FontStyles.Italic;


                    if (m[i, j, GOLD] != 0)
                    {
                        border.Background = Brushes.Gold;
                        border.Content = m[i, j, GOLD].ToString();

                    }
                    else if (m[i, j, HIDDEN_GOLD] != 0)
                    {

                        border.Background = Brushes.LightGoldenrodYellow;
                        border.Content = m[i, j, HIDDEN_GOLD].ToString();
                    }
                    else
                    {
                        border.Background = Brushes.Gray;
                    }



                    if (m[i, j, PLAYER] == 1)
                    {

                        border.Background = Brushes.Red;
                        border.Content = A_SELFGOLD;

                    }
                    else if (m[i, j, PLAYER] == 2)
                    {

                        border.Background = Brushes.Blue;
                        border.Content = B_SELFGOLD;

                    }
                    else if (m[i, j, PLAYER] == 3)
                    {

                        border.Background = Brushes.Orange;
                        border.Content = C_SELFGOLD;

                    }
                    else if (m[i, j, PLAYER] == 4)
                    {

                        border.Background = Brushes.Purple;
                        border.Content = D_SELFGOLD;
                    }







                    Grid.SetColumn(border, i);
                    Grid.SetRow(border, j);
                    ak47.Children.Add(border);
                }


            }


        }
        // random gold locations
        void randomizegolds(int x, int y, int[,,] m, int g, int h)
        {

            // randomize gold
            Random random = new Random();
            int X, Y, G;
            for (int i = 0; i < g; i++)
            {
                X = random.Next(0, x);
                Y = random.Next(0, y);
                G = random.Next(1, 6) * 5;

                if (m[X, Y, GOLD] == 0 && m[X, Y, HIDDEN_GOLD] == 0 && m[X, Y, PLAYER] == 0)
                {
                    m[X, Y, GOLD] = G;
                }
                else
                {
                    i--;
                }

            }
            for (int i = 0; i < h; i++)
            {
                X = random.Next(0, x);
                Y = random.Next(0, y);
                G = random.Next(1, 6) * 5;

                if (m[X, Y, GOLD] == 0 && m[X, Y, HIDDEN_GOLD] == 0 && m[X, Y, PLAYER] == 0)
                {
                    m[X, Y, HIDDEN_GOLD] = G;
                }
                else
                {
                    i--;
                }
            }


        }
        //players locations
        void setPlayers()
        {
            for (int i = 0; i < BORDER_SİZE_X; i++)
            {
                for (int j = 0; j < BORDER_SİZE_Y; j++)
                {
                    if (i == 0 && j == 0)
                    {

                        matris[i, j, PLAYER] = 1;
                    }
                    else if (i == BORDER_SİZE_X - 1 && j == 0)
                    {
                        matris[i, j, PLAYER] = 2;
                    }
                    else if (i == 0 && j == BORDER_SİZE_Y - 1)
                    {
                        matris[i, j, PLAYER] = 3;
                    }
                    else if (i == BORDER_SİZE_X - 1 && j == BORDER_SİZE_Y - 1)
                    {
                        matris[i, j, PLAYER] = 4;
                    }
                    else
                    {
                        matris[i, j, GOLD] = 0;
                        matris[i, j, HIDDEN_GOLD] = 0;
                    }

                }

            }
        }

    }
}
