﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Analogy.DataProviders.Extensions;
using Analogy.Interfaces;
using Analogy.Interfaces.DataTypes;

namespace Analogy.LogViewer.NLogProvider
{
    public partial class AnalogyColumnsMatcherUC : UserControl
    {
        public Dictionary<int, AnalogyLogMessagePropertyName> Mapping => GetMapping();
        public AnalogyColumnsMatcherUC()
        {
            InitializeComponent();
        }


        private void BtnMoveUp_Click(object sender, EventArgs e)
        {
            if (lstBAnalogyColumns.SelectedIndex <= 0) return;
            var selectedIndex = lstBAnalogyColumns.SelectedIndex;
            var currentValue = lstBAnalogyColumns.Items[selectedIndex];
            lstBAnalogyColumns.Items[selectedIndex] = lstBAnalogyColumns.Items[selectedIndex - 1];
            lstBAnalogyColumns.Items[selectedIndex - 1] = currentValue;
            lstBAnalogyColumns.SelectedIndex = lstBAnalogyColumns.SelectedIndex - 1;
        }

        private void BtnMoveDown_Click(object sender, EventArgs e)
        {
            if (lstBAnalogyColumns.SelectedIndex == lstBAnalogyColumns.Items.Count - 1) return;
            var selectedIndex = lstBAnalogyColumns.SelectedIndex;
            var currentValue = lstBAnalogyColumns.Items[selectedIndex + 1];
            lstBAnalogyColumns.Items[selectedIndex + 1] = lstBAnalogyColumns.Items[selectedIndex];
            lstBAnalogyColumns.Items[selectedIndex] = currentValue;
            lstBAnalogyColumns.SelectedIndex = lstBAnalogyColumns.SelectedIndex + 1;
        }

        private void lstBAnalogyColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            
                if (lstBAnalogyColumns.SelectedIndex > lstBoxItems.Items.Count - 1) return;
                lstBoxItems.SelectedIndex = lstBAnalogyColumns.SelectedIndex;
            

        }

        public void SetColumns(string[] columns)
        {
            lstBoxItems.Items.Clear();
            lstBoxItems.Items.AddRange(columns);
        }

        public void LoadMapping(ISplitterLogParserSettings parser)
        {
            lstBAnalogyColumns.Items.Clear();
            for (int i = 0; i < 21; i++)
            {
                if (parser.Maps.ContainsKey(i))
                    lstBAnalogyColumns.Items.Add(parser.Maps[i]);
                else
                    lstBAnalogyColumns.Items.Add("__ignore__");
            }

        }
        private Dictionary<int, AnalogyLogMessagePropertyName> GetMapping()
        {
            Dictionary<int, AnalogyLogMessagePropertyName> maps =
                new Dictionary<int, AnalogyLogMessagePropertyName>(lstBAnalogyColumns.Items.Count);
            for (int i = 0; i < lstBAnalogyColumns.Items.Count; i++)
            {
                if (lstBAnalogyColumns.Items[i].ToString()
                    .Contains("ignore", StringComparison.InvariantCultureIgnoreCase)) continue;
                maps.Add(i, (AnalogyLogMessagePropertyName)Enum.Parse(typeof(AnalogyLogMessagePropertyName),
                    lstBAnalogyColumns.Items[i].ToString()));
            }

            return maps;
        }
    }
}
