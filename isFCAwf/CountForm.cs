using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace isFCAwf
{
    public partial class CountForm : Form
    {
        string sqlStringConn;
        private int orderNum;
        public CountForm(string sqlStringConn, int orderNum)
        {
            InitializeComponent();
            this.sqlStringConn = sqlStringConn;
            this.orderNum = orderNum;
        }
        private List<double> variableNM = new List<double>(10);
        private void CountForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = "$Источник A$+$Источник B$+$Источник C$-$Долги D$";

            LoadCbox_with_nmU();
        }
        private bool cbIsLoad = false;
        private bool lbofNmaIsLoad = false;
        private int selected_nmU;
        private int selected_nmX;
        private int selected_nmA;
        private int selected_nmAToFilter;
        private void LoadCbox_with_nmU()
        {
            cbIsLoad = false;
            var dtcbOFnmU = DBConnectCreator.GetnmuONorderNum(sqlStringConn, orderNum);
            cbnmU.DataSource = dtcbOFnmU; //.DefaultView
            cbnmU.ValueMember = "Код_М_U";
            cbnmU.DisplayMember = "Описание";// "Код_М_U"
            //cbnmU.DataBindings.Add("SelectedValue", dtcbOFnmU.DefaultView, "Код_М_U"); //эта тварь все сломала
            cbnmU.SelectedIndex = 0;

            LoadListNMAs();
            SomeShitMethodGovna();
            LoadCbox_with_nmX();
            cbIsLoad = true;
        }
        private void LoadCbox_with_nmX()
        {
            cbIsLoad = false;
            var dtcbOFnmX = DBConnectCreator.GetNmXonNmUID(sqlStringConn, selected_nmU);
            cbnmX.DataSource = dtcbOFnmX;
            cbnmX.ValueMember = "Код_ЛП";
            cbnmX.DisplayMember = "Имя_набора_ЛП";
            if (cbnmX.SelectedIndex == -1)
            {
                cbIsLoad = true;
                cbnmX.Text = "ЛП отсутствуют";
                lbofnmLP.DataSource = null;
                return;
            }
            //cbnmX.DataBindings.Add("SelectedValue", dtcbOFnmX.DefaultView, "Код_ЛП");//Два связывания в коллекции привязываются к одному свойству.            Имя параметра: binding"




            //LoadListNmLPs();
            cbIsLoad = true;
            cbnmX.SelectedIndex = 0;

            EsheDermoMethod();

        }

        DataTable dt_nmAs;
        DataTable dt_nmLPs;
        public DataTable lbofNma2DataTable;
        //DataTable dt_nmAs_toFuzFilter;
        List<NmA_or_nmLP> collectOFnma = new List<NmA_or_nmLP>();
        List<NmA_or_nmLP> collectOFnmaToFilter = new List<NmA_or_nmLP>();
        List<NmA_or_nmLP> collectOFSELECTEDnmaToFilter = new List<NmA_or_nmLP>();
        List<NmA_or_nmLP> collectOF_LP = new List<NmA_or_nmLP>();
        private void LoadListNMAs()
        {
            lbofNmaIsLoad = false;
            dt_nmAs = DBConnectCreator.GetNmAonMnU(sqlStringConn, selected_nmU);
            lbofNma.DataSource = dt_nmAs;
            lbofNma.ValueMember = "Код_НМ";
            lbofNma.DisplayMember = "Имя_НМ";
            //dt_nmAs_toFuzFilter = dt_nmAs;
            //lbofNma2.DataSource = dt_nmAs_toFuzFilter;
            lbofNma2DataTable = DBConnectCreator.GetNmAonMnU(sqlStringConn, selected_nmU); //dt_nmAs.Clone();
            binsous_lbofNma2.DataSource = lbofNma2DataTable;
            bindingSource1.DataSource = lbofNma2DataTable;
            lbofNma2.DataSource = bindingSource1;
            lbofNma2.ValueMember = "Код_НМ";
            lbofNma2.DisplayMember = "Имя_НМ";



            lbofNmaIsLoad = true;
            //cbnmU.DataBindings.Add("SelectedValue", dtcbOFnmU.DefaultView, "Код_М_U");
            var drtNmA = dt_nmAs.Rows;
            collectOFnma = FORmethodFillLists(dt_nmAs.Rows);
            collectOFnmaToFilter = FORmethodFillLists(dt_nmAs.Rows);
            /*for (int i = 0; drtNmA.Count != 0 & i < drtNmA.Count; i++)
            {
                NmA_or_nmLP nma = new NmA_or_nmLP();
                nma.ID_nmA_nmLP = (int)drtNmA[i]["Код_НМ"];
                nma.ID_nmU_or_nmX = (int)drtNmA[i]["Код_М_U"];
                nma.Name = (string)drtNmA[i]["Имя_НМ"];
                nma.M1 = (int)drtNmA[i]["m1"];
                nma.M2 = (int)drtNmA[i]["m2"];
                nma.A = (int)drtNmA[i]["a"];
                nma.B = (int)drtNmA[i]["b"];
                collectOFnma.Add(nma);
                collectOFnmaToFilter.Add(nma);
            }*/
            if (selected_nmU >= 0)
            {
                LoadCbox_with_nmX();
            }
                
        }

        private List<NmA_or_nmLP> FORmethodFillLists(DataRowCollection dRColl)
        {
            List<NmA_or_nmLP> newList = new List<NmA_or_nmLP>();
            for (int i = 0; i < dRColl.Count; i++)
            {
                NmA_or_nmLP nm = new NmA_or_nmLP();
                nm.ID_nmA_nmLP = (int)dRColl[i][0];
                nm.ID_nmU_or_nmX = (int)dRColl[i][1];
                nm.Name = (string)dRColl[i][2];
                nm.M1 = (int)dRColl[i][3];
                nm.M2 = (int)dRColl[i][4];
                nm.A = (int)dRColl[i][5];
                nm.B = (int)dRColl[i][6];
                newList.Add(nm);
            }
            return newList;
        }

        private int CounterColLboxItems_LP = 0;
        private void LoadListNmLPs()
        {
            cbIsLoad = false;
            //cbnmU.DataBindings.Add("SelectedValue", dtcbOFnmU.DefaultView, "Код_М_U");
            dt_nmLPs = DBConnectCreator.GetNmLPonNmX(sqlStringConn, selected_nmX);
            //var drtNmLP = dt_nmLPs.Rows;
            collectOF_LP = FORmethodFillLists(dt_nmLPs.Rows);
            /*for (int i = 0; drtNmLP.Count != 0 & i < drtNmLP.Count; i++)
            {
                NmA_or_nmLP nma = new NmA_or_nmLP();
                nma.ID_nmA_nmLP = (int)drtNmLP[i]["Код_НМ_ЛП"];
                nma.ID_nmU_or_nmX = (int)drtNmLP[i]["Код_ЛП"];
                nma.Name = (string)drtNmLP[i]["Имя"];
                nma.M1 = (int)drtNmLP[i]["m1"];
                nma.M2 = (int)drtNmLP[i]["m2"];
                nma.A = (int)drtNmLP[i]["a"];
                nma.B = (int)drtNmLP[i]["b"];
                collectOF_LP.Add(nma);
            }*/
            bindingSource2.DataSource = dt_nmLPs;
            lbofnmLP.DataSource = bindingSource2;
            lbofnmLP.ValueMember = "Код_НМ_ЛП";
            lbofnmLP.DisplayMember = "Имя";
            if (lbofnmLP != null)
                CounterColLboxItems_LP = lbofnmLP.Items.Count;
            cbIsLoad = true;
            //lbofnmLP.DataSource = collectOF_LP;
            //lbofnmLP.ValueMember = "ID_nmA_nmLP";//Код_НМ_ЛП
            //lbofnmLP.DisplayMember = "Name";//Имя                   /////////////////////////////тут закончил!!!!!!!!!!!!
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lbresCount.Items.Clear();
            //AnalizatorStroki(textBox1.Text);
            FuzzyCounter fc = new FuzzyCounter();
            fc.nmu = selected_nmU;
            fc.sqlstringConn = sqlStringConn;
            double[] dmas = fc.GetRezult(textBox1.Text, out List<string> resultsOfCounts);
            string s = "";
            for (int i = 0; i < dmas.Length ; i++)
            {
                s += dmas[i].ToString();
                if (i != dmas.Length - 1)
                    s += "; ";
                variableNM.Add(dmas[i]);
            }
            tbnewnm.Text = s;
            for (int i = 0; fc.resCount != null & i < fc.resCount.Count; i++)
            {
                lbresCount.Items.Add(fc.resCount[i]);
            }
        }

        private void cbnmU_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedNMAsForFuzFilter = null;
            cbnmX.SelectedIndex = -1;
            lbofNma2.DataSource = null;
            lbSelectedToFilterNmA.DataSource = null;
            lbofnmLP.DataSource = null;
            lbofNma.DataSource = null;
            SomeShitMethodGovna();

        }

        public void SomeShitMethodGovna()
        {
            if (cbIsLoad)
            {
                if (cbnmU.SelectedIndex == 0)
                {
                    bool selected1 = true;
                }

                selected_nmU = int.Parse(cbnmU.SelectedValue.ToString());
                LoadListNMAs();
            }
        }
        public void EsheDermoMethod() //v someshit methodgovna?
        {
            if (cbIsLoad && cbnmX.SelectedIndex != -1)
            {
                selected_nmX = int.Parse(cbnmX.SelectedValue.ToString());
                LoadListNmLPs();
            }
        }

        private void lbofNma_SelectedValueChanged(object sender, EventArgs e)
        {
            if (lbofNmaIsLoad & lbofNma.SelectedIndex != -1 & cbnmX.SelectedIndex != -1)
            {
                tbAval.Text = SelValGetter(lbofNma, collectOFnma, out selected_nmA, out string strname);
                tbAconName.Text = strname;
                //selected_nmA = int.Parse(lbofNma.SelectedValue.ToString());
                //tbAval.Text = "$" + collectOFnma[lbofNma.SelectedIndex].Name + "$";
                //tbAconName.Text = "{ " + collectOFnma[lbofNma.SelectedIndex].M1 + "; " + collectOFnma[lbofNma.SelectedIndex].M2 + "; " + collectOFnma[lbofNma.SelectedIndex].A + "; " + collectOFnma[lbofNma.SelectedIndex].B + " }";
            }

        }
        public string SelValGetter(ListBox lb, List<NmA_or_nmLP> collectof, out int whatWasSelect, out string nameSet)
        {
            whatWasSelect = int.Parse(lb.SelectedValue.ToString());
            nameSet = "$" + collectof[lb.SelectedIndex].Name + "$";
            string res = "{ " + collectof[lb.SelectedIndex].M1 + "; " + collectof[lb.SelectedIndex].M2 + "; " + collectof[lb.SelectedIndex].A + "; " + collectof[lb.SelectedIndex].B + " }";
            // lbofNma.SelectedValue  + "; " + 
            return res;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tbnewnm.Text != "" & tbnewnm.Text != "0;0;0;0")
            {//tbnewnmaname
                //
                string[] newNma = tbnewnm.Text.Split(';');
                DBConnectCreator.InsertCountedNmA(sqlStringConn, selected_nmU, newNma, tbnewnmaname.Text);
                SomeShitMethodGovna();
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
        private DataTable selectedNMAsForFuzFilter;// DataRowCollection selectedNMAsForFuzFilter;
        //lbofNma2DataTable
        private bool filled_selectedNMAsForFuzFilter = true;
        int countColSrtWasSelect = 0;
        private void button4_Click(object sender, EventArgs e)
        {
            if (selectedNMAsForFuzFilter == null)
            {
                selectedNMAsForFuzFilter = lbofNma2DataTable.Clone();
            }


            filled_selectedNMAsForFuzFilter = false;
            selected_nmAToFilter = (int)lbofNma2.SelectedValue;
            int selIndexNmAFilter = lbofNma2.SelectedIndex;
            var r = lbofNma2DataTable.Rows[selIndexNmAFilter];
            object[] row = lbofNma2DataTable.Rows[selIndexNmAFilter].ItemArray;

            //selectedNMAsForFuzFilter.Rows.Clear();
            selectedNMAsForFuzFilter.Rows.Add(row);
            lbofNma2DataTable.Rows.Remove(r);
            binSOurForFilterNMA.DataSource = selectedNMAsForFuzFilter;
            lbSelectedToFilterNmA.DataSource = binSOurForFilterNMA;
            lbSelectedToFilterNmA.ValueMember = "Код_НМ";
            lbSelectedToFilterNmA.DisplayMember = "Имя_НМ";
            filled_selectedNMAsForFuzFilter = true;
            //lbofNma2DataTable.IndexOf();
            //lbofNma2DataTable[selIndexNmAFilter].Delete();
            //bindingSource1.Remove(lbofNma2.SelectedValue);
            //bindingSource2.Add(lbofNma2.SelectedValue);
            collectOFSELECTEDnmaToFilter = FORmethodFillLists(selectedNMAsForFuzFilter.Rows);
            countColSrtWasSelect++;
        }

        private void lbofNma2_SelectedValueChanged(object sender, EventArgs e)
        {
            if ((lbofNmaIsLoad & lbofNma2.SelectedIndex != -1) & filled_selectedNMAsForFuzFilter)
            {
                tbNmaVal.Text = SelValGetter(lbofNma2, collectOFnmaToFilter, out selected_nmAToFilter, out _);
                //lbofNma2.SelectedValue != null
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (selectedNMAsForFuzFilter == null)
            {
                selectedNMAsForFuzFilter = lbofNma2DataTable.Clone();
            }
            countColSrtWasSelect--;
            if (countColSrtWasSelect < 0)
                countColSrtWasSelect = 0;

            filled_selectedNMAsForFuzFilter = false;
            selected_nmAToFilter = (int)lbSelectedToFilterNmA.SelectedValue;
            int selIndexNmAFilter = lbSelectedToFilterNmA.SelectedIndex;
            var r = selectedNMAsForFuzFilter.Rows[selIndexNmAFilter];
            object[] row = selectedNMAsForFuzFilter.Rows[selIndexNmAFilter].ItemArray;

            //selectedNMAsForFuzFilter.Rows.Clear();
            lbofNma2DataTable.Rows.Add(row);
            selectedNMAsForFuzFilter.Rows.Remove(r);
            binSOurForFilterNMA.DataSource = selectedNMAsForFuzFilter;
            lbSelectedToFilterNmA.DataSource = binSOurForFilterNMA;
            lbSelectedToFilterNmA.ValueMember = "Код_НМ";
            lbSelectedToFilterNmA.DisplayMember = "Имя_НМ";
            filled_selectedNMAsForFuzFilter = true;
            binsous_lbofNma2.DataSource = lbofNma2DataTable;
            collectOFSELECTEDnmaToFilter = FORmethodFillLists(selectedNMAsForFuzFilter.Rows);
        }
        private void cbnmX_SelectedValueChanged(object sender, EventArgs e)
        {
            EsheDermoMethod(); //listbox filler for LPs_nm

        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<string> resultsOFfilter = new List<string>
            {
                "<--Вывод результата фильтрации осуществляется в формате :",
                " Нечеткое множество А(i):",
                " По X(A) i-тым лингвистическим переменным {Возможность, необходимость}-->"
            };

            DataTable dattab1 = (DataTable)binSOurForFilterNMA.DataSource;
            DataTable dattab2 = dt_nmLPs;
            var dateRowA1 = dattab1.Rows;
            var dateRowLP2 = dattab2.Rows;
            List<NmA_or_nmLP> listNma1 = FORmethodFillLists(dateRowA1);
            List<NmA_or_nmLP> listNlp2 = FORmethodFillLists(dateRowLP2);
            /*
            List<NmA_or_nmLP> listNma1 = fill(dateRowA1);
            List<NmA_or_nmLP> listNlp2 = fill(dateRowLP2);

            List<NmA_or_nmLP> fill(DataRowCollection dRColl)
            {
                List<NmA_or_nmLP> newList = new List<NmA_or_nmLP>();
                for (int i = 0; i < dRColl.Count; i++)
                {
                    NmA_or_nmLP nm = new NmA_or_nmLP();
                    nm.Name = (string)dRColl[i][2];
                    nm.M1 = (int)dRColl[i][3];
                    nm.M2 = (int)dRColl[i][4];
                    nm.A = (int)dRColl[i][5];
                    nm.B = (int)dRColl[i][6];
                    newList.Add(nm);
                }
                return newList;
            }
            */
            for (int i = 0; i < listNma1.Count; i++)
            {
                double softFil;
                double hardFil;
                resultsOFfilter.Add("По " + listNma1[i].Name);
                //int[] nma = {  };
                for (int j = 0; j < listNlp2.Count; j++)
                {
                    softFil = Math.Min(
                        Math.Max(0, Math.Min(1, (double)(1 + ((double)(listNma1[i].M2 - listNlp2[j].M1) / (listNma1[i].B + listNlp2[j].A))))),
                        Math.Max(0, Math.Min(1, (double)(1 + ((double)(listNlp2[j].M2 - listNma1[i].M1) / (listNlp2[j].B + listNma1[i].A)))))
                        );
                    hardFil = Math.Min(
                        Math.Max(0, Math.Min(1, ((double)(listNma1[i].M1 - listNlp2[j].M1 + listNlp2[j].A) / (listNma1[i].A + listNlp2[j].A)))),
                        Math.Max(0, Math.Min(1, ((double)(listNlp2[j].M2 - listNma1[i].M2 + listNlp2[j].B) / (listNma1[i].B + listNlp2[j].B))))
                        );
                    string res = $"     Для X(A){j} = '" + listNlp2[j].Name + "' {" + softFil.ToString() + "; " + hardFil.ToString() + "}";
                    resultsOFfilter.Add(res);
                    if (chbDebagYes.Checked)
                    {
                        string srtsoftFil = $"                      Soft = Min(Max(0, Min(1, 1 + ({listNma1[i].M2} - {listNlp2[j].M1}) / ({listNma1[i].B} + {listNlp2[j].A}))),"
                                            + $" Max(0, Min(1, 1 + ({listNlp2[j].M2} - {listNma1[i].M1}) / ({listNlp2[j].B} + {listNma1[i].A}))))";
                        string strhardFil = $"                      Hard =  Min(Max(0, Min(1, ({listNma1[i].M1} - {listNlp2[j].M1} + {listNlp2[j].A}) / ({listNma1[i].A} + {listNlp2[j].A}))),"
                                            + $"  Max(0, Min(1, ({listNlp2[j].M2} - {listNma1[i].M2} + {listNlp2[j].B}) / ({listNma1[i].B} + {listNlp2[j].B}))))";
                        resultsOFfilter.Add(srtsoftFil);
                        resultsOFfilter.Add(strhardFil);
                    }

                }
            }
            foreach (var item in resultsOFfilter)
            {
                lbResFilter.Items.Add(item);
            }
            //uuuuuuuuuuuuuuuf
        }

        private void button7_Click(object sender, EventArgs e)
        {
            lbResFilter.Items.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            cbIsLoad = false;
            lbofNmaIsLoad = false;
            selected_nmU = -1;
            selected_nmX = -1;
            LoadCbox_with_nmU();
        }

        private void lbSelectedToFilterNmA_SelectedValueChanged(object sender, EventArgs e)
        {
            if (countColSrtWasSelect > 0 & !formClose & lbSelectedToFilterNmA.SelectedIndex != -1)
            {
                tbNmaVal.Text = SelValGetter(lbSelectedToFilterNmA, collectOFSELECTEDnmaToFilter, out _, out _);
                //lbofNma2.SelectedValue != null
            }
        }

        private void lbofnmLP_SelectedValueChanged(object sender, EventArgs e)
        {
            if (CounterColLboxItems_LP > 0 && cbIsLoad & lbofnmLP.SelectedIndex != -1)
                tbValLP.Text = SelValGetter(lbofnmLP, collectOF_LP, out _, out _);
        }

        private bool formClose = false;
        private void CountForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            cbIsLoad = false;
            formClose = true;
        }


        public void SendDataToLiveCharToShow(List<NmA_or_nmLP> backSet, List<NmA_or_nmLP> frontSet )
        {
            SeriesCollection series = new SeriesCollection();
            ChartValues<double> fuzzyValues = new ChartValues<double>();
            List<string> line_oX_ = new List<string>();
            cartesianChart1.Series.Clear();
            LiveChartFiller(backSet, false);
            LiveChartFiller(frontSet, true);
            void LiveChartFiller(List<NmA_or_nmLP> listnmA, bool showPoint)
            {
                for (int i = 0; i < listnmA.Count; i++)
                {
                    LineSeries lineSeries = new LineSeries
                    {
                        Title = listnmA[i].Name,
                        Values = new ChartValues<ObservablePoint>
                        {
                            new ObservablePoint(listnmA[i].M1 - listnmA[i].A, 0), //x,y (x,0),(x,1),(x,1),(x,0)
                            new ObservablePoint(listnmA[i].M1, 100),
                            new ObservablePoint(listnmA[i].M2, 100),
                            new ObservablePoint(listnmA[i].M2 + listnmA[i].B, 0),
                            new ObservablePoint(listnmA[i].M1 - listnmA[i].A, 0)
                        },
                        PointGeometry = DefaultGeometries.Square,
                        PointGeometrySize = 5,
                        LineSmoothness = 0
                    };
                    if (!showPoint)
                        lineSeries.PointGeometry = null;
                    cartesianChart1.Series.Add(lineSeries);
                    //series.Add(lineSeries);
                }


            }
            //cartesianChart1.Series = series; //как варик
            //cartesianChart1.Series.Add(LineSeries);
            
            cartesianChart1.AxisX.Clear();
            cartesianChart1.AxisX.Add(new Axis
            {
                    Title = "Значения трапеций"
            });
            cartesianChart1.AxisY.Clear();
            string[] AxisY = new string[100];
            for (int i = 0; i < 10; i++)
                AxisY[i] = (i + i * 10).ToString();
            cartesianChart1.AxisY.Add(new Axis
            {
                Title = "%",
                Labels = AxisY,
                LabelFormatter = value => value.ToString()
            });

            cartesianChart1.LegendLocation = LegendLocation.Right;
            /*
            //modifying the series collection will animate and update the chart
            cartesianChart1.Series.Add(new LineSeries
            {
                Values = new ChartValues<double> { 0, 1, 1, 0, 0 },
                LineSmoothness = 0 //straight lines, 1 really smooth lines
            }); */

            //modifying any series values will also animate and update the chart
            //cartesianChart1.Series[2].Values.Add(5d);

            //cartesianChart1.AxisX.Clear();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (tbnewnm.Text == "")
            {
                return;
            }
            string[] countedNma = tbnewnm.Text.Split(new[] { ';' }, 4); //, StringSplitOptions.RemoveEmptyEntries

            //string[] nameParts = info.ФИО_клиента.Split(new[] { ' ' }, 3, System.StringSplitOptions.RemoveEmptyEntries);
            //string firstName = nameParts.ElementAtOrDefault(1);
            //string lastName = nameParts.ElementAtOrDefault(0);
            //string secondName = nameParts.ElementAtOrDefault(2);
            List<NmA_or_nmLP> countedNmaList = new List<NmA_or_nmLP>();
            NmA_or_nmLP nmA_Or_NmLP = new NmA_or_nmLP();
            nmA_Or_NmLP.Name = "Результат расчета";
            nmA_Or_NmLP.M1 = int.Parse(countedNma[0]);
            nmA_Or_NmLP.M2 = int.Parse(countedNma[1]);
            nmA_Or_NmLP.A = int.Parse(countedNma[2]);
            nmA_Or_NmLP.B = int.Parse(countedNma[3]);
            countedNmaList.Add(nmA_Or_NmLP);
            
            /*
            List<NmA_or_nmLP> countedNmaList = new List<NmA_or_nmLP>
            {
                new NmA_or_nmLP
                {
                    Name = "Результат расчета",
                    M1 = int.Parse(countedNma[0]),
                    M2 = int.Parse(countedNma[1]),
                    A = int.Parse(countedNma[2]),
                    B = int.Parse(countedNma[3])
                }
            };
            */
            //cartesianChart1
            SendDataToLiveCharToShow(collectOFnma, countedNmaList);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            SendDataToLiveCharToShow(collectOF_LP, collectOFSELECTEDnmaToFilter);
        }
    }
}
